using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class DodatnaUsluga : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private String naziv;
        private double cena;
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


        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
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
            return new DodatnaUsluga()
            {
                Id = id,
                Naziv = naziv,
                Cena = cena,
                Obrisan = obrisan
            };
        }

        public static DodatnaUsluga NadjiDodatnuUslugu(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.DodatnaUsluga)
            {
                if (tip.Id == idProsledjen)
                {
                    return tip;
                }

            }
            return null;
        }
    }
}
