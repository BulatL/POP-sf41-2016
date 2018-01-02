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
    class NamestajDAO
    {
        public enum TipPretrage
        {
            Naziv,
            Sifra,
            TipNamestaja,
        }
        public enum Sort
        {
            IdN,
            Naziv,
            Sifra,
            Cena,
            Kolicina,
            TipNamestaja
        }

        public enum TypeSort
        {
            asc,
            desc
        }
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT IdN, Naziv, Sifra, Cena, Kolicina , TipNamestajaId , Obrisan " +
                                    "FROM Namestaj " +
                                    "WHERE Obrisan = 0;";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(ds, "Namestaj");

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    Namestaj n = new Namestaj();
                    n.Id = int.Parse(row["IdN"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.Sifra = row["Sifra"].ToString();
                    n.JedinicnaCena = Double.Parse(row["Cena"].ToString());
                    n.KolicinaUMagacinu = int.Parse(row["Kolicina"].ToString());
                    n.TipNamestajaId = int.Parse(row["TipNamestajaId"].ToString());
                    n.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    Projekat.Instance.Namestaji.Add(n);
                }
            }
        }

        public static void Update(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"UPDATE Namestaj SET Naziv=@Naziv, Sifra=@Sifra, Cena=@Cena, Kolicina=@Kolicina, TipNamestajaId=@TipNamestajaId " +
                                   "WHERE IdN=@IdN;";
                cmd.Parameters.Add(new SqlParameter("@Naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Sifra", n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@Cena", n.JedinicnaCena));
                cmd.Parameters.Add(new SqlParameter("@Kolicina", n.KolicinaUMagacinu));
                cmd.Parameters.Add(new SqlParameter("@TipNamestajaId", n.TipNamestajaId));
                cmd.Parameters.Add(new SqlParameter("@IdN", n.Id));

                var uu = cmd.ExecuteNonQuery();

            }
            Namestaj.Update(n);
        }

        public static void Create(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"INSERT INTO Namestaj (Naziv, Sifra, Cena, Kolicina, Obrisan, TipNamestajaId) " +
                                   "VALUES (@Naziv, @Sifra, @Cena, @Kolicina, @Obrisan, @TipNamestajaId);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@Naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Sifra", n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@Cena", n.JedinicnaCena));
                cmd.Parameters.Add(new SqlParameter("@Kolicina", n.KolicinaUMagacinu));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", n.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@TipNamestajaId", n.TipNamestajaId));

                //cmd.ExecuteNonQuery();
                n.Id = int.Parse(cmd.ExecuteScalar().ToString());
                
            }
            Projekat.Instance.Namestaji.Add(n);
        }
        public static void Delete(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Namestaj SET Obrisan = 1 WHERE IdN=@IdN";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", n.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdN", n.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var Namestaj in Projekat.Instance.Namestaji)
                {
                    if (n.Id == Namestaj.Id)
                    {
                        Namestaj.Obrisan = true;
                    }
                }
            }
        }

        public static ObservableCollection<Namestaj> FindSort(String parametarZaPretragu, TipPretrage tipPretrage, Sort sort = Sort.IdN)
        {
            var listaPretraga = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                switch (tipPretrage)
                {
                    case TipPretrage.Naziv:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Namestaj n inner join TipNamestaj tn on n.TipNamestajaId = tn.IdTN " +
                                           "WHERE n.Naziv like @Parametar and n.Obrisan =0 "; /*+
                                           "ORDER BY CASE @sort WHEN 'IdN' THEN n.IdN " +
                                                               "WHEN 'Naziv' THEN n.Naziv " +
                                                               "WHEN 'Cena' THEN n.Cena " +
                                                               "WHEN 'Sifra' THEN n.Sifra " +
                                                               "WHEN 'Kolicina' THEN n.Kolicina " +
                                                               "WHEN 'TipNamestaja' THEN tn.Naziv " +
                                                               "END asc;";*/
                        break;
                    case TipPretrage.Sifra:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Namestaj n inner join TipNamestaj tn on n.TipNamestajaId = tn.IdTN " +
                                           "WHERE n.Sifra like @Parametar and n.Obrisan =0 "; /*+
                                           "ORDER BY CASE @sort WHEN 'IdN' THEN n.IdN " +
                                                               "WHEN 'Naziv' THEN n.Naziv " +
                                                               "WHEN 'Cena' THEN n.Cena " +
                                                               "WHEN 'Sifra' THEN n.Sifra " +
                                                               "WHEN 'Kolicina' THEN n.Kolicina " +
                                                               "WHEN 'TipNamestaja' THEN tn.Naziv " +
                                                               "END asc;";*/
                        break;
                    case TipPretrage.TipNamestaja:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Namestaj n inner join TipNamestaj tn on n.TipNamestajaId = tn.IdTN  " +
                                           "WHERE tn.Naziv like @Parametar and n.Obrisan = 0 "; /*+
                                           "ORDER BY CASE @sort WHEN 'IdN' THEN n.IdN " +
                                                               "WHEN 'Naziv' THEN n.Naziv " +
                                                               "WHEN 'Cena' THEN n.Cena " +
                                                               "WHEN 'Sifra' THEN n.Sifra " +
                                                               "WHEN 'Kolicina' THEN n.Kolicina " +
                                                               "WHEN 'TipNamestaja' THEN tn.Naziv " +
                                                               "END asc;";*/
                        break;
                }
                cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametarZaPretragu + "%"));
                //cmd.Parameters.Add(new SqlParameter("@sort", sort.ToString()));
                //cmd.Parameters.Add(new SqlParameter("@typeSort", typeSort));

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet ds = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(ds, "Namestaj");

                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    Namestaj n = new Namestaj();
                    n.Id = int.Parse(row["IdN"].ToString());
                    n.Naziv = row["Naziv"].ToString();
                    n.Sifra = row["Sifra"].ToString();
                    n.JedinicnaCena = Double.Parse(row["Cena"].ToString());
                    n.KolicinaUMagacinu = int.Parse(row["Kolicina"].ToString());
                    n.TipNamestajaId = int.Parse(row["TipNamestajaId"].ToString());
                    n.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    listaPretraga.Add(n);
                }
            }
            return listaPretraga;
        }
    }
}
