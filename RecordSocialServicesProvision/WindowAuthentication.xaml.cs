using System;
using System.Windows;
using System.Windows.Media;

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
        // При нажатии Button ButtonAuthentication, проверяется введенные логин и пароль запросом к таблице БД для их хрениния
        // Если значения не соответствуют содержимому таблицы выводится ошибка. Иначе открывается окно MainWindow и закрывается текущее
        private void ButtonAuthentication_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = mySqlBD.getPassword(Login.Text);
                if (password != " " && password == Password.Password)
                {
                    (new MainWindow(Login.Text)).Show();
                    this.Close();
                }
                else
                {
                    TextError.Text = "Неверно указан логин или пароль";
                }
            }catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // При фокусировке на TextBox Login, изменяется цвет содержимого TextBox через InputTextBox_GotFocus
        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Login.TextAlignment = TextAlignment.Left;
                if (Login.Text == "Логин")
                {
                    Login.Foreground = (SolidColorBrush)System.Windows.Application.Current.Resources["BasicTextColor"];
                    Login.Text = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // При расфокусировке с TextBox Login, изменяется цвет содержимого TextBox через InputTextBox_LostFocus 
        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text == string.Empty)
                {
                    Login.TextAlignment = TextAlignment.Center;
                    Login.Foreground = (SolidColorBrush)System.Windows.Application.Current.Resources["AdditionalTextColor"];
                    Login.Text = "Логин";
                }
            }catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // При фокусировке на Password, скрывается TextBlock PasswordText
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Hidden;
        }

        // При расфокусировке с PasswordBox Password, показывает TextBlock PasswordText, если Password не заполнено
        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Password.Password == "")
            {
                PasswordText.Visibility = Visibility.Visible;
            }
        }
    }
}