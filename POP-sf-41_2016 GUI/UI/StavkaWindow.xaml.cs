using POP_sf_41_2016_GUI.DAO;
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
        public NaAkciji naAkciji = new NaAkciji();
        public ProdajaNamestaj stavka;
        public ProdajaDodatnaUsluga dodatnaUsluga = new ProdajaDodatnaUsluga();
        public int akcijaId;
        private ICollectionView viewn;
        private ICollectionView viewd;
        private Parametar parametar;
        private double cenaAkcija = 0.0;
        public StavkaWindow(ProdajaNamestaj stavka, int akcijaId, Parametar parametar)
        {
            InitializeComponent();

            this.stavka = stavka;
            this.parametar = parametar;
            this.akcijaId = akcijaId;

            if (parametar == Parametar.DODATNAUSLUGA)
            {
                dataGridNamestaj.Visibility = Visibility.Collapsed;
                dgDodatnaUsluga.AutoGenerateColumns = false;
                dgDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
                dgDodatnaUsluga.DataContext = this;
                viewd = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
                viewd.Filter = DodatnaUslugaFilter;
                dgDodatnaUsluga.ItemsSource = viewd;

                tbKolicina.Visibility = Visibility.Collapsed;
                lbKolicina.Visibility = Visibility.Collapsed;
                tbPopust.Visibility = Visibility.Collapsed;
            }
            else
            {
                dgDodatnaUsluga.Visibility = Visibility.Collapsed;
                dataGridNamestaj.AutoGenerateColumns = false;
                dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
                dataGridNamestaj.DataContext = this;
                if (parametar == Parametar.PRODAJA)
                {
                    viewn = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
                    viewn.Filter = NamestajFilter;
                    dataGridNamestaj.ItemsSource = viewn;
                    tbKolicina.DataContext = stavka;
                    tbPopust.Visibility = Visibility.Collapsed;
                }
                else if (parametar == Parametar.AKCIJA)
                {
                    viewn = CollectionViewSource.GetDefaultView(NamestajDAO.LoadNamestajNotInAkcija());
                    dataGridNamestaj.ItemsSource = viewn;
                    tbKolicina.Visibility = Visibility.Collapsed;
                    tbPopust.DataContext = naAkciji;
                    tbPopust.Visibility = Visibility.Visible;
                    lbKolicina.Content = "Popust";
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
            var listaStavki = Projekat.Instance.ProdajaNamestaj;
            var listaAkcija = Projekat.Instance.Akcije;

            if (parametar == Parametar.DODATNAUSLUGA)
            {
                this.DialogResult = true;
                var du = viewd.CurrentItem as DodatnaUsluga;
                dodatnaUsluga.Cena = du.Cena;
                dodatnaUsluga.DodatnaUsluga = du;
                dodatnaUsluga.DodatnaUslugaId = du.Id;
                this.Close();
            }

            else if (parametar == Parametar.AKCIJA)
            {
                if (naAkciji.Popust > 99)
                {
                    MessageBox.Show("Popust ne moze biti veci od 99%", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (naAkciji.Popust <= 0)
                {
                    MessageBox.Show("Popust ne moze biti manji od 0%", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (naAkciji.Popust > 0 || naAkciji.Popust < 99)
                {
                    this.DialogResult = true;
                    namestaj = viewn.CurrentItem as Namestaj;
                    naAkciji.NamestajId = namestaj.Id;
                    naAkciji.AkcijaId = akcijaId;
                    Projekat.Instance.NaAkciji.Add(naAkciji);
                    this.Close();
                }   
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
                    foreach (var naAkciji in Projekat.Instance.NaAkciji)
                    {
                        if(naAkciji.Obrisan == false)
                        {
                            if(naAkciji.NamestajId == namestaj.Id)
                            {
                                cenaAkcija = (namestaj.JedinicnaCena - (namestaj.JedinicnaCena * naAkciji.Popust) / 100) * stavka.Kolicina;
                                break;
                            }
                        }
                    }
                                                        
                    stavka.Namestaj = namestaj;
                    if(cenaAkcija == 0.0)
                    {
                        stavka.UkupnaCena = namestaj.JedinicnaCena * stavka.Kolicina;
                    }
                    else
                    {
                        stavka.UkupnaCena = cenaAkcija;
                    }

                    var listaNamestaja = Projekat.Instance.Namestaji;
                    foreach (var item in listaNamestaja)
                    {
                        if (item.Id == namestaj.Id)
                        {
                            item.KolicinaUMagacinu -= stavka.Kolicina;
                        }
                    }
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
