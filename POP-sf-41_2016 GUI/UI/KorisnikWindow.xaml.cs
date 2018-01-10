using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        private string korisnickoIme;
        public KorisnikWindow(Korisnik korisnik, string korisnickoIme, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.korisnickoIme = korisnickoIme;
            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbPassword.DataContext = korisnik;
            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(Enums.TipKorisnika)).Cast<Enums.TipKorisnika>();
            cbTipKorisnika.DataContext = korisnik;
            tbIme.MaxLength = 20;
            tbPrezime.MaxLength = 30;
            tbKorisnickoIme.MaxLength = 25;
            tbPassword.MaxLength = 25;

            if (operacija == Operacija.IZMENA)
            {
                tbIme.IsEnabled = false;
                tbPrezime.IsEnabled = false;

            }
        }      
        
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            if (ForceValidation() == true)
            {
                return;
            }
            else
            {
                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        KorisnikDAO.Create(korisnik);
                        this.DialogResult = true;
                        this.Close();
                        break;
                    case Operacija.IZMENA:
                        bool provera = false;
                        foreach (var item in Projekat.Instance.Korisnici)
                        {
                            if(item.KorisnickoIme == korisnik.KorisnickoIme && korisnik.KorisnickoIme != korisnickoIme)
                            {
                                MessageBox.Show("Korisnicko ime je vec zauzeto", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                provera = true;
                                break;
                            }
                        }
                        if (provera == false)
                        {
                            KorisnikDAO.Update(korisnik);
                            this.DialogResult = true;
                            this.Close();
                        }
                        break;

                }
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ForceValidation()
        {
            BindingExpression be1 = tbIme.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbPrezime.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            BindingExpression be3 = tbKorisnickoIme.GetBindingExpression(TextBox.TextProperty);
            be3.UpdateSource();
            BindingExpression be4 = tbPassword.GetBindingExpression(TextBox.TextProperty);
            be4.UpdateSource();
            if (Validation.GetHasError(tbIme) == true || Validation.GetHasError(tbPrezime) == true || Validation.GetHasError(tbKorisnickoIme) == true || Validation.GetHasError(tbPassword) == true)
            {
                return true;
            }
            return false;
        }
    }
}
