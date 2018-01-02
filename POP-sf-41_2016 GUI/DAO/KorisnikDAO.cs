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
    class KorisnikDAO
    {
        public enum TipPretrage
        {
            Ime,
            Prezime,
            KorisnickoIme
        }

        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"Select * From Korisnik";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "korisnik");

                foreach (DataRow row in ds.Tables["korisnik"].Rows)
                {
                    Korisnik k = new Korisnik();
                    k.Id = int.Parse(row["IdK"].ToString());
                    k.Ime = row["Ime"].ToString();
                    k.Prezime = row["Prezime"].ToString();
                    k.KorisnickoIme = row["KorisnickoIme"].ToString();
                    k.Lozinka = row["Lozinka"].ToString();
                    k.TipKorisnika = Enums.GetValue(row["TipKorisnika"].ToString());
                    k.Id = int.Parse(row["IdK"].ToString());
                    k.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    Projekat.Instance.Korisnici.Add(k);

                }
            }
        }

        public static void Update(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"UPDATE Korisnik SET Ime=@Ime, Prezime=@Prezime, KorisnickoIme=@KorisnickoIme, Lozinka=@Lozinka, TipKorisnika=@TipKorisnika " +
                                   "WHERE IdK=@IdK";
                cmd.Parameters.Add(new SqlParameter("@Ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@Prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@KorisnickoIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@Lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@TipKorisnika", k.TipKorisnika.ToString()));
                cmd.Parameters.Add(new SqlParameter("@IdK", k.Id));

                var uu = cmd.ExecuteNonQuery();
            }

            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (k.Id == korisnik.Id)
                {
                    korisnik.Ime = k.Ime;
                    korisnik.Prezime = k.Prezime;
                    korisnik.KorisnickoIme = k.KorisnickoIme;
                    korisnik.Lozinka = k.Lozinka;
                    korisnik.TipKorisnika = k.TipKorisnika;
                    korisnik.Obrisan = k.Obrisan;
                }
            }
        }
        public static void Create(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"INSERT INTO Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, TipKorisnika, Obrisan) " +
                                   "VALUES (@Ime, @Prezime, @KorisnickoIme, @Lozinka, @TipKorisnika, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@Ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@Prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@KorisnickoIme", k.KorisnickoIme));
                cmd.Parameters.Add(new SqlParameter("@Lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@TipKorisnika", k.TipKorisnika.ToString()));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", k.Obrisan));

                //cmd.ExecuteNonQuery();
                k.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            Projekat.Instance.Korisnici.Add(k);
        }

        public static ObservableCollection<Korisnik> Find(String parametarZaPretragu, TipPretrage tipPretrage)
        {
            var listaPretraga = new ObservableCollection<Korisnik>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                switch (tipPretrage)
                {
                    case TipPretrage.Ime:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Korisnik " +
                                           "WHERE Obrisan = 0 and Ime like @Parametar;";
                        break;
                    case TipPretrage.Prezime:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Korisnik " +
                                           "WHERE Obrisan = 0 and Prezime like @Parametar;";
                        break;
                    case TipPretrage.KorisnickoIme:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Korisnik " +
                                           "WHERE Obrisan = 0 and KorisnickoIme like @Parametar;";
                        
                        break;
                }

                cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametarZaPretragu + "%"));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet();
                sqlDA.Fill(ds, "korisnik");

                foreach (DataRow row in ds.Tables["korisnik"].Rows)
                {
                    Korisnik k = new Korisnik();
                    k.Id = int.Parse(row["IdK"].ToString());
                    k.Ime = row["Ime"].ToString();
                    k.Prezime = row["Prezime"].ToString();
                    k.KorisnickoIme = row["KorisnickoIme"].ToString();
                    k.Lozinka = row["Lozinka"].ToString();
                    k.TipKorisnika = Enums.GetValue(row["TipKorisnika"].ToString());
                    k.Id = int.Parse(row["IdK"].ToString());
                    k.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    listaPretraga.Add(k);

                }
            }
            return listaPretraga;
        }

        public static void Delete(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();


                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Korisnik SET Obrisan = 1 WHERE IdK=@IdK";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", k.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdK", k.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var korisnik in Projekat.Instance.Korisnici)
                {
                    if (k.Id == korisnik.Id)
                    {
                        korisnik.Obrisan = true;
                    }
                }
            }
        }
    }
}
