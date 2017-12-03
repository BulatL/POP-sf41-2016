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
        public ProdajaWindow(ProdajaNamestaja prodaja, int index, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.index = index;
            this.operacija = operacija;



            tbKupac.DataContext = prodaja;
            dgNamestaj.AutoGenerateColumns = false;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.DataContext = prodaja;
            view = CollectionViewSource.GetDefaultView(PopuniDataGrid(kolicina,namestaj.Id));
            dgNamestaj.ItemsSource = view;

            cbDodatnaUsluga.ItemsSource = Projekat.Instance.DodatnaUsluga;
            cbDodatnaUsluga.DataContext = prodaja;
            cbDodatnaUsluga.DisplayMemberPath = "Naziv";

            foreach(var item in prodaja.ListaStavkiProdaje)
            {
                prodaja.UkupanIznos = item.UkupnaCena + prodaja.DodatnaUsluga.Cena + (prodaja.UkupanIznos * 0.02);
            }

            tbUkupanIznos.DataContext = prodaja;
            
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaProdaje = Projekat.Instance.ProdajaNamestaja;
            var listaStavkeProdaje = Projekat.Instance.StavkeProdaje;

            Random rn = new Random();

            if(operacija == Operacija.DODAVANJE)
            {
                prodaja.Id = listaProdaje.Count + 1;
                prodaja.DatumProdaje = DateTime.Now;
                prodaja.BrRacuna = "R" + rn.Next(1, 600).ToString();
                

                listaProdaje.Add(prodaja);
            }

            if(operacija == Operacija.IZMENA)
            {
                listaProdaje[index] = prodaja;
            }

            Projekat.Instance.ProdajaNamestaja = listaProdaje;
            GenericSerializer.Serializer<ProdajaNamestaja>("prodajaNamestaja.xml", listaProdaje);
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<StavkaProdaje> PopuniDataGrid(int kolicina, int namestajId)
        {
            var listaStavki = new List<StavkaProdaje>();
            if (namestajId == 0)
            {
                return listaStavki;
            }
            else
                {
                foreach (var item in listaStavki)
                {
                    item.Id = Projekat.Instance.StavkeProdaje.Count + 1;
                    item.Kolicina = kolicina;
                    item.NamestajId = namestajId;

                    listaStavki.Add(item);
                };
                return listaStavki;
            }
        }

        private void Dodaj_click(object sender, RoutedEventArgs e)
        {
            StavkaWindow stavkaWindow = new StavkaWindow();
            if(stavkaWindow.ShowDialog() == true)
            {
                namestaj = stavkaWindow.namestaj;
                kolicina = stavkaWindow.kolicina;
                PopuniDataGrid(kolicina, namestaj.Id);
            }

        }

        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = view.CurrentItem as StavkaProdaje;
            var listaStavki = Projekat.Instance.StavkeProdaje;
            foreach (var item in listaStavki)
            {
                if(item.Id == izabranaStavka.Id)
                {
                    listaStavki.Remove(item);
                    break;
                }
            }
            Projekat.Instance.StavkeProdaje = listaStavki;
            GenericSerializer.Serializer("stavkaProdaje.xml", listaStavki);
        }
    }
}
