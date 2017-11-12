using POP_sf41_2016.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();


        public List<Namestaj> namestaj;

        public List<Namestaj> Namestaj
        {
            get
            {
                this.namestaj = GenericSerializer.Deserializer<Namestaj>("namestaj.xml");
                return this.namestaj;
            }
            set
            {

                this.namestaj = value;
                GenericSerializer.Serializer<Namestaj>("namestaj.xml", namestaj);
            }
        }

        public List<TipNamestaja> tipNamestaj;

        public List<TipNamestaja> TipNamestaj
        {
            get
            {
                this.tipNamestaj = GenericSerializer.Deserializer<TipNamestaja>("tipNamestaja.xml");
                return this.tipNamestaj;
            }
            set
            {

                this.tipNamestaj = value;
                GenericSerializer.Serializer<TipNamestaja>("tipNamestaja.xml", tipNamestaj);
            }
        }

        public List<Akcija> akcija;

        public List<Akcija> Akcija
        {
            get
            {
                this.akcija = GenericSerializer.Deserializer<Akcija>("akcija.xml");
                return this.akcija;
            }
            set
            {

                this.akcija = value;
                GenericSerializer.Serializer<Akcija>("akcija.xml", akcija);
            }
        }

        public List<Korisnik> korisnik;

        public List<Korisnik> Korisnik
        {
            get
            {
                this.korisnik = GenericSerializer.Deserializer<Korisnik>("korisnici.xml");
                return this.korisnik;
            }
            set
            {

                this.korisnik = value;
                GenericSerializer.Serializer<Korisnik>("korisnici.xml", korisnik);
            }
        }


        public List<DodatnaUsluga> dodatnaUsluga;

        public List<DodatnaUsluga> DodatnaUsluga
        {
            get
            {
                this.dodatnaUsluga = GenericSerializer.Deserializer<DodatnaUsluga>("dodatnaUsluga.xml");
                return this.dodatnaUsluga;
            }
            set
            {

                this.dodatnaUsluga = value;
                GenericSerializer.Serializer<DodatnaUsluga>("dodatnaUsluga.xml", dodatnaUsluga);
            }
        }

        public List<ProdajaNamestaja> prodajaNamestaja;

        public List<ProdajaNamestaja> ProdajaNamestaja
        {
            get
            {
                this.prodajaNamestaja = GenericSerializer.Deserializer<ProdajaNamestaja>("prodajaNamestaja.xml");
                return this.prodajaNamestaja;
            }
            set
            {
                this.prodajaNamestaja = value;
                GenericSerializer.Serializer<ProdajaNamestaja>("prodajaNamestaja.xml", prodajaNamestaja);
            }
        }
    }
}
