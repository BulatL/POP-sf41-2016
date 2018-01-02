using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            DodatnaUsluga kopija = new DodatnaUsluga();
            kopija.Id = id;
            kopija.Naziv = Naziv;
            kopija.Cena = cena;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        public static DodatnaUsluga NadjiDodatnuUslugu(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.DodatneUsluge)
            {
                if (tip.Id == idProsledjen)
                {
                    return tip;
                }

            }
            return null;
        }

        public static ObservableCollection<DodatnaUsluga> NadjiListuDodatnihUsluga(ObservableCollection<int?> listaDodatnihUslugaProsledjeno)
        {
            var listaDodatnihUsluga = new ObservableCollection<DodatnaUsluga>();
            foreach (var du in Projekat.Instance.DodatneUsluge)
            {
                if (listaDodatnihUslugaProsledjeno != null)
                {
                    foreach (var item in listaDodatnihUslugaProsledjeno)
                    {
                        if (du.Id == item)
                        {
                            listaDodatnihUsluga.Add(du);

                        }
                    }
                }
            }
            return listaDodatnihUsluga;
        }

        public static ObservableCollection<DodatnaUsluga> Update(DodatnaUsluga primljenaDodatnaUsluga)
        {
            var lista = Projekat.Instance.DodatneUsluge;
            foreach (var item in lista)
            {
                if (item.Id == primljenaDodatnaUsluga.Id)
                {
                    item.Naziv = primljenaDodatnaUsluga.Naziv;
                    item.Cena = primljenaDodatnaUsluga.Cena;
                    break;
                }
            }
            return lista;
        }
    }
}
