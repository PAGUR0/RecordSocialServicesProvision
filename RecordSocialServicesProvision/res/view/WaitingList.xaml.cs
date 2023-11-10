using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;


namespace RecordSocialServicesProvision.res.view
{
    /// <summary>
    /// Interaction logic for WaitingList.xaml
    /// </summary>
    public partial class WaitingList : UserControl
    {
        MySQLBD MySQLBD = new MySQLBD();
        public WaitingList()
        {
            InitializeComponent();
            getData();
        }
        private void getData()
        {
            String[] user = MySQLBD.getDataApplication();
            ID.Text = user[0];
            Name.Text = user[1];
            Surname.Text = user[2];
            Patronymic.Text = user[3];
            DateBirth.Text = user[4];
            TypeDocument.Text = user[5];
            NumberDocument.Text = user[6];
            Region.Text = user[7];
            RegionSmall.Text = user[8];
            City.Text = user[9];
            Street.Text = user[10];
            Home.Text = user[11];
            Apartment.Text = user[12];
            Phone.Text = user[13];
            Email.Text = user[14];
            AdditionalUser.IsChecked = bool.Parse(user[15] ?? "false");
            AdditionalName.Text = user[16];
            AdditionalTypeDocument.Text = user[17];
            AdditionalNumberDocument.Text = user[18];
            AdditionalRegion.Text = user[19];
            AdditionalRegionSmall.Text = user[20];
            AdditionalCity.Text = user[21];
            AdditionalStreet.Text = user[22];
            AdditionalHome.Text = user[23];
            AdditionalApartment.Text = user[24];
            WhomOrganizations.Text = user[25];
            WhoOrganizations.Text = user[26];
            FormService.Text = user[27];
            Reason.Text = user[28];
            Domestic.IsChecked = bool.Parse(user[29] ?? "false");
            Medical.IsChecked = bool.Parse(user[30] ?? "false");
            Psyhological.IsChecked = bool.Parse(user[31] ?? "false");
            Pedagogical.IsChecked = bool.Parse(user[32] ?? "false");
            Labour.IsChecked = bool.Parse(user[33] ?? "false");
            Legal.IsChecked = bool.Parse(user[34] ?? "false");
            Urgent.IsChecked = bool.Parse(user[35] ?? "false");
            Communication.IsChecked = bool.Parse(user[36] ?? "false");
            Famaly.Text = user[37];
            Living.Text = user[38];
            Income.Text = user[39];
            Consent.IsChecked = bool.Parse(user[40] ?? "false");
            Date.Text = user[41];
            UserWork.Text = user[42];
            Snils.Text = user[43];
        }
    }
}
