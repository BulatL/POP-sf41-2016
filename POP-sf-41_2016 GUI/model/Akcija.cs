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
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
    
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavrsetka;
        private double popust;
        private bool obrisan;
        private ObservableCollection<int?> listaNamestajaNaPopustuId;
        private ObservableCollection<Namestaj> listaNamestajaNaPopustu;

        public Akcija()
        {
            DatumPocetka = DateTime.Now;
            DatumZavrsetka = DateTime.Now;
        }

        public ObservableCollection<int?> ListaNamestajaNaPopustuId
        {
            get { return listaNamestajaNaPopustuId; }
            set
            {
                listaNamestajaNaPopustuId = value;
                OnPropertyChanged("ListaNamestajaNaPopustuId");
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

        public double Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }


        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set
            {
                datumZavrsetka = value;
                OnPropertyChanged("DatumZavrsetka");
            }
        }


        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                datumPocetka = value;
                OnPropertyChanged("DatumPocetka");
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

        [XmlIgnore]
        public ObservableCollection<Namestaj> ListaNamestajaNaPopustu
        {
            get
            {
                if(listaNamestajaNaPopustu == null)
                {
                    listaNamestajaNaPopustu = Namestaj.NadjiListuNamestaja(listaNamestajaNaPopustuId);
                }

                return listaNamestajaNaPopustu;
            }
            set
            {
                listaNamestajaNaPopustu = value;

                var lista = new ObservableCollection<int?>();
                if (listaNamestajaNaPopustu != null)
                {
                    foreach (var item in listaNamestajaNaPopustu)
                    {
                        lista.Add(item.Id);
                    }
                    listaNamestajaNaPopustuId = lista;
                    OnPropertyChanged("ListaNamestajaNaPopustu");
                }
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
            Akcija kopija = new Akcija();
            kopija.Id = id;
            kopija.DatumPocetka = datumPocetka;
            kopija.DatumZavrsetka = datumZavrsetka;
            kopija.Popust = popust;
            kopija.Obrisan = obrisan;
            kopija.ListaNamestajaNaPopustu = listaNamestajaNaPopustu;
            kopija.ListaNamestajaNaPopustuId = listaNamestajaNaPopustuId;

            return kopija;
        }
    }
}