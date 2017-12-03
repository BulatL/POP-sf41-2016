﻿using POP_sf41_2016.model;
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
        private int index;
        private Operacija operacija;
        public DodatnaUslugaWindow(DodatnaUsluga dodatnaUsluga, int index, Operacija operacija = Operacija.DODAVANJE)
        {
            InitializeComponent();

            this.dodatnaUsluga = dodatnaUsluga;
            this.index = index;
            this.operacija = operacija;

            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }

        private void Odustani_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var listaDodatnihUsluga = Projekat.Instance.DodatnaUsluga;

            if (operacija == Operacija.DODAVANJE)
            {
                dodatnaUsluga.Id = listaDodatnihUsluga.Count + 1;

                listaDodatnihUsluga.Add(dodatnaUsluga);
            }
            else if( operacija == Operacija.IZMENA)
            {
                listaDodatnihUsluga[index] = dodatnaUsluga;
            }
            Projekat.Instance.DodatnaUsluga = listaDodatnihUsluga;
            GenericSerializer.Serializer("dodatnaUsluga.xml", listaDodatnihUsluga);
            this.Close();
        }
    }
}
