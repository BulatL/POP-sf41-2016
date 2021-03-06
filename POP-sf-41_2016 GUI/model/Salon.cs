﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Salon()
        {
            Email = "@gmail.com";
            AdresaInternetSajta = "https://www. ";
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
            Salon kopija = new Salon();
            kopija.Id = id;
            kopija.Naziv = naziv;
            kopija.Adresa = adresa;
            kopija.Telefon = telefon;
            kopija.AdresaInternetSajta = adresaInternetSajta;
            kopija.BrojZiroRacuna = brojZiroRacuna;
            kopija.Email = email;
            kopija.PIB = pib;
            kopija.MaticniBroj = maticniBroj;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        /* static ObservableCollection<Salon> Update(Salon primljeniSalon)
        {
            var lista = Projekat.Instance.Salon;
            foreach (var item in lista)
            {
                if (item.Id == primljeniSalon.Id)
                {
                    item.Naziv = primljeniSalon.Naziv;
                    item.Adresa = primljeniSalon.Adresa;
                    item.AdresaInternetSajta = primljeniSalon.AdresaInternetSajta;
                    item.BrojZiroRacuna = primljeniSalon.BrojZiroRacuna;
                    item.Email = primljeniSalon.Email;
                    item.MaticniBroj = primljeniSalon.MaticniBroj;
                    item.PIB = primljeniSalon.PIB;
                    break;
                }
            }
            return lista;
        }*/

        public override string ToString()
        {
            return ($"Naziv: {Naziv}"+"\n"+ $", Adresa: {Adresa}" + "\n" + $", Sajt: {AdresaInternetSajta}" + "\n" + 
                $", Email: {Email}" + "\n" + $", Broj ziro racuna: {BrojZiroRacuna}" + "\n" +
                $", Maticni broj: {MaticniBroj}" + "\n" + $", PIB: {PIB} ");
        }
    }
}
