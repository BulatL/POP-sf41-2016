using POP_sf_41_2016_GUI.UI;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
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
        public enum Parametar
        {
            Namestaj,
            TipNamestaja,
            Akcija,
            DodatnaUsluga,
            Korisnik,
            Salon,
            Prodaja
        }

        public Parametar parametar;
        private ICollectionView viewn;
        private ICollectionView viewt;
        private ICollectionView viewa;
        private ICollectionView viewd;
        private ICollectionView views;
        private ICollectionView viewk;
        private ICollectionView viewp;

        public PrikazWindow(Korisnik korisnik, Parametar parametar)
        {
            InitializeComponent();

            dataGridNamestaj.Visibility = Visibility.Hidden;
            dataGridTipNamestaja.Visibility = Visibility.Hidden;
            dataGridAkcija.Visibility = Visibility.Hidden;
            dataGridDodatnaUsluga.Visibility = Visibility.Hidden;
            dataGridKorisnik.Visibility = Visibility.Hidden;
            dataGridSalon.Visibility = Visibility.Hidden;
            dataGridProdaja.Visibility = Visibility.Hidden;


            this.korisnik = korisnik;
            this.parametar = parametar;

            if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                Dodajbtn.IsEnabled = false;
                btnIzmeni.IsEnabled = false;
                Obrisibtn.IsEnabled = false;
            }

            switch (parametar)
            {
                #region Namestaj punjenje dg
                case Parametar.Namestaj:
                    Uri namestaj = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(namestaj);
                    //Punjenje dataGrida
                    dataGridNamestaj.AutoGenerateColumns = false;
                    dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
                    dataGridNamestaj.DataContext = this;
                    viewn = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
                    viewn.Filter = NamestajFilter;
                    dataGridNamestaj.ItemsSource = viewn;
                    dataGridNamestaj.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazi
                    cbPretrazi.Items.Add("Nazivu");
                    cbPretrazi.Items.Add("Tipu namestaja");
                    cbPretrazi.Items.Add("Sifri");
                    cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Nazivu");
                    cbSortiraj.Items.Add("Sifri");
                    cbSortiraj.Items.Add("Ceni");
                    cbSortiraj.Items.Add("Kolicini");
                    cbSortiraj.Items.Add("Tipu namestaja");
                    cbSortiraj.SelectedIndex = 0;
                    break;
#endregion

                #region Tip namestaja punjenje dg
                case Parametar.TipNamestaja:
                    Uri tipNamestaja = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(tipNamestaja);
                    //Punjenje dataGrida
                    dataGridTipNamestaja.AutoGenerateColumns = false;
                    dataGridTipNamestaja.DataContext = this;
                    viewt = CollectionViewSource.GetDefaultView(Projekat.Instance.TipNamestaja);
                    viewt.Filter = TipNamestajaFilter;
                    dataGridTipNamestaja.ItemsSource = viewt;
                    dataGridTipNamestaja.IsSynchronizedWithCurrentItem = true;
                    dataGridTipNamestaja.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazi
                    cbPretrazi.Items.Add("Nazivu");
                    cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Nazivu");
                    cbSortiraj.SelectedIndex = 0;
                    break;
#endregion

                #region Akcija punjenje dg
                case Parametar.Akcija:
                    Uri akcija = new Uri(System.IO.Path.GetFullPath($"../../image/akcija-small.jpg"));
                    Icon = BitmapFrame.Create(akcija);
                    //Punjenje dataGrida
                    dataGridAkcija.AutoGenerateColumns = false;
                    dataGridAkcija.IsSynchronizedWithCurrentItem = true;
                    dataGridAkcija.DataContext = this;
                    viewa = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);
                    viewa.Filter = AkcijaFilter;
                    dataGridAkcija.ItemsSource = viewa;
                    dataGridAkcija.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazivanje
                    cbPretrazi.Items.Add("Datum pocetka");
                    cbPretrazi.Items.Add("Datum zavrsetka");
                    cbPretrazi.Items.Add("Namestaj");
                    cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Datumu pocetka");
                    cbSortiraj.Items.Add("Datumu zavrsetka");
                    cbSortiraj.Items.Add("Namestaju");
                    cbSortiraj.Items.Add("Popustu");
                    cbSortiraj.SelectedIndex = 0;
                    break;
