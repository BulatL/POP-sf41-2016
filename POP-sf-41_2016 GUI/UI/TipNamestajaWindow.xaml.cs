using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            if (ForceValidation() == true)
            {
                return;
            }
            else
            {
                this.DialogResult = true;
                if (operacija == Operacija.DODAVANJE)
                {
                    TipNamestajaDAO.Create(tipNamestaja);
                }
                else if (operacija == Operacija.IZMENA)
                {
                    TipNamestajaDAO.Update(tipNamestaja);
                }

                this.Close();
            }
        }

        private void Nazad_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if(Validation.GetHasError(tbNaziv) == true)
            {
                return true;
            }
            return false;
        }
    }
}
