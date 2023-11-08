using Microsoft.Win32;
using System;
using System.Threading;
using System.Timers;
using System.Windows;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String login;
        private MySQLBD mySQLBD = MySQLBD.getInstanse();
        WindowCMD windowCMD = new WindowCMD();
        private bool admin;

        public MainWindow(String login)
        {
            this.login = login;

            InitializeComponent();
            createProfileContent();
        }

        private void createProfileContent()
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

        private void adminBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (admin)
            {
                windowCMD.Show();
            }
            basicBtn.IsChecked = true;
        }
    }
}
