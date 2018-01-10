using POP_sf41_2016.model;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace POP_sf_41_2016_GUI.DAO
{
    class DodatnaUslugaDAO
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
                                       "FROM DodatnaUsluga " +
                                       "WHERE Obrisan = 0;";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet ds = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(ds, "DodatnaUsluga");

                    foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                    {
                        DodatnaUsluga du = new DodatnaUsluga();
                        du.Id = int.Parse(row["IdDU"].ToString());
                        du.Naziv = row["Naziv"].ToString();
                        du.Cena = Double.Parse(row["Cena"].ToString());
                        du.Obrisan = Boolean.Parse(row["Obrisan"].ToString());


                        Projekat.Instance.DodatneUsluge.Add(du);

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

        public static void Update(DodatnaUsluga du)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = @"UPDATE DodatnaUsluga SET Naziv=@Naziv, Cena=@Cena " +
                                       "WHERE IdDU=@IdDU;";
                    cmd.Parameters.Add(new SqlParameter("@Naziv", du.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@Cena", du.Cena));
                    cmd.Parameters.Add(new SqlParameter("@IdDU", du.Id));

                    var uu = cmd.ExecuteNonQuery();

                    foreach (var dodatnaUsluga in Projekat.Instance.DodatneUsluge)
                    {
                        if (du.Id == dodatnaUsluga.Id)
                        {
                            dodatnaUsluga.Naziv = du.Naziv;
                            dodatnaUsluga.Cena = du.Cena;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Create(DodatnaUsluga du)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();


                    cmd.CommandText = @"INSERT INTO DodatnaUsluga (Naziv, Cena, Obrisan) " +
                                       "VALUES (@Naziv, @Cena, @Obrisan);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Add(new SqlParameter("@Naziv", du.Naziv));
                    cmd.Parameters.Add(new SqlParameter("@Cena", du.Cena));
                    cmd.Parameters.Add(new SqlParameter("@Obrisan", du.Obrisan));

                    du.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
                Projekat.Instance.DodatneUsluge.Add(du);
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public static void Delete(DodatnaUsluga du)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();


                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"UPDATE DodatnaUsluga SET Obrisan = 1 WHERE IdDU=@IdDU";

                    cmd.Parameters.Add(new SqlParameter("@Obrisan", du.Obrisan));
                    cmd.Parameters.Add(new SqlParameter("@IdDU", du.Id));

                    var i = cmd.ExecuteNonQuery();

                    foreach (var dodatnaUsluga in Projekat.Instance.DodatneUsluge)
                    {
                        if (du.Id == dodatnaUsluga.Id)
                        {
                            dodatnaUsluga.Obrisan = true;
                        }
                    }
                }
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
                sortBy = @"ORDER BY IdDU;";
            }
            else if (sort == 1)
            {
                sortBy = @"ORDER BY Naziv;";
            }
            else if (sort == 2)
            {
                sortBy = @"ORDER BY Cena;";
            }
            return sortBy;
        }

        public static ObservableCollection<DodatnaUsluga> FindSort(String parametarZaPretragu, int sort)
        {
            try
            {
                var listaPretraga = new ObservableCollection<DodatnaUsluga>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = @"SELECT * " +
                                        "FROM DodatnaUsluga " +
                                        "WHERE Naziv like @Parametar and Obrisan = 0 ";

                    cmd.CommandText += SortBy(sort);
                    cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametarZaPretragu + "%"));

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet ds = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(ds, "DodatnaUsluga");

                    foreach (DataRow row in ds.Tables["DodatnaUsluga"].Rows)
                    {
                        DodatnaUsluga du = new DodatnaUsluga();
                        du.Id = int.Parse(row["IdDU"].ToString());
                        du.Naziv = row["Naziv"].ToString();
                        du.Cena = Double.Parse(row["Cena"].ToString());
                        du.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                        listaPretraga.Add(du);

                    }
                }
                return listaPretraga;
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }
    }
}
