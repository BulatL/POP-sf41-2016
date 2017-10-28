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
        static List<Namestaj> Namestaj { get; set; } = new List<Namestaj>();

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
                Console.WriteLine("0. Izlaz iz aplikacije" + "\n");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor < 0 || izbor > 2);


            switch (izbor)
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
                Console.WriteLine("0. Povratak u glavni meni" + "\n");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor < 0 || izbor > 4);


            switch (izbor)
            {
                case 1:
                    Console.WriteLine("=== IZLISTAJ NAMESTAJ ===");
                    IzlistajNamestaj();
                    IspisiMeniNamestaja();
                    break;
                case 2:
                    DodajNoviNamestaj();
                    break;
                case 3:
                    IzmeniNamestaj();
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
            for (int i = 0; i < Namestaj.Count; i++)
            {
                if (Namestaj[i].Obrisan == false)
                {
                    Console.WriteLine($"{i + 1}.{Namestaj[i].Naziv}, cena: {Namestaj[i].JedinicnaCena}, tip namestaja: {Namestaj[i].TipNamestaja.Naziv}" + "\n");
                }

            }

        }

        private static void tipNamestaja(Namestaj parametar)
        {

            int izbor = 0;
            do
            {
                Console.WriteLine("Postojeci tip namestaja 1) " + "\n" +
                    "Novi tip namestaja 2)" + "\n");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor < 0 || izbor > 2);


            switch (izbor)
            {
                case 1:
                    for (int i = 0; i < listaTipNamestaja.Count; i++)
                    {
                        if (listaTipNamestaja[i].Obrisano == false)
                        {

                            Console.WriteLine(value: $"{i + 1}.{listaTipNamestaja[i].Naziv}");
                        }

                    }
                    int redniBrNamestaja = int.Parse(Console.ReadLine());
                    parametar.TipNamestaja = listaTipNamestaja[redniBrNamestaja - 1];
                    Namestaj.Add(parametar);

                    IspisiMeniNamestaja();
                    break;
                case 2:
                    var noviTipNamestaja = new TipNamestaja();

                    Console.WriteLine("Unesite naziv: ");
                    noviTipNamestaja.Naziv = Console.ReadLine();

                    Console.WriteLine("Unesite id: ");
                    noviTipNamestaja.Id = int.Parse(Console.ReadLine());

                    parametar.TipNamestaja = noviTipNamestaja;
                    Namestaj.Add(parametar);

                    IspisiMeniNamestaja();
                    break;

            }
        }

        private static void DodajNoviNamestaj()
        {
            Console.WriteLine("\n" + "=== DODAJ NOVI NAMESTAJ ===");

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

            tipNamestaja(noviNamestaj);

            /*            TipNamestaja trazeniTipNamestaja = null;
                        string nazivTipaNamestaj = "";

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

                        IspisiMeniNamestaja();*/


        }

        private static void IzmeniNamestaj()
        {
            var pripremiNamestajZaMenjanje = new Namestaj();
            // var pripremiTipNamestajaZaMenjanje = new TipNamestaja();
            Console.WriteLine("=== IZMENI NAMESTAJ ===");
            IzlistajNamestaj();

            int izabraniNamestajaZaMenjanje = 0;
            int izbor = 0;
            do
            {
                Console.WriteLine("\n" + "Izaberite redni broj namestaja koji zelite da izmenite: ");
                izabraniNamestajaZaMenjanje = int.Parse(Console.ReadLine());
            } while (izabraniNamestajaZaMenjanje < 0 || izabraniNamestajaZaMenjanje > Namestaj.Count);

            do
            {
                Console.WriteLine("\n" + "Sta zelite da izmenite: ");
                Console.WriteLine("Id 1) ");
                Console.WriteLine("Naziv 2) ");
                Console.WriteLine("Cenu 3) ");
                Console.WriteLine("Sifru 4) ");
                Console.WriteLine("Kolicinu 5) ");
                Console.WriteLine("Tip namestaja 6) ");
                Console.WriteLine("Sve od ponudjenog 7) ");

                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 7);

            switch (izbor)
            {
                case 1:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];
                    Console.WriteLine("Unesite novi id: ");
                    pripremiNamestajZaMenjanje.Id = int.Parse(Console.ReadLine());

                    IspisiMeniNamestaja();
                    break;
                case 2:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];
                    Console.WriteLine("Unesite novi naziv: ");
                    pripremiNamestajZaMenjanje.Naziv = Console.ReadLine();

                    IspisiMeniNamestaja();
                    break;
                case 3:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];
                    Console.WriteLine("Unesite novu cenu: ");
                    pripremiNamestajZaMenjanje.JedinicnaCena = int.Parse(Console.ReadLine());

                    IspisiMeniNamestaja();
                    break;
                case 4:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];
                    Console.WriteLine("Unesite novu sifru: ");
                    pripremiNamestajZaMenjanje.Sifra = Console.ReadLine();

                    IspisiMeniNamestaja();
                    break;
                case 5:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];
                    Console.WriteLine("Unesite novu kolicinu: ");
                    pripremiNamestajZaMenjanje.KolicinaUMagacinu = int.Parse(Console.ReadLine());

                    IspisiMeniNamestaja();
                    break;
                case 6:
                    tipNamestaja(pripremiNamestajZaMenjanje);
                    break;
                case 7:
                    pripremiNamestajZaMenjanje = Namestaj[izabraniNamestajaZaMenjanje - 1];

                    Console.WriteLine("Unesite novi id: ");
                    pripremiNamestajZaMenjanje.Id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Unesite novi naziv: ");
                    pripremiNamestajZaMenjanje.Naziv = Console.ReadLine();

                    Console.WriteLine("Unesite novu cenu: ");
                    pripremiNamestajZaMenjanje.JedinicnaCena = int.Parse(Console.ReadLine());

                    Console.WriteLine("Unesite novu sifru: ");
                    pripremiNamestajZaMenjanje.Sifra = Console.ReadLine();

                    Console.WriteLine("Unesite novu kolicinu: ");
                    pripremiNamestajZaMenjanje.KolicinaUMagacinu = int.Parse(Console.ReadLine());

                    tipNamestaja(pripremiNamestajZaMenjanje);

                    IspisiMeniNamestaja();
                    break;

                default:
                    break;
            }


            /*          Console.WriteLine("=== IZMENI NAMESTAJ ===");
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

                       IspisiMeniNamestaja();*/
        }
    }
}

