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

            var ak = new Akcija()
            {
                Id = 1,
                DatumPocetka = (DateTime)DateTime.Today.Date,
                DatumZavrsetka = (DateTime)DateTime.Today.Date,

                NamestajNaPopustuId = 1,
                Popust = 20,
                Obrisan = false
            };

            List<Akcija> lista = new List<Akcija>();
            lista.Add(ak);
            GenericSerializer.Serializer<Akcija>("akcija.xml", lista);

           
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var listaKorisnika = Projekat.Instance.Korisnik;
           
                string korisnickoIme = tbKorisnickoIme.Text.Trim();
                string sifra = pbSifra.Password.ToString().Trim();

                foreach (var korisnik in listaKorisnika)
                {
                    if (korisnickoIme.ToUpper().Equals(korisnik.KorisnickoIme.ToUpper()) && sifra.ToUpper().Equals(korisnik.Lozinka.ToUpper()))
                    {
                        var tipKorisnika = korisnik.TipKorisnika;
                        var mainWindow = new MainWindow(tipKorisnika.ToString());

                        mainWindow.Show();
                        this.Close();

                    break;
                    }
                }
                
                MessageBox.Show("Pogresno korisnicko ime ili sifra", "Neuspesan login", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
