using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_sf_41_2016_GUI.model
{
    public class StavkaProdaje : INotifyPropertyChanged
    {
        private int id;
        private int namestajId;
        private int kolicina;
        private double ukupnaCena;
        private Namestaj namestaj;

 
        public int Id
        {
            get { return id; }
            set
            { id = value;
                OnPropertyChanged("Id");
            }
        }

        public int NamestajId
        {
            get { return namestajId; }
            set
            { namestajId = value;
                OnPropertyChanged("NamestajId");
            }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set
            { kolicina = value;
                OnPropertyChanged("Kolicina");
            }
        }

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set
            { ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
            }
        }

        [XmlIgnore]
        public Namestaj Namestaj
        {
            get
            {
                if (namestaj == null)
                {
                    namestaj = Namestaj.NadjiNamestaj(NamestajId);
                }
                return namestaj;
            }
            set
            {
                namestaj = value;
                NamestajId = namestaj.Id;
                OnPropertyChanged("Namestaj");
            }
        }

        public override string ToString()
        {
            return ($"id:{Id}, namestaj: {Namestaj}, kolicina: {Kolicina}, cena: {UkupnaCena}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static List<StavkaProdaje> NadjiStavkuProdaje(List<int?> listaStavkiProsledjeno)
        {
            var listaStavki = new List<StavkaProdaje>();
            foreach (var tip in Projekat.Instance.StavkeProdaje)
            {
                if(listaStavkiProsledjeno != null)
                {
                    foreach (var s in listaStavkiProsledjeno)
                    {
                        if (tip.Id == s)
                        {
                            listaStavki.Add(tip);

                        }
                    }
                }               
            }
            return listaStavki; 
        }
    }
}
