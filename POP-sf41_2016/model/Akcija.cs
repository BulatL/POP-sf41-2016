using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public class Akcija
    {
        public int Id { get; set; }

        public DateTime DatukPocetka { get; set; }

        public DateTime DatumZavrsetka { get; set; }

        public List<Namestaj> NamestajNaPopustu { get; set; }

        public bool Obrisan { get; set; }

        public double Popust { get; set; }
    }
}