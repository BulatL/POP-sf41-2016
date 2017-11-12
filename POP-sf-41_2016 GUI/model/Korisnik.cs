using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016.model
{
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }

    public class Korisnik
    {
        public int Id { get; set; }

        public bool obrisan { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public TipKorisnika TipKorisnika { get; set; }
    }
}
