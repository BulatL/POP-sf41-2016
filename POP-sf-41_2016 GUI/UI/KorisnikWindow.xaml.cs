using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
using System;
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
    /// Interaction logic for KorisnikWindow.xaml
    /// </summary>
    public partial class KorisnikWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Korisnik korisnik;
        private Operacija operacija;
        public KorisnikWindow(Korisnik korisnik, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbPassword.DataContext = korisnik;
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(Enums.TipKorisnika)).Cast<Enums.TipKorisnika>();
            cbTipKorisnika.DataContext = korisnik;

            if (operacija == Operacija.IZMENA)
            {
                tbIme.IsEnabled = false;
                tbPrezime.IsEnabled = false;

            }
        }      
        
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaKorisnika = Projekat.Instance.Korisnici;

            if (operacija == Operacija.DODAVANJE)
            {
                korisnik.Id = listaKorisnika.Count + 1;
                KorisnikDAO.Create(korisnik);
            }
            else if ( operacija == Operacija.IZMENA)
            {
                listaKorisnika = Korisnik.Update(korisnik);
                KorisnikDAO.Update(korisnik);
            }
            Projekat.Instance.Korisnici = listaKorisnika;
            GenericSerializer.Serializer("korisnici.xml", listaKorisnika);
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
