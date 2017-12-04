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
        private int index;
        private Operacija operacija;
        private ICollectionView view;
        private Namestaj namestaj;
        private int kolicina;
        private StavkaProdaje preuzetaStavkaProdaje = new StavkaProdaje();
        private ObservableCollection<StavkaProdaje> listaStavki = new ObservableCollection<StavkaProdaje>();
        private ObservableCollection<int?> listaStavkiProdajeId = new ObservableCollection<int?>();
        public ProdajaWindow(ProdajaNamestaja prodaja, int index, Operacija operacija)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.index = index;
            this.operacija = operacija;

            listaStavkiProdajeId = new ObservableCollection<int?>();
            prodaja.ListaStavkiProdajeId = listaStavkiProdajeId;

            tbKupac.DataContext = prodaja;
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = prodaja;
            view = CollectionViewSource.GetDefaultView(prodaja.ListaStavkiProdaje);
            dgNamestaj.ItemsSource = view;

            var listaDodatnihUsluga = new List<DodatnaUsluga>();

            foreach (var item in Projekat.Instance.DodatnaUsluga)
            {
                if (item.Obrisan == false)
                {
                    listaDodatnihUsluga.Add(item);

                }
            }

            cbDodatnaUsluga.ItemsSource = listaDodatnihUsluga;
            cbDodatnaUsluga.DataContext = prodaja;
            cbDodatnaUsluga.DisplayMemberPath = "Naziv";

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaProdaje = Projekat.Instance.ProdajaNamestaja;
            var listaStavkeProdaje = Projekat.Instance.StavkeProdaje;
            var ukupnaCena = 0.0;
            prodaja.ListaStavkiProdajeId = listaStavkiProdajeId;
            var listaStavki = StavkaProdaje.NadjiStavkuProdaje(prodaja.ListaStavkiProdajeId);
            foreach (var item in prodaja.ListaStavkiProdaje)
            {
                if(prodaja.DodatnaUsluga == null)
                {
                    ukupnaCena = item.UkupnaCena + (prodaja.UkupanIznos * 0.02);
                }
                else 
                ukupnaCena = item.UkupnaCena + prodaja.DodatnaUsluga.Cena + (prodaja.UkupanIznos * 0.02);
            }

            Random rn = new Random();

            if(operacija == Operacija.DODAVANJE)
            {
                prodaja.Id = listaProdaje.Count + 1;
                prodaja.DatumProdaje = DateTime.Now;
                prodaja.BrRacuna = "R" + rn.Next(1, 600).ToString();
                prodaja.UkupanIznos = ukupnaCena;



                listaProdaje.Add(prodaja);
            }

            if(operacija == Operacija.IZMENA)
            {
                listaProdaje[index] = prodaja;
                prodaja.UkupanIznos = ukupnaCena;
            }

            Projekat.Instance.ProdajaNamestaja = listaProdaje;
            GenericSerializer.Serializer("prodajaNamestaja.xml", listaProdaje);
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow();
            if(stavkaWindow.ShowDialog() == true)
            {
                namestaj = stavkaWindow.namestaj;
                kolicina = stavkaWindow.kolicina;
                preuzetaStavkaProdaje = stavkaWindow.stavkaProdaje;
                prodaja.ListaStavkiProdajeId.Add(preuzetaStavkaProdaje.Id);
                listaStavki.Add(preuzetaStavkaProdaje);
                prodaja.ListaStavkiProdaje.Add(preuzetaStavkaProdaje);
                //listaStavkiProdajeId.Add(preuzetaStavkaProdaje.Id);
            }

        }

        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = view.CurrentItem as StavkaProdaje;
            var lista = prodaja.ListaStavkiProdaje;
            foreach (var item in lista)
            {
                if(item.Id == izabranaStavka.Id)
                {
                    item.Obrisan = true;
                    lista.Remove(item);
                    break;
                }
            }
            Projekat.Instance.StavkeProdaje = lista;
            GenericSerializer.Serializer("stavkaProdaje.xml", listaStavki);
        }
    }
}
