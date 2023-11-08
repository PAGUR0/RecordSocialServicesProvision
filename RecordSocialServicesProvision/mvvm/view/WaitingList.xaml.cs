using MySql.Data.MySqlClient;
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

namespace RecordSocialServicesProvision.res.view
{
    /// <summary>
    /// Interaction logic for WaitingList.xaml
    /// </summary>
    public partial class WaitingList : UserControl
    {
        MySQLBD mySQLBD = new MySQLBD();
        public WaitingList()
        {
            InitializeComponent();
            DataApplications.ItemsSource = mySQLBD.getDataAdapter().DefaultView;
        }

        private void getData()
        {
            
        }
    }
}
