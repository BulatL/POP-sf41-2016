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
    public class ProdajaDodatnaUsluga : INotifyPropertyChanged
    {

        private int id;
        private int dodatnaUslugaId;
        private double cena;
        private DodatnaUsluga dodatnaUsluga;
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
                return DodatnaUsluga.Naziv;
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
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int DodatnaUslugaId
        {
            get { return dodatnaUslugaId; }
            set
            {
                dodatnaUslugaId = value;
                OnPropertyChanged("DodatnaUslugaId");
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
        public DodatnaUsluga DodatnaUsluga
        {
            get
            {
                if (dodatnaUsluga == null)
                {
                    dodatnaUsluga = DodatnaUsluga.NadjiDodatnuUslugu(DodatnaUslugaId);
                }
                return dodatnaUsluga;
            }
            set
            {
                dodatnaUsluga = value;
                DodatnaUslugaId = dodatnaUsluga.Id;
                OnPropertyChanged("DodatnaUsluga");
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

        public static ObservableCollection<ProdajaDodatnaUsluga> NadjiDodatnuUsluguProdaje(ObservableCollection<int?> prosledjenaListaDodatnihUslugaProdaje)
        {
            var lista = new ObservableCollection<ProdajaDodatnaUsluga>();
            foreach (var tip in Projekat.Instance.ProdajaDodatneUsluge)
            {
                if (prosledjenaListaDodatnihUslugaProdaje != null)
                {
                    foreach (var pdn in prosledjenaListaDodatnihUslugaProdaje)
                    {
                        if (tip.Id == pdn)
                        {
                            lista.Add(tip);

                        }
                    }
                }
            }
            return lista;
        }
    }
}
