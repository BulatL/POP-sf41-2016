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
        private ProdajaNamestaja prodaja;
        private Operacija operacija;
        private ICollectionView view;
        private ICollectionView viewDU;
        private double cenaBezPdva;
        private ObservableCollection<int?> listaStavkiProdajeId = new ObservableCollection<int?>();
        public ProdajaWindow(ProdajaNamestaja prodaja, Operacija operacija)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.operacija = operacija;

            listaStavkiProdajeId = new ObservableCollection<int?>();
            cenaBezPdva = 0.0;

            tbKupac.DataContext = prodaja;
            tbCena.DataContext = prodaja;
            tbCenaPDV.DataContext = cenaBezPdva;
            prodaja.UkupanIznos = prodaja.UkupanIznos + (prodaja.UkupanIznos * 0.02);
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = prodaja;
            view = CollectionViewSource.GetDefaultView(prodaja.ListaStavkiProdaje);
            dgNamestaj.ItemsSource = view;

            dgDodatnaUsluga.AutoGenerateColumns = false;
            dgDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
            dgDodatnaUsluga.DataContext = prodaja;
            viewDU = CollectionViewSource.GetDefaultView(prodaja.ListaDodatnihUsluga);
            dgDodatnaUsluga.ItemsSource = viewDU;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaProdaje = Projekat.Instance.ProdajaNamestaja;
            prodaja.ListaStavkiProdajeId = listaStavkiProdajeId;
            var listaStavki = StavkaProdaje.NadjiStavkuProdaje(prodaja.ListaStavkiProdajeId);

            Random rn = new Random();

            if(operacija == Operacija.DODAVANJE)
            {
                prodaja.Id = listaProdaje.Count + 1;
                prodaja.DatumProdaje = DateTime.Now;
                prodaja.BrRacuna = "R" + rn.Next(1, 600).ToString();
                prodaja.UkupanIznos = prodaja.UkupanIznos + (prodaja.UkupanIznos * 0.02);

                listaProdaje.Add(prodaja);
            }

            if(operacija == Operacija.IZMENA)
            {
                prodaja.UkupanIznos = prodaja.UkupanIznos + (prodaja.UkupanIznos * 0.02);
                listaProdaje = ProdajaNamestaja.Update(prodaja);
            }

            Projekat.Instance.ProdajaNamestaja = listaProdaje;
            GenericSerializer.Serializer("prodajaNamestaja.xml", listaProdaje);
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaj;
            foreach(var item in listaNamestaja)
            {
                foreach(var prodaja in prodaja.ListaStavkiProdaje)
                {
                    if(prodaja.NamestajId == item.Id)
                    {
                        item.KolicinaUMagacinu = item.KolicinaUMagacinu + prodaja.Kolicina;
                    }
                }
            }

            Projekat.Instance.Namestaj = listaNamestaja;
            GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
            this.Close();
        }

        private void DodajN_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow(new StavkaProdaje(), StavkaWindow.Parametar.PRODAJA);
            if(stavkaWindow.ShowDialog() == true)
            {
                var novaStavka = stavkaWindow.stavka;
                prodaja.ListaStavkiProdaje.Add(novaStavka);
                cenaBezPdva = cenaBezPdva + novaStavka.UkupnaCena;
                prodaja.UkupanIznos = +cenaBezPdva;
            }

        }

        private void DodajDU_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow(null, StavkaWindow.Parametar.DODATNAUSLUGA);
            if (stavkaWindow.ShowDialog() == true)
            {
                var novaDodatnaUsluga = stavkaWindow.dodatnaUsluga;
                prodaja.ListaDodatnihUsluga.Add(novaDodatnaUsluga);
                cenaBezPdva = cenaBezPdva + novaDodatnaUsluga.Cena;
                prodaja.UkupanIznos = +cenaBezPdva;
            }

        }

        private void ObrisiN_click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = view.CurrentItem as StavkaProdaje;
            var listaStavki = Projekat.Instance.StavkeProdaje;
            var listaNamestaja = Projekat.Instance.Namestaj;
            var lista = prodaja.ListaStavkiProdaje;
            cenaBezPdva = cenaBezPdva - izabranaStavka.UkupnaCena;
            prodaja.UkupanIznos = +cenaBezPdva;
            foreach (var item in lista)
            {
                if(item.Id == izabranaStavka.Id)
                {
                    lista.Remove(item);
                    break;
                }
            }

            foreach (var item in listaStavki)
            {
                if(item.Id == izabranaStavka.Id)
                {
                    item.Obrisan = true;
                    break;
                }
            }
            foreach(var item in listaNamestaja)
            {
                if(item.Id == izabranaStavka.Id)
                {
                    item.KolicinaUMagacinu = item.KolicinaUMagacinu + izabranaStavka.Kolicina;
                    listaNamestaja.Add(item);
                    break;
                }
            }
            Projekat.Instance.StavkeProdaje = lista;
            GenericSerializer.Serializer("stavkaProdaje.xml", listaStavki);
            Projekat.Instance.Namestaj = listaNamestaja;
            GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
        }

        private void ObrisiDU_click(object sender, RoutedEventArgs e)
        {
            var izabranaDodatnaUsluga = viewDU.CurrentItem as DodatnaUsluga;
            cenaBezPdva = cenaBezPdva - izabranaDodatnaUsluga.Cena;
            prodaja.UkupanIznos = +cenaBezPdva;
            foreach (var item in prodaja.ListaDodatnihUsluga)
            {
                if (izabranaDodatnaUsluga.Id == item.Id)
                {
                    prodaja.ListaDodatnihUsluga.Remove(item);
                    break;
                }
            }
        }

        private void ProdajaWindow_Closed(object sender, EventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaj;
            foreach (var item in listaNamestaja)
            {
                foreach (var prodaja in prodaja.ListaStavkiProdaje)
                {
                    if (prodaja.NamestajId == item.Id)
                    {
                        item.KolicinaUMagacinu = item.KolicinaUMagacinu + prodaja.Kolicina;
                    }
                }
            }

            Projekat.Instance.Namestaj = listaNamestaja;
            GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
            this.Close();
        }
    }
}
