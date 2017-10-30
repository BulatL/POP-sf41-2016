using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class TipNamestaja
    {
        public int Id { get; set; }

        public bool Obrisano { get; set; }

        public string Naziv { get; set; }


        public static string NadjiNamestaj(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.TipNamestaj)
            {
                if(tip.Id == idProsledjen)
                {
                    return tip.Naziv;
                }

            }
            return null;
        }
    }

}
