using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf41_2016
{
    class Program
    {
        static List<Namestaj> Namestaj { get; set; } = new List<Namestaj>() ;

        static List<TipNamestaja> listaTipNamestaja { get; set; } = new List<TipNamestaja>();

        static void Main(string[] args)
        {
            var s1 = new Salon()
            {
                Id = 1,
                Naziv = "forma FTNale",
                Adresa = "Trg Dositeja Obradovica 6",
                BrojZiroRacuna = 840 - 00037666 - 83,
                Email = "Kontakt@ftn.uns.ac.rs",
                MaticniBroj = 234324,
                PIB = 323443,
                Telefon = "021/342 343",
                AdresaInternetSajta = "http://www.ftn.uns.ac.rs"
            };

            var tn1 = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Sofa"
            };

            var tn2 = new TipNamestaja()
            {
                Id = 2,
                Naziv = "regal"
            };

            listaTipNamestaja.Add(tn2);
            listaTipNamestaja.Add(tn1);

            var n1 = new Namestaj()
            {

                Id = 1,
                Naziv = "Super sofra",
                Sifra = "Sf sifra za sofe",
                JedinicnaCena = 52,
                TipNamestaja = tn1,
                KolicinaUMagacinu = 3
            };
            Namestaj.Add(n1);

            Console.WriteLine($"Dobro dosli u Salon {s1.Naziv}");
            IspisGlavnogMenija();
        }

            private static void IspisGlavnogMenija()
        {
            int izbor = 0;

            do
            {
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom namestaja");
                Console.WriteLine("0. Izlaz iz aplikacije");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor <0 || izbor >2);


            switch(izbor)
            {
                case 1:
                    IspisiMeniNamestaja();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private static void IspisiMeniNamestaja()
        {
            int izbor = 0;

            do
            {
                Console.WriteLine("=== MENI NAMESTAJA ===");
                Console.WriteLine("1. Izlistaj namestaj");
                Console.WriteLine("2. Dodaj novi namestaj");
                Console.WriteLine("3. Izmeni postojeci namestaj");
                Console.WriteLine("4. Obrisi postojeci");
                Console.WriteLine("0. Povratak u glavni meni");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor <0 || izbor > 4);


            switch (izbor)
            {
                case 1:
                    IzlistajNamestaj();
                    break;
                case 2:
                    DodajNoviNamestaj();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void IzlistajNamestaj()
        {
           
            Console.WriteLine("=== IZLISTAJ NAMESTAJ ===");
            for(int i = 0; i < Namestaj.Count; i++)
            {
                if (Namestaj[i].Obrisan == false)
                {
                    Console.WriteLine($"{i + 1}.{Namestaj[i].Naziv}, cena: {Namestaj[i].JedinicnaCena}, tip namestaja: {Namestaj[i].TipNamestaja.Naziv}");
                }
                
            }

            IspisiMeniNamestaja();
        }

        private static void DodajNoviNamestaj()
        {
            Console.WriteLine("=== DODAJ NOVI NAMESTAJ ===");

            var noviNamestaj = new Namestaj();

            noviNamestaj.Id = Namestaj.Count + 1;

            Console.WriteLine("Naziv namestaj: ");
            noviNamestaj.Naziv = Console.ReadLine();

            Console.WriteLine("Sifra namestaja: ");
            noviNamestaj.Sifra = Console.ReadLine();

            Console.WriteLine("Cena namestaja: ");
            noviNamestaj.JedinicnaCena = double.Parse(Console.ReadLine());

            Console.WriteLine("Kolicina namestaja u magacinu: ");
            noviNamestaj.KolicinaUMagacinu = int.Parse(Console.ReadLine());

            string nazivTipaNamestaj = "";
            TipNamestaja trazeniTipNamestaja = null;
            do
            {
                Console.WriteLine("Tip namestaja: ");
                nazivTipaNamestaj = Console.ReadLine();

                foreach (var tipNamestaja in listaTipNamestaja)
                {
                    if(tipNamestaja.Naziv == nazivTipaNamestaj)
                    {
                        trazeniTipNamestaja = tipNamestaja;
                    }
                }

            } while (trazeniTipNamestaja == null);

            noviNamestaj.TipNamestaja = trazeniTipNamestaja;    

            Namestaj.Add(noviNamestaj);

            IspisiMeniNamestaja();


        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("=== IZMENI NAMESTAJ ===");
            Namestaj trazeniNamestaj = null;
            string nazivTrazenogNamestaja;
            do
            {
                Console.WriteLine("Unesite naziv namestaja kojeg menjate: ");
                nazivTrazenogNamestaja = Console.ReadLine();

                foreach(var namestaj in Namestaj)
                {
                    if(namestaj.Naziv == nazivTrazenogNamestaja)
                    {
                        trazeniNamestaj = namestaj;
                    }
                }
            } while (trazeniNamestaj == null);

            Console.WriteLine("Unesite novi naziv namestaja: ");
            trazeniNamestaj.Naziv = Console.ReadLine();

            Console.WriteLine("Unesite novu sifru namestaja: ");
            trazeniNamestaj.Sifra = Console.ReadLine();

            Console.WriteLine("Unesite novu cenu namestaja: ");
            trazeniNamestaj.JedinicnaCena =double.Parse(Console.ReadLine());

            Console.WriteLine("Unesite novu kolicinu namestaja: ");
            trazeniNamestaj.KolicinaUMagacinu = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesite novi naziv namestaja: ");

            IspisiMeniNamestaja();
        }
    }
}

