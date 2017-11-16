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
    /// Interaction logic for DodatnaUslugaWindow.xaml
    /// </summary>
    public partial class DodatnaUslugaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private DodatnaUsluga dodatnaUsluga;
        private Operacija operacija;
        public DodatnaUslugaWindow(DodatnaUsluga dodatnaUsluga, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(dodatnaUsluga, operacija);
        }

        private void InicijalizujVrednosti(DodatnaUsluga dodatnaUsluga, Operacija operacija)
        {
            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;

            tbNaziv.Text = dodatnaUsluga.Naziv;
            tbCena.Text = dodatnaUsluga.Cena.ToString("0.00");
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        var novaDodatnaUsluga = new DodatnaUsluga()
                        {
                            Id = listaDodatnihUsluga.Count +1,
                            Naziv = tbNaziv.Text,
                            Cena = double.Parse(tbCena.Text)
                        };
                        listaDodatnihUsluga.Add(novaDodatnaUsluga);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;

                case Operacija.IZMENA:
                    try
                    {
                        foreach (var du in listaDodatnihUsluga)
                        {
                            if(du.Id == dodatnaUsluga.Id)
                            {
                                du.Naziv = tbNaziv.Text;
                                du.Cena = double.Parse(tbCena.Text);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
            }
            Projekat.Instance.DodatnaUsluga = listaDodatnihUsluga;
            this.Close();
        }
    }
}
