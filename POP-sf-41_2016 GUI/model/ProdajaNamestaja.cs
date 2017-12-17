using POP_sf_41_2016_GUI.model;
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
    public class ProdajaNamestaja : INotifyPropertyChanged, ICloneable
    {

        //public const double PDV = 0.02;

        
        private bool obrisan;
        private ObservableCollection<int?> listaStavkiProdajeId;
        private int id;
        private DateTime datumProdaje;
        private string brRacuna;
        private string kupac;
        private double ukupanIznos;
        private const double pdv =0.02;
        private ObservableCollection<StavkaProdaje> listaStavkiProdaje;
        private ObservableCollection<int?> listaDodatnihUslugaId;
        private ObservableCollection<DodatnaUsluga> listaDodatnihUsluga;


        public ObservableCollection<int?> ListaDodatnihUslugaId
        {
            get { return listaDodatnihUslugaId; }
            set
            {
                listaDodatnihUslugaId = value;
                OnPropertyChanged("ListaDodatnihUslugaId");
            }
        }

        [XmlIgnore]
        public ObservableCollection<DodatnaUsluga> ListaDodatnihUsluga
        {
            get
            {
                if(listaDodatnihUsluga == null)
                {
                    listaDodatnihUsluga = DodatnaUsluga.NadjiListuDodatnihUsluga(listaDodatnihUslugaId);
                }
                return listaDodatnihUsluga;
            }
            set
            {
                listaDodatnihUsluga = value;

                var lista = new ObservableCollection<int?>();
                if (listaDodatnihUsluga != null)
                {
                    foreach (var item in listaDodatnihUsluga)
                    {
                        lista.Add(item.Id);
                    }
                    ListaDodatnihUslugaId = lista;
                    OnPropertyChanged("ListaDodatnihUsluga");
                }
            }
        }


        [XmlIgnore]
         public ObservableCollection<StavkaProdaje> ListaStavkiProdaje
         {
             get
             {
                 if(listaStavkiProdaje == null)
                 {
                     listaStavkiProdaje = StavkaProdaje.NadjiStavkuProdaje(listaStavkiProdajeId);
                 }
                 return listaStavkiProdaje; }
             set
            {
                listaStavkiProdaje = value;

                var lista = new ObservableCollection<int?>();
                if(listaStavkiProdaje != null)
                {
                    foreach (var item in listaStavkiProdaje)
                    {
                        lista.Add(item.Id);
                    }
                    ListaStavkiProdajeId = lista;
                    OnPropertyChanged("ListaStavkiProdaje");
                }
                
            }
         }

        public double UkupanIznos
        {
            get { return ukupanIznos; }
            set
            {
                ukupanIznos = value;
                OnPropertyChanged("UkupanIznos");
            }
        }

        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }


        public string BrRacuna
        {
            get { return brRacuna; }
            set
            {
                brRacuna = value;
                OnPropertyChanged("BrRacuna");
            }
        }


        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
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


        public ObservableCollection<int?> ListaStavkiProdajeId
        {
            get { return listaStavkiProdajeId; }
            set
            { listaStavkiProdajeId = value;
                OnPropertyChanged("ListaStavkiProdajeId");
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

            ProdajaNamestaja kopija = new ProdajaNamestaja();
            kopija.Id = id;
            kopija.BrRacuna = brRacuna;
            kopija.DatumProdaje = datumProdaje;
            kopija.Kupac = kupac;
            kopija.ListaStavkiProdaje = listaStavkiProdaje;
            kopija.ListaStavkiProdajeId = listaStavkiProdajeId;
            kopija.UkupanIznos = ukupanIznos;
            kopija.Obrisan = obrisan;

            return kopija;
        }
        
    }
}
