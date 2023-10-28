using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    public class Functions
    {

        public static void InputTextBox_GotFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == text)
            {
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["BasicTextColor"];
                textBox.Text = string.Empty;
            }
        }

        public static void InputTextBox_LostFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["AdditionalTextColor"];
                textBox.Text = text;
            }
        }

    }
}
