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
    public class NaAkciji : INotifyPropertyChanged, ICloneable
    {

        private int namestajId;
        private Namestaj namestaj;
        private bool obrisan;
        private int id;
        private int akcijaId;
        private int popust;


        public NaAkciji()
        {
            namestaj = null;
        }
        public int Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }


        public int AkcijaId
        {
            get { return akcijaId; }
            set
            {
                akcijaId = value;
                OnPropertyChanged("AkcijaId");
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


        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
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
                OnPropertyChanged("Namestaji");
            }
        }


        public int NamestajId
        {
            get { return namestajId; }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
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
            NaAkciji kopija = new NaAkciji();
            kopija.Namestaj = namestaj;
            kopija.NamestajId = namestajId;
            kopija.AkcijaId = akcijaId;
            kopija.Popust = popust;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        public static ObservableCollection<NaAkciji> NadjiNaAkciji(ObservableCollection<int?> listaNaAkcijiProsledjeno)
        {
            var listaNaAkciji = new ObservableCollection<NaAkciji>();
            foreach (var tip in Projekat.Instance.NaAkciji)
            {
                if (listaNaAkcijiProsledjeno != null)
                {
                    foreach (var na in listaNaAkcijiProsledjeno)
                    {
                        if (tip.Id == na)
                        {
                            listaNaAkciji.Add(tip);
                        }
                    }
                }
            }
            return listaNaAkciji;
        }
    }
}
