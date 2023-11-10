
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace RecordSocialServicesProvision.res.view
{
    /// <summary>
    /// Interaction logic for AllApplications.xaml
    /// </summary>
    public partial class AllApplications : UserControl
    {
        MySQLBD mySQLBD = new MySQLBD();
        public AllApplications()
        {
            InitializeComponent();
            DataApplications.ItemsSource = mySQLBD.getDataAdapter().DefaultView;
        }

    }
}
