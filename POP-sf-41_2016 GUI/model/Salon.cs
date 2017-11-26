using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Salon : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private String naziv;
        private String adresa;
        private String telefon;
        private String email;
        private String adresaInternetSajta;
        private int pib;
        private int maticniBroj;
        private int brojZiroRacuna;
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


        public int BrojZiroRacuna
        {
            get { return brojZiroRacuna; }
            set
            {
                brojZiroRacuna = value;
                OnPropertyChanged("BrojZiroRacuna");
            }
        }


        public int MaticniBroj
        {
            get { return maticniBroj; }
            set
            {
                maticniBroj = value;
                OnPropertyChanged("MaticniBroj");
            }
        }


        public int PIB
        {
            get { return pib; }
            set
            {
                pib = value;
                OnPropertyChanged("PIB");
            }
        }


        public String AdresaInternetSajta
        {
            get { return adresaInternetSajta; }
            set
            {
                adresaInternetSajta = value;
                OnPropertyChanged("AdresaInternetSajta");
            }
        
        }


        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        public String Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                OnPropertyChanged("Telefon");
            }
        }


        public String Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }


        public String Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
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
            return new Salon()
            {
                Id = id,
                Naziv = naziv,
                Telefon = telefon,
                Email = email,
                AdresaInternetSajta = adresaInternetSajta,
                Adresa = adresa,
                BrojZiroRacuna = brojZiroRacuna,
                PIB = pib, 
                MaticniBroj = maticniBroj,
                Obrisan = obrisan
            };
        }
    }
}
