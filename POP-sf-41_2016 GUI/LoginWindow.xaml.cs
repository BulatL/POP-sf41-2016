using POP_sf_41_2016_GUI.model;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace POP_sf_41_2016_GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static List<DodatnaUsluga> listaDodatnaUsluga { get; set; } = new List<DodatnaUsluga>();

        public LoginWindow()
        {

            InitializeComponent();

        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var listaKorisnika = Projekat.Instance.Korisnici;
           
            string korisnickoIme = tbKorisnickoIme.Text.Trim();
            string sifra = pbSifra.Password.ToString().Trim();

            bool logovaniKorisnik = false;
            bool praznoPolje = false;
            if (korisnickoIme.Equals("") || sifra.Equals("")) {
                praznoPolje = true;
                MessageBox.Show("Morate popuniti polja", "Neuspesan login", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            foreach (var korisnik in listaKorisnika)
                {
                if (korisnickoIme.Equals(korisnik.KorisnickoIme) && sifra.Equals(korisnik.Lozinka))
                {
                    var mainWindow = new MainWindow(korisnik);
                    logovaniKorisnik = true;
                    mainWindow.Show();
                    this.Close();
                    break;
                }
            }
            if (logovaniKorisnik == false && praznoPolje == false)
            MessageBox.Show("Pogresno korisnicko ime ili sifra", "Neuspesan login", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
