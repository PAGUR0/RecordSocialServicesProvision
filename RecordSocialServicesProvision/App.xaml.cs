using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

    }

    public static class Functions
    {
        /// <summary>
        /// Изменение цвета и выравнивания текста при фокусировки на TextBox
        /// </summary>
        public static void InputTextBox_GotFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            textBox.TextAlignment = TextAlignment.Left;
            if (textBox.Text == text)
            {
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["BasicTextColor"];
                textBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Изменение цвета и выравнивания текста при расфокусировки с TextBox
        /// </summary>

        public static void InputTextBox_LostFocus(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.TextAlignment = TextAlignment.Center;
                textBox.Foreground = (SolidColorBrush)Application.Current.Resources["AdditionalTextColor"];
                textBox.Text = text;
            }
        }

    }
}
