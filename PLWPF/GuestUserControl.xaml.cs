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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestUserControl.xaml
    /// </summary>
    public partial class GuestUserControl : UserControl
    {
        public GuestUserControl()
        {
            InitializeComponent();
        }
        Guest guest;
        private void Add_Guest(object sender, RoutedEventArgs e)
        {
            if (ID.Text.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
            }
            int number = 0;

            if (int.TryParse(ID.Text, out number) == false)
            {
                
                MessageBox.Show("Invalid ID!");

            }
            if (First_Name.Text == "")
            {
                First_Name.BorderBrush = Brushes.Red;
               
            }
            if (Last_Name.Text == "")
                Last_Name.BorderBrush = Brushes.Red;
            if (Email.Text == "")
                Email.BorderBrush = Brushes.Red;
            if (MainWindow.ibl.checkEmail(Email.Text) == false)
                MessageBox.Show("Invalid Email");
            if (DatePicker_Entry.SelectedDate == null)
                MessageBox.Show("No date selected!");
            if (DatePicker_Release.SelectedDate == null)
                MessageBox.Show("No date selected!");
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Guest: " + First_Name + "was added!");
            }
           
        }
    }
}
