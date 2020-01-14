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
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for addorder.xaml
    /// </summary>
    public partial class addorder : Window
    {
        string ID;
        public addorder()
        {
            InitializeComponent();
        }
        public addorder(string id)
        {
            ID = id;
            InitializeComponent();
        }

        private void select_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> hu = MainWindow.ibl.GetHubyHost(ID);
            var combo = sender as ComboBox;
            combo.ItemsSource = hu;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)//change to when pressed red
        {
            ((Button)sender).Background = (Brush)Brushes.Red;
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;


        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width /= 1.1;
            ((Button)sender).Height /= 1.1;
        }
        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_MinimizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
        private void Button_Click_MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(this);
            else
                SystemCommands.MaximizeWindow(this);
        }

        private void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = sender as ComboBox;
            string name = result.SelectedItem as string;
            HostingUnit unit = new HostingUnit();
            foreach(HostingUnit hosting in MainWindow.ibl.GetAllHostingUnits())
            {
                if (hosting.HostingUnitName == name)
                {
                    unit = hosting;
                    break;
                }
            }
            //   IEnumerable<Guest> g = MainWindow.ibl.GetAllGuests(MainWindow.ibl.BuildPredicate(unit));
            List<Guest> l = MainWindow.ibl.GetAllGuests();
            Guests.Visibility = Visibility.Visible;
            Guests.ItemsSource = l;

        }

        private void Guests_Loaded(object sender, RoutedEventArgs e)
        {
            List<Guest> l = MainWindow.ibl.GetAllGuests();
            var grid = sender as DataGrid;
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            /*foreach (DataGridCheckBoxColumn itemrow in Guests.Columns)
            {
                CheckBox d = (CheckBox)(itemrow.);
                if ((bool)d.IsChecked)
                {
                    try
                    {
                        Order ord = new Order();
                        ord.GuestRequestKey=

                        MainWindow.ibl.AddOrder()
                    }
                }
            }*/
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                // Do not close the window  
            }
        }
    }
}
