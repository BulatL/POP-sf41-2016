using POP_sf_41_2016_GUI.DAO;
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

            dataGridNamestaj.Visibility = Visibility.Collapsed;
            dataGridTipNamestaja.Visibility = Visibility.Collapsed;
            dataGridAkcija.Visibility = Visibility.Collapsed;
            dataGridDodatnaUsluga.Visibility = Visibility.Collapsed;
            dataGridKorisnik.Visibility = Visibility.Collapsed;
            dataGridSalon.Visibility = Visibility.Collapsed;
            dataGridProdaja.Visibility = Visibility.Collapsed;
            dpPretrazi.Visibility = Visibility.Collapsed;


            this.korisnik = korisnik;
            this.parametar = parametar;

            if (korisnik.TipKorisnika == Enums.TipKorisnika.Prodavac)
            {
                btnIzmeni.Visibility = Visibility.Collapsed;
                Obrisibtn.Visibility = Visibility.Collapsed;
            }

            PopuniDataGrid(parametar);
        }

        #region PopuniDataGrid
        public void PopuniDataGrid(Parametar parametar)
        {
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
                    viewn = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaji);
                    viewn.Filter = NamestajFilter;
                    dataGridNamestaj.ItemsSource = viewn;
                    dataGridNamestaj.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazi
                    if (cbPretrazi.Items.Count < 1)
                    {
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
                    }
                    break;
                #endregion

                #region Tip namestaja punjenje dg
                case Parametar.TipNamestaja:
                    Uri tipNamestaja = new Uri(System.IO.Path.GetFullPath($"../../image/namestaj.png"));
                    Icon = BitmapFrame.Create(tipNamestaja);
                    //Punjenje dataGrida
                    dataGridTipNamestaja.AutoGenerateColumns = false;
                    dataGridTipNamestaja.DataContext = this;
                    viewt = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
                    viewt.Filter = TipNamestajaFilter;
                    dataGridTipNamestaja.ItemsSource = viewt;
                    dataGridTipNamestaja.IsSynchronizedWithCurrentItem = true;
                    dataGridTipNamestaja.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazi
                    if (cbPretrazi.Items.Count < 1)
                    {
                        cbPretrazi.Items.Add("Nazivu");
                        cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                        cbSortiraj.Items.Add("Id-u");
                        cbSortiraj.Items.Add("Nazivu");
                        cbSortiraj.SelectedIndex = 0;
                    }

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
                    viewa = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcije);
                    viewa.Filter = AkcijaFilter;
                    dataGridAkcija.ItemsSource = viewa;
                    dataGridAkcija.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazivanje
                    if (cbPretrazi.Items.Count < 1)
                    {
                        cbPretrazi.Items.Add("Datum pocetka");
                        cbPretrazi.Items.Add("Datum zavrsetka");
                        cbPretrazi.Items.Add("Naziv akcije");
                        cbPretrazi.Items.Add("Namestaj na popustu");
                        cbPretrazi.SelectedIndex = 0;
                    //Punjenje comboboxa za Sortiranje
                        cbSortiraj.Items.Add("Id-u");
                        cbSortiraj.Items.Add("Datumu pocetka");
                        cbSortiraj.Items.Add("Datumu zavrsetka");
                        cbSortiraj.Items.Add("Nazivu");
                        cbSortiraj.SelectedIndex = 0;
                    }
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
                    viewd = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatneUsluge);
                    viewd.Filter = DodatnaUslugaFilter;
                    dataGridDodatnaUsluga.ItemsSource = viewd;
                    dataGridDodatnaUsluga.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretrazivanje
                    if (cbPretrazi.Items.Count < 1)
                    {
                        cbPretrazi.Items.Add("Nazivu");
                        cbPretrazi.SelectedIndex = 0;
                        //Punjenje comboboxa za Sortiranje
                        cbSortiraj.Items.Add("Id-u");
                        cbSortiraj.Items.Add("Nazivu");
                        cbSortiraj.Items.Add("Ceni");
                        cbSortiraj.SelectedIndex = 0;
                    }
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
                    viewk = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnici);
                    viewk.Filter = KorisnikFilter;
                    dataGridKorisnik.ItemsSource = viewk;
                    dataGridKorisnik.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretragu
                    if (cbPretrazi.Items.Count < 1)
                    {
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
                    }
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
                    cbPretrazi.Visibility = Visibility.Collapsed;
                    cbSortiraj.Visibility = Visibility.Collapsed;
                    lblSortiraj.Visibility = Visibility.Collapsed;
                    lbPretrazi.Visibility = Visibility.Collapsed;
                    tbPretrazi.Visibility = Visibility.Collapsed;
                    Dodajbtn.Visibility = Visibility.Collapsed;
                    Obrisibtn.Visibility = Visibility.Collapsed;
                    lbPretrazi.Visibility = Visibility.Collapsed;
                    tbPretrazi.Visibility = Visibility.Collapsed;
                    dpPretrazi.Visibility = Visibility.Collapsed;
                    lblSortiraj.Visibility = Visibility.Collapsed;
                    cbSortiraj.Visibility = Visibility.Collapsed;
                    btnPretrazi.Visibility = Visibility.Collapsed;
                    if (korisnik.TipKorisnika == Enums.TipKorisnika.Prodavac)
                        btnIzmeni.Visibility = Visibility.Collapsed;
                    break;
                #endregion

                #region Prodaja punjenje dg
                case Parametar.Prodaja:
                    dataGridProdaja.AutoGenerateColumns = false;
                    dataGridProdaja.IsSynchronizedWithCurrentItem = true;
                    dataGridProdaja.DataContext = this;
                    viewp = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaja);
                    viewp.Filter = ProdajaFilter;
                    dataGridProdaja.ItemsSource = viewp;
                    dataGridProdaja.Visibility = Visibility.Visible;
                    //Punjenje comboboxa za Pretragu
                    if (cbPretrazi.Items.Count < 1)
                    {
                        cbPretrazi.Items.Add("Kupcu");
                        cbPretrazi.Items.Add("Prodavcu");
                        cbPretrazi.Items.Add("Broju racuna");
                        cbPretrazi.Items.Add("Prodatom namestaju");
                        cbPretrazi.Items.Add("Datumu prodaje");
                        cbPretrazi.SelectedIndex = 0;
                        //Punjenje comboboxa za Sortiranje
                        cbSortiraj.Items.Add("Id-u");
                        cbSortiraj.Items.Add("Kupcu");
                        cbSortiraj.Items.Add("Broju racuna");
                        cbSortiraj.Items.Add("Prodatom namestaju");
                        cbSortiraj.Items.Add("Ukupnom iznosu");
                        cbSortiraj.SelectedIndex = 0;
                    }
                    break;
                    #endregion
            }
        }

