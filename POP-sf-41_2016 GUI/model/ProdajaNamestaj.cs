using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_sf_41_2016_GUI.model
{
    public class ProdajaNamestaj : INotifyPropertyChanged
    {
        private int id;
        private int namestajId;
        private int kolicina;
        private double ukupnaCena;
        private Namestaj namestaj;
        private bool obrisan;
        private int prodajaId;

        public int ProdajaId
        {
            get { return prodajaId; }
            set
            {
                prodajaId = value;
                OnPropertyChanged("ProdajaId");
            }
        }

        public string Naziv
        {
            get
            {
                return Namestaj.Naziv;
            }
        }


        public bool Obrisan
        {
            get { return obrisan; }
            set
            { obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }



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
                OnPropertyChanged("Namestaji");
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

        public static ObservableCollection<ProdajaNamestaj> NadjiStavkuProdaje(ObservableCollection<int?> listaStavkiProsledjeno)
        {
            var listaStavki = new ObservableCollection<ProdajaNamestaj>();
            foreach (var tip in Projekat.Instance.ProdajaNamestaj)
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
