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
        private MySQLBD mySqlBD = MySQLBD.getInstanse();

        public WindowAuthentication()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При нажатии Button ButtonAuthentication, проверяется введенные логин и пароль запросом к таблице БД для их хрениния
        /// Если значения не соответствуют содержимому таблицы выводится ошибка. Иначе открывается окно MainWindow и закрывается текущее
        /// </summary>
        private void ButtonAuthentication_Click(object sender, RoutedEventArgs e)
        {
            string password = mySqlBD.getPassword(Login.Text);
            if (password != " " && password == Password.Password)
            {
                MainWindow mainWindow = new MainWindow(Login.Text);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                TextError.Text = "Неверно указан логин или пароль";
            }
        }

        /// <summary>
        /// При фокусировке на TextBox Login, изменяется цвет содержимого TextBox через InputTextBox_GotFocus
        /// </summary>
        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_GotFocus(sender, "Логин");
        }

        /// <summary>
        /// При расфокусировке с TextBox Login, изменяется цвет содержимого TextBox через InputTextBox_LostFocus 
        /// </summary>
        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Functions.InputTextBox_LostFocus(sender, "Логин");
        }

        /// <summary>
        /// При фокусировке на Password, скрывается TextBlock PasswordText
        /// </summary>
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// При расфокусировке с PasswordBox Password, показывает TextBlock PasswordText, если Password не заполнено
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
