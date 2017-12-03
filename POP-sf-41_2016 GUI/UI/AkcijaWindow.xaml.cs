using POP_sf41_2016.model;
using POP_sf41_2016.util;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace POP_sf_41_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for AkcijaWindow.xaml
    /// </summary>
    public partial class AkcijaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Akcija akcija;
        private int index;
        private Operacija operacija;

        public AkcijaWindow(Akcija akcija, int index , Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.akcija = akcija;
            this.index = index;
            this.operacija = operacija;

            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            tbPopust.DataContext = akcija;

            var listaNamestaja = new List<Namestaj>();

            foreach (var item in Projekat.Instance.Namestaj)
            {
                if (item.Obrisan == false)
                {
                   listaNamestaja.Add(item);

                }
            }

            cbNamestaj.ItemsSource = listaNamestaja;
            cbNamestaj.DisplayMemberPath = "Naziv";
            cbNamestaj.DataContext = akcija;


            if (akcija.Id == 0)
            {
                dpPocetak.DisplayDateStart = DateTime.Now;
                dpKraj.DisplayDateStart = dpPocetak.SelectedDate;
                dpKraj.DisplayDateStart = DateTime.Now;
            }

        }
        private bool NamestajFilter(object obj)
        {
            return !((Namestaj)obj).Obrisan;
        }


        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaAkcija = Projekat.Instance.Akcija;
            int idAkcije = listaAkcija.Count + 1;
            
            if (operacija == Operacija.DODAVANJE)
            {
                if (akcija.DatumPocetka.Date < DateTime.Today)
                {
                    MessageBox.Show("Datum pocetka akcije ne moze biti manji od danasnjeg dana", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if (akcija.DatumZavrsetka.Date < akcija.DatumPocetka.Date)
            {
                MessageBox.Show("Datum zavrsetka akcije mora biti veci od datuma pocetka akcije", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(akcija.DatumPocetka.Date >= DateTime.Today || akcija.DatumZavrsetka.Date > akcija.DatumPocetka.Date)
            {
                if (operacija == Operacija.DODAVANJE)
                {
                    akcija.Id = idAkcije;
                    listaAkcija.Add(akcija);

                } 
                else if( operacija == Operacija.IZMENA)
                {
                    listaAkcija[index] = akcija;
                }
                Projekat.Instance.Akcija = listaAkcija;
                GenericSerializer.Serializer("akcija.xml", listaAkcija);
                this.Close();
            }
        }
    }
}
