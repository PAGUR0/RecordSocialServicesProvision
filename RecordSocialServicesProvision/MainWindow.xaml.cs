using System;
using System.Windows;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static String login;
        private bool admin;
        private MySQLBD mySQLBD = MySQLBD.getInstanse();

        // при создании окна
        // в конструктор передается значения логина, которое сохраняется в локальной переменной
        // а также обращение к методы CreateProfileContent
        public MainWindow(String login)
        {
            MainWindow.login = login;
            InitializeComponent();
            createProfileContent();
        }

        // выводит всю информацию о текущем пользователе
        private void createProfileContent()
        {
            try
            {
                String[] profileContent = mySQLBD.getUserWorker(login);
                LoginText.Text = login;
                NameText.Text = profileContent[0];
                SurnameText.Text = profileContent[1];
                PatronymicText.Text = profileContent[2];
                if (profileContent[3] == "1")
                {
                    admin = true;
                    RoleText.Text = "Администратор";
                }
                else
                {
                    admin = false;
                    RoleText.Text = "Пользователь";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // выводит всю информацию о текущем пользователе
        private void adminBtn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowCMD windowCMD = new WindowCMD();
                if (admin)
                {
                    windowCMD.Show();
                }
                basicBtn.IsChecked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
