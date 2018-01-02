using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_sf41_2016.model
{
    public class Namestaj : INotifyPropertyChanged, ICloneable
    {

        private int id;
        private String naziv;
        private String sifra;
        private double jedinicnaCena;
        private int kolicinaUMagacinu;
        private int? tipNamestajaId;
        private bool obrisan;
        private TipNamestaja tipNamestaja;

       /* public Namestaj()
        {
            naziv = "";
            sifra = "";
            jedinicnaCena = 0.0;
            kolicinaUMagacinu = 0;
            tipNamestajaId = 0;
            tipNamestaja = new TipNamestaja();
        }*/

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public String Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }

        public double JedinicnaCena
        {
            get { return jedinicnaCena; }
            set
            {
                jedinicnaCena = value;
                OnPropertyChanged("JedinicnaCena");
            }
        }

        public int KolicinaUMagacinu
        {
            get { return kolicinaUMagacinu; }
            set
            {
                kolicinaUMagacinu = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }

        public int? TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                tipNamestajaId = value;
                OnPropertyChanged("TipNamestajaId");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }

        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            {
                if (tipNamestaja == null)
                {
                    tipNamestaja = TipNamestaja.NadjiTipNamestaj(TipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return ($"id:{Id}, naziv: {Naziv}, sifra: {Sifra}, cena: {JedinicnaCena}, kolicina: {KolicinaUMagacinu} , tip namestaja: {TipNamestaja.NadjiTipNamestaj(TipNamestajaId)} ");
        }

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Namestaj NadjiNamestaj(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.Namestaji)
            {
                if (tip.Id == idProsledjen)
                {
                    return tip;
                }

            }
            return null;
        }

        public static ObservableCollection<Namestaj> NadjiListuNamestaja(ObservableCollection<int?> listaNamestajaProsledjeno)
        {
            var listaNamestaja = new ObservableCollection<Namestaj>();
            foreach (var n in Projekat.Instance.Namestaji)
            {
                if (listaNamestajaProsledjeno != null)
                {
                    foreach (var namestaj in listaNamestajaProsledjeno)
                    {
                        if (n.Id == namestaj)
                        {
                            listaNamestaja.Add(n);

                        }
                    }
                }
            }
            return listaNamestaja;
        }

        public static void Update(Namestaj primljenNamestaj)
        {
            var lista = Projekat.Instance.Namestaji;
            foreach (var item in lista)
            {
                if(item.Id == primljenNamestaj.Id)
                {
                    item.Naziv = primljenNamestaj.Naziv;
                    item.JedinicnaCena = primljenNamestaj.JedinicnaCena;
                    item.KolicinaUMagacinu = primljenNamestaj.KolicinaUMagacinu;
                    item.Sifra = primljenNamestaj.Sifra;
                    item.TipNamestaja = primljenNamestaj.TipNamestaja;
                    item.TipNamestajaId = primljenNamestaj.TipNamestajaId;
                    break;
                }
            }
        }

        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = id;
            kopija.Naziv = naziv;
            kopija.JedinicnaCena = jedinicnaCena;
            kopija.TipNamestaja = tipNamestaja;
            kopija.TipNamestajaId = tipNamestajaId;
            kopija.Obrisan = obrisan;
            kopija.KolicinaUMagacinu = kolicinaUMagacinu;
            kopija.Sifra = sifra;

            return kopija;
        }
    }
}
