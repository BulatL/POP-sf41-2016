using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using System.Windows;

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
        public SalonWindow(Salon salon, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.salon = salon;
            this.operacija = operacija;

            tbNaziv.DataContext = salon;
            tbAdresa.DataContext = salon;
            tbEmail.DataContext = salon;
            tbSajt.DataContext = salon;
            tbTelefon.DataContext = salon;
            tbPIB.DataContext = salon;
            tbZiroRacun.DataContext = salon;
            tbMaticniBr.DataContext = salon;

            tbNaziv.MaxLength = 40;
            tbAdresa.MaxLength = 60;
            tbEmail.MaxLength = 40;
            tbSajt.MaxLength = 60;
            tbTelefon.MaxLength = 30;
            tbPIB.MaxLength = 255;
            tbZiroRacun.MaxLength = 255;
            tbMaticniBr.MaxLength = 255;

        }


        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            if (operacija == Operacija.DODAVANJE)
            {
                SalonDAO.Create(salon);
            }
            if ( operacija == Operacija.IZMENA)
            {
                SalonDAO.Update(salon);
            }
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
