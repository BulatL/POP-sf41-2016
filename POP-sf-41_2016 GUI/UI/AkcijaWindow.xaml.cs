using POP_sf41_2016.model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
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
            IZMENA,
            INFO
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
            if(operacija == Operacija.INFO)
            {
                dpPocetak.IsEnabled = false;
                dpKraj.IsEnabled = false;
                tbNaziv.IsReadOnly = true;
                tbNaziv.IsReadOnly = true;
                btnDodaj.IsEnabled = false;
                btnObrisi.IsEnabled = false;
            }

        }
        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {            
            if(operacija == Operacija.INFO)
            {
                this.Close();
            }
            if(ForceValidation() == true)
            {
                return;
            }
            if (akcija.ListaNaAkciji.Count < 1)
            {
                MessageBox.Show("Barem jedan namestaj mora biti na akciji", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (akcija.DatumZavrsetka.Date < akcija.DatumPocetka.Date)
                {
                    MessageBox.Show("Datum zavrsetka akcije mora biti veci od datuma pocetka akcije", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    switch (operacija)
                    {
                        case Operacija.DODAVANJE:
                            if (akcija.DatumPocetka.Date < DateTime.Today)
                            {
                                MessageBox.Show("Datum pocetka akcije ne moze biti manji od danasnjeg dana", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                AkcijaDAO.Create(akcija);
                                foreach (var naAkciji in akcija.ListaNaAkciji)
                                {
                                    NaAkcijiDAO.Create(naAkciji);
                                }
                                DialogResult = true;
                            }
                            break;
                        case Operacija.IZMENA:
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
                            break;
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
                var novoNaAkciji = noviProzor.naAkciji;
                bool postojiNamestaj = false;
                foreach (var item in akcija.ListaNaAkciji)
                {
                    if(noviProzor.naAkciji.NamestajId == item.NamestajId)
                    {
                        MessageBox.Show("Ovaj namestaj je vec dodat na akciju", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        postojiNamestaj = true;
                    }
                }
                if(postojiNamestaj == false)
                {
                    akcija.ListaNaAkciji.Add(novoNaAkciji);
                    akcija.ListaNaAkcijiId.Add(novoNaAkciji.Id);
                }
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

        private bool ForceValidation()
        {
            BindingExpression be1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbNaziv) == true)
            {
                return true;
            }
            return false;
        }
    }
}
