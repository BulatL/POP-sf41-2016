using POP_sf_41_2016_GUI.DAO;
using POP_sf41_2016.model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace POP_sf_41_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Namestaj namestaj;
        private Operacija operacija;
        public NamestajWindow(Namestaj namestaj, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            cbTipNamestaja.DataContext = namestaj;
            cbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            tbNaziv.MaxLength = 60;
            tbSifra.MaxLength = 20;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            if (cbTipNamestaja.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati tip namestaja", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if(ForceValidation() == true)
            {
                return;
            }
            
            else
            {
                this.DialogResult = true;

                if (operacija == Operacija.DODAVANJE)
                {
                    NamestajDAO.Create(namestaj);
                }
                else if (operacija == Operacija.IZMENA)
                {
                    NamestajDAO.Update(namestaj);
                }
                this.Close();
            }
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            BindingExpression be2 = tbSifra.GetBindingExpression(TextBox.TextProperty);
            be2.UpdateSource();
            BindingExpression be3 = tbKolicina.GetBindingExpression(TextBox.TextProperty);
            be3.UpdateSource();
            BindingExpression be4 = tbCena.GetBindingExpression(TextBox.TextProperty);
            be4.UpdateSource();
            if (Validation.GetHasError(tbNaziv) == true || Validation.GetHasError(tbSifra) == true || Validation.GetHasError(tbKolicina) == true || Validation.GetHasError(tbCena) == true)
            {
                return true;
            }
            return false;
        }
    }
}
