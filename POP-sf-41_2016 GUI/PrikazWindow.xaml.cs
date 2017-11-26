﻿using POP_sf_41_2016_GUI.UI;
using POP_sf41_2016.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Namestaj selektovaniNamestaj { get; set; }
        public TipNamestaja selektovaniTipNamestaja { get; set; }
        public Akcija selektovanaAkcija { get; set; }
        public DodatnaUsluga selektovanaDodatnaUsluga { get; set; }
        public Korisnik selektovaniKorisnik { get; set; }
        public Salon selektovaniSalon { get; set; }

        Korisnik korisnik;
        int parametar;


        public PrikazWindow(Korisnik korisnik, int parametar)
        {
            InitializeComponent();

            dataGridNamestaj.Visibility = Visibility.Hidden;
            dataGridTipNamestaja.Visibility = Visibility.Hidden;
            dataGridAkcija.Visibility = Visibility.Hidden;
            dataGridDodatnaUsluga.Visibility = Visibility.Hidden;
            dataGridKorisnik.Visibility = Visibility.Hidden;
            dataGridSalon.Visibility = Visibility.Hidden;
            dataGridProdaja.Visibility = Visibility.Hidden;

            switch (parametar)
            {
                case 1:
                    Uri namestaj = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(namestaj);
                    dataGridNamestaj.AutoGenerateColumns = false;
                    dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
                    dataGridNamestaj.DataContext = this;
                    var listaNamestaja = new ArrayList();
                    foreach (var n in Projekat.Instance.Namestaj)
                    {
                        if (n.Obrisan == false)
                        {
                            listaNamestaja.Add(n);
                        }
                    }
                    dataGridNamestaj.ItemsSource = listaNamestaja;
                    dataGridNamestaj.Visibility = Visibility.Visible;
                    break;
                case 2:
                    Uri tipNamestaja = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(tipNamestaja);
                    dataGridTipNamestaja.AutoGenerateColumns = false;
                    dataGridTipNamestaja.IsSynchronizedWithCurrentItem = true;
                    dataGridTipNamestaja.DataContext = this;
                    var listaTipaNamestaja = new ArrayList();
                    foreach (var t in Projekat.Instance.TipNamestaja)
                    {
                        if (t.Obrisan == false)
                        {
                            listaTipaNamestaja.Add(t);
                        }
                    }
                    dataGridTipNamestaja.ItemsSource = listaTipaNamestaja;
                    dataGridTipNamestaja.Visibility = Visibility.Visible;
                    break;
                case 3:
                    Uri akcija = new Uri(System.IO.Path.GetFullPath($"../../image/akcija-small.jpg"));
                    Icon = BitmapFrame.Create(akcija);
                    dataGridAkcija.AutoGenerateColumns = false;
                    dataGridAkcija.IsSynchronizedWithCurrentItem = true;
                    dataGridAkcija.DataContext = this;
                    var listaAkcija = new ArrayList();
                    foreach (var a in Projekat.Instance.Akcija)
                    {
                        if(a.DatumZavrsetka < DateTime.Now)
                        {
                            a.Obrisan = true;
                        }
                        if (a.Obrisan == false)
                        {
                            listaAkcija.Add(a);
                        }
                    }
                    dataGridAkcija.ItemsSource = listaAkcija;
                    dataGridAkcija.Visibility = Visibility.Visible;
                    break;
                case 4:
                    Uri dodatnaUsluga = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(dodatnaUsluga);
                    dataGridDodatnaUsluga.AutoGenerateColumns = false;
                    dataGridDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
                    dataGridDodatnaUsluga.DataContext = this;
                    var listaDodatnihUsluga = new ArrayList();
                    foreach (var du in Projekat.Instance.DodatnaUsluga)
                    {
                        if (du.Obrisan == false)
                        {
                            listaDodatnihUsluga.Add(du);
                        }
                    }
                    dataGridDodatnaUsluga.ItemsSource = listaDodatnihUsluga;
                    dataGridDodatnaUsluga.Visibility = Visibility.Visible;
                    break;
                case 5:
                    Uri korisnici = new Uri(System.IO.Path.GetFullPath($"../../image/user3.jpg"));
                    Icon = BitmapFrame.Create(korisnici);
                    dataGridKorisnik.AutoGenerateColumns = false;
                    dataGridKorisnik.IsSynchronizedWithCurrentItem = true;
                    dataGridKorisnik.DataContext = this;
                    var listaKorisnika = new ArrayList();
                    foreach (var k in Projekat.Instance.Korisnik)
                    {
                        if (k.Obrisan == false)
                        {
                            listaKorisnika.Add(k);
                        }
                    }
                    dataGridKorisnik.ItemsSource = listaKorisnika;
                    dataGridKorisnik.Visibility = Visibility.Visible;
                    break;
                case 6:
                    Uri salon = new Uri(System.IO.Path.GetFullPath($"../../image/home.jpg"));
                    Icon = BitmapFrame.Create(salon);
                    dataGridSalon.AutoGenerateColumns = false;
                    dataGridSalon.IsSynchronizedWithCurrentItem = true;
                    dataGridSalon.DataContext = this;
                    var listaSalona = new ArrayList();
                    foreach (var s in Projekat.Instance.Salon)
                    {
                        if (s.Obrisan == false)
                        {
                            listaSalona.Add(s);
                        }
                    }
                    dataGridSalon.ItemsSource = listaSalona;
                    dataGridSalon.Visibility = Visibility.Visible;
                    break;
            }

            this.korisnik = korisnik;
            this.parametar = parametar;

            if(korisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                Dodajbtn.IsEnabled  = false;
                btnIzmeni.IsEnabled = false;
                Obrisibtn.IsEnabled = false;
                
            }
 
            //dataGrid.Visibility = Visibility.Hidden;
            
        }
        
        /*public void OsveziPrikaz()
        {
            dataGrid.Columns.Clear();
            dataGrid.Items.Refresh();
            
            switch (parametar)
            {
                case 1:
                   
                    DataGridTextColumn id1 = new DataGridTextColumn();
                    id1.Header = "Id";
                    dataGrid.Columns.Add(id1);
                    DataGridTextColumn naziv1 = new DataGridTextColumn();
                    naziv1.Header = "Naziv";
                    dataGrid.Columns.Add(naziv1);
                    DataGridTextColumn cena1 = new DataGridTextColumn();
                    cena1.Header = "Cena";
                    dataGrid.Columns.Add(cena1);
                    DataGridTextColumn kolicina1 = new DataGridTextColumn();
                    kolicina1.Header = "Kolicina";
                    dataGrid.Columns.Add(kolicina1);
                    DataGridTextColumn tipNamestaja1 = new DataGridTextColumn();
                    tipNamestaja1.Header = "Tip namestaja";
                    dataGrid.Columns.Add(tipNamestaja1);
                    DataGridTextColumn akcija1 = new DataGridTextColumn();
                    akcija1.Header = "Akcija";
                    dataGrid.Columns.Add(akcija1);


                    var listaNamestaja = new ArrayList();
                    foreach (var namestaj in Projekat.Instance.Namestaj)
                    {
                        if (namestaj.Obrisan == false)
                        {
                            var tipNamestaja = TipNamestaja.NadjiTipNamestaj(namestaj.TipNamestajaId).Naziv;
                            listaNamestaja.Add(namestaj);
                            id1.Binding           = new Binding("Id");
                            naziv1.Binding        = new Binding("Naziv");
                            cena1.Binding         = new Binding("JedinicnaCena");
                            kolicina1.Binding     = new Binding("KolicinaUMagacinu");
                            akcija1.Binding = new Binding("AkcijaId");
                            foreach(var i in Projekat.Instance.TipNamestaj)
                            {
                                if(i.Id == namestaj.TipNamestajaId)
                                {
                                    tipNamestaja1.Binding = new Binding("Naziv");
                                }
                            }
                        }
                    }

                    dataGrid.ItemsSource = listaNamestaja;
                    /*
                    cbPretrazi.Items.Add("Naziv");
                    cbPretrazi.Items.Add("Tip namestaja");
                    cbPretrazi.Items.Add("Sifra");
                    break;

                case 2:
                    
                    DataGridTextColumn id2 = new DataGridTextColumn();
                    id2.Header = "Id";
                    dataGrid.Columns.Add(id2);
                    DataGridTextColumn naziv2 = new DataGridTextColumn();
                    naziv2.Header = "Naziv";
                    dataGrid.Columns.Add(naziv2);

                    var listaTipNamestaja = new ArrayList();
                    foreach (var tipNamestaja in Projekat.Instance.TipNamestaj)
                    {
                        if(tipNamestaja.Obrisan == false)
                        {
                            listaTipNamestaja.Add(tipNamestaja);
                            id2.Binding    = new Binding("Id");
                            naziv2.Binding = new Binding("Naziv");
                        }
                    }
                    dataGrid.ItemsSource = listaTipNamestaja;
                    /*cbPretrazi.Visibility = Visibility.Hidden;
                    cbPretrazi.Items.Add("Nazivu");
                    break;

                case 3:
                    
                    DataGridTextColumn id3 = new DataGridTextColumn();
                    id3.Header = "Id";
                    dataGrid.Columns.Add(id3);

                    DataGridTextColumn datumPocetka = new DataGridTextColumn();
                    datumPocetka.Header = "Datum pocetka";
                    dataGrid.Columns.Add(datumPocetka);

                    DataGridTextColumn datumZavrsetka = new DataGridTextColumn();
                    datumZavrsetka.Header = "Datum zavrsetka";
                    dataGrid.Columns.Add(datumZavrsetka);

                    DataGridTextColumn popust = new DataGridTextColumn();
                    popust.Header = "Popust";
                    dataGrid.Columns.Add(popust);

                    DataGridTextColumn namestajNaPopustu = new DataGridTextColumn();
                    namestajNaPopustu.Header = "Namestaj";
                    dataGrid.Columns.Add(namestajNaPopustu);

                    var listaAkcija = new ArrayList();
                    foreach (var akcija in Projekat.Instance.Akcija)
                    {
                        if (akcija.Obrisan == false)
                        {
                            listaAkcija.Add(akcija);
                            id3.Binding               = new Binding("Id");
                            datumPocetka.Binding      = new Binding("DatumPocetka");
                            datumZavrsetka.Binding    = new Binding("DatumZavrsetka");
                            popust.Binding            = new Binding("Popust");
                            namestajNaPopustu.Binding = new Binding("NamestajNaPopustuId");
                           
                        }
                    }
                    dataGrid.ItemsSource = listaAkcija;

                    /*cbPretrazi.Items.Add("Datum pocetka");
                    cbPretrazi.Items.Add("Datum zavrsetka");
                    cbPretrazi.Items.Add("Namestaj");

                    break;

                case 4:
                    
                    DataGridTextColumn id5 = new DataGridTextColumn();
                    id5.Header = "Id";
                    dataGrid.Columns.Add(id5);

                    DataGridTextColumn naziv3 = new DataGridTextColumn();
                    naziv3.Header = "Naziv";
                    dataGrid.Columns.Add(naziv3);

                    DataGridTextColumn cena2 = new DataGridTextColumn();
                    cena2.Header = "Cena";
                    dataGrid.Columns.Add(cena2);

                    var listaDodatnihUsluga = new ArrayList();
                    foreach (var dodatnaUsluga in Projekat.Instance.DodatnaUsluga)
                    {
                        if (dodatnaUsluga.Obrisan == false)
                        {
                            listaDodatnihUsluga.Add(dodatnaUsluga);
                            id5.Binding    = new Binding("Id");
                            naziv3.Binding = new Binding("Naziv");
                            cena2.Binding  = new Binding("Cena");
                        }
                    }
                    dataGrid.ItemsSource = listaDodatnihUsluga;
                   
                    break;


                case 5:
                    
                    DataGridTextColumn id4 = new DataGridTextColumn();
                    id4.Header = "Id";
                    dataGrid.Columns.Add(id4);

                    DataGridTextColumn ime = new DataGridTextColumn();
                    ime.Header = "Ime";
                    dataGrid.Columns.Add(ime);

                    DataGridTextColumn prezime = new DataGridTextColumn();
                    prezime.Header = "Prezime";
                    dataGrid.Columns.Add(prezime);

                    DataGridTextColumn korisnickoIme = new DataGridTextColumn();
                    korisnickoIme.Header = "Korisnicko ime";
                    dataGrid.Columns.Add(korisnickoIme);

                    DataGridTextColumn tipKorisnika = new DataGridTextColumn();
                    tipKorisnika.Header = "Tip Korisnika";
                    dataGrid.Columns.Add(tipKorisnika);


                    var listaKorisnika = new ArrayList();
                    foreach (var korisnik in Projekat.Instance.Korisnik)
                    {
                        if (korisnik.obrisan == false)
                        {
                            listaKorisnika.Add(korisnik);
                            id4.Binding = new Binding("Id");
                            ime.Binding = new Binding("Ime");
                            prezime.Binding = new Binding("Prezime");
                            korisnickoIme.Binding = new Binding("KorisnickoIme");
                            tipKorisnika.Binding = new Binding("TipKorisnika");
                        }
                    }
                    dataGrid.ItemsSource = listaKorisnika;

                    /*cbPretrazi.Items.Add("Imenu");
                    cbPretrazi.Items.Add("Prezimenu");
                    cbPretrazi.Items.Add("Korisnickom imenu");

                    break;

                case 6:
                    
                    DataGridTextColumn id6 = new DataGridTextColumn();
                    id6.Header = "Id";
                    dataGrid.Columns.Add(id6);

                    DataGridTextColumn naziv4 = new DataGridTextColumn();
                    naziv4.Header = "Naziv";
                    dataGrid.Columns.Add(naziv4);

                    DataGridTextColumn adresa = new DataGridTextColumn();
                    adresa.Header = "Adresa";
                    dataGrid.Columns.Add(adresa);

                    DataGridTextColumn telefon = new DataGridTextColumn();
                    telefon.Header = "Telefon";
                    dataGrid.Columns.Add(telefon);

                    DataGridTextColumn email = new DataGridTextColumn();
                    email.Header = "Email";
                    dataGrid.Columns.Add(email);

                    DataGridTextColumn adresaInternetSajta = new DataGridTextColumn();
                    adresaInternetSajta.Header = "Sajta";
                    dataGrid.Columns.Add(adresaInternetSajta);

                    DataGridTextColumn PIB = new DataGridTextColumn();
                    PIB.Header = "PIB";
                    dataGrid.Columns.Add(PIB);

                    DataGridTextColumn maticniBroj = new DataGridTextColumn();
                    maticniBroj.Header = "Maticni broj";
                    dataGrid.Columns.Add(maticniBroj);

                    DataGridTextColumn brojZiroRacuna = new DataGridTextColumn();
                    brojZiroRacuna.Header = "Broj ziro racuna";
                    dataGrid.Columns.Add(brojZiroRacuna);

                    var listaSalon = new ArrayList();
                    foreach (var salon in Projekat.Instance.Salon)
                    {
                        if (salon.Obrisan == false)
                        {
                            listaSalon.Add(salon);
                            id6.Binding                 = new Binding("Id");
                            naziv4.Binding              = new Binding("Naziv");
                            adresa.Binding              = new Binding("Adresa");
                            telefon.Binding             = new Binding("Telefon");
                            email.Binding               = new Binding("Email");
                            adresaInternetSajta.Binding = new Binding("AdresaInternetSajta");
                            PIB.Binding                 = new Binding("PIB");
                            maticniBroj.Binding         = new Binding("MaticniBroj");
                            brojZiroRacuna.Binding      = new Binding("BrojZiroRacuna");
                        }
                    }
                    dataGrid.ItemsSource = listaSalon;
                    
                    break;
            }

        }*/

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            
            switch (parametar)
            {
                case 1:
                    Namestaj kopija = (Namestaj)selektovaniNamestaj.Clone();
                    var namestajWindow = new NamestajWindow(kopija, NamestajWindow.Operacija.IZMENA);
                    namestajWindow.ShowDialog();

                    break;

                case 2:
                    var izabraniTipNamestaj = (TipNamestaja)selektovaniTipNamestaja;
                    var tipNamestajWindow = new TipNamestajaWindow(izabraniTipNamestaj, TipNamestajaWindow.Operacija.IZMENA);
                    tipNamestajWindow.Show();
                    
                    break;
                    

                case 3:
                    var izabranaAkcija = (Akcija)selektovanaAkcija;
                    var akcijaWindow = new AkcijaWindow(izabranaAkcija, AkcijaWindow.Operacija.IZMENA);
                    akcijaWindow.Show();
                    
                    break;
                    

                case 4:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)selektovanaDodatnaUsluga;
                    var dodatnaUslugaWindow = new DodatnaUslugaWindow(izabranaDodatnaUsluga, DodatnaUslugaWindow.Operacija.IZMENA);
                    dodatnaUslugaWindow.Show();
                    
                    break;
                    

                case 5:
                    var izabraniKorisnik = (Korisnik)selektovaniKorisnik;
                    var korisnikWindow = new KorisnikWindow(izabraniKorisnik, KorisnikWindow.Operacija.IZMENA);
                    korisnikWindow.Show();
                    
                    break;

                case 6:
                    var izabraniSalon = (Salon)selektovaniSalon;
                    var salonWindow = new SalonWindow(izabraniSalon, SalonWindow.Operacija.IZMENA);
                    salonWindow.Show();
                    
                    break;
            }
            

        }

        private void Nazad_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(korisnik);
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
                    var tipNamestajaWindow = new TipNamestajaWindow(noviTipNamestaja, TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaWindow.Show();
                    
                    break;


                case 3:
                    var novaAkcija = new Akcija();
                    var akcijaWindow = new AkcijaWindow(novaAkcija, AkcijaWindow.Operacija.DODAVANJE);
                    akcijaWindow.Show();
                    
                    break;


                case 4:
                    var novaDodatnaUsluga = new DodatnaUsluga();
                    var dodatnaUslugaWindow = new DodatnaUslugaWindow(novaDodatnaUsluga, DodatnaUslugaWindow.Operacija.DODAVANJE);
                    dodatnaUslugaWindow.Show();
                    
                    break;


                case 5:
                    var noviKorisnik = new Korisnik();
                    var korisnikWindow = new KorisnikWindow(noviKorisnik, KorisnikWindow.Operacija.DODAVANJE);
                    korisnikWindow.Show();
                    
                    break;

                case 6:
                    var noviSalon = new Salon();
                    var salonWindow = new SalonWindow(noviSalon, SalonWindow.Operacija.DODAVANJE);
                    salonWindow.Show();
                    
                    break;
            }
        }

        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            switch (parametar)

            {
                case 1:
                    var izabraniNamestaj = (Namestaj)selektovaniNamestaj;
                    var listaNamestaja = Projekat.Instance.Namestaj;
                    if(MessageBox.Show("Da li ste sigurni da zelite da obrisete namestaj", "Obrisi namestaj", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var namestaj in listaNamestaja)
                        {
                            if(izabraniNamestaj.Id == namestaj.Id)
                            {
                                namestaj.Obrisan = true;
                            }
                        }
                        Projekat.Instance.Namestaj = listaNamestaja;
                    }
                    
                    break;

                case 2:
                    var izabraniTipNamestaja = (TipNamestaja)selektovaniTipNamestaja;
                    var listaTipNamestaja = Projekat.Instance.TipNamestaja;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete tip namestaja", "Obrisi tip namestaja", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var tipNamestaja in listaTipNamestaja)
                        {
                            if(izabraniTipNamestaja.Id == tipNamestaja.Id)
                            {
                                tipNamestaja.Obrisan = true;
                            }
                        }
                        Projekat.Instance.TipNamestaja = listaTipNamestaja;
                        
                    }
                    
                    break;


                case 3:
                    var izabranaAkcija = (Akcija)selektovanaAkcija;
                    var listaAkcija = Projekat.Instance.Akcija;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete akciju", "Obrisi akciju", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var akcija in listaAkcija)
                        {
                            if(izabranaAkcija.Id == akcija.Id)
                            {
                                akcija.Obrisan = false;
                            }
                        }
                        Projekat.Instance.Akcija = listaAkcija;
                        
                    }
                    
                    break;


                case 4:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)selektovanaDodatnaUsluga;
                    var listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete dodatnu uslug", "Obrisi dodatnu uslug", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var dodatnaUsluga in listaDodatnihUsluga)
                        {
                            if (izabranaDodatnaUsluga.Id == dodatnaUsluga.Id)
                            {
                                dodatnaUsluga.Obrisan = true;
                            }
                        }

                        Projekat.Instance.DodatnaUsluga = listaDodatnihUsluga;
                        
                    }
                    
                    break;


                case 5:
                    var izabraniKorisnik = (Korisnik)selektovaniKorisnik;
                    var listaKorisnika = Projekat.Instance.Korisnik;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete korisnika", "Obrisi korisnika", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var korisnik in listaKorisnika)
                        {
                            if(izabraniKorisnik.Id == korisnik.Id)
                            {
                                korisnik.Obrisan = true;
                            }
                        }
                        Projekat.Instance.Korisnik = listaKorisnika;
                        
                    }
                    
                    break;
                    
                    case 6:
                        var izabraniSalon = (Salon)selektovaniSalon;
                        var listaSalona = Projekat.Instance.Salon;
                        if (MessageBox.Show("Da li ste sigurni da zelite da obrisete salon", "Obrisi salon", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            foreach(var salon in listaSalona)
                            {
                                if(izabraniSalon.Id == salon.Id)
                                {
                                salon.Obrisan = true;
                                }
                            }
                        Projekat.Instance.Salon = listaSalona;
                        }
                        
                        break;
            }
        }

        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
