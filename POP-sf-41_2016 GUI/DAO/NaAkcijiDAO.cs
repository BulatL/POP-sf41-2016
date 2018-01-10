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

    public enum TipBrisanja {
        PoAkcijaId,
        PoNaAkciji,
        PoNamestajId
    }
    class NaAkcijiDAO
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
                                       "FROM NaAkciji " +
                                       "WHERE Obrisan = 0; ";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    DataSet dsA = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(dsA, "NaAkciji");

                    foreach (DataRow row in dsA.Tables["NaAkciji"].Rows)
                    {
                        NaAkciji naAkciji = new NaAkciji();
                        naAkciji.AkcijaId = int.Parse(row["AkcijaId"].ToString());
                        naAkciji.Id = int.Parse(row["IdNA"].ToString());
                        naAkciji.NamestajId = int.Parse(row["NamestajId"].ToString());
                        naAkciji.Popust = int.Parse(row["Popust"].ToString());
                        naAkciji.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                        Projekat.Instance.NaAkciji.Add(naAkciji);
                    }

                    foreach (var akcija in Projekat.Instance.Akcije)
                    {
                        if (akcija.ListaNaAkcijiId == null)
                        {
                            akcija.ListaNaAkcijiId = new ObservableCollection<int?>();
                        }
                        foreach (var naAkcijii in Projekat.Instance.NaAkciji)
                        {
                            if (akcija.Id == naAkcijii.AkcijaId)
                            {
                                akcija.ListaNaAkcijiId.Add(naAkcijii.Id);
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

        public static ObservableCollection<NaAkciji> LoadByAkcijaId(int AkcijaId)
        {
            try
            {
                var lista = new ObservableCollection<NaAkciji>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * " +
                                       "FROM NaAkciji " +
                                       "WHERE Obrisan = 0 and AkcijaId=@AkcijaId;";

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = cmd;

                    cmd.Parameters.Add(new SqlParameter("@AkcijaId", AkcijaId));

                    DataSet dsA = new DataSet(); // izvrsavanje upita
                    sqlDA.Fill(dsA, "NaAkciji");

                    foreach (DataRow row in dsA.Tables["NaAkciji"].Rows)
                    {
                        NaAkciji naAkciji = new NaAkciji();
                        naAkciji.AkcijaId = int.Parse(row["AkcijaId"].ToString());
                        naAkciji.Id = int.Parse(row["IdNA"].ToString());
                        naAkciji.NamestajId = int.Parse(row["NamestajId"].ToString());
                        naAkciji.Popust = int.Parse(row["Popust"].ToString());
                        naAkciji.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                        lista.Add(naAkciji);
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

        public static void Update(NaAkciji naAkciji)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmdA = conn.CreateCommand();


                    cmdA.CommandText = @"UPDATE NaAkciji SET NamestajId=@NamestajId, AkcijaId=@AkcijaId, Obrisan=@Obrisan, Popust=@Popust " +
                                        "WHERE IdNA=@IdNA;";

                    cmdA.Parameters.Add(new SqlParameter("@NamestajId", naAkciji.NamestajId));
                    cmdA.Parameters.Add(new SqlParameter("@AkcijaId", naAkciji.AkcijaId));
                    cmdA.Parameters.Add(new SqlParameter("@Obrisan", naAkciji.Obrisan));
                    cmdA.Parameters.Add(new SqlParameter("@Popust", naAkciji.Popust));
                    cmdA.Parameters.Add(new SqlParameter("@IdNA", naAkciji.Id));

                    var uuA = cmdA.ExecuteNonQuery();


                    foreach (var na in Projekat.Instance.NaAkciji)
                    {
                        if (naAkciji.Id == na.Id)
                        {
                            na.NamestajId = naAkciji.NamestajId;
                            na.Popust = naAkciji.Popust;
                            na.Obrisan = naAkciji.Obrisan;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Create(NaAkciji naAkciji)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();


                    cmd.CommandText = @"INSERT INTO NaAkciji (AkcijaId, NamestajId, Obrisan, Popust) " +
                                       "VALUES (@AkcijaId, @NamestajId, @Obrisan, @Popust);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.Add(new SqlParameter("@AkcijaId", naAkciji.AkcijaId));
                    cmd.Parameters.Add(new SqlParameter("@NamestajId", naAkciji.NamestajId));
                    cmd.Parameters.Add(new SqlParameter("@Obrisan", naAkciji.Obrisan));
                    cmd.Parameters.Add(new SqlParameter("@Popust", naAkciji.Popust));

                    naAkciji.Id = int.Parse(cmd.ExecuteScalar().ToString());
                }
                Projekat.Instance.NaAkciji.Add(naAkciji);
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public static void Delete(NaAkciji naAkciji, TipBrisanja tipBrisanja, int akcijaId, int namestajId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    switch (tipBrisanja)
                    {
                        case TipBrisanja.PoAkcijaId:
                            cmd.CommandText = @"UPDATE NaAkciji SET Obrisan = 1 WHERE AkcijaId=@AkcijaId";

                            cmd.Parameters.Add(new SqlParameter("@Obrisan", 1));
                            cmd.Parameters.Add(new SqlParameter("@AkcijaId", akcijaId));
                            var i = cmd.ExecuteNonQuery();

                            break;
                        case TipBrisanja.PoNaAkciji:
                            cmd.CommandText = @"UPDATE NaAkciji SET Obrisan = 1 WHERE AkcijaId=@AkcijaId and NamestajId=@NamestajId and Popust=@Popust";

                            cmd.Parameters.Add(new SqlParameter("@AkcijaId", naAkciji.AkcijaId));
                            cmd.Parameters.Add(new SqlParameter("@NamestajId", naAkciji.NamestajId));
                            cmd.Parameters.Add(new SqlParameter("@Popust", naAkciji.Popust));
                            var ii = cmd.ExecuteNonQuery();
                            break;
                        case TipBrisanja.PoNamestajId:
                            cmd.CommandText = @"UPDATE NaAkciji SET Obrisan = 1 WHERE NamestajId=@NamestajId";

                            cmd.Parameters.Add(new SqlParameter("@Obrisan", 1));
                            cmd.Parameters.Add(new SqlParameter("@NamestajId", namestajId));
                            var iii = cmd.ExecuteNonQuery();

                            break;
                    }
                    if(naAkciji != null)
                    {
                        foreach (var na in Projekat.Instance.NaAkciji)
                        {
                            if (na.Id == naAkciji.Id)
                            {
                                na.Obrisan = true;
                                break;
                            }
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
