﻿using System;
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
using BE;
using BL;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for hostprop.xaml
    /// </summary>
    public partial class hostprop : Window
    {
        Host h;
        public hostprop()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        public hostprop(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            h = MainWindow.ibl.FindHost(id);
            welcome.Content = "Welcome " + h.FirstName + " " + h.LastName + "!";
           
         
        }
        
        private void Addhu_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new AddHostingUnit(h).ShowDialog();
        }

      

        private void Delete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            HostingUnit unit = new HostingUnit();
            unit.HostingUnitName = name;
           unit.HostingUnitKey= MainWindow.ibl.GetHUkeyBuName(name);
            try
            {
                MainWindow.ibl.DelHostingUnit(unit);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Oops!/n" + ex.Message);
                return;
            }
            MessageBox.Show(name + "was removed succesfully");
        }

        private void Update_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hu = MainWindow.ibl.GetHubyHost(h.ID);
            var combo = sender as ComboBox;
            combo.ItemsSource = hu;
            //combo.SelectedIndex = 0;
        }

        private void Update_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            
        }
    }
}
