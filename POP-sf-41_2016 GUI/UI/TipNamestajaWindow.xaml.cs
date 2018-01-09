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
    /// Interaction logic for TipNamestajaWindow.xaml
    /// </summary>
    public partial class TipNamestajaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private TipNamestaja tipNamestaja;
        private Operacija operacija;

        public TipNamestajaWindow(TipNamestaja tipNamestaja, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;

            tbNaziv.DataContext = tipNamestaja;
            tbNaziv.MaxLength = 60;

        }


        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (operacija == Operacija.DODAVANJE)
            {
                TipNamestajaDAO.Create(tipNamestaja);
                TipNamestaja.Update(tipNamestaja);
            }
            else if (operacija == Operacija.IZMENA)
            {
                TipNamestajaDAO.Update(tipNamestaja);
            }

            this.Close();
        }

        private void Nazad_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
