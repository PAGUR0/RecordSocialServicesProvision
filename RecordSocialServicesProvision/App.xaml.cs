using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using MySql.Data.MySqlClient;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string connStr = "server=localhost;user=root;database=socialservices;password=12345678;";
        public static MySqlConnection conn = new MySqlConnection(connStr);
    }

    public class Functions
    {
        // Изменение цвета и выравнивания текста при выборе TextBox
        public static void InputTextBox_GotFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            textBox.TextAlignment = TextAlignment.Left;
            if (textBox.Text == text)
            {
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["BasicTextColor"];
                textBox.Text = string.Empty;
            }
        }

        public static void InputTextBox_LostFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            textBox.TextAlignment = TextAlignment.Center;
            if (textBox.Text == string.Empty)
            {
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["AdditionalTextColor"];
                textBox.Text = text;
            }
        }
    }
}
