using System;
using System.Collections.Generic;
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
        private int? namestajNaPopustuId;
        private Namestaj namestajNaPopustu;
        private bool obrisan;

        public Akcija()
        {
            DatumPocetka = DateTime.Now;
            DatumZavrsetka = DateTime.Now;
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


        public int? NamestajNaPopustuId
        {
            get { return namestajNaPopustuId; }
            set
            {
                namestajNaPopustuId = value;
                OnPropertyChanged("NamestajNaPopustuId");
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
        public Namestaj NamestajNaPopustu
        {
            get
            {
                if (namestajNaPopustu == null)
                {
                    namestajNaPopustu = Namestaj.NadjiNamestaj(namestajNaPopustuId);
                }
                return namestajNaPopustu;
            }
            set
            {
                namestajNaPopustu = value;
                NamestajNaPopustuId = namestajNaPopustu.Id;
                OnPropertyChanged("NamestajNaPopustu");
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

            return kopija;
        }
    }
}