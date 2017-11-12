using POP_sf_41_2016_GUI.UI;
using POP_sf41_2016.model;
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

namespace POP_sf_41_2016_GUI
{
    /// <summary>
    /// Interaction logic for PrikazWindow.xaml
    /// </summary>
    public partial class PrikazWindow : Window
    {
        string tipKorisnika;
        int parametar;


        public PrikazWindow(string tipKorisnika, int parametar)
        {
            InitializeComponent();

            this.tipKorisnika = tipKorisnika;
            this.parametar = parametar;

            if(tipKorisnika.Equals("Prodavac"))
            {
                Dodajbtn.IsEnabled=false;
                btnIzmeni.IsEnabled = false;
            }
            OsveziPrikaz();
            
        }

        private void OsveziPrikaz()
        {
            switch (parametar)
            {
                case 1:
                    //dataGrid.Items.Clear();
                    var listaNamestaja = new ArrayList();
                    foreach (var namestaj in Projekat.Instance.Namestaj)
                    {
                        
                        if (namestaj.Obrisan == false)
                        {
                            var nadjeniTip = TipNamestaja.NadjiNamestaj(namestaj.TipNamestajaId);

                            //namestaj.TipNamestajaId = int.Parse(nadjeniTip);
                            listaNamestaja.Add(namestaj);
                        }
                    }
                    dataGrid.ItemsSource = listaNamestaja;
                    break;

                case 2:
                    var listaTipNamestaja = new ArrayList();
                    foreach (var tipNamestaja in Projekat.Instance.TipNamestaj)
                    {
                        if(tipNamestaja.Obrisan == false)
                        {  
                            listaTipNamestaja.Add(tipNamestaja);
                        }
                    }
                    dataGrid.ItemsSource = listaTipNamestaja;
                    break;

                case 3:
                    var listaAkcija = new ArrayList();
                    foreach (var akcija in Projekat.Instance.Akcija)
                    {
                        if (akcija.Obrisan == false)
                        {
                            
                            listaAkcija.Add(akcija);
                        }
                    }
                    dataGrid.ItemsSource = listaAkcija;
                    break;

                case 4:
                    var listaKorisnika = new ArrayList();
                    foreach (var korisnik in Projekat.Instance.Korisnik)
                    {
                        if (korisnik.obrisan == false)
                        {
                            listaKorisnika.Add(korisnik);
                        }
                    }
                    dataGrid.ItemsSource = listaKorisnika;
                    break;

                case 5:
                    var listaDodatnihUsluga = new ArrayList();
                    foreach (var dodatnaUsluga in Projekat.Instance.DodatnaUsluga)
                    {
                        if (dodatnaUsluga.Obrisan == false)
                        {
                            listaDodatnihUsluga.Add(dodatnaUsluga);
                        }
                    }
                    dataGrid.ItemsSource = listaDodatnihUsluga;
                    break;
            }

        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            
            switch (parametar)
            {
                case 1:
                    var izabraniNamestaj = (Namestaj)dataGrid.SelectedItem;
                    var namestajWindow = new NamestajWindow(izabraniNamestaj, NamestajWindow.Operacija.IZMENA);
                    namestajWindow.Show();
                    break;

                case 2:
                    var izabraniTipNamestaj = (TipNamestaja)dataGrid.SelectedItem;
                    var tipNamestajWindow = new TipNamestajaWindow(izabraniTipNamestaj, TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajWindow.Show();
                    break;
                    

                case 3:
                    var izabranaAkcija = (Akcija)dataGrid.SelectedItem;
                    var akcijaWindow = new AkcijaWindow(izabranaAkcija, AkcijaWindow.Operacija.IZMENA);
                    akcijaWindow.Show();
                    break;
                    

                case 4:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)dataGrid.SelectedItem;
                    //drugi Prozor
                    break;
                    

                case 5:
                    var izabraniKorisnik = (Korisnik)dataGrid.SelectedItem;
                    //drugi Prozor
                    break;
            }

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            OsveziPrikaz();
        }

        private void Nazad_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(tipKorisnika);
            mainWindow.Show();

            this.Close();
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Dodaj_click(object sender, RoutedEventArgs e)
        {

            switch (parametar)
            {
                case 1:
                    var noviNamestaj = new Namestaj();
                    var namestajWindow = new NamestajWindow(noviNamestaj, NamestajWindow.Operacija.DODAVANJE);
                    namestajWindow.Show();
                    break;

                case 2:
                    var noviTipNamestaja = new TipNamestaja();
                    //drugi prozor
                    //tipNamestajWindow.Show();
                    break;


                case 3:
                    var novaAkcija = new Akcija();
                    //drugi Prozor
                    break;


                case 4:
                    var novaDodatnaUsluga = new DodatnaUsluga();
                    //drugi Prozor
                    break;


                case 5:
                    var noviKorisnik = new Korisnik();
                    //drugi Prozor
                    break;
            }
        }
    }
}
