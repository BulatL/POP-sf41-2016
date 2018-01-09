Insert into TipNamestaj (Naziv, Obrisan) values ('Ostalo', 0);
Insert into TipNamestaj (Naziv, Obrisan) values ('Kauc', 0);
Insert into TipNamestaj (Naziv, Obrisan) values ('Regal', 0);
Insert into TipNamestaj (Naziv, Obrisan) values ('Fotelja', 0);
Insert into TipNamestaj (Naziv, Obrisan) values ('Garnitura', 0);

Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Polica', 'P1', 1564.2 ,21 , 0, 1);
Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Super kauc', 'SK41', 512 ,41 , 0, 2);
Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Regal', 'R41', 512 ,43 , 0, 3);
Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Socijalna fotelja', 'SF41', 123 ,46 , 0, 4);
Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Ugaona garnitura', 'UG41', 141 ,41 , 0, 5);
Insert into Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) values ('Socijalni kauc', 'SK51', 36 ,15 , 0, 2);

Insert into DodatnaUsluga (Naziv, Cena, Obrisan) values ('Prevoz', 541, 0);
Insert into DodatnaUsluga (Naziv, Cena, Obrisan) values ('Montiranje', 2541, 0);
Insert into DodatnaUsluga (Naziv, Cena, Obrisan) values ('Presvlacenje', 1342, 0);

Insert into Akcija (DatumPocetka, DatumZavrsetka, Naziv, Obrisan) values ('12.24.2017', '12.26.2017', 'Snizenje 123', 0);
Insert into Akcija (DatumPocetka, DatumZavrsetka, Naziv, Obrisan) values ('12.24.2017', '12.30.2017', 'Novogodisnje Snizenje', 0);

Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (1, 2, 35, 0);
Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (1, 3, 25, 0);
Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (1, 1, 15, 0);
Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (2, 5, 35, 0);
Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (2, 6, 20, 0);
Insert into NaAkciji (AkcijaId, NamestajId, Popust, Obrisan) values (2, 4, 35, 0);

Insert into Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, TipKorisnika, Obrisan) values ('Marko', 'Markovic', 'marko', '123', 'Administrator', 0);
Insert into Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, TipKorisnika, Obrisan) values ('Nikola', 'Nikolic', 'nikola', '123', 'Administrator', 0);
Insert into Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, TipKorisnika, Obrisan) values ('Milos', 'Jovanovic', 'milos', '123', 'Prodavac', 0);

Insert into Salon (Naziv, Adresa, Sajt, Email, BrojZiroRacuna, MaticniBroj, PIB, Telefon, Obrisan) 
values ('Socijalni salon', 'Bulervar oslobodjenja 14', 'www.SocijalniSalon.com' , 'SocijalniSalon@hotmail.com', 1421313123, 5123512, 151235, '022-553-124', 0);

Insert into Prodaja (BrojRacuna, Kupac, ProdavacId, DatumProdaje, UkupanIznos, Obrisan) values ('R412', 'Milos', 3, '12.24.2017', 3286.44, 0);

Insert into ProdajaNamestaj (ProdajaId, NamestajId, Kolicina, Cena, Obrisan) values (1, 5, 4, 564, 0);
Insert into ProdajaNamestaj (ProdajaId, NamestajId, Kolicina, Cena, Obrisan) values (1, 6, 4, 144, 0);

Insert into ProdajaDodatnaUsluga (ProdajaId, DodatnaUslugaId, Cena, Obrisan) values (1, 2, 2514, 0);