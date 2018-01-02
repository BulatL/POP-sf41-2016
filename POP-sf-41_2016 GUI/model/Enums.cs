using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf_41_2016_GUI.model
{
    class Enums
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public enum TipPretrage
        {
            Naziv,
            Sifra,
            TipNamestajaNaziv,
            DatumProdaje,
            BrRacuna,
            Kupac,
            ProdatiNamestaj,
            Ime,
            Prezime,
            KorisnickoIme
        } 
        public enum Sort
        {
            Id,
            Naziv,
            Sifra,
            Cena,
            Kolicina,
            TipNamestajaNaziv,
            DatumProdaje,
            BrRacuna,
            Kupac,
            ProdatiNamestaj,
            Ime,
            Prezime,
            KorisnickoIme,
            Lozinka,
            TipKorisnika
        }

        public enum TypeSort
        {
            asc,
            desc
        }

        public enum TipBrisanja
        {
            PoAkcijaId,
            PoNaAkciji
        }
    }
}
