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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String login;
        private MySQLBD mySQLBD = MySQLBD.getInstanse();
        private bool admin;

        public MainWindow(String login)
        {
            this.login = login;

            InitializeComponent();
            createProfileContent();
        }

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {

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
    }
}
