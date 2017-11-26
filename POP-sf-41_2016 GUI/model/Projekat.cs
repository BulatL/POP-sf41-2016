using POP_sf41_2016.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Projekat
    {
        public static Projekat Instance { get; private set; } = new Projekat();

        public ObservableCollection<Namestaj> Namestaj { get; set; }

        public ObservableCollection<TipNamestaja> TipNamestaja { get; set; }

        public ObservableCollection<Akcija> Akcija { get; set; }

        public ObservableCollection<DodatnaUsluga> DodatnaUsluga { get; set; }

        public ObservableCollection<Korisnik> Korisnik { get; set; }

        public ObservableCollection<Salon> Salon { get; set; }

        public ObservableCollection<ProdajaNamestaja> ProdajaNamestaja { get; set; }

        private Projekat()
        {
            Namestaj = GenericSerializer.Deserializer<Namestaj>("namestaj.xml");
            TipNamestaja = GenericSerializer.Deserializer<TipNamestaja>("tipNamestaja.xml");
            Akcija = GenericSerializer.Deserializer<Akcija>("akcija.xml");
            DodatnaUsluga = GenericSerializer.Deserializer<DodatnaUsluga>("dodatnaUsluga.xml");
            Korisnik = GenericSerializer.Deserializer<Korisnik>("korisnici.xml");
            Salon = GenericSerializer.Deserializer<Salon>("salon.xml");
            ProdajaNamestaja = GenericSerializer.Deserializer<ProdajaNamestaja>("prodajaNamestaja.xml");
        }
    }
}
