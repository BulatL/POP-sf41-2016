using POP_sf_41_2016_GUI.model;
using System;
using System.Collections.Generic;
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
        private List<int?> listaStavkiProdajeId;
        private int id;
        private DateTime datumProdaje;
        private string brRacuna;
        private string kupac;
        private double ukupanIznos;
        private int? dodatnaUslugaId;
        private DodatnaUsluga dodatnaUsluga;
        private const double pdv =0.02;
        private List<StavkaProdaje> listaStavkiProdaje;


         [XmlIgnore]
         public List<StavkaProdaje> ListaStavkiProdaje
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
                //ListaStavkiProdajeId = listaStavkiProdaje;
                OnPropertyChanged("ListaStavkiProdaje");
            }
         }


        [XmlIgnore]
        public DodatnaUsluga DodatnaUsluga
        {
            get
            {
                if (dodatnaUsluga == null)
                {
                    dodatnaUsluga = DodatnaUsluga.NadjiDodatnuUslugu(dodatnaUslugaId);
                }
                return dodatnaUsluga;
            }
            set
            {
                dodatnaUsluga = value;
                DodatnaUslugaId = DodatnaUsluga.Id;
                OnPropertyChanged("DodatnaUsluga");
            }
        }

        public int? DodatnaUslugaId
        {
            get { return dodatnaUslugaId; }
            set
            {
                dodatnaUslugaId = value;
                OnPropertyChanged("DodatnaUslugaId");
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


        public List<int?> ListaStavkiProdajeId
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
            kopija.DodatnaUslugaId = DodatnaUslugaId;
            kopija.DodatnaUsluga = dodatnaUsluga;
            kopija.Kupac = kupac;
            kopija.ListaStavkiProdaje = listaStavkiProdaje;
            kopija.ListaStavkiProdajeId = listaStavkiProdajeId;
            kopija.UkupanIznos = ukupanIznos;
            kopija.Obrisan = obrisan;

            return kopija;
        }
        
    }
}
