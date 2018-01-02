﻿CREATE TABLE TipNamestaj(
	IdTN int primary key  identity(1, 1),
	Naziv varchar (80) not null,
	Obrisan bit 
);

CREATE TABLE Namestaj(
	IdN int primary key  identity(1, 1),
	TipNamestajaId int,
	Naziv varchar (100) not null,
	Sifra varchar (20) not null,
	Cena money not null, 
	Kolicina smallint,
	Obrisan bit,

	FOREIGN KEY (TipNamestajaId) references TipNamestaj(IdTN)
);

CREATE TABLE DodatnaUsluga(
	IdDU int primary key  identity(1, 1),
	Naziv varchar(60) not null,
	Cena decimal(9, 2),
	Obrisan bit,
);

CREATE TABLE Akcija(
	IdA int primary key  identity(1, 1),
	Naziv varchar(60) not null,
	DatumPocetka date not null,
	DatumZavrsetka date not null,
	Obrisan bit,
);

CREATE TABLE NaAkciji(
	IdNA int primary key  identity(1, 1),
	AkcijaId int not null,
	NamestajId int not null,
	Popust smallint not null,
	Obrisan bit,

	FOREIGN KEY (AkcijaId) references Akcija(IdA),
	FOREIGN KEY (NamestajId) references Namestaj(IdN)
);

CREATE TABLE Korisnik(
	IdK int primary key  identity(1, 1),
	Ime varchar(30) not null,
	Prezime varchar(60) not null,
	KorisnickoIme varchar(20) not null,
	Lozinka varchar(20) not null,
	TipKorisnika varchar(13) not null,
	Obrisan bit,
);

CREATE TABLE Salon(
	IdS int primary key  identity(1, 1),
	Naziv varchar(40) not null,
	Adresa varchar(60) not null,
	Telefon varchar(30) not null,
	Email varchar(30) not null,
	Sajt varchar(60) not null,
	PIB smallint not null,
	MaticniBroj smallint not null,
	BrojZiroRacuna smallint not null,
	Obrisan bit,
);

CREATE TABLE Prodaja(
	IdP int primary key  identity(1, 1),
	DatumProdaje date not null,
	BrojRacuna varchar(30) not null,
	Kupac varchar(60) not null,
	Prodavac varchar(30) not null,
	UkupanIznos money not null,
	Obrisan bit,
);

CREATE TABLE ProdajaNamestaj(
	IdPN int primary key  identity(1, 1),
	ProdajaId int not null,
	NamestajId int not null,
	Kolicina smallint not null,
	Cena money not null,
	Obrisan bit,

	FOREIGN KEY (ProdajaId) references Prodaja(IdP),
	FOREIGN KEY (NamestajId) references Namestaj(IdN)
);

CREATE TABLE ProdajaDodatnaUsluga(
	IdPDU int primary key  identity(1, 1),
	ProdajaId int not null,
	DodatnaUslugaId int not null,
	Cena money not null,
	Obrisan bit,

	FOREIGN KEY (ProdajaId) references Prodaja(IdP),
	FOREIGN KEY (DodatnaUslugaId) references DodatnaUsluga(IdDU)
);