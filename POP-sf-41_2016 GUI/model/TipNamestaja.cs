using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class TipNamestaja : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private String naziv;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
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

        public static TipNamestaja NadjiTipNamestaj(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.TipNamestaja)
            {
                if (tip.Id == idProsledjen)
                {
                    return tip;
                }

            }
            return null;
        }

        public static int? NadjiTipNamestajaString(string naziv)
        {
            foreach (var tip in Projekat.Instance.TipNamestaja)
            {
                if (tip.Naziv.ToUpper().Equals(naziv.ToUpper()))
                {
                    return tip.Id;
                }

            }
            return null;
        }

        public override string ToString()
        {
            return Naziv;
        }

        public object Clone()
        {
            TipNamestaja kopija = new TipNamestaja();
            kopija.Id = id;
            kopija.Naziv = Naziv;
            kopija.Obrisan = obrisan;

            return kopija;
        }

        public static ObservableCollection<TipNamestaja> Update(TipNamestaja primljeniTipNamestaja)
        {
            var lista = Projekat.Instance.TipNamestaja;
            foreach (var item in lista)
            {
                if (item.Id == primljeniTipNamestaja.Id)
                {
                    item.Naziv = primljeniTipNamestaja.Naziv;
                    break;
                }
            }
            return lista;
        }
    }

}
