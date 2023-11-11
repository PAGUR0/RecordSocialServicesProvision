using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace RecordSocialServicesProvision.res.view
{
    /// <summary>
    /// Interaction logic for AddApplications.xaml
    /// </summary>
    public partial class AddApplications : UserControl
    { 
        private MySQLBD MySQLBD = new MySQLBD();
        private List<string>[] document = new List<string>[3] { new List<string>(), new List<string>(), new List<string>() };
        public AddApplications()
        {
            InitializeComponent();
            RevertData();
        }

        // Обновление данных
        private void RevertData()
        {
            document = MySQLBD.getDocument();

            TypeDocument.ItemsSource = document[0];
            AdditionalTypeDocument.ItemsSource = document[0];

            Region.ItemsSource = MySQLBD.getRegion();
            AdditionalRegion.ItemsSource = MySQLBD.getRegion();
            FormService.ItemsSource = MySQLBD.getForm();
            Living.ItemsSource = MySQLBD.getLiving();
            WhomOrganizations.ItemsSource = MySQLBD.getSocialOrganizations();
            WhoOrganizations.ItemsSource = MySQLBD.getOrganizations();
        }

        // Проверка ввода даты
        private void DateBirth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParseExact(DateBirth.Text, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                DateBirth.Text = string.Empty;
            }
        }

        private void TypeDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumberDocument.Mask = document[1][TypeDocument.SelectedIndex];
        }

        private void AdditionalTypeDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdditionalNumberDocument.Mask = document[1][AdditionalTypeDocument.SelectedIndex];
        }

        private void AdditionalUser_Checked(object sender, RoutedEventArgs e)
        {
            AdditionalName.IsEnabled = true;
            AdditionalNumberDocument.IsEnabled = true;
            AdditionalRegionSmall.IsEnabled = true;
            AdditionalCity.IsEnabled = true;
            AdditionalStreet.IsEnabled = true;
            AdditionalHome.IsEnabled = true;
            AdditionalApartment.IsEnabled = true;
            AdditionalRegion.IsHitTestVisible = true;
            AdditionalTypeDocument.IsHitTestVisible = true;
        }

        private void AdditionalUser_Unchecked(object sender, RoutedEventArgs e)
        {
            AdditionalName.IsEnabled = false;
            AdditionalNumberDocument.IsEnabled = false;
            AdditionalRegionSmall.IsEnabled = false;
            AdditionalCity.IsEnabled = false;
            AdditionalStreet.IsEnabled = false;
            AdditionalHome.IsEnabled = false;
            AdditionalApartment.IsEnabled = false;
            AdditionalRegion.IsHitTestVisible = false;
            AdditionalTypeDocument.IsHitTestVisible = false;
        }

        private void AddApplication_Click(object sender, RoutedEventArgs e)
        {
            RevertData();
            string snils = Snils.Text;
            if(!Regex.IsMatch(snils, @"\d{3}-\d{3}-\d{3} \d{2}"))
            {
                ErrorText.Text = "Неверно указан снилс";
                return;
            }
            string date = DateBirth.Text;
            if (!Regex.IsMatch(date, @"\d{2}.\d{2}.\d{4}"))
            {
                ErrorText.Text = "Неверено указана дата";
                return;
            }
            int typeDocument = TypeDocument.SelectedIndex;
            string numberDocument = NumberDocument.Text;
            if (!Regex.IsMatch(numberDocument, document[2][typeDocument]))
            {
                ErrorText.Text = "Неверно указан номер документа";
                return;
            }
            string phone = Phone.Text;
            if (!Regex.IsMatch(phone, @"\+7\(\d{3}\) \d{3}-\d{2}-\d{2}"))
            {
                ErrorText.Text = "Неверно указан телефон";
                return;
            }
            string email = Email.Text;
            if (!Regex.IsMatch(email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            {
                ErrorText.Text = "Неверно указан email";
                return;
            }
            try
            {
                MySQLBD.setUser(snils, Name.Text, Surname.Text, Patronymic.Text, date, typeDocument, numberDocument, Region.SelectedIndex, RegionSmall.Text, City.Text, Street.Text, int.Parse(Home.Text), int.Parse(Apartment.Text), phone, email);
            }
            catch
            {
                ErrorText.Text = "Неверно указаны данные заявителя";
                return;
            }
            bool additionalUser = AdditionalUser.IsChecked ?? false;
            string? additionalNumber = null;
            if (additionalUser)
            {
                additionalNumber = AdditionalNumberDocument.Text;
                int addtypeDocument = TypeDocument.SelectedIndex;
                if (!Regex.IsMatch(additionalNumber, document[2][addtypeDocument]))
                {
                    ErrorText.Text = "Неверно указан номер документа";
                    return;
                }
                if (MySQLBD.getAdditionalUser(additionalNumber))
                {
                    try
                    {
                        MySQLBD.setAdditionalUser(additionalNumber, addtypeDocument, AdditionalName.Text, AdditionalRegion.SelectedIndex, AdditionalRegionSmall.Text, AdditionalCity.Text, AdditionalStreet.Text, int.Parse(AdditionalHome.Text), int.Parse(AdditionalApartment.Text));
                    }
                    catch
                    {
                        ErrorText.Text = "Неверно указан адрес дома или квартиры заявителя";
                        return;
                    }
                }
            }
            int whomOrganizations = WhomOrganizations.SelectedIndex;
            if(whomOrganizations == -1 && WhomOrganizations.Text != "")
            {
                MySQLBD.setWhoOrganizations(WhomOrganizations.Text);
                whomOrganizations = WhomOrganizations.Items.Count - 1;
            }
            int whoOrganizations = WhoOrganizations.SelectedIndex;
            if (whoOrganizations == -1 &&  WhoOrganizations.Text != "")
            { 
                MySQLBD.setOrganizations(WhoOrganizations.Text);
                whoOrganizations = WhoOrganizations.Items.Count -1;
            }
            int living = Living.SelectedIndex;
            if (living == -1 && Living.Text != "")
            {
                MySQLBD.setLiving(Living.Text);
                living = Living.Items.Count - 1;
            }
            bool consent = Consent.IsChecked ?? false;
            if(!consent)
            {
                ErrorText.Text = "нет согласия на обработку данных";
                return;
            }
            try
            {
                MySQLBD.setApplication(
                    snils,
                    additionalUser ? 1 : 0,
                    additionalNumber,
                    whomOrganizations,
                    whoOrganizations,
                    FormService.SelectedIndex,
                    Reason.Text,
                    Domestic.IsChecked ?? false ? 1 : 0,
                    Medical.IsChecked ?? false ? 1 : 0,
                    Psyhological.IsChecked ?? false ? 1 : 0,
                    Pedagogical.IsChecked ?? false ? 1 : 0,
                    Labour.IsChecked ?? false ? 1 : 0,
                    Legal.IsChecked ?? false ? 1 : 0,
                    Communication.IsChecked ?? false ? 1 : 0,
                    Urgent.IsChecked ?? false ? 1 : 0,
                    Famaly.Text,
                    living,
                    int.Parse(Income.Text),
                    consent ? 1:0,
                    DateTime.Today,
                    MainWindow.login);
                ErrorText.Text = "Подано";
            } catch
            {
                ErrorText.Text = "Некоректно введены данные";
            }
        }

        private void Snils_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] user = MySQLBD.getUser(Snils.Text);
            if(user[0] != null)
            {
                Name.Text = user[1];
                Surname.Text = user[2];
                Patronymic.Text = user[3];
                DateBirth.Text = DateTime.Parse(user[4].ToString()).ToString("dd.MM.yyyy");
                TypeDocument.SelectedIndex = int.Parse(user[5]);
                NumberDocument.Text = user[6];
                Region.SelectedIndex = int.Parse(user[7]);
                RegionSmall.Text = user[8];
                City.Text = user[9];
                Street.Text = user[10];
                Home.Text = user[11];
                Apartment.Text = user[12];
                Phone.Text = user[13];
                Email.Text = user[14];
            }
        }

        private void AdditionalNumberDocument_LostFocus(object sender, RoutedEventArgs e)
        {
            string[] userAdd = MySQLBD.getUserAdd(AdditionalNumberDocument.Text);
            if (userAdd[0] != null)
            {
                AdditionalName.Text = userAdd[0];
                AdditionalTypeDocument.SelectedIndex = int.Parse(userAdd[1]);
                AdditionalNumberDocument.Text = userAdd[2];
                AdditionalRegion.SelectedIndex = int.Parse(userAdd[3]);
                AdditionalRegionSmall.Text = userAdd[4];
                AdditionalCity.Text = userAdd[5];
                AdditionalStreet.Text = userAdd[6];
                AdditionalHome.Text = userAdd[7];
                AdditionalApartment.Text = userAdd[8];
            }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
