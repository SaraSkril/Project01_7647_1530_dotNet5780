using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
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
    /// Interaction logic for Mail.xaml
    /// </summary>
    public partial class Mail : Window
    {
        int guestkey;
        string id;
        Guest gu;
        HostingUnit hu;
        string text;
        string pic;

        public Mail()
        {
            InitializeComponent();
            starttextbox();
        }
        public Mail(int Guestkey, HostingUnit hU)
        {
            InitializeComponent();
            guestkey = Guestkey;
            hu = hU;
            starttextbox();
        }
        private void starttextbox()
        {
            
            foreach(Guest g in MainWindow.ibl.GetAllGuests())
            {
                if (g.GuestRequestKey == guestkey)
                    gu = g;
            }

            string t = "Hello " + gu.FirstName + "," + "\n";
            t += "Thank you for visiting Vakantie!\n";
            t += "My Hosting Unit - " + hu.HostingUnitName + "  fills your requirements and I'd be more than glad to host you." + "\n";
            t += "Please contact me at: " + hu.Owner.EmailAddress + " to follow up with your order." + "\n";
            t += "All the best, " + "\n";
            t+=hu.Owner.FirstName + " " + hu.Owner.LastName;
            main.Text = t;
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string s= "";
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                s = op.FileName;
            }
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
