using POP_sf_41_2016_GUI.DAO;
using POP_sf_41_2016_GUI.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        public ObservableCollection<Namestaj> Namestaji { get; set; }

        public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }

        public ObservableCollection<Akcija> Akcije { get; set; }

        public ObservableCollection<NaAkciji> NaAkciji { get; set; }

        public ObservableCollection<DodatnaUsluga> DodatneUsluge { get; set; }

        public ObservableCollection<Korisnik> Korisnici { get; set; }

        public ObservableCollection<Salon>  Salon { get; set; }

        public ObservableCollection<Prodaja> Prodaja { get; set; }

        public ObservableCollection<ProdajaNamestaj> ProdajaNamestaj { get; set; }

        public ObservableCollection<ProdajaDodatnaUsluga> ProdajaDodatneUsluge { get; set; }

        private Projekat()
        {
            Namestaji = new ObservableCollection<Namestaj>();
            TipoviNamestaja = new ObservableCollection<TipNamestaja>();
            Akcije = new ObservableCollection<Akcija>();
            NaAkciji = new ObservableCollection<NaAkciji>();
            DodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            Korisnici = new ObservableCollection<Korisnik>();
            Salon = new ObservableCollection<Salon>();
            Prodaja = new ObservableCollection<Prodaja>();
            ProdajaNamestaj = new ObservableCollection<ProdajaNamestaj>();
            ProdajaDodatneUsluge = new ObservableCollection<ProdajaDodatnaUsluga>();
        }

        public void LoadTestData()
        {
            SalonDAO.Load();
            TipNamestajaDAO.Load();
            AkcijaDAO.Load();
            NaAkcijiDAO.Load();
            DodatnaUslugaDAO.Load();
            KorisnikDAO.Load();
            NamestajDAO.Load();
            ProdajaDAO.Load();
            ProdajaDodatnaUslugaDAO.Load();
            ProdajaNamestajDAO.Load();
        }
    }
}
