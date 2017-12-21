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

        public enum Parametar
        {
            PRODAJA,
            AKCIJA,
            DODATNAUSLUGA
        };

        public Namestaj namestaj;
        public StavkaProdaje stavka;
        public DodatnaUsluga dodatnaUsluga;
        private ICollectionView viewn;
        private ICollectionView viewd;
        private Parametar parametar;
        private double cenaAkcija = 0.0;
        public StavkaWindow(StavkaProdaje stavka, Parametar parametar)
        {
            InitializeComponent();

            this.stavka = stavka;
            this.parametar = parametar;

            if (parametar == Parametar.DODATNAUSLUGA)
            {
                dataGridNamestaj.Visibility = Visibility.Hidden;
                dgDodatnaUsluga.AutoGenerateColumns = false;
                dgDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
                viewd = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga);
                viewd.Filter = DodatnaUslugaFilter;
                dgDodatnaUsluga.ItemsSource = viewd;

                tbKolicina.Visibility = Visibility.Hidden;
                lbKolicina.Visibility = Visibility.Hidden;
            }
            else
            {
                dgDodatnaUsluga.Visibility = Visibility.Hidden;
                dataGridNamestaj.AutoGenerateColumns = false;
                dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
                dataGridNamestaj.DataContext = stavka;
                viewn = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
                viewn.Filter = NamestajFilter;
                dataGridNamestaj.ItemsSource = viewn;
                tbKolicina.DataContext = stavka;
                if (parametar == Parametar.AKCIJA)
                {
                    tbKolicina.Visibility = Visibility.Hidden;
                    lbKolicina.Visibility = Visibility.Hidden;
                }
            }
        }

        private bool DodatnaUslugaFilter(object obj)
        {
            return !((DodatnaUsluga)obj).Obrisan;
        }

        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaStavki = Projekat.Instance.StavkeProdaje;
            var listaAkcija = Projekat.Instance.Akcija;

            if(parametar == Parametar.DODATNAUSLUGA)
            {
                this.DialogResult = true;
                dodatnaUsluga = viewd.CurrentItem as DodatnaUsluga;
                this.Close();
            }

            else if (parametar == Parametar.AKCIJA)
            {
                this.DialogResult = true;
                namestaj = viewn.CurrentItem as Namestaj;
                this.Close();
            }
            else if (parametar == Parametar.PRODAJA)
            {
                namestaj = viewn.CurrentItem as Namestaj;

                if (stavka.Kolicina > namestaj.KolicinaUMagacinu)
                {
                    MessageBox.Show("Dostupna kolicina je manja od unete", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (stavka.Kolicina <= 0)
                {
                    MessageBox.Show("Kolicina mora biti veca od 0", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (stavka.Kolicina <= namestaj.KolicinaUMagacinu)
                {

                    this.DialogResult = true;
                    foreach (var akcija in listaAkcija)
                    {
                        if (akcija.Obrisan == false)
                        {
                            foreach (var item in akcija.ListaNamestajaNaPopustuId)
                            {
                                if (namestaj.Id == item)
                                {
                                    cenaAkcija = (namestaj.JedinicnaCena - namestaj.JedinicnaCena * akcija.Popust / 100) * stavka.Kolicina;
                                    break;
                                }
                            }
                        }
                    }   
                    
                    stavka.Id = listaStavki.Count + 1;
                    stavka.Namestaj = namestaj;
                    if(cenaAkcija == 0.0)
                    {
                        stavka.UkupnaCena = namestaj.JedinicnaCena * stavka.Kolicina;
                    }
                    else
                    {
                        stavka.UkupnaCena = cenaAkcija;
                    }
                    listaStavki.Add(stavka);

                    var listaNamestaja = Projekat.Instance.Namestaj;
                    foreach (var item in listaNamestaja)
                    {
                        if (item.Id == namestaj.Id)
                        {
                            item.KolicinaUMagacinu -= stavka.Kolicina;
                        }
                    }

                    Projekat.Instance.StavkeProdaje = listaStavki;
                    Projekat.Instance.Namestaj = listaNamestaja;
                    GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
                    GenericSerializer.Serializer("stavkaProdaje.xml", listaStavki);

                    this.Close();
                } 
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
