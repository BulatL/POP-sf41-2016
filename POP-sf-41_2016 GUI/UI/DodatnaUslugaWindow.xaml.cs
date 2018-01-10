using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        public DodatnaUslugaWindow(DodatnaUsluga dodatnaUsluga, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.dodatnaUsluga = dodatnaUsluga;
            this.operacija = operacija;

            tbNaziv.DataContext = dodatnaUsluga;
            tbNaziv.MaxLength = 30;
            tbCena.DataContext = dodatnaUsluga;
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            
            if(ForceValidation() == true)
            {
                return;
            }
            else
            {
                this.DialogResult = true;
                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        DodatnaUslugaDAO.Create(dodatnaUsluga);
                        break;
                    case Operacija.IZMENA:
                        DodatnaUslugaDAO.Update(dodatnaUsluga);
                        break;
                }
            }
            this.Close();
        }

        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbCena.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            if (Validation.GetHasError(tbNaziv) == true || Validation.GetHasError(tbCena) == true)
            {
                return true;
            }
            return false;
        }
    }
}