#endregion

                #region Dodatna usluga punjenje dg
                case Parametar.DodatnaUsluga:
                    Uri dodatnaUsluga = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(dodatnaUsluga);
                    //Punjenje dataGrida
                    dataGridDodatnaUsluga.AutoGenerateColumns = false;
                    dataGridDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
                    dataGridDodatnaUsluga.DataContext = this;
                    viewd = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga);
                    viewd.Filter = DodatnaUslugaFilter;
                    dataGridDodatnaUsluga.ItemsSource = viewd;
                    dataGridDodatnaUsluga.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Nazivu");
                    cbSortiraj.Items.Add("Ceni");
                    cbSortiraj.SelectedIndex = 0;
                    break;
#endregion

                #region Korisnik punjenje dg
                case Parametar.Korisnik:
                    Uri korisnici = new Uri(System.IO.Path.GetFullPath($"../../image/user3.jpg"));
                    Icon = BitmapFrame.Create(korisnici);
                    //Punjenje dataGrida
                    dataGridKorisnik.AutoGenerateColumns = false;
                    dataGridKorisnik.IsSynchronizedWithCurrentItem = true;
                    dataGridKorisnik.DataContext = this;
                    viewk = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnik);
                    viewk.Filter = KorisnikFilter;
                    dataGridKorisnik.ItemsSource = viewk;
                    dataGridKorisnik.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretragu
                    cbPretrazi.Items.Add("Imenu");
                    cbPretrazi.Items.Add("Prezimenu");
                    cbPretrazi.Items.Add("Korisnickom imenu");
                    cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Imenu");
                    cbSortiraj.Items.Add("Prezimenu");
                    cbSortiraj.Items.Add("Korisnickom imenu");
                    cbSortiraj.Items.Add("Lozinci");
                    cbSortiraj.Items.Add("Tipu korisnika");
                    cbSortiraj.SelectedIndex = 0;
                    break;
#endregion

                #region Salon punjenje dg
                case Parametar.Salon:
                    /*Uri salon = new Uri(System.IO.Path.GetFullPath($"../../image/home.jpg"));
                    Icon = BitmapFrame.Create(salon);*/
                    //Punjenje DataGrida
                    dataGridSalon.AutoGenerateColumns = false;
                    dataGridSalon.IsSynchronizedWithCurrentItem = true;
                    dataGridSalon.DataContext = this;
                    views = CollectionViewSource.GetDefaultView(Projekat.Instance.Salon);
                    views.Filter = SalonFilter;
                    dataGridSalon.ItemsSource = views;
                    dataGridSalon.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Sortiranje
                    cbSortiraj.Items.Add("Id-u");
                    cbSortiraj.Items.Add("Nazivu");
                    cbSortiraj.Items.Add("Adresi");
                    cbSortiraj.Items.Add("Telefonu");
                    cbSortiraj.Items.Add("Emailu");
                    cbSortiraj.Items.Add("Sajtu");
                    cbSortiraj.Items.Add("PIB-u");
                    cbSortiraj.Items.Add("Maticnom broju");
                    cbSortiraj.Items.Add("Ziro racunu");
                    cbSortiraj.SelectedIndex = 0;
                    break;
                #endregion

                #region Prodaja punjenje dg
                case Parametar.Prodaja:
                    dataGridProdaja.AutoGenerateColumns = false;
                    dataGridProdaja.IsSynchronizedWithCurrentItem = true;
                    dataGridProdaja.DataContext = this;
                    viewp = CollectionViewSource.GetDefaultView(Projekat.Instance.ProdajaNamestaja);
                    viewp.Filter = ProdajaFilter;
                    dataGridProdaja.ItemsSource = viewp;
                    dataGridProdaja.Visibility = Visibility.Visible;

                    if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
                    {
                        Dodajbtn.IsEnabled = true;
                        btnIzmeni.IsEnabled = true;
                    }
                    break;
