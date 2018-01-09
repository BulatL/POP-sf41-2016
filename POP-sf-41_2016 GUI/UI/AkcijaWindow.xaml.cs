using POP_sf41_2016.model;
using POP_sf41_2016.util;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using POP_sf_41_2016_GUI.DAO;
using POP_sf_41_2016_GUI.model;

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
        private ICollectionView viewn;

        public AkcijaWindow(Akcija akcija, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.akcija = akcija;
            this.operacija = operacija;

            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            tbNaziv.DataContext = akcija;
            tbNaziv.MaxLength = 60;

            dataGridNamestaj.AutoGenerateColumns = false;
            dataGridNamestaj.IsSynchronizedWithCurrentItem = true;
            dataGridNamestaj.DataContext = akcija;
            viewn = CollectionViewSource.GetDefaultView(akcija.ListaNaAkciji);
            dataGridNamestaj.ItemsSource = viewn;


            if (akcija.Id == 0)
            {
                dpPocetak.DisplayDateStart = DateTime.Now;
                dpKraj.DisplayDateStart = dpPocetak.SelectedDate;
                dpKraj.DisplayDateStart = DateTime.Now;
            }

        }
        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            
            var listaAkcija = Projekat.Instance.Akcije;

            if (akcija.ListaNaAkciji.Count < 1)
            {
                MessageBox.Show("Barem jedan namestaj mora biti na akciji", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (operacija == Operacija.DODAVANJE)
                {
                    if (akcija.DatumPocetka.Date < DateTime.Today)
                    {
                        MessageBox.Show("Datum pocetka akcije ne moze biti manji od danasnjeg dana", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                if (akcija.DatumZavrsetka.Date < akcija.DatumPocetka.Date)
                {
                    MessageBox.Show("Datum zavrsetka akcije mora biti veci od datuma pocetka akcije", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (akcija.DatumPocetka.Date >= DateTime.Today || akcija.DatumZavrsetka.Date > akcija.DatumPocetka.Date)
                {
                    if (operacija == Operacija.DODAVANJE)
                    {

                        AkcijaDAO.Create(akcija);
                        foreach (var naAkciji in akcija.ListaNaAkciji)
                        {
                            NaAkcijiDAO.Create(naAkciji);
                        }
                        DialogResult = true;

                    }
                    else if (operacija == Operacija.IZMENA)
                    {
                        var listaProvera = NaAkcijiDAO.LoadByAkcijaId(akcija.Id);
                        foreach (var naAkciji in akcija.ListaNaAkciji.ToList())
                        {
                            bool postojiNaAkciji = false;
                            foreach (var item in listaProvera.ToList())
                            {
                                if (item.Id == naAkciji.Id)
                                {
                                    postojiNaAkciji = true;
                                    listaProvera.Remove(item);
                                    break;
                                }
                                if (item.NamestajId == naAkciji.NamestajId)
                                {
                                    if (item.Popust != naAkciji.Popust)
                                    {
                                        NaAkcijiDAO.Update(item);
                                        break;
                                    }
                                }
                            }
                            if (postojiNaAkciji == false)
                            {
                                NaAkcijiDAO.Create(naAkciji);
                            }
                        }
                        foreach (var item in listaProvera.ToList())
                        {
                            NaAkcijiDAO.Delete(item, TipBrisanja.PoNaAkciji, 0, 0);
                        }
                        AkcijaDAO.Update(akcija);
                        DialogResult = true;
                    }
                    this.Close();
                }
            }
        }

        private void Dodaj_click(object sender, RoutedEventArgs e)
        {
            var akcijaId = 0;
            if (akcija.Id == 0)
            {
                akcijaId = AkcijaDAO.GetLastId()+1;
            }
            else
            {
                akcijaId = akcija.Id;
            }
            var noviProzor = new StavkaWindow(null, akcijaId, StavkaWindow.Parametar.AKCIJA);
            if (noviProzor.ShowDialog() == true)
            {
                akcija.ListaNaAkciji.Add(noviProzor.naAkciji);
                akcija.ListaNaAkcijiId.Add(noviProzor.naAkciji.Id);
            }
        }

        private void Obrisi_click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestajNaAkciji = viewn.CurrentItem as NaAkciji;
            var lista = akcija.ListaNaAkciji;
            var listaId = akcija.ListaNaAkcijiId;

            foreach (var item in lista.ToList())
            {
                if (item.Id == izabraniNamestajNaAkciji.Id)
                {
                    lista.Remove(item);
                    break;
                }
            }
            foreach (var item in listaId.ToList())
            {
                if (item == izabraniNamestajNaAkciji.Id)
                {
                    listaId.Remove(item);
                    break;
                }
            }
        }
    }
}
