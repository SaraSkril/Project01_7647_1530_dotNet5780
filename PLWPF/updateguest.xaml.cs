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
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for updateguest.xaml
    /// </summary>
    public partial class updateguest : Window
    {
        Guest g;
        public updateguest()
        {
            InitializeComponent();
        }
        public updateguest(string id)
        {
            InitializeComponent();
            g = MainWindow.ibl.GetGuest(id);
            this.Resort.ItemsSource = Enum.GetValues(typeof(BE.TypeUnit));
            this.Area.ItemsSource = Enum.GetValues(typeof(BE.Area));
            this.Pool.ItemsSource = Enum.GetValues(typeof(BE.Pool));
            this.Jaccuzi.ItemsSource = Enum.GetValues(typeof(BE.Jacuzzi));
            this.Garden.ItemsSource = Enum.GetValues(typeof(BE.Garden));
            this.childAtt.ItemsSource = Enum.GetValues(typeof(BE.ChildrensAttractions));
            this.Wifi.ItemsSource = Enum.GetValues(typeof(BE.Wifi));

            setAllDetails();
            

        }
        private void setAllDetails()
        {
            DateTime d = new DateTime(g.ReleaseDate.Year, g.ReleaseDate.Month, g.ReleaseDate.Day);
            ID.Content = g.ID;
            fname.Text = g.FirstName;
            lname.Text = g.LastName;
            email.Text = g.EmailAddress;
            status.Content = g.GuestStatus;
            Resort.Text = g.TypeUnit.ToString();
            regdate.Content = g.RegistrationDate.ToString();
            edate.SelectedDate = g.EntryDate;
            edate.DisplayDate = g.EntryDate;
            rdate.SelectedDate = d;
            rdate.DisplayDate = d;
            Area.Text = g.Area.ToString();
            Adult.Text = g.Adults.ToString();
            Child.Text = g.Children.ToString();
            Jaccuzi.Text = g.Jacuzzi.ToString();
            Pool.Text = g.Pool.ToString();
            Garden.Text = g.Garden.ToString();
            childAtt.Text = g.ChildrensAttractions.ToString();
            Wifi.Text = g.Wifi.ToString();

        }
        private void Button_Click_MinimizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
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

        private void Button_Click_MaximizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)Brushes.Red;
            ((Button)sender).Width *= 1.1;
            ((Button)sender).Height *= 1.1;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
