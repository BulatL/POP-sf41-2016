using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class DodatnaUsluga
    {

        public int Id { get; set; }

        public bool Obrisan { get; set; }

        public string Naziv { get; set; }

        public double Cena { get; set; }

        public static DodatnaUsluga NadjiDodatnuUslugu(int? idProsledjen)
        {
            foreach (var tip in Projekat.Instance.DodatnaUsluga)
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
