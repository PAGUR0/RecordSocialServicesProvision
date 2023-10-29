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

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for WindowAuthentication.xaml
    /// </summary>
    public partial class WindowAuthentication : Window
    {
        public WindowAuthentication()
        {
            InitializeComponent();
        }

        private void ButtonAuthentication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_GotFocus(sender, "Логин");
        }

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_LostFocus(sender, "Логин");
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Hidden;
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Password.Password == "")
            {
                PasswordText.Visibility = Visibility.Visible;
            }
        }
    }
}
