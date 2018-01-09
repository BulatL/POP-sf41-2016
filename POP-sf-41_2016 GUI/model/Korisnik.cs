using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Enums
    {
        public enum TipKorisnika
        {
            Administrator,
            Prodavac
        }

        public static TipKorisnika GetValue(String k)
        {
            if (TipKorisnika.Administrator.ToString().Equals(k))
                return TipKorisnika.Administrator;
            else
                return TipKorisnika.Prodavac;
        }
    }

    public class Korisnik : INotifyPropertyChanged, ICloneable
    {
        
        private int id;
        private String ime;
        private String prezime;
        private String korisnickoIme;
        private String lozinka;
        Enums.TipKorisnika tipKorisnika;
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


        public Enums.TipKorisnika TipKorisnika
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
            Korisnik kopija = new Korisnik();
            kopija.Id = id;
            kopija.Ime = ime;
            kopija.Prezime = prezime;
            kopija.KorisnickoIme = korisnickoIme;
            kopija.Lozinka = lozinka;
            kopija.TipKorisnika = tipKorisnika;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        public static ObservableCollection<Korisnik> Update(Korisnik primljeniKorisnik)
        {
            var lista = Projekat.Instance.Korisnici;
            foreach (var item in lista)
            {
                if (item.Id == primljeniKorisnik.Id)
                {
                    item.Ime = primljeniKorisnik.Ime;
                    item.KorisnickoIme = primljeniKorisnik.KorisnickoIme;
                    item.Lozinka = primljeniKorisnik.Lozinka;
                    item.Prezime = primljeniKorisnik.Prezime;
                    item.TipKorisnika = primljeniKorisnik.TipKorisnika;
                    break;
                }
            }
            return lista;
        }

        public static Korisnik NadjiKorisnika(int prosledjenId)
        {
            var korisnik = new Korisnik();
            foreach (var tip in Projekat.Instance.Korisnici)
            {
                if (tip.Id == prosledjenId)
                {
                    korisnik = tip;
                }
            }
            return korisnik;
        }
    }
}
