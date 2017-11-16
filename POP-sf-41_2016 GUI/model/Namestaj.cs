using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
     public class Namestaj
    {
        public int Id { get; set; }

        public bool Obrisan { get; set; }

        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public double JedinicnaCena { get; set; }

        public int KolicinaUMagacinu{ get; set; }

        public int? TipNamestajaId { get; set; }

        public int? AkcijaId { get; set; }

        public override string ToString()
        {
            return ($"id:{Id}, naziv: {Naziv}, sifra: {Sifra}, cena: {JedinicnaCena}, kolicina: {KolicinaUMagacinu} , tip namestaja: {TipNamestaja.NadjiNamestaj(TipNamestajaId)} ");
        }

        public static Namestaj NadjiNamestaj(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.Namestaj)
            {
                if (tip.Id == idProsledjen)
                {
                    return tip;
                }

            }
            return null;
        }

    }
}