#endregion

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
            return !((Prodaja)obj).Obrisan;
        }
        #endregion

        #region Izmeni
        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            
            switch (parametar)
            {
                case Parametar.Namestaj:
                    Namestaj izabraniNamestaj = viewn.CurrentItem as Namestaj; // uzima trenutni selektovan red u dataGridu
                    var listaNamestaja = Projekat.Instance.Namestaji;
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
                         Prodaja izabranaProdaja = viewp.CurrentItem as Prodaja;  // uzima trenutni selektovan red u dataGridu
                         if (izabranaProdaja != null)
                         {
                             Prodaja kopija = (Prodaja)izabranaProdaja.Clone();

                             ProdajaWindow prodajaWindow = new ProdajaWindow(korisnik ,kopija, ProdajaWindow.Operacija.IZMENA);
                             prodajaWindow.ShowDialog();
                         }
                         break;
            }
            PopuniDataGrid(parametar);
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
                    var novaProdaja = new Prodaja();
                    var prodajaWindow = new ProdajaWindow(korisnik ,novaProdaja , ProdajaWindow.Operacija.DODAVANJE);
                    prodajaWindow.ShowDialog();
                    break;
            }
            PopuniDataGrid(parametar);
        }
        #endregion

        #region Obrisi
        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            switch (parametar)

            {
                case Parametar.Namestaj:
                    var izabraniNamestaj = (Namestaj)viewn.CurrentItem;
                    var listaNamestaja = Projekat.Instance.Namestaji;
                    if(MessageBox.Show("Da li ste sigurni da zelite da obrisete namestaj: " + izabraniNamestaj.Naziv + " ?", "Obrisi namestaj", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        NamestajDAO.Delete(izabraniNamestaj);
                        NaAkcijiDAO.Delete(null, TipBrisanja.PoNamestajId, 0, izabraniNamestaj.Id);
                    }
                    break;

                case Parametar.TipNamestaja:
                    var izabraniTipNamestaja = viewt.CurrentItem as TipNamestaja;
                    var listaTipNamestaja = Projekat.Instance.TipoviNamestaja;
                    var listaNamestajaa = Projekat.Instance.Namestaji;
                    if (izabraniTipNamestaja.Id == 1)
                    {
                        MessageBox.Show("Ne mozete obrisati: " + izabraniTipNamestaja.Naziv + " ?", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Da li ste sigurni da zelite da obrisete tip namestaja: " + izabraniTipNamestaja.Naziv + " ?", "Obrisi tip namestaja", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            TipNamestajaDAO.Delete(izabraniTipNamestaja);
                            foreach(var namestaj in listaNamestajaa)
                            {
                                if(namestaj.TipNamestajaId == izabraniTipNamestaja.Id)
                                {
                                    namestaj.TipNamestajaId = 1;
                                }
                            }
                        }
                    }
                    break;


                case Parametar.Akcija:
                    var izabranaAkcija = (Akcija) viewa.CurrentItem;
                    var listaAkcija = Projekat.Instance.Akcije;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete akciju: " + izabranaAkcija.Naziv + " ?", "Obrisi akciju", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        AkcijaDAO.Delete(izabranaAkcija);
                    }
                    break;


                case Parametar.DodatnaUsluga:
                    var izabranaDodatnaUsluga = (DodatnaUsluga)viewd.CurrentItem;
                    var listaDodatnihUsluga = Projekat.Instance.DodatneUsluge;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete dodatn uslugu: " + izabranaDodatnaUsluga.Naziv + " ?", "Obrisi dodatnu uslug", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        DodatnaUslugaDAO.Delete(izabranaDodatnaUsluga);
                    }
                    break;


                case Parametar.Korisnik:
                    var izabraniKorisnik = (Korisnik)viewk.CurrentItem;
                    var listaKorisnika = Projekat.Instance.Korisnici;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete korisnika: " + izabraniKorisnik.Ime + " " + izabraniKorisnik.Prezime + " ?", "Obrisi korisnika", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        KorisnikDAO.Delete(izabraniKorisnik);
                    }
                    break;
                    
                case Parametar.Salon:
                    var izabraniSalon = (Salon)views.CurrentItem;
                    var listaSalona = Projekat.Instance.Salon;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete salon: " + izabraniSalon.Naziv + " ?", "Obrisi salon", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        SalonDAO.Delete(izabraniSalon);
                    }
                    break;

                case Parametar.Prodaja:
                    var izabranaProdaja = (Prodaja)viewp.CurrentItem;
                    var listaProdaja = Projekat.Instance.Prodaja;
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete prodaju: " + izabranaProdaja.BrRacuna + " ?", "Obrisi prodaju", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        foreach (var prodaja in listaProdaja)
                        {
                            if (izabranaProdaja.Id == prodaja.Id)
                            {
                                prodaja.Obrisan = true;
                                break;
                            }
                        }
                    }
                    break;
            }

            PopuniDataGrid(parametar);
        }
        #endregion

        #region Pretrazi
        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {
            switch (parametar)  
            {
                case Parametar.Namestaj:
                    if(cbPretrazi.SelectedIndex == 0)
                    {
                        var sort = cbSortiraj.SelectedItem;
                        viewn = CollectionViewSource.GetDefaultView(NamestajDAO.FindSort(tbPretrazi.Text, NamestajDAO.TipPretrage.Naziv, cbSortiraj.SelectedIndex));
                        dataGridNamestaj.ItemsSource = viewn;
                    }else if(cbPretrazi.SelectedIndex == 1)
                    {
                        viewn = CollectionViewSource.GetDefaultView(NamestajDAO.FindSort(tbPretrazi.Text, NamestajDAO.TipPretrage.TipNamestaja, cbSortiraj.SelectedIndex));
                        dataGridNamestaj.ItemsSource = viewn;
                    }else if(cbPretrazi.SelectedIndex == 2)
                    {
                        viewn = CollectionViewSource.GetDefaultView(NamestajDAO.FindSort(tbPretrazi.Text, NamestajDAO.TipPretrage.Sifra, cbSortiraj.SelectedIndex));
                        dataGridNamestaj.ItemsSource = viewn;
                    }
                    break;
                case Parametar.TipNamestaja:
                    viewt = CollectionViewSource.GetDefaultView(TipNamestajaDAO.FindSort(tbPretrazi.Text, cbSortiraj.SelectedIndex));
                    dataGridTipNamestaja.ItemsSource = viewt;
                    break;
                case Parametar.DodatnaUsluga:
                    viewd = CollectionViewSource.GetDefaultView(DodatnaUslugaDAO.FindSort(tbPretrazi.Text, cbSortiraj.SelectedIndex));
                    dataGridDodatnaUsluga.ItemsSource = viewd;
                    break;
                case Parametar.Akcija:
                    if (cbPretrazi.SelectedIndex == 0)
                    {
                        viewa = CollectionViewSource.GetDefaultView(AkcijaDAO.FindSort("", AkcijaDAO.TipPretrage.DatumPocetka, dpPretrazi.SelectedDate, cbSortiraj.SelectedIndex));
                        dataGridAkcija.ItemsSource = viewa;
                    }
                    else if (cbPretrazi.SelectedIndex == 1)
                    {
                        viewa = CollectionViewSource.GetDefaultView(AkcijaDAO.FindSort("", AkcijaDAO.TipPretrage.DatumZavrsetka, dpPretrazi.SelectedDate, cbSortiraj.SelectedIndex));
                        dataGridAkcija.ItemsSource = viewa;
                    }
                    else if (cbPretrazi.SelectedIndex == 2)
                    {
                        viewa = CollectionViewSource.GetDefaultView(AkcijaDAO.FindSort(tbPretrazi.Text, AkcijaDAO.TipPretrage.Naziv, null, cbSortiraj.SelectedIndex));
                        dataGridAkcija.ItemsSource = viewa;
                    }
                    else if (cbPretrazi.SelectedIndex == 3)
                    {
                        viewa = CollectionViewSource.GetDefaultView(AkcijaDAO.FindSort(tbPretrazi.Text, AkcijaDAO.TipPretrage.Namestaji, null, cbSortiraj.SelectedIndex));
                        dataGridAkcija.ItemsSource = viewa;
                    }
                    break;
                case Parametar.Korisnik:
                    if (cbPretrazi.SelectedIndex == 0)
                    {
                        viewk = CollectionViewSource.GetDefaultView(KorisnikDAO.FindSort(tbPretrazi.Text, KorisnikDAO.TipPretrage.Ime, cbSortiraj.SelectedIndex));
                        dataGridKorisnik.ItemsSource = viewk;
                    }
                    else if (cbPretrazi.SelectedIndex == 1)
                    {
                        viewk = CollectionViewSource.GetDefaultView(KorisnikDAO.FindSort(tbPretrazi.Text, KorisnikDAO.TipPretrage.Prezime, cbSortiraj.SelectedIndex));
                        dataGridKorisnik.ItemsSource = viewk;
                    }
                    else if (cbPretrazi.SelectedIndex == 2)
                    {
                        viewk = CollectionViewSource.GetDefaultView(KorisnikDAO.FindSort(tbPretrazi.Text, KorisnikDAO.TipPretrage.KorisnickoIme, cbSortiraj.SelectedIndex));
                        dataGridKorisnik.ItemsSource = viewk;
                    }
                    break;
                case Parametar.Prodaja:
                    if (cbPretrazi.SelectedIndex == 0)
                    {
                        viewp = CollectionViewSource.GetDefaultView(ProdajaDAO.FindSort("", ProdajaDAO.TipPretrage.Kupac, null, cbSortiraj.SelectedIndex));
                        dataGridProdaja.ItemsSource = viewp;
                    }
                    else if (cbPretrazi.SelectedIndex == 1)
                    {
                        viewp = CollectionViewSource.GetDefaultView(ProdajaDAO.FindSort("", ProdajaDAO.TipPretrage.Prodavac, null, cbSortiraj.SelectedIndex));
                        dataGridProdaja.ItemsSource = viewp;
                    }
                    else if (cbPretrazi.SelectedIndex == 2)
                    {
                        viewp = CollectionViewSource.GetDefaultView(ProdajaDAO.FindSort(tbPretrazi.Text, ProdajaDAO.TipPretrage.BrRacuna, null, cbSortiraj.SelectedIndex));
                        dataGridProdaja.ItemsSource = viewp;
                    }
                    else if (cbPretrazi.SelectedIndex == 3)
                    {
                        viewa = CollectionViewSource.GetDefaultView(ProdajaDAO.FindSort(tbPretrazi.Text, ProdajaDAO.TipPretrage.ProdatiNamestaj, null, cbSortiraj.SelectedIndex));
                        dataGridProdaja.ItemsSource = viewp;
                    }
                    else if (cbPretrazi.SelectedIndex == 4)
                    {
                        viewa = CollectionViewSource.GetDefaultView(ProdajaDAO.FindSort(tbPretrazi.Text, ProdajaDAO.TipPretrage.DatumProdaje, dpPretrazi.SelectedDate, cbSortiraj.SelectedIndex));
                        dataGridProdaja.ItemsSource = viewp;
                    }
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

        private void cbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(parametar == Parametar.Akcija)
            {
                if (cbPretrazi.SelectedIndex == 0 || cbPretrazi.SelectedIndex == 1)
                {
                    dpPretrazi.Visibility = Visibility.Visible;
                    tbPretrazi.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbPretrazi.Visibility = Visibility.Visible;
                    dpPretrazi.Visibility = Visibility.Collapsed;
                }
            }
            if (parametar == Parametar.Prodaja)
            {
                if (cbPretrazi.SelectedIndex == 4 )
                {
                    dpPretrazi.Visibility = Visibility.Visible;
                    tbPretrazi.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}