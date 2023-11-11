using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RecordSocialServicesProvision
{
    /// <summary>
    /// Interaction logic for WindowCMD.xaml
    /// </summary>
    /// 
    public partial class WindowCMD : Window
    {
        private MySQLBD MySQLBD = MySQLBD.getInstanse();
        public WindowCMD()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Left && e.Key != Key.Right && e.Key != Key.Up && e.Key != Key.Down)
            {
                textBox.SelectionStart = textBox.Text.Length;
            }
            if(e.Key == Key.Enter)
            {
                string[] lines = textBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                if (lines.Length > 0)
                {
                    textBox.AppendText("\n" + MySQLBD.ExecuteSqlQuery(lines[lines.Length-1].Substring(1)));
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var lines = textBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].StartsWith(">"))
                {
                    lines[i] = ">" + lines[i];
                }
            }
            textBox.Text = string.Join(Environment.NewLine, lines);
        }


    }
}
