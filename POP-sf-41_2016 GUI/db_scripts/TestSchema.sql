CREATE TABLE TipNamestaj(
	IdTN int primary key  identity(1, 1),
	Naziv varchar (60) not null,
	Obrisan bit 
);

CREATE TABLE Namestaj(
	IdN int primary key  identity(1, 1),
	TipNamestajaId int,
	Naziv varchar (60) not null,
	Sifra varchar (20) not null,
	Cena numeric(15 , 2) not null, 
	Kolicina smallint,
	Obrisan bit,

	FOREIGN KEY (TipNamestajaId) references TipNamestaj(IdTN)
);

CREATE TABLE DodatnaUsluga(
	IdDU int primary key  identity(1, 1),
	Naziv varchar(30) not null,
	Cena numeric(15, 2),
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
	Ime varchar(20) not null,
	Prezime varchar(30) not null,
	KorisnickoIme varchar(25) not null,
	Lozinka varchar(25) not null,
	TipKorisnika varchar(13) not null,
	Obrisan bit,
);

CREATE TABLE Salon(
	IdS int primary key  identity(1, 1),
	Naziv varchar(40) not null,
	Adresa varchar(60) not null,
	Telefon varchar(30) not null,
	Email varchar(40) not null,
	Sajt varchar(60) not null,
	PIB int not null,
	MaticniBroj int not null,
	BrojZiroRacuna int not null,
	Obrisan bit,
);

CREATE TABLE Prodaja(
	IdP int primary key  identity(1, 1),
	DatumProdaje date not null,
	BrojRacuna varchar(10) not null,
	Kupac varchar(20) not null,
	ProdavacId int not null,
	UkupanIznos numeric(15 , 2) not null,
	Obrisan bit,

	FOREIGN KEY (ProdavacId) references Korisnik(IdK),
);

CREATE TABLE ProdajaNamestaj(
	IdPN int primary key  identity(1, 1),
	ProdajaId int not null,
	NamestajId int not null,
	Kolicina smallint not null,
	Cena numeric(15 , 2) not null,
	Obrisan bit,

	FOREIGN KEY (ProdajaId) references Prodaja(IdP),
	FOREIGN KEY (NamestajId) references Namestaj(IdN)
);

CREATE TABLE ProdajaDodatnaUsluga(
	IdPDU int primary key  identity(1, 1),
	ProdajaId int not null,
	DodatnaUslugaId int not null,
	Cena numeric(15 , 2) not null,
	Obrisan bit,

	FOREIGN KEY (ProdajaId) references Prodaja(IdP),
	FOREIGN KEY (DodatnaUslugaId) references DodatnaUsluga(IdDU)
);