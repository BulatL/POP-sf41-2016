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

namespace POP_sf_41_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Namestaj namestaj;
        private Operacija operacija;
        private int index;
        public NamestajWindow(Namestaj namestaj, int index , Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.index = index;
            this.operacija = operacija;

            var listaTipaNamestaj = new List<TipNamestaja>();

            foreach (var item in Projekat.Instance.TipNamestaja)
            {
                if (item.Obrisan == false)
                {
                    listaTipaNamestaj.Add(item);

                }
            }

            cbTipNamestaja.ItemsSource = listaTipaNamestaj;
            cbTipNamestaja.DisplayMemberPath = "Naziv";

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var ListaNamestaja = Projekat.Instance.Namestaj;

            if (operacija == Operacija.DODAVANJE)
            {
                namestaj.Id = ListaNamestaja.Count + 1;

                ListaNamestaja.Add(namestaj);
            }
            else if( operacija == Operacija.IZMENA)
            {
                ListaNamestaja[index] = namestaj;
            }

            Projekat.Instance.Namestaj = ListaNamestaja;
            GenericSerializer.Serializer("namestaj.xml", ListaNamestaja);
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