#endregion
            }           
        }

        #region Filteri Dg
        private bool NamestajFilter(object obj) //Prima namestaj i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((Namestaj)obj).Obrisan;
        }

        private bool TipNamestajaFilter(object obj) //Prima tip namestaja i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((TipNamestaja)obj).Obrisan;
        }

        private bool AkcijaFilter(object obj) //Prima akciju i ukoliko je obrisan true i datum zavrsetka veci od danasnjeg ne vraca ga nazad
        {
            bool akcijaa = false;
            if (((Akcija)obj).DatumZavrsetka < DateTime.Today) ((Akcija)obj).Obrisan = true ;
            if (((Akcija)obj).Obrisan == false) akcijaa = true;
            else { akcijaa = false; }
            return akcijaa;
        }

        private bool DodatnaUslugaFilter(object obj) //Prima dodatnu uslugu i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((DodatnaUsluga)obj).Obrisan;
        }

        private bool KorisnikFilter(object obj) //Prima korisnika i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((Korisnik)obj).Obrisan;
        }

        private bool SalonFilter(object obj) //Prima salon i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((Salon)obj).Obrisan;
        }

        private bool ProdajaFilter(object obj) //Prima prodaju i ukoliko je obrisan true ne vraca ga nazad
        {
            return !((ProdajaNamestaja)obj).Obrisan;
        }
        #endregion

        #region Izmeni
        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            
            switch (parametar)
            {
                case Parametar.Namestaj:
                    Namestaj izabraniNamestaj = viewn.CurrentItem as Namestaj; // uzima trenutni selektovan red u dataGridu
                    var listaNamestaja = Projekat.Instance.Namestaj;
                    if(izabraniNamestaj != null)
                    {
                        Namestaj kopija = (Namestaj)izabraniNamestaj.Clone();

                        NamestajWindow namestajWindow = new NamestajWindow(kopija, NamestajWindow.Operacija.IZMENA);
                        namestajWindow.ShowDialog();
                    }
                    break;

                case Parametar.TipNamestaja:

                    TipNamestaja izabraniTipNamestaja = viewt.CurrentItem as TipNamestaja; // uzima trenutni selektovan red u dataGridu
                    if (izabraniTipNamestaja.Id == 0)
                    {
                        MessageBox.Show("Ne mozete izmeniti ovaj tip namestaja", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (izabraniTipNamestaja != null)
                        {
                            TipNamestaja kopija = (TipNamestaja)izabraniTipNamestaja.Clone();

                            TipNamestajaWindow tipNamestajWindow = new TipNamestajaWindow(kopija, TipNamestajaWindow.Operacija.IZMENA);
                            tipNamestajWindow.ShowDialog();
                        }
                    }
                    break;
                    

                case Parametar.Akcija:
                    Akcija izabranaAkcija = viewa.CurrentItem as Akcija;  // uzima trenutni selektovan red u dataGridu
                    if (izabranaAkcija != null)
                    {
                        Akcija kopija = (Akcija)izabranaAkcija.Clone();

                        AkcijaWindow akcijaWindow = new AkcijaWindow(kopija, AkcijaWindow.Operacija.IZMENA);
                        akcijaWindow.ShowDialog();
                    }
                    break;
                    

                case Parametar.DodatnaUsluga:
                    DodatnaUsluga izabranaDodatnaUsluga = viewd.CurrentItem as DodatnaUsluga;  // uzima trenutni selektovan red u dataGridu
                    if (izabranaDodatnaUsluga != null)
                    {
                        DodatnaUsluga kopija = (DodatnaUsluga)izabranaDodatnaUsluga.Clone();

                        DodatnaUslugaWindow dodatnaUslugaWindow = new DodatnaUslugaWindow(kopija, DodatnaUslugaWindow.Operacija.IZMENA);
                        dodatnaUslugaWindow.ShowDialog();
                    }
                    break;
                    
                case Parametar.Korisnik:
                    Korisnik izabraniKorisnik = viewk.CurrentItem as Korisnik;  // uzima trenutni selektovan red u dataGridu
                    if (izabraniKorisnik != null)
                    {
                        Korisnik kopija = (Korisnik)izabraniKorisnik.Clone();

                        KorisnikWindow korisnikWindow = new KorisnikWindow(kopija, KorisnikWindow.Operacija.IZMENA);
                        korisnikWindow.ShowDialog();
                    }
                    break;

                case Parametar.Salon:
                    Salon izabraniSalon = (Salon)views.CurrentItem; // uzima trenutni selektovan red u dataGridu
                    if (izabraniSalon != null)
                    {
                        Salon kopija = (Salon)izabraniSalon.Clone();

                        SalonWindow salonWindow = new SalonWindow(kopija, SalonWindow.Operacija.IZMENA);
                        salonWindow.ShowDialog();
                    }
                    break;

                    case Parametar.Prodaja:
                         ProdajaNamestaja izabranaProdaja = viewp.CurrentItem as ProdajaNamestaja;  // uzima trenutni selektovan red u dataGridu
                         if (izabranaProdaja != null)
                         {
                             ProdajaNamestaja kopija = (ProdajaNamestaja)izabranaProdaja.Clone();

                             ProdajaWindow prodajaWindow = new ProdajaWindow(kopija, ProdajaWindow.Operacija.IZMENA);
                             prodajaWindow.ShowDialog();

                             viewp.Refresh();
                         }
                         break;
            }
        }
        #endregion

        #region Dodaj
        private void Dodaj_click(object sender, RoutedEventArgs e)
        {

            switch (parametar)
            {
                case Parametar.Namestaj:
                    var noviNamestaj = new Namestaj();
                    var namestajWindow = new NamestajWindow(noviNamestaj ,NamestajWindow.Operacija.DODAVANJE);
                    namestajWindow.ShowDialog();
                    
                    break;

                case Parametar.TipNamestaja:
                    var noviTipNamestaja = new TipNamestaja();
                    var tipNamestajaWindow = new TipNamestajaWindow(noviTipNamestaja , TipNamestajaWindow.Operacija.DODAVANJE);
                    tipNamestajaWindow.ShowDialog();
                    viewn.Refresh();
                    break;


                case Parametar.Akcija:
                    var novaAkcija = new Akcija();
                    var akcijaWindow = new AkcijaWindow(novaAkcija , AkcijaWindow.Operacija.DODAVANJE);
                    akcijaWindow.ShowDialog();
                    
                    break;


                case Parametar.DodatnaUsluga:
                    var novaDodatnaUsluga = new DodatnaUsluga();
                    var dodatnaUslugaWindow = new DodatnaUslugaWindow(novaDodatnaUsluga , DodatnaUslugaWindow.Operacija.DODAVANJE);
                    dodatnaUslugaWindow.ShowDialog();
                    
                    break;


                case Parametar.Korisnik:
                    var noviKorisnik = new Korisnik();
                    var korisnikWindow = new KorisnikWindow(noviKorisnik , KorisnikWindow.Operacija.DODAVANJE);
                    korisnikWindow.ShowDialog();
                    
                    break;

                case Parametar.Salon:
                    var noviSalon = new Salon();
                    var salonWindow = new SalonWindow(noviSalon , SalonWindow.Operacija.DODAVANJE);
                    salonWindow.ShowDialog();
                    
                    break;

                case Parametar.Prodaja:
                    var novaProdaja = new ProdajaNamestaja();
                    var prodajaWindow = new ProdajaWindow(novaProdaja , ProdajaWindow.Operacija.DODAVANJE);
                    prodajaWindow.ShowDialog();

                    break;
            }
        }
        #endregion

        #region Obrisi
        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            switch (parametar)

            {
                case Parametar.Namestaj:
                    var izabraniNamestaj = (Namestaj)viewn.CurrentItem;
                    var listaNamestaja = Projekat.Instance.Namestaj;
                    if(MessageBox.Show("Da li ste sigurni da zelite da obrisete namestaj", "Obrisi namestaj", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var namestaj in listaNamestaja)
                        {
                            if(izabraniNamestaj.Id == namestaj.Id)
                            {
                                namestaj.Obrisan = true;
                                break;
                            }
                        }
                        Projekat.Instance.Namestaj = listaNamestaja;
                        GenericSerializer.Serializer("namestaj.xml", listaNamestaja);
                    }
                    viewn.Refresh();
                    break;

                case Parametar.TipNamestaja:
                    var izabraniTipNamestaja = viewt.CurrentItem as TipNamestaja;
                    var listaTipNamestaja = Projekat.Instance.TipNamestaja;
                    var listaNamestajaa = Projekat.Instance.Namestaj;
                    if (izabraniTipNamestaja.Id == 0)
                    {
                        MessageBox.Show("Ne mozete obrisati ovaj tip namestaja", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Da li ste sigurni da zelite da obrisete tip namestaja", "Obrisi tip namestaja", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            foreach (var tipNamestaja in listaTipNamestaja)
                            {
                                if(izabraniTipNamestaja.Id == tipNamestaja.Id)
                                {
                                    tipNamestaja.Obrisan = true;
                                    viewt.Refresh();
                                    break;
                                    }
                            }
                            foreach(var namestaj in listaNamestajaa)
                                {
                                    if(namestaj.TipNamestajaId == izabraniTipNamestaja.Id)
                                    {
                                        namestaj.TipNamestajaId = 0;
                                    }
                                }
                            Projekat.Instance.Namestaj = listaNamestajaa;
                            GenericSerializer.Serializer("namestaj.xml", listaNamestajaa);
                            Projekat.Instance.TipNamestaja = listaTipNamestaja;
                            GenericSerializer.Serializer("tipNamestaja.xml", listaTipNamestaja);
                        }
                    }
                    break;


                case Parametar.Akcija:
                    var izabranaAkcija = (Akcija) viewa.CurrentItem;
                    var listaAkcija = Projekat.Instance.Akcija;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete akciju", "Obrisi akciju", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var akcija in listaAkcija)
                        {
                            if(izabranaAkcija.Id == akcija.Id)
                            {
                                akcija.Obrisan = true;
                                break;
                            }
                        }
                        Projekat.Instance.Akcija = listaAkcija;
                        GenericSerializer.Serializer("akcija.xml", listaAkcija);
                    }
                    viewa.Refresh();
                    break;


                case Parametar.DodatnaUsluga:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)viewd.CurrentItem;
                    var listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete dodatnu uslug", "Obrisi dodatnu uslug", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var dodatnaUsluga in listaDodatnihUsluga)
                        {
                            if (izabranaDodatnaUsluga.Id == dodatnaUsluga.Id)
                            {
                                dodatnaUsluga.Obrisan = true;
                                break;
                            }
                        }

                        Projekat.Instance.DodatnaUsluga = listaDodatnihUsluga;
                        GenericSerializer.Serializer("didatnaUsluga.xml", listaDodatnihUsluga);
                    }
                    viewd.Refresh();
                    break;


                case Parametar.Korisnik:
                    var izabraniKorisnik = (Korisnik)viewk.CurrentItem;
                    var listaKorisnika = Projekat.Instance.Korisnik;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete korisnika", "Obrisi korisnika", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var korisnik in listaKorisnika)
                        {
                            if(izabraniKorisnik.Id == korisnik.Id)
                            {
                                korisnik.Obrisan = true;
                                break;
                            }
                        }
                        Projekat.Instance.Korisnik = listaKorisnika;
                        GenericSerializer.Serializer("korisnici.xml", listaKorisnika);
                    }
                    viewk.Refresh();
                    break;
                    
                case Parametar.Salon:
                    var izabraniSalon = (Salon)views.CurrentItem;
                    var listaSalona = Projekat.Instance.Salon;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete salon", "Obrisi salon", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach(var salon in listaSalona)
                        {
                            if(izabraniSalon.Id == salon.Id)
                            {
                                salon.Obrisan = true;
                                break;
                            }
                        }
                    Projekat.Instance.Salon = listaSalona;
                    GenericSerializer.Serializer("salon.xml", listaSalona);
                    }
                    views.Refresh();
                    break;

                case Parametar.Prodaja:
                    var izabranaProdaja = (ProdajaNamestaja)viewp.CurrentItem;
                    var listaProdaja = Projekat.Instance.ProdajaNamestaja;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete prodaju", "Obrisi prodaju", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var prodaja in listaProdaja)
                        {
                            if (izabranaProdaja.Id == prodaja.Id)
                            {
                                prodaja.Obrisan = true;
                                break;
                            }
                        }
                        Projekat.Instance.ProdajaNamestaja = listaProdaja;
                        GenericSerializer.Serializer("prodaja.xml", listaProdaja);
                    }
                    viewp.Refresh();
                    break;
            }
        }
        #endregion

        #region Pretrazi
        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Sort

        private void cbSortiraj_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (parametar)
            {
                case Parametar.Namestaj:
                    var pripremiNamestaj = Projekat.Instance.Namestaj;
                    List<Namestaj> sortiranaListaNamestaj = new List<Namestaj>();

                    if (cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.Id).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.Naziv).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 2)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.Sifra).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 3)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.JedinicnaCena).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 4)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.KolicinaUMagacinu).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 5)
                    {
                        sortiranaListaNamestaj = pripremiNamestaj.OrderBy(o => o.TipNamestaja.Naziv).ToList();
                    }

                    viewn = CollectionViewSource.GetDefaultView(sortiranaListaNamestaj);
                    viewn.Filter = NamestajFilter;
                    dataGridNamestaj.ItemsSource = viewn;
                    break;

                case Parametar.TipNamestaja:
                    var pripremiTipNamestaja = Projekat.Instance.TipNamestaja;
                    List<TipNamestaja> sortiranaListaTipNamestaja = new List<TipNamestaja>();

                    if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaTipNamestaja = pripremiTipNamestaja.OrderBy(o => o.Naziv).ToList();
                    }
                    else if(cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaTipNamestaja = pripremiTipNamestaja.OrderBy(o => o.Id).ToList();
                    }
                    viewt = CollectionViewSource.GetDefaultView(sortiranaListaTipNamestaja);
                    viewt.Filter = TipNamestajaFilter;
                    dataGridTipNamestaja.ItemsSource = viewt;
                    break;

                case Parametar.Akcija:
                    var pripremiAkciju = Projekat.Instance.Akcija;
                    List<Akcija> sortiranaListaAkcija = new List<Akcija>();

                    if (cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaAkcija = pripremiAkciju.OrderBy(o => o.Id).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaAkcija = pripremiAkciju.OrderBy(o => o.DatumPocetka).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 2)
                    {
                        sortiranaListaAkcija = pripremiAkciju.OrderBy(o => o.DatumZavrsetka).ToList();
                    }
                   /* else if (cbSortiraj.SelectedIndex == 3)
                    {
                        sortiranaListaAkcija = pripremiAkciju.OrderBy(o => o.NamestajNaPopustu.Naziv).ToList();
                    }*/
                    else if (cbSortiraj.SelectedIndex == 4)
                    {
                        sortiranaListaAkcija = pripremiAkciju.OrderBy(o => o.Popust).ToList();
                    }

                    viewa = CollectionViewSource.GetDefaultView(sortiranaListaAkcija);
                    viewa.Filter = AkcijaFilter;
                    dataGridAkcija.ItemsSource = viewa;
                    break;

                case Parametar.DodatnaUsluga:
                    var pripremiDodatnuUslugu = Projekat.Instance.DodatnaUsluga;
                    List<DodatnaUsluga> sortiranaListaDodatnaUsluga = new List<DodatnaUsluga>();
                    if (cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaDodatnaUsluga = pripremiDodatnuUslugu.OrderBy(o => o.Id).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaDodatnaUsluga = pripremiDodatnuUslugu.OrderBy(o => o.Naziv).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 2)
                    {
                        sortiranaListaDodatnaUsluga = pripremiDodatnuUslugu.OrderBy(o => o.Cena).ToList();
                    }

                    viewd = CollectionViewSource.GetDefaultView(sortiranaListaDodatnaUsluga);
                    viewd.Filter = DodatnaUslugaFilter;
                    dataGridDodatnaUsluga.ItemsSource = viewd;
                    break;

                case Parametar.Korisnik:
                    var pripremiKorisnika = Projekat.Instance.Korisnik;
                    List<Korisnik> sortiranaListaKorisnik = new List<Korisnik>();

                    if (cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.Id).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.Ime).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 2)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.Prezime).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 3)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.KorisnickoIme).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 4)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.Lozinka).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 5)
                    {
                        sortiranaListaKorisnik = pripremiKorisnika.OrderBy(o => o.TipKorisnika).ToList();
                    }

                    viewk = CollectionViewSource.GetDefaultView(sortiranaListaKorisnik);
                    viewk.Filter = KorisnikFilter;
                    dataGridKorisnik.ItemsSource = viewk;
                    break;

                case Parametar.Salon:
                    var pripremiSalon = Projekat.Instance.Salon;
                    List<Salon> sortiranaListaSalon = new List<Salon>();
                    if (cbSortiraj.SelectedIndex == 0)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.Id).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 1)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.Naziv).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 2)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.Adresa).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 3)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.Telefon).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 4)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.Email).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 5)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.AdresaInternetSajta).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 6)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.PIB).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 7)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.MaticniBroj).ToList();
                    }
                    else if (cbSortiraj.SelectedIndex == 8)
                    {
                        sortiranaListaSalon = pripremiSalon.OrderBy(o => o.BrojZiroRacuna).ToList();
                    }

                    views = CollectionViewSource.GetDefaultView(sortiranaListaSalon);
                    views.Filter = SalonFilter;
                    dataGridSalon.ItemsSource = views;
                    break;

                case Parametar.Prodaja:
                    break;
            }
        }
        #endregion


        #region Dg row nummber
        void dataGridNamestaj_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        void dataGridTipNamestaja_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        void dataGridAkcija_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        void dataGridDodatnaUsluga_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        void dataGridKorisnik_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        void dataGridProdaja_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        #endregion

        #region Nazad click
        private void Nazad_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(korisnik);
            mainWindow.Show();

            this.Close();
        }
        #endregion

        #region Logout
        private void Logout_click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
#endregion
    }
}
