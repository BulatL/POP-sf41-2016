using POP_sf41_2016.model;
using POP_sf41_2016.util;
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

        static List<Korisnik> korisnici { get; set; } = new List<Korisnik>();

        static List<DodatnaUsluga> dodatnaUSluga { get; set; } = new List<DodatnaUsluga>();
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

            var k1 = new Korisnik()
            {
                Ime = "Marko",
                Prezime = "Markovic",
                KorisnickoIme = "marko",
                Lozinka = "123"
            };

            var k2 = new Korisnik()
            {
                Ime = "Nikola",
                Prezime = "Nikolic",
                KorisnickoIme = "nikola",
                Lozinka = "123"
            };
            /*
            korisnici.Add(k1);
            korisnici.Add(k2);
            GenericSerializer.Serializer<Korisnik>("korisnici.xml", korisnici);
            var listaKorisnika = Projekat.Instance.Korisnik;
            Projekat.Instance.Korisnik = listaKorisnika;*/

            var du1 = new DodatnaUsluga()
            {
                Id = 1,
                Naziv = "Prevoz",
                Cena = 500
            };

            var du2 = new DodatnaUsluga()
            {
                Id = 2,
                Naziv = "Montaza",
                Cena = 1500
            };

            dodatnaUSluga.Add(du1);
            dodatnaUSluga.Add(du2);
            GenericSerializer.Serializer<DodatnaUsluga>("dodatnaUsluga.xml", dodatnaUSluga);
            var listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;
            Projekat.Instance.DodatnaUsluga = listaDodatnihUsluga;


            Console.WriteLine($"Dobro dosli u Salon {s1.Naziv}");
            

            int brojPokusaja = 0;
            var korisnici = Projekat.Instance.Korisnik;
            do
            {
                Console.WriteLine("Unesite korisnicko ime");
                string korisnickoIme=Console.ReadLine();
                Console.WriteLine("Unesite sifru");
                string sifra = Console.ReadLine();

                foreach(var korisnik in korisnici)
                {
                    if (korisnickoIme.Equals(korisnik.KorisnickoIme) && sifra.Equals(korisnik.Lozinka))
                    {
                        Console.WriteLine("\n" + "Uspesno ste se prijavili" + "\n");
                        IspisGlavnogMenija();
                    }
                }

                brojPokusaja++;
            } while (brojPokusaja<3);
            Environment.Exit(0);
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

                case 2:
                    IspisiMeniTipNamestaja();
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
                Console.WriteLine("4. Pretrazi namestaj");
                Console.WriteLine("5. Sortiraj namestaj");
                Console.WriteLine("6. Obrisi postojeci");
                Console.WriteLine("0. Povratak u glavni meni" + "\n");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor < 0 || izbor > 6);


            switch (izbor)
            {
                case 1:
                    Console.WriteLine("=== IZLISTAJ NAMESTAJ ===");
                    var lista = Projekat.Instance.Namestaj;
                    IzlistajNamestaj(lista);
                    IspisiMeniNamestaja();
                    break;
                case 2:
                    DodajNoviNamestaj();
                    break;
                case 3:
                    IzmeniNamestaj();
                    break;


                case 4:
                    PretraziNamestaj();
                    break;

                case 5:
                    SortirajNamestaj();
                    break;

                case 6:
                    ObrisiNamestaj();
                    break;


                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void IspisiMeniTipNamestaja()
        {
            int izbor = 0;

            do
            {
                Console.WriteLine("=== MENI TIP NAMESTAJA ===");
                Console.WriteLine("1. Izlistaj tipove");
                Console.WriteLine("2. Dodaj novi tip");
                Console.WriteLine("3. Izmeni postojeci tip");
                Console.WriteLine("4. Pretrazi tip");
                Console.WriteLine("5. Sortiraj tip");
                Console.WriteLine("6. Obrisi postojeci");
                Console.WriteLine("0. Povratak u glavni meni" + "\n");
                izbor = int.Parse(Console.ReadLine());

            } while (izbor < 0 || izbor > 6);

            switch (izbor)
            {
                case 1:
                    Console.WriteLine("=== IZLISTAJ TIP NAMESTAJA ===");
                    var lista = Projekat.Instance.TipNamestaj;
                    IzlistajTipNamestaja(lista);
                    IspisiMeniTipNamestaja();
                    break;
                case 2:
                    Console.WriteLine("\n" + "=== DODAJ NOVI TIP NAMESTAJA ===");
                    DodajNoviTipNamestaja();
                    IspisiMeniTipNamestaja();
                    break;
                case 3:
                    Console.WriteLine("\n" + "=== IZMENI TIP NAMESTAJA ===");
                    IzmeniTipNamestaja();
                    break;

                case 4:
                    PretraziTipNamestaja();
                    break;

                case 5:
                    SortirajTipNamestaja();
                    break;

                case 6:
                    ObrisiTipNamestaja();
                    break;

                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }

        }

        private static void IzlistajTipNamestaja(List<TipNamestaja> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Obrisano == false)
                {
                    Console.WriteLine($"{i + 1}.id: {lista[i].Id},{lista[i].Naziv}" + "\n");
                }

            }

        }


        private static void DodajNoviTipNamestaja()
        {

            var tipNamestaja = Projekat.Instance.TipNamestaj;

            Console.WriteLine("Unesite naziv");
            string naziv = Console.ReadLine();

            tipNamestaja.Add(new TipNamestaja() { Naziv = naziv, Id = tipNamestaja.Count + 1 });

            Projekat.Instance.TipNamestaj = tipNamestaja;
        }


        private static void IzmeniTipNamestaja()
        {
            var tipNamestaja = Projekat.Instance.TipNamestaj;
            IzlistajTipNamestaja(tipNamestaja);

            int id = 0;
            do
            {
                Console.WriteLine("Unesite id tipa namestaja koji zelite da izmenite");
                id = int.Parse(Console.ReadLine());

            } while ( id > tipNamestaja.Count);


            foreach (var tip in tipNamestaja)
            {
                if (tip.Id == id)
                {
                    Console.WriteLine("Unesite novi naziv: ");
                    tip.Naziv = Console.ReadLine();

                    Projekat.Instance.TipNamestaj = tipNamestaja;

                }

            }

            IspisiMeniTipNamestaja();

        }

        private static void ObrisiTipNamestaja()
        {

            var tipNamestaja = Projekat.Instance.TipNamestaj;
            Console.WriteLine("=== OBRISI TIP NAMESTAJA ===");
            IzlistajTipNamestaja(tipNamestaja);

            int id = 0;
            do
            {
                Console.WriteLine("\n" + "Izaberite id tipa namestaja koji zelite da obrisete: ");
                id = int.Parse(Console.ReadLine());
            } while (id < 0 || id > tipNamestaja.Count);



            foreach (var tip in tipNamestaja)
            {
                if (tip.Id == id)
                {
                    tip.Obrisano = true;

                    Projekat.Instance.TipNamestaj = tipNamestaja;

                }

            }
            IspisiMeniTipNamestaja();
        }


        private static void PretraziTipNamestaja()
        {
            List<TipNamestaja> listaTipNamestaj = new List<TipNamestaja>();
            var tipNamestajaZaPretragu = Projekat.Instance.TipNamestaj;
            Console.WriteLine("=== PRETRAZI TIP NAMESTAJ ===");


            Console.WriteLine("Unesite naziv za pretragu: ");
            string naziv = Console.ReadLine();

            foreach (var tipNamestaja in tipNamestajaZaPretragu)
            {
                if (naziv.ToUpper().Equals(tipNamestaja.Naziv.ToUpper()))
                {
                    listaTipNamestaj.Add(tipNamestaja);

                }
            }
            IzlistajTipNamestaja(listaTipNamestaj);

            if (listaTipNamestaj.Count == 0)
            {
                Console.WriteLine("Nismo pronasli tip namestaja koji odgovara unesenom nazivu" + "\n");
            }

            IspisiMeniTipNamestaja();

            Projekat.Instance.TipNamestaj = tipNamestajaZaPretragu;

        }

        private static void SortirajTipNamestaja()
        {
            var tipNamestaja = Projekat.Instance.TipNamestaj;
            List<TipNamestaja> sortiranaLista = new List<TipNamestaja>();
            sortiranaLista = tipNamestaja.OrderBy(o => o.Naziv).ToList();
            IzlistajTipNamestaja(sortiranaLista);
            IspisiMeniNamestaja();

        }

        private static void IzlistajNamestaj(List<Namestaj> lista)
        {
            //var lista = Projekat.Instance.Namestaj;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Obrisan == false)
                {
                    var tipNamestaja = lista[i].TipNamestajaId;
                    Console.WriteLine($"{i + 1}.id: {lista[i].Id},{lista[i].Naziv}, cena: {lista[i].JedinicnaCena}, kolicina: {lista[i].KolicinaUMagacinu}, tip namestaja: {TipNamestaja.NadjiNamestaj(tipNamestaja)}" + "\n");
                }

            }

        }

        private static string tipNamestaja()
        {
            var listaNamestaja = Projekat.Instance.TipNamestaj;
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
                    for (int i = 0; i < listaNamestaja.Count; i++)
                    {
                        if (listaNamestaja[i].Obrisano == false)
                        {

                            Console.WriteLine(value: $"{i + 1}.{listaNamestaja[i].Naziv}");
                        }

                    }
                    Console.WriteLine("Unesite redni broj namestaja: ");
                    int redniBrNamestaja = int.Parse(Console.ReadLine());
                    var tipNamestaja = listaNamestaja[redniBrNamestaja - 1];
                    string tipNamestajaId = tipNamestaja.Id.ToString();
                    
                    return tipNamestajaId;

                case 2:
                    var noviTipNamestaja = Projekat.Instance.TipNamestaj;

                    Console.WriteLine("Unesite naziv: ");
                    string Naziv = Console.ReadLine();

                    int noviTipNamestajaId = Projekat.Instance.TipNamestaj.Count + 1;

                    noviTipNamestaja.Add(new TipNamestaja() { Id = noviTipNamestajaId, Naziv = Naziv });
                    Projekat.Instance.TipNamestaj = noviTipNamestaja;


                    return noviTipNamestajaId.ToString();

            }
            return null;
        }

        private static void DodajNoviNamestaj()
        {
            Console.WriteLine("\n" + "=== DODAJ NOVI NAMESTAJ ===");

            var noviNamestaj = Projekat.Instance.Namestaj;

            Console.WriteLine("Naziv namestaj: ");
            string Naziv = Console.ReadLine();

            Console.WriteLine("Sifra namestaja: ");
            string Sifra = Console.ReadLine();

            Console.WriteLine("Cena namestaja: ");
            double JedinicnaCena = double.Parse(Console.ReadLine());

            Console.WriteLine("Kolicina namestaja u magacinu: ");
            int KolicinaUMagacinu = int.Parse(Console.ReadLine());

            int TipNamestajaId = int.Parse(tipNamestaja());


            noviNamestaj.Add(new Namestaj() {Id= Projekat.Instance.Namestaj.Count + 1, Naziv = Naziv, Sifra = Sifra, JedinicnaCena = JedinicnaCena, KolicinaUMagacinu= KolicinaUMagacinu, TipNamestajaId=TipNamestajaId, AkcijaId = null  });
            Projekat.Instance.Namestaj = noviNamestaj;

            IspisiMeniNamestaja();

        }

        private static void IzmeniNamestaj()
        {
            var namestajZaMenjanjeLista = Projekat.Instance.Namestaj;
            //var pripremiNamestajZaMenjanje = new Namestaj();
            Console.WriteLine("=== IZMENI NAMESTAJ ===");
            IzlistajNamestaj(namestajZaMenjanjeLista);

            int izabraniNamestajaZaMenjanje = 0;
            int izbor = 0;
            do
            {
                Console.WriteLine("\n" + "Izaberite id namestaja koji zelite da izmenite: ");
                izabraniNamestajaZaMenjanje = int.Parse(Console.ReadLine());
            } while (izabraniNamestajaZaMenjanje < 0 || izabraniNamestajaZaMenjanje > namestajZaMenjanjeLista.Count);



            foreach(var pronadjeniNamestaj in namestajZaMenjanjeLista)
            {
                if(izabraniNamestajaZaMenjanje == pronadjeniNamestaj.Id)
                {
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
                            Console.WriteLine("Unesite novi id: ");
                            pronadjeniNamestaj.Id = int.Parse(Console.ReadLine());
                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;
                        case 2:
                            Console.WriteLine("Unesite novi naziv: ");
                            pronadjeniNamestaj.Naziv = Console.ReadLine();
                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;
                        case 3:
                            Console.WriteLine("Unesite novu cenu: ");
                            pronadjeniNamestaj.JedinicnaCena = int.Parse(Console.ReadLine());
                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;
                        case 4:
                            Console.WriteLine("Unesite novu sifru: ");
                            pronadjeniNamestaj.Sifra = Console.ReadLine();
                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;
                        case 5:
                            Console.WriteLine("Unesite novu kolicinu: ");
                            pronadjeniNamestaj.KolicinaUMagacinu = int.Parse(Console.ReadLine());
                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;
                        case 6:
                            tipNamestaja();
                            break;
                        case 7:
                            Console.WriteLine("Unesite novi id: ");
                            pronadjeniNamestaj.Id = int.Parse(Console.ReadLine());

                            Console.WriteLine("Unesite novi naziv: ");
                            pronadjeniNamestaj.Naziv = Console.ReadLine();

                            Console.WriteLine("Unesite novu cenu: ");
                            pronadjeniNamestaj.JedinicnaCena = int.Parse(Console.ReadLine());

                            Console.WriteLine("Unesite novu sifru: ");
                            pronadjeniNamestaj.Sifra = Console.ReadLine();

                            Console.WriteLine("Unesite novu kolicinu: ");
                            pronadjeniNamestaj.KolicinaUMagacinu = int.Parse(Console.ReadLine());

                            tipNamestaja();

                            Projekat.Instance.Namestaj = namestajZaMenjanjeLista;

                            IspisiMeniNamestaja();
                            break;

                        default:
                            break;
                    }
                }
            }

        }

        private static void ObrisiNamestaj()
        {
            var pripremiNamestajZaBrisanje = Projekat.Instance.Namestaj;
            Console.WriteLine("=== OBRISI NAMESTAJ ===");
            IzlistajNamestaj(pripremiNamestajZaBrisanje);

            int izabraniNamestajaZaBrisanje = 0;
            do
            {
                Console.WriteLine("\n" + "Izaberite id namestaja koji zelite da obrisete: ");
                izabraniNamestajaZaBrisanje = int.Parse(Console.ReadLine());
            } while (izabraniNamestajaZaBrisanje < 0 || izabraniNamestajaZaBrisanje > pripremiNamestajZaBrisanje.Count);



            foreach (var tip in pripremiNamestajZaBrisanje)
            {
                if (tip.Id == izabraniNamestajaZaBrisanje)
                {
                    tip.KolicinaUMagacinu = 0;
                    tip.Obrisan = true;

                    Projekat.Instance.Namestaj = pripremiNamestajZaBrisanje;

                }

            }
            IspisiMeniNamestaja();



            
        }

        private static void PretraziNamestaj()
        {
            List<Namestaj> listaNamestaj = new List<Namestaj>();
            var namestajiZaPretragu = Projekat.Instance.Namestaj;
            var tipNamestajaZaPretragu = Projekat.Instance.TipNamestaj;
            Console.WriteLine("=== PRETRAZI NAMESTAJ ===");

            int tipPretrage = 0;
            do
            {
                Console.WriteLine("Pretrazi po nazivu 1)");
                Console.WriteLine("Pretrazi po sifri 2)");
                Console.WriteLine("Pretrazi po tipu namestaja 3)");
                tipPretrage = int.Parse(Console.ReadLine());

            } while (tipPretrage <0 || tipPretrage >3);

            switch(tipPretrage){

                case 1:
                    Console.WriteLine("Unesite naziv za pretragu: ");
                    string naziv = Console.ReadLine();

                    foreach( var namestaj in namestajiZaPretragu)
                    {
                        if (naziv.ToUpper().Equals(namestaj.Naziv.ToUpper()))
                        {
                            listaNamestaj.Add(namestaj);
                            
                        }
                    }
                    IzlistajNamestaj(listaNamestaj);
                    break;
                case 2:
                    Console.WriteLine("Unesite sifru za pretragu: ");
                    string sifra = Console.ReadLine();

                    foreach (var namestaj in namestajiZaPretragu)
                    {
                        if (sifra.ToUpper().Equals(namestaj.Sifra.ToUpper()))
                        {
                            
                            listaNamestaj.Add(namestaj);
                            
                        }
                    }
                    IzlistajNamestaj(listaNamestaj);
                    break;

                case 3:
                    Console.WriteLine("Unesite tip namestaja za pretragu: ");
                    string tipNamestajaUnesen = Console.ReadLine();
                    int tipNamestajaId = 0;
                    foreach (var tipNamestaja in tipNamestajaZaPretragu)
                    {
                        if (tipNamestajaUnesen.ToUpper().Equals(tipNamestaja.Naziv.ToUpper())) 
                        {
                            tipNamestajaId = tipNamestaja.Id;
                            foreach (var namestaj in namestajiZaPretragu)
                            {
                                if (tipNamestajaId == namestaj.TipNamestajaId)
                                {
                                    listaNamestaj.Add(namestaj);
                                    
                                }
                            }
                            {

                            }
                        }
                    }
                    IzlistajNamestaj(listaNamestaj);
                    break;
            }
            if (listaNamestaj.Count == 0)
            {
                Console.WriteLine("Nismo pronasli namestaj koji odgovara unesenim parametrima"+"\n");
            }

            IspisiMeniNamestaja();

            Projekat.Instance.Namestaj = namestajiZaPretragu;

        }

        private static void SortirajNamestaj()
        {
            var namestaj = Projekat.Instance.Namestaj;
            Console.WriteLine("=== SORTIRAJ NAMESTAJ ===");
            int izbor = 0;

            do
            {
                Console.WriteLine("Sortiraj po idu 1)");
                Console.WriteLine("Sortiraj po nazivu 2)");
                Console.WriteLine("Sortiraj po sifri 3)");
                Console.WriteLine("Sortiraj po ceni 4)");
                Console.WriteLine("Sortiraj po kolicni 5)");
                Console.WriteLine("Sortiraj po tipu namestaja 6)");
                Console.WriteLine("Sortiraj po akciji 7)");

                izbor = int.Parse(Console.ReadLine());

            } while ( izbor < 1 || izbor > 7 );

            List<Namestaj> sortiranaLista = new List<Namestaj>();
            switch (izbor)
            {
                case 1:
                    sortiranaLista = namestaj.OrderBy(o => o.Id).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 2:
                    sortiranaLista = namestaj.OrderBy(o => o.Naziv).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 3:
                    sortiranaLista = namestaj.OrderBy(o => o.Sifra).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 4:
                    sortiranaLista = namestaj.OrderBy(o => o.JedinicnaCena).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 5:
                    sortiranaLista = namestaj.OrderBy(o => o.KolicinaUMagacinu).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 6:
                    sortiranaLista = namestaj.OrderBy(o => o.TipNamestajaId).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
                case 7:
                    sortiranaLista = namestaj.OrderBy(o => o.AkcijaId).ToList();
                    IzlistajNamestaj(sortiranaLista);
                    break;
            }


            IspisiMeniNamestaja();
        }
    }
}

