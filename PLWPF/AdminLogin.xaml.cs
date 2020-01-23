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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Password == "" || name.Text == "")
            {
                MessageBox.Show("Please Fill Everything");
                return;
            }

            if (pass.Password == "1111" && name.Text == "vakantie")
            {
                Close();
                new Admin().ShowDialog();
            }
            else

                MessageBox.Show("Incorrect User or password");

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
