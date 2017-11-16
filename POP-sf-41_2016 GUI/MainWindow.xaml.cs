using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_sf_41_2016_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Korisnik korisnik;
        public MainWindow(Korisnik korisnik)
        {
            InitializeComponent();
/*
            Uri home = new Uri(System.IO.Path.GetFullPath($"../../image/home.jpg"));
            this.Icon = BitmapFrame.Create(home);*/

            this.korisnik = korisnik;
            
        }


        private void Namestaj_click(object sender, RoutedEventArgs e)
        {
            var prikazWindow = new PrikazWindow(korisnik, 1);
            prikazWindow.Show();
            this.Close();
        }
        private void TipNamestaja_Click(object sender, RoutedEventArgs e)
        {

            var prikazWindow = new PrikazWindow(korisnik, 2);
            prikazWindow.Show();
            this.Close();
        }

        private void Akcija_click(object sender, RoutedEventArgs e)
        {
            var prikazWindow = new PrikazWindow(korisnik, 3);
            prikazWindow.Show();
            this.Close();
        }

        private void DodatneUsluge_click(object sender, RoutedEventArgs e)
        {
            var prikazWindow = new PrikazWindow(korisnik, 4);
            prikazWindow.Show();
            this.Close();
        }

        private void Korisnici_click(object sender, RoutedEventArgs e)
        {
            var prikazWindow = new PrikazWindow(korisnik, 5);
            prikazWindow.Show();
            this.Close();
        }

        private void Salon_click(object sender, RoutedEventArgs e)
        {
            var prikazWindow = new PrikazWindow(korisnik, 6);
            prikazWindow.Show();
            this.Close();
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
