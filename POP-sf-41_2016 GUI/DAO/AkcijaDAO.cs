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
    class AkcijaDAO
    {
        public enum TipPretrage
        {
            DatumPocetka,
            DatumZavrsetka,
            Naziv,
            Namestaji
        }

        public static void Load()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * " +
                                   "FROM Akcija " +
                                   "WHERE Obrisan = 0; ";

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Akcija");


                foreach (DataRow row in dsA.Tables["Akcija"].Rows)
                {
                    Akcija akcija = new Akcija();
                    akcija.Id = int.Parse(row["IdA"].ToString());
                    akcija.Naziv = row["Naziv"].ToString();
                    akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akcija.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                    akcija.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    if(akcija.DatumZavrsetka < DateTime.Now.Date)
                    {
                        NaAkciji na = new NaAkciji()
                        {
                            AkcijaId = akcija.Id
                        };
                        NaAkcijiDAO.Delete(na, TipBrisanja.PoAkcijaId);
                        Delete(akcija);
                    }
                    else
                    {
                        Projekat.Instance.Akcije.Add(akcija);
                    }
                }
            }
        }

        public static void Update(Akcija akcija)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmdA = conn.CreateCommand();


                cmdA.CommandText = @"UPDATE Akcija SET Naziv=@Naziv, DatumPocetka=@DatumPocetka, DatumZavrsetka=@DatumZavrsetka, Obrisan=@Obrisan " +
                                    "WHERE IdA=@IdA;";

                cmdA.Parameters.Add(new SqlParameter("@Naziv", akcija.Naziv));
                cmdA.Parameters.Add(new SqlParameter("@DatumPocetka", akcija.DatumPocetka));
                cmdA.Parameters.Add(new SqlParameter("@DatumZavrsetka", akcija.DatumZavrsetka));
                cmdA.Parameters.Add(new SqlParameter("@Obrisan", akcija.Obrisan));
                cmdA.Parameters.Add(new SqlParameter("@IdA", akcija.Id));

                var uuA = cmdA.ExecuteNonQuery();

                foreach (var ak in Projekat.Instance.Akcije)
                {
                    if (akcija.Id == ak.Id)
                    {
                        ak.Naziv = akcija.Naziv;
                        ak.DatumZavrsetka = akcija.DatumZavrsetka;
                        ak.Obrisan = akcija.Obrisan;
                        ak.DatumPocetka = akcija.DatumPocetka;
                    }
                }
            }
        }

        public static void Create(Akcija akcija)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();


                cmd.CommandText = @"INSERT INTO Akcija (DatumPocetka, DatumZavrsetka, Naziv, Obrisan) " +
                                   "VALUES (@DatumPocetka, @DatumZavrsetka, @Naziv, @Obrisan);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.Add(new SqlParameter("@Naziv", akcija.Naziv));
                cmd.Parameters.Add(new SqlParameter("@DatumPocetka", akcija.DatumPocetka));
                cmd.Parameters.Add(new SqlParameter("@DatumZavrsetka", akcija.DatumZavrsetka));
                cmd.Parameters.Add(new SqlParameter("@Obrisan", akcija.Obrisan));

                //cmd.ExecuteNonQuery();
                akcija.Id = int.Parse(cmd.ExecuteScalar().ToString());
            }
            Projekat.Instance.Akcije.Add(akcija);
        }

        public static ObservableCollection<Akcija> Find(String parametarZaPretragu, TipPretrage tipPretrage, DateTime? dateTime)
        {
            var listaPretraga = new ObservableCollection<Akcija>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                switch (tipPretrage)
                {
                    case TipPretrage.DatumPocetka:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Akcija " +
                                           "WHERE Obrisan = 0 and DatumPocetka >= @dateTime;";
                        cmd.Parameters.Add(new SqlParameter("@dateTime", dateTime));
                        break;
                    case TipPretrage.DatumZavrsetka:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Akcija " +
                                           "WHERE Obrisan = 0 and DatumPocetka <= @dateTime;";
                        cmd.Parameters.Add(new SqlParameter("@dateTime", dateTime));
                        break;
                    case TipPretrage.Naziv:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Akcija " +
                                           "WHERE Obrisan = 0 and Naziv like @ParametarNaziv;";
                        cmd.Parameters.Add(new SqlParameter("@ParametarNaziv", "%" + parametarZaPretragu + "%"));
                        break;
                    case TipPretrage.Namestaji:
                        cmd.CommandText = @"SELECT * " +
                                           "FROM Akcija a inner join NaAkciji na on a.IdA = na.AkcijaId inner join Namestaj n on n.IdN = na.NamestajId  " +
                                           "WHERE a.Obrisan = 0 and na.Obrisan = 0 and n.Naziv like @ParametarNaziv;";
                        cmd.Parameters.Add(new SqlParameter("@ParametarNaziv", "%" + parametarZaPretragu + "%"));
                        break;
                }

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;

                DataSet dsA = new DataSet(); // izvrsavanje upita
                sqlDA.Fill(dsA, "Akcija");


                foreach (DataRow row in dsA.Tables["Akcija"].Rows)
                {
                    Akcija akcija = new Akcija();
                    akcija.Id = int.Parse(row["IdA"].ToString());
                    akcija.Naziv = row["Naziv"].ToString();
                    akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                    akcija.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                    akcija.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    listaPretraga.Add(akcija);
                }
            }
            return listaPretraga;
        }

    public static void Delete(Akcija akcija)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                conn.Open();


                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE Akcija SET Obrisan = 1 WHERE IdA=@IdA";

                cmd.Parameters.Add(new SqlParameter("@Obrisan", akcija.Obrisan));
                cmd.Parameters.Add(new SqlParameter("@IdA", akcija.Id));

                var i = cmd.ExecuteNonQuery();

                foreach (var ak in Projekat.Instance.Akcije)
                {
                    if (ak.Id == akcija.Id)
                    {
                        ak.Obrisan = true;
                    }
                }
            }
        }
    }
}
