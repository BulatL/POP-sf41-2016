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
        public NamestajWindow(Namestaj namestaj, Operacija operacija)
        {
            InitializeComponent();

            InicijalizujVrednosti(namestaj, operacija);

        }

        private void InicijalizujVrednosti(Namestaj namestaj, Operacija operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;
            try
            {
                this.tbNaziv.Text = namestaj.Naziv;
                this.tbCena.Text = namestaj.JedinicnaCena.ToString("0.00");
                this.tbKolicina.Text = namestaj.KolicinaUMagacinu.ToString("0");
                this.tbSifra.Text = namestaj.Sifra;
            }catch(Exception ex) { }

            ComboBoxItem comboBoxItem = new ComboBoxItem();
            var lista = new ArrayList();
            foreach (var tipNamestaj in Projekat.Instance.TipNamestaj)
            {

                cbTipNamestaja.Items.Add(tipNamestaj.Naziv);
                /*
                comboBoxItem.Text = tipNamestaj.Naziv;
                comboBoxItem.Value = tipNamestaj.Id;

                cbTipNamestaja.Items.Add(comboBoxItem);*/
            }

            cbTipNamestaja.SelectedItem = TipNamestaja.NadjiNamestaj(namestaj.TipNamestajaId);
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            var ListaNamestaja = Projekat.Instance.Namestaj;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try { 
                    var noviNamestaj = new Namestaj()
                    {
                     
                        Id = ListaNamestaja.Count + 1,
                        Sifra = tbSifra.Text,
                        JedinicnaCena = double.Parse(tbCena.Text),
                        KolicinaUMagacinu = int.Parse(tbKolicina.Text),
                        AkcijaId = null,
                        Naziv = tbNaziv.Text.Trim(),
                        TipNamestajaId = TipNamestaja.NadjiTipNamestaja(cbTipNamestaja.SelectedItem.ToString()),
                        Obrisan = false
                        
                    };
                        ListaNamestaja.Add(noviNamestaj);
                    }
                    catch (Exception ex) { }
                    break;

                case Operacija.IZMENA:
                    try
                    {
                        foreach (var n in ListaNamestaja)
                        {
                            if (n.Id == namestaj.Id)
                            {
                                n.Naziv = tbNaziv.Text;
                                n.Sifra = tbSifra.Text;
                                n.JedinicnaCena = double.Parse(tbCena.Text);
                                n.KolicinaUMagacinu = int.Parse(tbKolicina.Text);
                                n.AkcijaId = null;
                                n.TipNamestajaId = TipNamestaja.NadjiTipNamestaja(cbTipNamestaja.SelectedItem.ToString());

                            }

                        }
                    }catch(Exception ex) { }

                    break;
            }

            Projekat.Instance.Namestaj = ListaNamestaja;
            this.Close();
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
