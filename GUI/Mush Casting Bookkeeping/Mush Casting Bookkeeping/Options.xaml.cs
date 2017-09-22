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
using System.Windows.Shapes;
using Winforms = System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Mush_Casting_Bookkeeping
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private Winforms.FolderBrowserDialog fbd = new Winforms.FolderBrowserDialog();

        public Options()
        {
            InitializeComponent();

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            TimeZoneDropDown.ItemsSource = timeZones;

            TimeZoneDropDown.SelectedItem = TimeZoneInfo.FindSystemTimeZoneById(Properties.Settings.Default.DefaultTimeZone);

            if (Properties.Settings.Default.DefaultPath == "")
            {
                directoryPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            else
            {
                directoryPath.Text = Properties.Settings.Default.DefaultPath;
            }

            TimeFormatCheckbox.IsChecked = Properties.Settings.Default.ShowTimeIn24hrs;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultTimeZone = ((TimeZoneInfo)TimeZoneDropDown.SelectedItem).Id;
            Properties.Settings.Default.DefaultPath = directoryPath.Text;
            Properties.Settings.Default.ShowTimeIn24hrs = TimeFormatCheckbox.IsChecked.Value;

            Properties.Settings.Default.Save();

            MessageBoxResult result = MessageBox.Show("Save Succesfull!", "", MessageBoxButton.OK);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DefaultTimeZone != ((TimeZoneInfo)TimeZoneDropDown.SelectedItem).Id ||
                Properties.Settings.Default.DefaultPath != directoryPath.Text ||
                Properties.Settings.Default.ShowTimeIn24hrs != TimeFormatCheckbox.IsChecked)
            {
                MessageBoxResult result = MessageBox.Show("You have unsaved changes. Are you certain that you wish to close?", "Unsaved Changes", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            this.Close();
        }

        private void directorySelectBTN_Click(object sender, EventArgs e)
        {
            //error.SetError(directoryPath, "");

            fbd.SelectedPath = directoryPath.Text;

            Winforms.DialogResult result = fbd.ShowDialog();

            if (result == Winforms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                directoryPath.Text = fbd.SelectedPath;
            }
        }
    }
}
