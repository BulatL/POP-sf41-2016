using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using POP_sf41_2016.util;
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


        }


        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaSalona = Projekat.Instance.Salon;

            if (operacija == Operacija.DODAVANJE)
            {
                salon.Id = listaSalona.Count + 1;
                SalonDAO.Create(salon);
            }
            else if ( operacija == Operacija.IZMENA)
            {
                listaSalona = Salon.Update(salon);
                SalonDAO.Update(salon);
            }
            Projekat.Instance.Salon = listaSalona;
            GenericSerializer.Serializer("salon.xml", listaSalona);
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
