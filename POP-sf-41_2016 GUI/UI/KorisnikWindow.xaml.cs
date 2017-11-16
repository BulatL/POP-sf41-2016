using POP_sf41_2016.model;
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
        public KorisnikWindow(Korisnik korisnik, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(korisnik, operacija);
        }

        private void InicijalizujVrednosti(Korisnik korisnik, Operacija operacija)
        {
            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.Text = korisnik.Ime;
            tbPrezime.Text = korisnik.Prezime;
            tbKorisnickoIme.Text = korisnik.KorisnickoIme;
            pbPassword.Password = korisnik.Lozinka;
            cbTipKorisnika.Items.Add(TipKorisnika.Administrator);
            cbTipKorisnika.Items.Add(TipKorisnika.Prodavac);

            cbTipKorisnika.SelectedItem = korisnik.TipKorisnika;

            if(operacija == Operacija.IZMENA)
            {
                tbIme.IsEnabled = false;
                tbPrezime.IsEnabled = false;

            }
        }
        

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaKorisnika = Projekat.Instance.Korisnik;

            TipKorisnika tipKorisnika =(TipKorisnika) cbTipKorisnika.SelectedItem;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        var noviKorisnik = new Korisnik()
                        {

                            Id = listaKorisnika.Count + 1,
                            Ime = tbIme.Text.Trim(),
                            Prezime = tbPrezime.Text.Trim(),
                            KorisnickoIme = tbKorisnickoIme.Text.Trim(),
                            Lozinka = pbPassword.Password.ToString().Trim(),
                            TipKorisnika = tipKorisnika

                        };
                        listaKorisnika.Add(noviKorisnik);
                    }
                    catch (Exception ex) { }
                    break;

                case Operacija.IZMENA:
                    try
                    {
                        foreach (var k in listaKorisnika)
                        {
                            if (k.Id == korisnik.Id)
                            {
                                k.Ime = tbIme.Text.Trim();
                                k.Prezime = tbPrezime.Text.Trim();
                                k.KorisnickoIme = tbKorisnickoIme.Text.Trim();
                                k.Lozinka = pbPassword.Password.ToString().Trim();
                                k.TipKorisnika = tipKorisnika;

                            }

                        }
                    }
                    catch (Exception ex) { }

                    break;
            }

            Projekat.Instance.Korisnik = listaKorisnika;
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
