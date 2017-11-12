using POP_sf41_2016.model;
using System;
using System.Collections;
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
    /// Interaction logic for AkcijaWindow.xaml
    /// </summary>
    public partial class AkcijaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Akcija akcija;
        private Operacija operacija;

        public AkcijaWindow(Akcija akcija, Operacija operacija )
        {
            InitializeComponent();

            InicijalizujVrednosti(akcija, operacija);
        }

        private void InicijalizujVrednosti(Akcija akcija, Operacija operacija)
        {
            this.akcija = akcija;
            this.operacija = operacija;

            this.dpPocetak.SelectedDate = DateTime.Now;
            this.dpPocetak.DisplayDateStart = DateTime.Now;
            this.dpKraj.DisplayDateStart = dpPocetak.SelectedDate;
            //this.dpKraj.SelectedDate = DateTime.Now;
           // this.tbPopust.Text = akcija.Popust.ToString("0.00");

            var listaNamestaja = new ArrayList();
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {

                if (namestaj.Obrisan == false)
                {
                    var nadjeniTip = TipNamestaja.NadjiNamestaj(namestaj.TipNamestajaId);

                    
                    listaNamestaja.Add(namestaj);
                }
            }
            lbNamestaj.ItemsSource = listaNamestaja;

        }
        /*
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var akcija = Projekat.Instance.Akcija;
                    var listaNamestaja = new List<Namestaj>();
                    listaNamestaja.Add(cbNamestaj.Items);
                    var novaAkcija = new Akcija()
                    {
                        Id = akcija.Count + 1,
                        DatumPocetka = (DateTime)dpPocetak.SelectedDate,
                        DatumZavrsetka = (DateTime)dpKraj.SelectedDate,
                        Popust = double.Parse(tbPopust.Text),
                        NamestajNaPopustu = lvNamestaj.Items 
                    };

                    break;

                case Operacija.IZMENA:



                    break;
                
            }
        }*/
    }
}
