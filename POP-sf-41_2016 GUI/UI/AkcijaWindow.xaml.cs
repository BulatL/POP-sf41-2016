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
            this.tbPopust.Text = akcija.Popust.ToString("0.00");

            var listaNamestaja = new ArrayList();
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {

                if (namestaj.Obrisan == false)
                {
                    lbNamestaj.Items.Add(namestaj);
                }
            }
            //lbNamestaj.ItemsSource = listaNamestaja;

        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var listaAkcija = Projekat.Instance.Akcija;
            int idAkcije = listaAkcija.Count + 1;

            var listaNamestaja = Projekat.Instance.Namestaj;
            var namestaj = lbNamestaj.SelectedItem as Namestaj;

            if (namestaj != null)
            {
                foreach (var n in listaNamestaja) //u listu namestaja ubacujem id akcije
                {
                    if (n.Id == namestaj.Id)
                    {
                        n.AkcijaId = idAkcije;
                    }
                }
            }

            switch (operacija)
           {
               case Operacija.DODAVANJE:
                    try { 
                        var novaAkcija = new Akcija()
                        {
                            Id = idAkcije,
                            DatumPocetka = (DateTime)dpPocetak.SelectedDate,
                            DatumZavrsetka = (DateTime)dpKraj.SelectedDate,
                            Popust = double.Parse(tbPopust.Text),
                            NamestajNaPopustuId = namestaj.Id
                        };
                        listaAkcija.Add(novaAkcija);
                    }
                    catch (Exception) { }
                    break;

               case Operacija.IZMENA:
                    try
                    {
                        foreach (var a in listaAkcija)
                        {
                            if(a.Id == akcija.Id)
                            {
                                a.DatumZavrsetka = (DateTime) dpKraj.SelectedDate;
                                a.NamestajNaPopustuId = namestaj.Id;
                                a.Popust = double.Parse(tbPopust.Text);
                            }
                        }
                    }catch (Exception) { }

                    
                    break;
           }
            Projekat.Instance.Akcija = listaAkcija;
            Projekat.Instance.Namestaj = listaNamestaja;
            this.Close();
        }
    }
}
