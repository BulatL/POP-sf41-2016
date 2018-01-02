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
    class TipNamestajaDAO
    {
        public static void Load()
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

        public static void Update(TipNamestaja tn)
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
                if(namestaj.TipNamestajaId == tn.Id)
                {
                    namestaj.TipNamestaja.Naziv = tn.Naziv;
                }
            }
        }

        public static void Create(TipNamestaja tn)
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

        public static ObservableCollection<TipNamestaja> Find (String parametar)
        {
            var lista = new ObservableCollection<TipNamestaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"SELECT * " +
                                   "FROM TipNamestaj "+
                                   "WHERE Naziv like @Parametar and Obrisan = 0;";

                cmd.Parameters.Add(new SqlParameter("@Parametar","%" + parametar + "%" ));

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

        public static void Delete(TipNamestaja tn)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE TipNamestaj SET Obrisan = 1 WHERE IdTN=@IdTN";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", tn.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdTN", tn.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var tipNamestaja in Projekat.Instance.TipoviNamestaja)
                {
                    if (tn.Id == tipNamestaja.Id)
                    {
                        tipNamestaja.Obrisan = true;
                    }
                }
            }
        }

    }
}
