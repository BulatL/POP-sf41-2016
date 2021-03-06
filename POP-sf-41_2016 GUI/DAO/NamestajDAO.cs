﻿using POP_sf41_2016.model;
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
    class NamestajDAO
    {
        public enum TipPretrage
        {
            Naziv,
            Sifra,
            TipNamestaja,
        }
        public static void Load()
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanje iz baze, Molimo vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static ObservableCollection<Namestaj> LoadNamestajNotInAkcija()
        {
            try
            {
                var lista = new ObservableCollection<Namestaj>();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * " +
                                        "FROM Namestaj " +
                                        "WHERE Obrisan = 0 and IdN not in (SELECT NamestajId " +
                                                                       "FROM NaAkciji " +
                                                                       "WHERE Obrisan = 0);";

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

                        lista.Add(n);
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

        public static void Update(Namestaj n)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void Create(Namestaj n)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public static void Delete(Namestaj n)
        {
            try
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
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom snimanja u bazu, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static string SortBy (int sort)
        {
            String sortBy = "";
            if (sort == 0)
            {
                sortBy = @"ORDER BY n.IdN; ";
            }
            else if (sort == 1)
            {
                sortBy = @"ORDER BY n.Naziv; ";
            }
            else if (sort == 2)
            {
                sortBy = @"ORDER BY n.Sifra; ";
            }
            else if (sort == 3)
            {
                sortBy = @"ORDER BY n.Cena; ";
            }
            else if (sort == 4)
            {
                sortBy = @"ORDER BY n.Kolicina; ";
            }
            else if (sort == 5)
            {
                sortBy = @"ORDER BY tn.Naziv; ";
            }
            return sortBy;
        }

        public static ObservableCollection<Namestaj> FindSort(String parametarZaPretragu, TipPretrage tipPretrage, int sort)
        {
            try
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
                                               "WHERE n.Naziv like @Parametar and n.Obrisan =0 ";
                            cmd.CommandText += SortBy(sort);
                            break;
                        case TipPretrage.Sifra:
                            cmd.CommandText = @"SELECT * " +
                                               "FROM Namestaj n inner join TipNamestaj tn on n.TipNamestajaId = tn.IdTN " +
                                               "WHERE n.Sifra like @Parametar and n.Obrisan =0 ";
                            cmd.CommandText += SortBy(sort);
                            break;
                        case TipPretrage.TipNamestaja:
                            cmd.CommandText = @"SELECT * " +
                                               "FROM Namestaj n inner join TipNamestaj tn on n.TipNamestajaId = tn.IdTN  " +
                                               "WHERE tn.Naziv like @Parametar and n.Obrisan = 0 ";
                            cmd.CommandText += SortBy(sort);
                            break;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Parametar", "%" + parametarZaPretragu + "%"));

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
            catch (Exception)
            {
                MessageBox.Show("Doslo je do greske prilikom ucitavanja iz baze, Molimo Vas pokusajte ponovo", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }
    }
}
