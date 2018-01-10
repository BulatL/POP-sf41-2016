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
using System.Windows;

namespace POP_sf_41_2016_GUI.DAO
{
    class ProdajaNamestajDAO
    {
        public enum TipBrisanja
        {
            ProdajaId,
            ProdajaNamestaj
        }
        public static void Load()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * " +
                                       "FROM ProdajaNamestaj " +
                                       "WHERE Obrisan = 0; ";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet dsA = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(dsA, "ProdajaNamestaj");

                    foreach (DataRow row in dsA.Tables["ProdajaNamestaj"].Rows)
                    {
                        ProdajaNamestaj prodajaNamestaj = new ProdajaNamestaj();
                        prodajaNamestaj.Id = int.Parse(row["IdPN"].ToString());
                        prodajaNamestaj.NamestajId = int.Parse(row["NamestajId"].ToString());
                        prodajaNamestaj.ProdajaId = int.Parse(row["ProdajaId"].ToString());
                        prodajaNamestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                        prodajaNamestaj.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                        prodajaNamestaj.UkupnaCena = double.Parse(row["Cena"].ToString());
                        Projekat.Instance.ProdajaNamestaj.Add(prodajaNamestaj);
                    }

                    foreach (var prodaja in Projekat.Instance.Prodaja)
                    {
                        if (prodaja.ListaProdajeNamestajaId == null)
                        {
                            prodaja.ListaProdajeNamestajaId = new ObservableCollection<int?>();
                        }
                        foreach (var prodajaNamestaj in Projekat.Instance.ProdajaNamestaj)
                        {
                            if (prodaja.Id == prodajaNamestaj.ProdajaId)
                            {
                                prodaja.ListaProdajeNamestajaId.Add(prodajaNamestaj.Id);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static ObservableCollection<ProdajaNamestaj> LoadByProdajaId(int prodajaId)
        {
            try
            {
                var lista = new ObservableCollection<ProdajaNamestaj>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * " +
                                       "FROM ProdajaNamestaj " +
                                       "WHERE Obrisan = 0 and ProdajaId=@ProdajaId;";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaId));

                    DataSet dsA = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(dsA, "ProdajaNamestaj");

                    foreach (DataRow row in dsA.Tables["ProdajaNamestaj"].Rows)
                    {
                        ProdajaNamestaj prodajaNamestaj = new ProdajaNamestaj();
                        prodajaNamestaj.Id = int.Parse(row["IdPN"].ToString());
                        prodajaNamestaj.NamestajId = int.Parse(row["NamestajId"].ToString());
                        prodajaNamestaj.ProdajaId = int.Parse(row["ProdajaId"].ToString());
                        prodajaNamestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                        prodajaNamestaj.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                        prodajaNamestaj.UkupnaCena = double.Parse(row["Cena"].ToString());
                        lista.Add(prodajaNamestaj);
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom citanja iz baze, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }

        public static void Update(ProdajaNamestaj prodajaNamestaj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();


                    cmd.CommandText = @"UPDATE ProdajaNamestaj SET ProdajaId=@ProdajaId , NamestajId=@NamestajId, Kolicina=@Kolicina, Cena=@Cena, Obrisan=@Obrisan " +
                                        "WHERE IdPN=@IdPN;";

                    cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaNamestaj.ProdajaId));
                    cmd.Parameters.Add(new SqlParameter("@NamestajId", prodajaNamestaj.NamestajId));
                    cmd.Parameters.Add(new SqlParameter("@Kolicina", prodajaNamestaj.Kolicina));
                    cmd.Parameters.Add(new SqlParameter("@Cena", prodajaNamestaj.UkupnaCena));
                    cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaNamestaj.Obrisan));
                    cmd.Parameters.Add(new SqlParameter("@IdPN", prodajaNamestaj.Id));

                    var uu = cmd.ExecuteNonQuery();

                    foreach (var pn in Projekat.Instance.ProdajaNamestaj)
                    {
                        if (prodajaNamestaj.Id == pn.Id)
                        {
                            pn.NamestajId = prodajaNamestaj.NamestajId;
                            pn.Kolicina = prodajaNamestaj.Kolicina;
                            pn.UkupnaCena = prodajaNamestaj.UkupnaCena;
                            pn.Obrisan = prodajaNamestaj.Obrisan;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Create(ProdajaNamestaj prodajaNamestaj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();


                    cmd.CommandText = @"INSERT INTO ProdajaNamestaj (ProdajaId, NamestajId, Kolicina, Cena, Obrisan) " +
                                       "VALUES (@ProdajaId, @NamestajId, @Kolicina, @Cena, @Obrisan);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaNamestaj.ProdajaId));
                    cmd.Parameters.Add(new SqlParameter("@NamestajId", prodajaNamestaj.NamestajId));
                    cmd.Parameters.Add(new SqlParameter("@Kolicina", prodajaNamestaj.Kolicina));
                    cmd.Parameters.Add(new SqlParameter("@Cena", prodajaNamestaj.UkupnaCena));
                    cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaNamestaj.Obrisan));
                    cmd.Parameters.Add(new SqlParameter("@IdPN", prodajaNamestaj.Id));

                    prodajaNamestaj.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
                Projekat.Instance.ProdajaNamestaj.Add(prodajaNamestaj);
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Delete(ProdajaNamestaj prodajaNamestaj, TipBrisanja tipBrisanja, int prodajaId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    switch (tipBrisanja)
                    {
                        case TipBrisanja.ProdajaId:
                            cmd.CommandText = @"UPDATE ProdajaNamestaj SET Obrisan = 1 WHERE ProdajaId=@ProdajaId";

                            cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaNamestaj.Obrisan));
                            cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaId));
                            var i = cmd.ExecuteNonQuery();

                            break;
                        case TipBrisanja.ProdajaNamestaj:
                            cmd.CommandText = @"UPDATE ProdajaNamestaj SET Obrisan = 1 " +
                                               "WHERE IdPN=@IdPN";

                            cmd.Parameters.Add(new SqlParameter("@IdPN", prodajaNamestaj.Id)); ;
                            var ii = cmd.ExecuteNonQuery();
                            break;
                    }

                    foreach (var pn in Projekat.Instance.ProdajaNamestaj)
                    {
                        if (pn.Id == prodajaNamestaj.Id)
                        {
                            pn.Obrisan = true;
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
