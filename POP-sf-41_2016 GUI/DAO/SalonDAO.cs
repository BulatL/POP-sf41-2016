using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_sf_41_2016_GUI.DAO
{
    class SalonDAO
    {

        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT IdS, Naziv, Adresa, Telefon, Sajt, Email, PIB, MaticniBroj, BrojZiroRacuna, Obrisan " +
                                   "FROM Salon " +
                                   "WHERE Obrisan = 0;";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(ds, "Salon");

                foreach (DataRow row in ds.Tables["Salon"].Rows)
                {
                    Salon salon = new Salon();
                    salon.Id = int.Parse(row["IdS"].ToString());
                    salon.Naziv = row["Naziv"].ToString();
                    salon.Adresa = row["Adresa"].ToString();
                    salon.Telefon = row["Telefon"].ToString();
                    salon.AdresaInternetSajta = row["Sajt"].ToString();
                    salon.Email = row["Email"].ToString();
                    salon.PIB = int.Parse(row["PIB"].ToString());
                    salon.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                    salon.BrojZiroRacuna = int.Parse(row["BrojZiroRacuna"].ToString());
                    salon.Obrisan = Boolean.Parse(row["Obrisan"].ToString());


                    Projekat.Instance.Salon.Add(salon);

                }
            }
        }

        public static void Update(Salon salon)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"UPDATE Salon SET Naziv=@Naziv, Adresa=@Adresa, Telefon=@Telefon, Sajt=@Sajt, Email=@Email, " +
                                   "PIB=@PIB, MaticniBroj=@MaticniBroj, BrojZiroRacuna=@BrojZiroRacuna " +
                                   "WHERE IdS=@IdS;";
                cmd.Parameters.Add(new SqlParameter("@Naziv", salon.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Adresa", salon.Adresa));
                cmd.Parameters.Add(new SqlParameter("@Telefon", salon.Telefon));
                cmd.Parameters.Add(new SqlParameter("@Sajt", salon.AdresaInternetSajta));
                cmd.Parameters.Add(new SqlParameter("@Email", salon.Email));
                cmd.Parameters.Add(new SqlParameter("@PIB", salon.PIB));
                cmd.Parameters.Add(new SqlParameter("@MaticniBroj", salon.MaticniBroj));
                cmd.Parameters.Add(new SqlParameter("@BrojZiroRacuna", salon.BrojZiroRacuna));
                cmd.Parameters.Add(new SqlParameter("@IdS", salon.Id));

                var uu = cmd.ExecuteNonQuery();

                foreach (var sa in Projekat.Instance.Salon)
                {
                    if (salon.Id == sa.Id)
                    {
                        sa.Naziv = salon.Naziv;
                        sa.Adresa = salon.Adresa;
                        sa.Telefon = salon.Telefon;
                        sa.AdresaInternetSajta = salon.AdresaInternetSajta;
                        sa.Email = salon.Email;
                        sa.PIB = salon.PIB;
                        sa.MaticniBroj = salon.MaticniBroj;
                        sa.BrojZiroRacuna = salon.BrojZiroRacuna;
                    }
                }
            }
        }

        public static void Create(Salon salon)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"INSERT INTO Salon (Naziv, Adresa, Telefon, Sajt, Email, PIB, MaticniBroj, BrojZiroRacuna, Obrisan) " +
                                   "VALUES (@Naziv, @Adresa, @Telefon, @Sajt, @Email, @PIB, @MaticniBroj, @BrojZiroRacuna, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@Naziv", salon.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Adresa", salon.Adresa));
                cmd.Parameters.Add(new SqlParameter("@Telefon", salon.Telefon));
                cmd.Parameters.Add(new SqlParameter("@Sajt", salon.AdresaInternetSajta));
                cmd.Parameters.Add(new SqlParameter("@Email", salon.Email));
                cmd.Parameters.Add(new SqlParameter("@PIB", salon.PIB));
                cmd.Parameters.Add(new SqlParameter("@MaticniBroj", salon.MaticniBroj));
                cmd.Parameters.Add(new SqlParameter("@BrojZiroRacuna", salon.BrojZiroRacuna));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", salon.Obrisan));

                //cmd.ExecuteNonQuery();
                salon.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            Projekat.Instance.Salon.Add(salon);
        }
        public static void Delete(Salon salon)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();


                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Salon SET Obrisan = 1 WHERE IdS=@IdS";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", salon.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdS", salon.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var sa in Projekat.Instance.Salon)
                {
                    if (sa.Id == salon.Id)
                    {
                        salon.Obrisan = true;
                    }
                }
            }
        }
    }
}
