using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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

        /// <summary>
        /// Обработка нажатия кнопки "Войти"
        /// </summary>

        private void ButtonAuthentication_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).connect.Open();
            string passwordSQL = "SELECT Password FROM user_worker WHERE login = '" + Login.Text + "'";
            MySqlCommand passwordCommand = new MySqlCommand(passwordSQL, ((App)Application.Current).connect);
            object password = passwordCommand.ExecuteScalar();
            if (password == null)
            {
                TextError.Text = "Неверно указан логин";
            }
            else if(password.ToString() != Password.Password)
            {
                TextError.Text = "Неверно указан пароль";
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                ((App)Application.Current).connect.Close();
                this.Close();
            }
            ((App)Application.Current).connect.Close();
        }

        /// <summary>
        /// Фокусировка на TextBox Login
        /// Обращение к методу InputTextBox_GotFocus для изменения цвета вводимого текста
        /// </summary>

        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_GotFocus(sender, "Логин");
        }

        /// <summary>
        /// Расфокусировка с TextBox Login
        /// Обращение к методу InputTextBox_LostFocus для изменения цвета введенного текста
        /// </summary>

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_LostFocus(sender, "Логин");
        }

        /// <summary>
        /// Фокусировка на Password
        /// Скрывается TextBlock 
        /// </summary>

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Расфокусировка с Password
        /// 
        /// </summary>

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Password.Password == "")
            {
                PasswordText.Visibility = Visibility.Visible;
            }
        }
    }
}
