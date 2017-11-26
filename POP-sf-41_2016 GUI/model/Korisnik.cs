using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }

    public class Korisnik : INotifyPropertyChanged, ICloneable
    {
        
        private int id;
        private String ime;
        private String prezime;
        private String korisnickoIme;
        private String lozinka;
        private TipKorisnika tipKorisnika;
        private bool obrisan;

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        public String Lozinka
        {
            get { return lozinka; }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }


        public String KorisnickoIme
        {
            get { return korisnickoIme; }
            set
            {
                korisnickoIme = value;
                OnPropertyChanged("KorisnickoIme");
            }
        }


        public String Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }


        public String Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return new Korisnik()
            {
                Id = id,
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka,
                TipKorisnika = tipKorisnika,
                Obrisan = obrisan
            };
        }
    }
}
