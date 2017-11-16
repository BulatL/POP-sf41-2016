using POP_sf41_2016.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_sf_41_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for SalonWindow.xaml
    /// </summary>
    public partial class SalonWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Salon salon;
        private Operacija operacija;
        public SalonWindow(Salon salon, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(salon, operacija);
        }

        private void InicijalizujVrednosti(Salon salon, Operacija operacija)
        {
            this.salon = salon;
            this.operacija = operacija;

            tbNaziv.Text = salon.Naziv;
            tbAdresa.Text = salon.Adresa;

            if (operacija == Operacija.DODAVANJE)
            {
                tbEmail.Text = "@gmail.com";
                tbSajt.Text = "https://www. ";
                tbTelefon.Text = "";
            }
            else
            {
                tbEmail.Text = salon.Email;
                tbSajt.Text = salon.AdresaInternetSajta;
                tbTelefon.Text = salon.Telefon;
            }

            tbPIB.Text = salon.PIB.ToString();
            tbZiroRacun.Text = salon.BrojZiroRacuna.ToString();
            tbMaticniBr.Text = salon.MaticniBroj.ToString();


        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaSalona = Projekat.Instance.Salon;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        var noviSalon = new Salon()
                        {

                            Id = listaSalona.Count + 1,
                            Naziv = tbNaziv.Text.Trim(),
                            Adresa = tbAdresa.Text.Trim(),
                            Email = tbEmail.Text.Trim(),
                            AdresaInternetSajta = tbSajt.Text.Trim(),
                            Telefon = tbTelefon.Text.Trim(),
                            PIB = int.Parse(tbPIB.Text.Trim()),
                            BrojZiroRacuna = int.Parse(tbZiroRacun.Text.Trim()),
                            MaticniBroj = int.Parse(tbMaticniBr.Text.Trim()),

                        };
                        listaSalona.Add(noviSalon);
                    }
                    catch (Exception ex) { }
                    break;

                case Operacija.IZMENA:
                    try
                    {
                        foreach (var s in listaSalona)
                        {
                            if (s.Id == salon.Id)
                            {
                                s.Naziv = tbNaziv.Text.Trim();
                                s.Adresa = tbAdresa.Text.Trim();
                                s.Email = tbEmail.Text.Trim();
                                s.AdresaInternetSajta = tbSajt.Text.Trim();
                                s.Telefon = tbTelefon.Text.Trim();
                                s.PIB = int.Parse(tbPIB.Text.Trim());
                                s.BrojZiroRacuna = int.Parse(tbZiroRacun.Text.Trim());
                                s.MaticniBroj = int.Parse(tbMaticniBr.Text.Trim());

                            }

                        }
                    }
                    catch (Exception ex) { }

                    break;
            }

            Projekat.Instance.Salon = listaSalona;
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
