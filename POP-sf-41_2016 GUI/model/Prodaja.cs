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
    public class Prodaja : INotifyPropertyChanged, ICloneable
    {

        //public const double PDV = 0.02;

        
        private bool obrisan;
        private ObservableCollection<int?> listaProdajeNamestajaId;
        private int id;
        private DateTime datumProdaje;
        private string brRacuna;
        private string kupac;
        private double ukupanIznos;
        public const double pdv =0.2;
        private ObservableCollection<ProdajaNamestaj> listaProdajeNamestaja;
        private ObservableCollection<int?> listaDodatnihUslugaId;
        private ObservableCollection<ProdajaDodatnaUsluga> listaDodatnihUsluga;
        private string prodavac;

        public string Prodavac
        {
            get { return prodavac; }
            set { prodavac = value; }
        }



        public ObservableCollection<int?> ListaDodatnihUslugaId
        {
            get { return listaDodatnihUslugaId; }
            set
            {
                listaDodatnihUslugaId = value;
                OnPropertyChanged("ListaDodatnihUslugaId");
            }
        }

        public ObservableCollection<ProdajaDodatnaUsluga> ListaDodatnihUsluga
        {
            get
            {
                if(listaDodatnihUsluga == null)
                {
                    listaDodatnihUsluga = ProdajaDodatnaUsluga.NadjiDodatnuUsluguProdaje(listaDodatnihUslugaId);
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

         public ObservableCollection<ProdajaNamestaj> ListaStavkiProdaje
         {
             get
             {
                 if(listaProdajeNamestaja == null)
                 {
                     listaProdajeNamestaja = ProdajaNamestaj.NadjiStavkuProdaje(listaProdajeNamestajaId);
                 }
                 return listaProdajeNamestaja; }
             set
            {
                listaProdajeNamestaja = value;

                var lista = new ObservableCollection<int?>();
                if(listaProdajeNamestaja != null)
                {
                    foreach (var item in listaProdajeNamestaja)
                    {
                        lista.Add(item.Id);
                    }
                    ListaProdajeNamestajaId = lista;
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


        public ObservableCollection<int?> ListaProdajeNamestajaId
        {
            get { return listaProdajeNamestajaId; }
            set
            { listaProdajeNamestajaId = value;
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

            Prodaja kopija = new Prodaja();
            kopija.Id = id;
            kopija.BrRacuna = brRacuna;
            kopija.DatumProdaje = datumProdaje;
            kopija.Kupac = kupac;
            kopija.Prodavac = prodavac;
            kopija.ListaStavkiProdaje = listaProdajeNamestaja;
            kopija.ListaProdajeNamestajaId = listaProdajeNamestajaId;
            kopija.UkupanIznos = ukupanIznos;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        public static ObservableCollection<Prodaja> Update(Prodaja primljenaProdaja)
        {
            var lista = Projekat.Instance.Prodaja;
            foreach (var item in lista)
            {
                if (item.Id == primljenaProdaja.Id)
                {
                    item.BrRacuna = primljenaProdaja.BrRacuna;
                    item.DatumProdaje = primljenaProdaja.DatumProdaje;
                    item.Kupac = primljenaProdaja.Kupac;
                    item.Prodavac = primljenaProdaja.Prodavac;
                    item.ListaDodatnihUsluga = primljenaProdaja.ListaDodatnihUsluga;
                    item.ListaDodatnihUslugaId = primljenaProdaja.ListaDodatnihUslugaId;
                    item.ListaProdajeNamestajaId = primljenaProdaja.ListaProdajeNamestajaId;
                    item.ListaStavkiProdaje = primljenaProdaja.ListaStavkiProdaje;
                    break;
                }
            }
            return lista;
        }

    }
}
