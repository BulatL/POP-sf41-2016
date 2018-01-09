using POP_sf_41_2016_GUI.model;
using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf_41_2016_GUI.DAO
{
    class ProdajaDAO
    {
        public enum TipPretrage
        {
            Kupac,
            Prodavac,
            BrRacuna,
            ProdatiNamestaj,
            DatumProdaje
        }
        public static int GetLastId()
        {
            int lastId = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT TOP(1) IdP " +
                                   "FROM Prodaja " +
                                   "ORDER BY IdP DESC; ";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Prodaja");

                foreach (DataRow row in dsA.Tables["Prodaja"].Rows)
                {
                    lastId = int.Parse(row["IdP"].ToString());
                }
            }
            return lastId;
        }

        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * " +
                                   "FROM Prodaja " +
                                   "WHERE Obrisan = 0; ";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Prodaja");


                foreach (DataRow row in dsA.Tables["Prodaja"].Rows)
                {
                    Prodaja prodaja = new Prodaja();
                    prodaja.Id = int.Parse(row["IdP"].ToString());
                    prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    prodaja.BrRacuna = row["BrojRacuna"].ToString();
                    prodaja.Kupac = row["Kupac"].ToString();
                    prodaja.ProdavacId = int.Parse(row["ProdavacId"].ToString());
                    prodaja.UkupanIznos = double.Parse(row["UkupanIznos"].ToString());
                    prodaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    Projekat.Instance.Prodaja.Add(prodaja);
                }
            }
        }

        public static void Update(Prodaja prodaja)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"UPDATE Prodaja SET UkupanIznos=@UkupanIznos, Obrisan=Obrisan " +
                                    "WHERE IdP=@IdP;";

                cmd.Parameters.Add(new SqlParameter("@UkupanIznos", prodaja.UkupanIznos));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", prodaja.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdP", prodaja.Id));

                var uu = cmd.ExecuteNonQuery();

                foreach (var p in Projekat.Instance.Prodaja)
                {
                    if (prodaja.Id == p.Id)
                    {
                        p.UkupanIznos = prodaja.UkupanIznos;
                        p.Obrisan = prodaja.Obrisan;
                    }
                }
            }
        }

        public static void Create(Prodaja prodaja)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"INSERT INTO Prodaja (DatumProdaje, BrojRacuna, Kupac, ProdavacId, UkupanIznos, Obrisan) " +
                                   "VALUES (@DatumProdaje, @BrojRacuna, @Kupac, @ProdavacId, @UkupanIznos, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@DatumProdaje", prodaja.DatumProdaje));
                cmd.Parameters.Add(new SqlParameter("@BrojRacuna", prodaja.BrRacuna));
                cmd.Parameters.Add(new SqlParameter("@Kupac", prodaja.Kupac));
                cmd.Parameters.Add(new SqlParameter("@ProdavacId", prodaja.ProdavacId));
                cmd.Parameters.Add(new SqlParameter("@UkupanIznos", prodaja.UkupanIznos));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", prodaja.Obrisan));

                //cmd.ExecuteNonQuery();
                prodaja.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            Projekat.Instance.Prodaja.Add(prodaja);
        }

        public static ObservableCollection<Prodaja> Find(String parametarZaPretragu, TipPretrage tipPretrage, DateTime? dateTime)
        {
            var listaPretraga = new ObservableCollection<Prodaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                switch (tipPretrage)
                {
                    case TipPretrage.Kupac:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja " +
                                           "WHERE Obrisan = 0 and kupac like @ParametarZaPretragu;";
                        break;
                    case TipPretrage.BrRacuna:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja " +
                                           "WHERE Obrisan = 0 and BrRacuna like @ParametarZaPretragu;";
                        break;
                    case TipPretrage.ProdatiNamestaj:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p inner join ProdajaNamestaj pn on p.IdP=pn.ProdajaId inner join Namestaj n on pn.NamestajId=n.IdN " +
                                           "WHERE Obrisan = 0 and n.Naziv like @ParametarZaPretragu;";                        
                        break;
                    case TipPretrage.DatumProdaje:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja " +
                                           "WHERE Obrisan = 0 and DatumProdaje = @DatumProdaje;";
                        break;
                }

                cmd.Parameters.Add(new SqlParameter("@ParametarZaPretragu", "%" + parametarZaPretragu + "%"));
                cmd.Parameters.Add(new SqlParameter("@DatumProdaje", dateTime));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Prodaja");


                foreach (DataRow row in dsA.Tables["Prodaja"].Rows)
                {
                    Prodaja prodaja = new Prodaja();
                    prodaja.Id = int.Parse(row["IdP"].ToString());
                    prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    prodaja.BrRacuna = row["BrojRacuna"].ToString();
                    prodaja.Kupac = row["Kupac"].ToString();
                    prodaja.ProdavacId = int.Parse(row["ProdavacId"].ToString());
                    prodaja.UkupanIznos = double.Parse(row["UkupanIznos"].ToString());
                    prodaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    listaPretraga.Add(prodaja);
                }
            }
            return listaPretraga;
        }

        public static void Delete(Prodaja prodaja)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();


                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Prodaja SET Obrisan = 1 WHERE IdP=@IdP";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", prodaja.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdP", prodaja.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var p in Projekat.Instance.Prodaja)
                {
                    if (p.Id == prodaja.Id)
                    {
                        p.Obrisan = true;
                    }
                }
                ProdajaDodatnaUslugaDAO.Delete(null, ProdajaDodatnaUslugaDAO.TipBrisanja.ProdajaId, prodaja.Id);
                ProdajaNamestajDAO.Delete(null, ProdajaNamestajDAO.TipBrisanja.ProdajaId, prodaja.Id);
            }
        }
        public static string SortBy(int sort)
        {
            String sortBy = "";
            if (sort == 0)
            {
                sortBy = @"ORDER BY p.IdP; ";
            }
            else if (sort == 1)
            {
                sortBy = @"ORDER BY p.Kupac; ";
            }
            else if (sort == 2)
            {
                sortBy = @"ORDER BY p.BrRacuna; ";
            }
            else if (sort == 3)
            {
                sortBy = @"ORDER BY n.Naziv; ";
            }
            else if (sort == 4)
            {
                sortBy = @"ORDER BY p.UkupanIznos; ";
            }
            return sortBy;
        }

        public static ObservableCollection<Prodaja> FindSort(String parametarZaPretragu, TipPretrage tipPretrage, DateTime? date,int sort)
        {
            var listaPretraga = new ObservableCollection<Prodaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                switch (tipPretrage)
                {
                    case TipPretrage.Kupac:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p " +
                                           "WHERE p.Kupac like @Parametar and p.Obrisan = 0 ";
                        cmd.CommandText += SortBy(sort);
                        break;
                    case TipPretrage.Prodavac:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p inner join Korisnik k on p.ProdavacId = k.IdK " +
                                           "WHERE k.KorisnickoIme like @Parametar and p.Obrisan = 0 ";
                        cmd.CommandText += SortBy(sort);
                        break;
                    case TipPretrage.BrRacuna:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p  " +
                                           "WHERE p.BrRacuna like @Parametar and p.Obrisan = 0 ";
                        cmd.CommandText += SortBy(sort);
                        break;
                    case TipPretrage.ProdatiNamestaj:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p INNER JOIN ProdajaNamestaj pn ON p.IdP = pn.ProdajaId INNER JOIN Namestaj n ON pn.NamestajId = n.IdN  " +
                                           "WHERE n.Naziv like @Parametar and p.Obrisan = 0  and pn.Obrisan = 0 and n.Obrisan = 0 ";
                        cmd.CommandText += SortBy(sort);
                        break;
                    case TipPretrage.DatumProdaje:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Prodaja p  " +
                                           "WHERE p.DatumProdaje like @DatumProdaje and p.Obrisan = 0 ";
                        cmd.CommandText += SortBy(sort);
                        break;
                }
                cmd.Parameters.Add(new SqlParameter("@DatumProdaje", date));
                cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametarZaPretragu + "%"));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Prodaja");


                foreach (DataRow row in dsA.Tables["Prodaja"].Rows)
                {
                    Prodaja prodaja = new Prodaja();
                    prodaja.Id = int.Parse(row["IdP"].ToString());
                    prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    prodaja.BrRacuna = row["BrojRacuna"].ToString();
                    prodaja.Kupac = row["Kupac"].ToString();
                    prodaja.ProdavacId = int.Parse(row["ProdavacId"].ToString());
                    prodaja.UkupanIznos = double.Parse(row["UkupanIznos"].ToString());
                    prodaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    listaPretraga.Add(prodaja);
                }
            }
            return listaPretraga;
        }
    }
}
