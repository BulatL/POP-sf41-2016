using POP_sf_41_2016_GUI.model;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_sf_41_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for StavkaWindow.xaml
    /// </summary>
    public partial class StavkaWindow : Window
    {

        public Namestaj namestaj;
        public int kolicina;
        private ICollectionView viewn;
        public StavkaWindow()
        {
            InitializeComponent();

            dataGridNamestaj.AutoGenerateColumns = false;
            dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
            dataGridNamestaj.DataContext = this;
            viewn = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            viewn.Filter = NamestajFilter;
            dataGridNamestaj.ItemsSource = viewn;
            dataGridNamestaj.Visibility = Visibility.Visible;
            tbKolicina.DataContext = kolicina;
        }

        private bool NamestajFilter(object obj) //Prima namestaj i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((Namestaj)obj).Obrisan;
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaStavki = Projekat.Instance.StavkeProdaje;
            var izabraniNamestaj = viewn.CurrentItem as Namestaj;


            if (int.Parse(tbKolicina.Text) > izabraniNamestaj.KolicinaUMagacinu)
            {
                MessageBox.Show("Dostupna kolicina je manja od unete", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (int.Parse(tbKolicina.Text) <= izabraniNamestaj.KolicinaUMagacinu)
            {
                this.DialogResult = true;
                namestaj = viewn.CurrentItem as Namestaj;
                kolicina = int.Parse(tbKolicina.Text);


                var listaNamestaja = Projekat.Instance.Namestaj;
                foreach (var item in listaNamestaja)
                {
                    if (item.Id == izabraniNamestaj.Id)
                    {
                        item.KolicinaUMagacinu -= kolicina;
                    }
                }
                var cenaSaAkcijom = 0.0;
                foreach (var item in Projekat.Instance.Akcija)
                {
                    if(item.NamestajNaPopustuId == izabraniNamestaj.Id)
                    {
                        cenaSaAkcijom = (izabraniNamestaj.JedinicnaCena * item.Popust) / 100;
                    }
                }

                StavkaProdaje stavkaProdaje = new StavkaProdaje()
                {
                    Id = listaStavki.Count + 1,
                    Kolicina = kolicina,
                    NamestajId = izabraniNamestaj.Id,
                    UkupnaCena = izabraniNamestaj.JedinicnaCena * kolicina,
                };

                listaStavki.Add(stavkaProdaje);
                Projekat.Instance.StavkeProdaje = listaStavki;
                Projekat.Instance.Namestaj = listaNamestaja;
                GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
                GenericSerializer.Serializer("stavkaProdaje.xml", listaStavki);

                this.Close();
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
