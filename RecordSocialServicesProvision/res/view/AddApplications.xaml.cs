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
using Xceed.Wpf.Toolkit.Primitives;

namespace RecordSocialServicesProvision.res.view
{
    /// <summary>
    /// Interaction logic for AddApplications.xaml
    /// </summary>
    public partial class AddApplications : UserControl
    {
        private MySQLBD MySQLBD = new MySQLBD();
        private List<string>[] document = new List<string>[2] { new List<string>(), new List<string>() };
        public AddApplications()
        {
            InitializeComponent();

            document = MySQLBD.getDocument();
            TypeDocument.ItemsSource = document[0];
            Region.ItemsSource = MySQLBD.getRegion();
        }

        private void DateBirth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParseExact(DateBirth.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                DateBirth.Text = string.Empty;
            }
        }

        private void TypeDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumberDocument.Mask = document[1][TypeDocument.SelectedIndex];
        }
    }
}
