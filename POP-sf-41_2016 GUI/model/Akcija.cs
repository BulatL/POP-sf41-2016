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
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
    
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavrsetka;
        private bool obrisan;
        private ObservableCollection<int?> listaNaAkcijiId;
        private ObservableCollection<NaAkciji> listaNaAkciji;
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public Akcija()
        {
            ListaNaAkcijiId = new ObservableCollection<int?>();
            DatumPocetka = DateTime.Now;
            DatumZavrsetka = DateTime.Now;
        }

        public ObservableCollection<int?> ListaNaAkcijiId
        {
            get { return listaNaAkcijiId; }
            set
            {
                listaNaAkcijiId = value;
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
        public ObservableCollection<NaAkciji> ListaNaAkciji
        {
            get
            {
                if(listaNaAkciji == null)
                {
                    listaNaAkciji = NaAkciji.NadjiNaAkciji(listaNaAkcijiId);
                }

                return listaNaAkciji;
            }
            set
            {
                listaNaAkciji = value;

                var lista = new ObservableCollection<int?>();
                if (listaNaAkciji != null)
                {
                    foreach (var item in listaNaAkciji)
                    {
                        lista.Add(item.Id);
                    }
                    listaNaAkcijiId = lista;
                    OnPropertyChanged("ListaNaAkciji");
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
            kopija.Obrisan = obrisan;
            kopija.ListaNaAkciji = listaNaAkciji;
            kopija.ListaNaAkcijiId = listaNaAkcijiId;
            kopija.Naziv = naziv;

            return kopija;
        }

        public static ObservableCollection<Akcija> Update(Akcija primljenaAkcija)
        {
            var lista = Projekat.Instance.Akcije;
            foreach (var item in lista)
            {
                if (item.Id == primljenaAkcija.Id)
                {
                    item.DatumPocetka = primljenaAkcija.DatumPocetka;
                    item.DatumZavrsetka = primljenaAkcija.DatumZavrsetka;
                    item.ListaNaAkciji = primljenaAkcija.ListaNaAkciji;
                    item.ListaNaAkcijiId = primljenaAkcija.ListaNaAkcijiId;
                    item.Naziv = primljenaAkcija.Naziv;
                    break;
                }
            }
            return lista;
        }
    }
}