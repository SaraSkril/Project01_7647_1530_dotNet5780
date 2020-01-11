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
    /// Interaction logic for HostLogin.xaml
    /// </summary>
    public partial class HostLogin : Window
    {
        public HostLogin()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void button1_Click_Login_Host(object sender, RoutedEventArgs e)
        {
            if (Idtextbox.Text.Length != 9)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            int number = 0;

            if (int.TryParse(Idtextbox.Text, out number) == false)
            {
                MessageBox.Show("Invalid ID!");
                return;
            }
            else
                if (MainWindow.ibl.checkifHost(Idtextbox.Text) == false)
            {
                MessageBox.Show("This Id does not exist!");
                return;
            }
            Close();
           // add neupdate host


        }

        private void button2_Click_addnewHost(object sender, RoutedEventArgs e)
        {
            Close();
            new NewHost().ShowDialog();
            //add new window
        }
    }
}
