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
    /// Interaction logic for AddGuests.xaml
    /// </summary>
    public partial class AddGuests : Window
    {
        public AddGuests()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Resort.ItemsSource = Enum.GetValues(typeof(BE.TypeUnit));
            this.Area.ItemsSource = Enum.GetValues(typeof(BE.Area));
            this.Pool.ItemsSource = Enum.GetValues(typeof(BE.Pool));
            this.Jaccuzi.ItemsSource = Enum.GetValues(typeof(BE.Jacuzzi));
            this.Garden.ItemsSource = Enum.GetValues(typeof(BE.Garden));
            this.Children_att.ItemsSource = Enum.GetValues(typeof(BE.ChildrensAttractions));
            this.Wifi.ItemsSource = Enum.GetValues(typeof(BE.Wifi));

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width /= 1.1;
            ((Button)sender).Height /= 1.1;

        }

        private void Button_MouseEnter_RED(object sender, MouseEventArgs e)
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
        private void Button_Click_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(this);
            else
                SystemCommands.MaximizeWindow(this);

        }

        private void Button_Click_MinimizeWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
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

        private void Add_Guest(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest();


            if (ID.Text.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }

            int number = 0;

            if (int.TryParse(ID.Text, out number) == false)
            {

                MessageBox.Show("Invalid ID!");
                return;
            }
            if (First_Name.Text == "")
            {
                First_Name.BorderBrush = Brushes.Red;
                return;
            }
            if (Last_Name.Text == "")
            {
                Last_Name.BorderBrush = Brushes.Red;
                return;
            }
            if (Email.Text == "")
            {
                Email.BorderBrush = Brushes.Red;
                return;
            }
            if (MainWindow.ibl.checkEmail(Email.Text) == false)
            {
                MessageBox.Show("Invalid Email");
                return;
            }
            if (DatePicker_Entry.SelectedDate == null)
            {
                MessageBox.Show("No date selected!");
                return;
            }

            if (DatePicker_Release.SelectedDate == null)
            {
                MessageBox.Show("No date selected!");
                return;
            }
            if (Area.SelectedItem != null && Resort.SelectedItem != null && Adult.Text != "" && Pool.SelectedItem != null
                && Jaccuzi.SelectedItem != null && Garden.SelectedItem != null && Children_att.SelectedItem != null && Wifi.SelectedItem != null)
            {
                guest.ID = ID.Text;
                guest.FirstName = First_Name.Text;
                guest.LastName = Last_Name.Text;
                guest.RegistrationDate = DateTime.Now;
                guest.EntryDate = DatePicker_Entry.SelectedDate.Value;
                guest.ReleaseDate = DatePicker_Release.SelectedDate.Value;
                guest.Adults = int.Parse(Adult.Text);
                if (Children.Text == "")
                    guest.Children = 0;
                else
                    guest.Children = int.Parse(Children.Text);
                guest.EmailAddress = Email.Text;
                guest.ChildrensAttractions = (ChildrensAttractions)Children_att.SelectedItem;
                guest.Area = (Area)Area.SelectedItem;
                guest.Garden = (Garden)Garden.SelectedItem;
                guest.Jacuzzi = (Jacuzzi)Jaccuzi.SelectedItem;
                guest.Pool = (Pool)Pool.SelectedItem;
                guest.Wifi = (Wifi)Wifi.SelectedItem;
                guest.TypeUnit = (TypeUnit)Resort.SelectedItem;

                try
                {
                    MainWindow.ibl.AddGuestReq(guest);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show("Guest: " + First_Name.Text + " was added succesfully!");
                ID.Text = "";
                First_Name.Text = "";
                Last_Name.Text = "";
                Email.Text = "";
                DatePicker_Entry.SelectedDate = null;
                DatePicker_Entry.DisplayDate = DateTime.Now;
                DatePicker_Release.SelectedDate = null;
                DatePicker_Release.DisplayDate = DateTime.Now;
                Resort.Text = "Choose Resort";
                Pool.Text = "Are You Intrested?";
                Jaccuzi.Text = "Are You Intrested?";
                Area.Text = "Choose Area";
                Garden.Text = "Are You Intrested?";
                Wifi.Text = "Are You Intrested?";
                Children_att.Text = "Are You Intrested?";
                Adult.Text = "";
                Children.Text = "";
            }
        }
    }
}
