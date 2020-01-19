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
    /// Interaction logic for UpdateHost.xaml
    /// </summary>
    public partial class UpdateHost : Window
    {
        Host h = new Host();
        public UpdateHost()
        {
            InitializeComponent();
        }
        public UpdateHost(string id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            h = MainWindow.ibl.FindHost(id);
            SetAll();
            

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

        public void SetAll()
        {
            ID.Content = h.ID;
            First_Name.Text = h.FirstName;
            Last_Name.Text = h.LastName;
            Email.Text = h.EmailAddress;
            number.Text = h.PhoneNumber.ToString();
            if (h.CollectionClearance == CollectionClearance.Yes)
                Yes.IsChecked = true;
            else
                No.IsChecked = true;
            {
                //bankdetails
            }

        }

        private void Update_Host_Click(object sender, RoutedEventArgs e)
        {
            int number1;
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

            if (int.TryParse(number.Text, out number1) == false)
            {

                MessageBox.Show("Invalid Number!");
                return;
            }
            //check if 9/10 digit
            if (Yes.IsChecked == false && No.IsChecked == false)
            {

                MessageBox.Show("Please select Automatic billing");
                return;
            }
            h.FirstName = First_Name.Text;
            h.LastName = Last_Name.Text;
            h.PhoneNumber = int.Parse(number.Text);
            if (Yes.IsChecked == true)
                h.CollectionClearance = CollectionClearance.Yes;
            else
                h.CollectionClearance = CollectionClearance.No;
            h.EmailAddress = Email.Text;
            {
                h.BankAccountNumber = 11111;
                BankAccount b = new BankAccount();
                b.BankName = "bbb";
                b.BankNumber = 12;
                b.BranchAddress = "bbb";
                b.BranchCity = "bbb";
                b.BranchNumber = 2;
                h.BankDetails = b;
            }

            //bank
            try
            {
                MainWindow.ibl.UpdateHost(h);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Host " + First_Name.Text + " Was Updated Succefully!");
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to leave?\n Your changes will not be saved!", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
                new hostprop(h.ID).ShowDialog();
            }
            else
            {
                // Do not close the window  
            }
        }
    }
}
