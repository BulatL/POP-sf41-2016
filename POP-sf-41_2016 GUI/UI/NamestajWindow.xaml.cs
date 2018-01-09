using POP_sf_41_2016_GUI.DAO;
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
        public NamestajWindow(Namestaj namestaj, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            tbNaziv.MaxLength = 60;
            tbSifra.MaxLength = 20;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            if (cbTipNamestaja.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati tip namestaja", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            else
            {
                this.DialogResult = true;

                if (operacija == Operacija.DODAVANJE)
                {
                    NamestajDAO.Create(namestaj);
                }
                else if (operacija == Operacija.IZMENA)
                {
                    NamestajDAO.Update(namestaj);
                }
                this.Close();
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
