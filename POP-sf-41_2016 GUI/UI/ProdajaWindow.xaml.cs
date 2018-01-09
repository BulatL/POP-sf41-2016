using POP_sf_41_2016_GUI.DAO;
using POP_sf_41_2016_GUI.model;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProdajaWindow.xaml
    /// </summary>
    public partial class ProdajaWindow : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Korisnik korisnik;
        private Prodaja prodaja;
        private Operacija operacija;
        private ICollectionView view;
        private ICollectionView viewDU;
        public ProdajaWindow(Korisnik korisnik ,Prodaja prodaja, Operacija operacija)
        {
            InitializeComponent();

            this.korisnik = korisnik;
            this.prodaja = prodaja;
            this.operacija = operacija;

            if(prodaja.Id == 0)
            {
                lblImeProdavaca.Content = korisnik.KorisnickoIme;
                lblDatumProdaje.Content = DateTime.Now.Date;
            }
            else
            {
                lblImeProdavaca.DataContext = prodaja;
                lblDatumProdaje.Content = prodaja.DatumProdaje;
            }
            tbKupac.MaxLength = 20;
            prodaja.DatumProdaje = DateTime.Now.Date;
            tbKupac.DataContext = prodaja;
            tbCenaPDV.DataContext = prodaja;
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = this;
            view = CollectionViewSource.GetDefaultView(prodaja.ListaProdajeNamestaja);
            dgNamestaj.ItemsSource = view;

            dgDodatnaUsluga.AutoGenerateColumns = false;
            dgDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
            dgDodatnaUsluga.DataContext = this;
            viewDU = CollectionViewSource.GetDefaultView(prodaja.ListaDodatnihUsluga);
            dgDodatnaUsluga.ItemsSource = viewDU;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaStavki = ProdajaNamestaj.NadjiStavkuProdaje(prodaja.ListaProdajeNamestajaId);

            if (prodaja.ListaProdajeNamestaja.Count < 1)
            {
                MessageBox.Show("Morate kupiti barem jedan namestaj", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Random rn = new Random();

                if (operacija == Operacija.DODAVANJE)
                {
                    int num = rn.Next(0, 26);
                    char let = (char)('a' + num);
                    prodaja.BrRacuna = "R" + rn.Next(1, 1000).ToString() + let;
                    prodaja.ProdavacId = korisnik.Id;

                    ProdajaDAO.Create(prodaja);
                    foreach (var item in prodaja.ListaProdajeNamestaja)
                    {
                        item.ProdajaId = ProdajaDAO.GetLastId();
                        ProdajaNamestajDAO.Create(item);
                    }
                    foreach (var item in prodaja.ListaDodatnihUsluga)
                    {
                        item.ProdajaId = ProdajaDAO.GetLastId();
                        ProdajaDodatnaUslugaDAO.Create(item);
                    }
                }

                if (operacija == Operacija.IZMENA)
                {
                    var listaProvera = ProdajaNamestajDAO.LoadByProdajaId(prodaja.Id);
                    foreach (var prodajaNamestaj in prodaja.ListaProdajeNamestaja.ToList())
                    {
                        bool postojiProdajaNamestaj = false;
                        foreach (var item in listaProvera.ToList())
                        {
                            if (item.Id == prodajaNamestaj.Id)
                            {
                                postojiProdajaNamestaj = true;
                                listaProvera.Remove(item);
                                break;
                            }
                            if (item.NamestajId == prodajaNamestaj.NamestajId)
                            {
                                if (item.Kolicina != prodajaNamestaj.Kolicina)
                                {
                                    ProdajaNamestajDAO.Update(item);
                                    break;
                                }
                            }
                        }
                        if (postojiProdajaNamestaj == false)
                        {
                            prodajaNamestaj.ProdajaId = prodaja.Id;
                            ProdajaNamestajDAO.Create(prodajaNamestaj);
                        }
                    }
                    foreach (var item in listaProvera.ToList())
                    {
                        ProdajaNamestajDAO.Delete(item, ProdajaNamestajDAO.TipBrisanja.ProdajaNamestaj, 0);
                    }
                    //Provera za Dodatnu uslugu
                    var listaProveraDU = ProdajaDodatnaUslugaDAO.LoadByProdajaId(prodaja.Id);
                    foreach (var prodajaDodatnaUsluga in prodaja.ListaDodatnihUsluga.ToList())
                    {
                        bool postojiProdajaDU = false;
                        foreach (var item in listaProveraDU.ToList())
                        {
                            if (item.Id == prodajaDodatnaUsluga.Id)
                            {
                                postojiProdajaDU = true;
                                listaProveraDU.Remove(item);
                                break;
                            }
                        }
                        if (postojiProdajaDU == false)
                        {
                            prodajaDodatnaUsluga.ProdajaId = prodaja.Id;
                            ProdajaDodatnaUslugaDAO.Create(prodajaDodatnaUsluga);
                        }
                    }
                    foreach (var item in listaProveraDU.ToList())
                    {
                        ProdajaDodatnaUslugaDAO.Delete(item, ProdajaDodatnaUslugaDAO.TipBrisanja.ProdajaDodatnaUsluga, 0);
                    }
                    ProdajaDAO.Update(prodaja);
                }
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaji;
            Projekat.Instance.Namestaji.Clear();
            NamestajDAO.Load();
            this.Close();
        }

        private void DodajN_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow(new ProdajaNamestaj(), 0, StavkaWindow.Parametar.PRODAJA);
            if(stavkaWindow.ShowDialog() == true)
            {
                var novaStavka = stavkaWindow.stavka;
                prodaja.ListaProdajeNamestaja.Add(novaStavka);
                prodaja.UkupanIznos += novaStavka.UkupnaCena * Prodaja.PDV;
            }

        }

        private void DodajDU_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow(null, 0, StavkaWindow.Parametar.DODATNAUSLUGA);
            if (stavkaWindow.ShowDialog() == true)
            {
                var novaDodatnaUsluga = stavkaWindow.dodatnaUsluga;
                prodaja.ListaDodatnihUsluga.Add(novaDodatnaUsluga);
                prodaja.UkupanIznos += novaDodatnaUsluga.Cena * Prodaja.PDV;
            }

        }

        private void ObrisiN_click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = view.CurrentItem as ProdajaNamestaj;
            var lista = prodaja.ListaProdajeNamestaja;
            var listaId = prodaja.ListaProdajeNamestajaId;
            prodaja.UkupanIznos -= (izabranaStavka.UkupnaCena * 100/120);
            if(izabranaStavka.Id == 0)
            {
                foreach (var item in lista)
                {
                    if (item.NamestajId == izabranaStavka.NamestajId && item.Kolicina == izabranaStavka.Kolicina)
                    {
                        lista.Remove(item);
                        break;
                    }
                }
            }
            else
            {
                if (listaId.Count != 0)
                {
                    foreach (var item in listaId)
                    {
                        if (item == izabranaStavka.Id)
                        {
                            listaId.Remove(item);
                            break;
                        }
                    }
                }

                foreach (var item in lista)
                {
                    if (item.Id == izabranaStavka.Id)
                    {
                        lista.Remove(item);
                        break;
                    }
                }
            }
            foreach(var item in Projekat.Instance.Namestaji)
            {
                if(item.Id == izabranaStavka.NamestajId)
                {
                    item.KolicinaUMagacinu = item.KolicinaUMagacinu + izabranaStavka.Kolicina;
                    break;
                }
            }
        }

        private void ObrisiDU_click(object sender, RoutedEventArgs e)
        {
            var izabranaDodatnaUsluga = viewDU.CurrentItem as ProdajaDodatnaUsluga;
            prodaja.UkupanIznos -= (izabranaDodatnaUsluga.Cena * 100 / 120);

            foreach (var item in prodaja.ListaDodatnihUsluga)
            {
                if (izabranaDodatnaUsluga.Naziv == item.Naziv && izabranaDodatnaUsluga.Cena == item.Cena)
                {
                    prodaja.ListaDodatnihUsluga.Remove(item);
                    break;
                }
            }
        }

        private void ProdajaWindow_Closed(object sender, EventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaji;
            Projekat.Instance.Namestaji.Clear();
            NamestajDAO.Load();
            this.Close();
        }
    }
}
