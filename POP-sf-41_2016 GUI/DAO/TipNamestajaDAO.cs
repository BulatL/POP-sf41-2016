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
using System.Windows;

namespace POP_sf_41_2016_GUI.DAO
{
    class TipNamestajaDAO
    {
        public static void Load()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * " +
                                        "FROM TipNamestaj " +
                                        "WHERE Obrisan = 0;";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet ds = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(ds, "TipNamestaj");

                    foreach (DataRow row in ds.Tables["TipNamestaj"].Rows)
                    {
                        TipNamestaja tn = new TipNamestaja();
                        tn.Id = int.Parse(row["IdTN"].ToString());
                        tn.Naziv = row["Naziv"].ToString();
                        tn.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                        Projekat.Instance.TipoviNamestaja.Add(tn);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Update(TipNamestaja tn)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = @"UPDATE TipNamestaj SET Naziv=@Naziv " +
                                       "WHERE IdTN=@IdTN;";
                    cmd.Parameters.Add(new SqlParameter("@Naziv", tn.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@IdTN", tn.Id));

                    var uu = cmd.ExecuteNonQuery();
                }
                foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
                {
                    if (tn.Id == tipNamestaja.Id)
                    {
                        tipNamestaja.Naziv = tn.Naziv;
                    }
                }

                foreach (var namestaj in Projekat.Instance.Namestaji)
                {
                    if (namestaj.TipNamestajaId == tn.Id)
                    {
                        namestaj.TipNamestaja.Naziv = tn.Naziv;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Create(TipNamestaja tn)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = @"INSERT INTO TipNamestaj (Naziv, Obrisan) " +
                                       "VALUES (@Naziv, @Obrisan);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Add(new SqlParameter("@Naziv", tn.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@Obrisan", tn.Obrisan));

                    tn.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
                Projekat.Instance.TipoviNamestaja.Add(tn);
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static string SortBy(int sort)
        {
            String sortBy = "";
            if (sort == 0)
            {
                sortBy = @"ORDER BY IdTN;";
            }
            else if (sort == 1)
            {
                sortBy = @"ORDER BY Naziv;";
            }
            return sortBy;
        }

        public static ObservableCollection<TipNamestaja> FindSort (String parametar, int sort)
        {
            try
            {
                var lista = new ObservableCollection<TipNamestaja>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = @"SELECT * " +
                                       "FROM TipNamestaj " +
                                       "WHERE Naziv like @Parametar and Obrisan = 0 ";

                    cmd.CommandText += SortBy(sort);

                    cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametar + "%"));

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet ds = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(ds, "TipNamestaj");

                    foreach (DataRow row in ds.Tables["TipNamestaj"].Rows)
                    {
                        TipNamestaja tn = new TipNamestaja();
                        tn.Id = int.Parse(row["IdTN"].ToString());
                        tn.Naziv = row["Naziv"].ToString();
                        tn.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                        lista.Add(tn);
                    }

                }
                return lista;
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }

        public static void Delete(TipNamestaja tn)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"UPDATE TipNamestaj SET Obrisan = 1 WHERE IdTN=@IdTN; " +
                                       "UPDATE Namestaj SET TipNamestajaId = 1 where TipNamestajaId=@IdTN;";

                    cmd.Parameters.Add(new SqlParameter("@Obrisan", tn.Obrisan));
                    cmd.Parameters.Add(new SqlParameter("@IdTN", tn.Id));

                    var i = cmd.ExecuteNonQuery();

                    foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
                    {
                        if (tn.Id == tipNamestaja.Id)
                        {
                            tipNamestaja.Obrisan = true;
                            break;
                        }
                    }

                    foreach (var namestaj in Projekat.Instance.Namestaji)
                    {
                        if (namestaj.TipNamestajaId == tn.Id)
                        {
                            namestaj.TipNamestajaId = 1;
                            namestaj.TipNamestaja.Naziv = "Ostalo";
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
