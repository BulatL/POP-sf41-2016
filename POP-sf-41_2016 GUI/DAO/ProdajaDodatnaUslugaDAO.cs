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
    class ProdajaDodatnaUslugaDAO
    {

        public enum TipBrisanja
        {
            ProdajaId,
            ProdajaDodatnaUsluga
        }
        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * " +
                                   "FROM ProdajaDodatnaUsluga " +
                                   "WHERE Obrisan = 0; ";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "ProdajaDodatnaUsluga");

                foreach (DataRow row in dsA.Tables["ProdajaDodatnaUsluga"].Rows)
                {
                    ProdajaDodatnaUsluga prodajaDodatnaUsluga = new ProdajaDodatnaUsluga();
                    prodajaDodatnaUsluga.Id = int.Parse(row["IdPDU"].ToString());
                    prodajaDodatnaUsluga.DodatnaUslugaId = int.Parse(row["DodatnaUslugaId"].ToString());
                    prodajaDodatnaUsluga.ProdajaId = int.Parse(row["ProdajaId"].ToString());
                    prodajaDodatnaUsluga.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    prodajaDodatnaUsluga.Cena = double.Parse(row["Cena"].ToString());
                    Projekat.Instance.ProdajaDodatneUsluge.Add(prodajaDodatnaUsluga);
                }

                foreach (var prodaja in Projekat.Instance.Prodaja)
                {
                    if (prodaja.ListaDodatnihUslugaId == null)
                    {
                        prodaja.ListaDodatnihUslugaId = new ObservableCollection<int?>();
                    }
                    foreach (var dodatnaUsluga in Projekat.Instance.ProdajaDodatneUsluge)
                    {
                        if (prodaja.Id == dodatnaUsluga.ProdajaId)
                        {
                            prodaja.ListaDodatnihUslugaId.Add(dodatnaUsluga.Id);
                        }
                    }
                }
            }
        }

        public static ObservableCollection<ProdajaDodatnaUsluga> LoadByProdajaId(int prodajaId)
        {
            var lista = new ObservableCollection<ProdajaDodatnaUsluga>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * " +
                                   "FROM ProdajaDodatnaUsluga " +
                                   "WHERE Obrisan = 0 and ProdajaId=@ProdajaId;";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaId));

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "ProdajaDodatnaUsluga");

                foreach (DataRow row in dsA.Tables["ProdajaDodatnaUsluga"].Rows)
                {
                    ProdajaDodatnaUsluga prodajaDodatnaUsluga = new ProdajaDodatnaUsluga();
                    prodajaDodatnaUsluga.Id = int.Parse(row["IdPDU"].ToString());
                    prodajaDodatnaUsluga.DodatnaUslugaId = int.Parse(row["DodatnaUslugaId"].ToString());
                    prodajaDodatnaUsluga.ProdajaId = int.Parse(row["ProdajaId"].ToString());
                    prodajaDodatnaUsluga.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    prodajaDodatnaUsluga.Cena = double.Parse(row["Cena"].ToString());
                    lista.Add(prodajaDodatnaUsluga);
                }
            }
            return lista;
        }

        public static void Update(ProdajaDodatnaUsluga prodajaDodatnaUsluga)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"UPDATE ProdajaDodatnaUsluga SET ProdajaId=@ProdajaId , DodatnaUslugaId=@DodatnaUslugaId, Cena=@Cena, Obrisan=@Obrisan " +
                                    "WHERE IdPDU=@IdPDU;";

                cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaDodatnaUsluga.ProdajaId));
                cmd.Parameters.Add(new SqlParameter("@DodatnaUslugaId", prodajaDodatnaUsluga.DodatnaUslugaId));
                cmd.Parameters.Add(new SqlParameter("@Cena", prodajaDodatnaUsluga.Cena));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaDodatnaUsluga.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdPDU", prodajaDodatnaUsluga.Id));

                var uu = cmd.ExecuteNonQuery();

                foreach (var pdu in Projekat.Instance.ProdajaDodatneUsluge)
                {
                    if (prodajaDodatnaUsluga.Id == pdu.Id)
                    {
                        pdu.DodatnaUslugaId = prodajaDodatnaUsluga.DodatnaUslugaId;
                        pdu.Cena = prodajaDodatnaUsluga.Cena;
                        pdu.Obrisan = prodajaDodatnaUsluga.Obrisan;
                    }
                }
            }
        }

        public static void Create(ProdajaDodatnaUsluga prodajaDodatnaUsluga)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"INSERT INTO ProdajaDodatnaUsluga (ProdajaId, DodatnaUslugaId, Cena, Obrisan) " +
                                   "VALUES (@ProdajaId, @DodatnaUslugaId, @Cena, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaDodatnaUsluga.ProdajaId));
                cmd.Parameters.Add(new SqlParameter("@DodatnaUslugaId", prodajaDodatnaUsluga.DodatnaUslugaId));
                cmd.Parameters.Add(new SqlParameter("@Cena", prodajaDodatnaUsluga.Cena));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaDodatnaUsluga.Obrisan));

                prodajaDodatnaUsluga.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            Projekat.Instance.ProdajaDodatneUsluge.Add(prodajaDodatnaUsluga);
        }

        public static void Delete(ProdajaDodatnaUsluga prodajaDodatnaUsluga, TipBrisanja tipBrisanja, int prodajaId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                switch (tipBrisanja)
                {
                    case TipBrisanja.ProdajaId:
                        cmd.CommandText = @"UPDATE ProdajaDodatnaUsluga SET Obrisan = 1 WHERE ProdajaId=@ProdajaId";

                        cmd.Parameters.Add(new SqlParameter("@Obrisan", prodajaDodatnaUsluga.Obrisan));
                        cmd.Parameters.Add(new SqlParameter("@ProdajaId", prodajaId));
                        var i = cmd.ExecuteNonQuery();

                        break;
                    case TipBrisanja.ProdajaDodatnaUsluga:
                        cmd.CommandText = @"UPDATE ProdajaDodatnaUsluga SET Obrisan = 1 " +
                                           "WHERE IdPDU=@IdPDU";

                        cmd.Parameters.Add(new SqlParameter("@IdPDU", prodajaDodatnaUsluga.Id)); ;
                        var ii = cmd.ExecuteNonQuery();
                        break;
                }

                foreach (var pdu in Projekat.Instance.ProdajaDodatneUsluge)
                {
                    if (pdu.Id == prodajaDodatnaUsluga.Id)
                    {
                        pdu.Obrisan = true;
                    }
                }
            }
        }
    }
}
