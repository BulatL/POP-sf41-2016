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
        public StavkaProdaje stavkaProdaje = new StavkaProdaje();
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
            namestaj = viewn.CurrentItem as Namestaj;
            kolicina = int.Parse(tbKolicina.Text);


            if (kolicina > namestaj.KolicinaUMagacinu)
            {
                MessageBox.Show("Dostupna kolicina je manja od unete", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (kolicina <= namestaj.KolicinaUMagacinu)
            {
                this.DialogResult = true;
                /*int id = Projekat.Instance.StavkeProdaje.Count + 1;
                stavkaProdaje.Id = id;
                stavkaProdaje.Kolicina = int.Parse(tbKolicina.Text);
                stavkaProdaje.NamestajId = namestaj.Id;*/



                var listaNamestaja = Projekat.Instance.Namestaj;
                foreach (var item in listaNamestaja)
                {
                    if (item.Id == namestaj.Id)
                    {
                        item.KolicinaUMagacinu -= kolicina;
                    }
                }
                /* var cenaSaAkcijom = 0.0;
                 foreach (var item in Projekat.Instance.Akcija)
                 {
                     if(item.NamestajNaPopustuId == namestaj.Id)
                     {
                         cenaSaAkcijom = (namestaj.JedinicnaCena * item.Popust) / 100;
                     }
                 }*/

                StavkaProdaje novaStavkaProdaje = new StavkaProdaje()
                {
                    Id = listaStavki.Count + 1,
                    Kolicina = kolicina,
                    NamestajId = namestaj.Id,
                    UkupnaCena = namestaj.JedinicnaCena * kolicina,
                };

                stavkaProdaje = novaStavkaProdaje;

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
