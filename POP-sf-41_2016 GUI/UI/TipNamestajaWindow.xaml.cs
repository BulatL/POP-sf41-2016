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

        public TipNamestajaWindow(TipNamestaja tipNamestaja, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(tipNamestaja, operacija);

        }

        private void InicijalizujVrednosti(TipNamestaja tipNamestaja, Operacija operacija)
        {
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;

            this.tbNaziv.Text = tipNamestaja.Naziv;

        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaTipNamestaj = Projekat.Instance.TipNamestaj;

            switch (operacija)
            {
                case Operacija.DODAVANJE:

                    var noviTipNamestaja = new TipNamestaja()
                    {
                        Id = listaTipNamestaj.Count + 1,
                        Naziv = tbNaziv.Text.Trim(),
                        Obrisan = false
                    };

                    listaTipNamestaj.Add(noviTipNamestaja);
                    break;

                case Operacija.IZMENA:

                    foreach (var tn in listaTipNamestaj)
                    {
                        if (tn.Id == tipNamestaja.Id)
                        {
                            tn.Naziv = tbNaziv.Text.Trim();
                        }
                    }

                    break;
            }

            Projekat.Instance.TipNamestaj = listaTipNamestaj;
            this.Close();
        }
    }
}
