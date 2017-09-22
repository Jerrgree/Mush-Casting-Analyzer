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
using Winform = System.Windows.Forms;
using System.Collections.ObjectModel;
using System.IO;

namespace Mush_Casting_Bookkeeping
{
    public partial class MainWindow : Window
    {
        private int NumberOfDays;
        private Winform.FolderBrowserDialog fbd = new Winform.FolderBrowserDialog();

        public MainWindow()
        {
            InitializeComponent();
            analyzeByDaysRadio.IsChecked = true;

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

        private void directorySelectBTN_Click(object sender, RoutedEventArgs e)
        {
            fbd.SelectedPath = directoryPath.Text;

            Winform.DialogResult result = fbd.ShowDialog();

            if (result == Winform.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                directoryPath.Text = fbd.SelectedPath;
            }

        }

        private void analyzeByDaysRadio_Checked(object sender, RoutedEventArgs e)
        {
            analyzeByDaysPanel.IsEnabled = true;
            analyzeByTimespanPanel.IsEnabled = false;
        }

        private void analyzeByTimeSpanRadio_Checked(object sender, RoutedEventArgs e)
        {
            analyzeByDaysPanel.IsEnabled = false;
            analyzeByTimespanPanel.IsEnabled = true;
        }

        private void analyzeBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NumberOfDays = Int32.Parse(NumberOfDaysEntry.Text);
                bool isValid;
                List<CastingDataInstances> data = LoadData(out isValid);

                if (isValid)
                {
                    Results frm = new Results((TimeZoneInfo)TimeZoneDropDown.SelectedItem, TimeFormatCheckbox.IsChecked.Value);
                    frm.AnalyzeData(NumberOfDays, ref data);
                    frm.ShowDialog();
                }
            }

            catch
            {
                
            }
        }

        private void optionsBTN_Click(object sender, RoutedEventArgs e)
        {
            Options option = new Mush_Casting_Bookkeeping.Options();
            option.ShowDialog();
        }

        private List<CastingDataInstances> LoadData(out bool isValid)
        {
            string[] files;
            List<CastingDataInstances> dataInstances = new List<CastingDataInstances>();

            try
            {
                files = Directory.GetFiles(directoryPath.Text, "*", SearchOption.AllDirectories);
            }

            catch (Exception ex)
            {
                //error.SetError(directoryPath, ex.Message);
                isValid = false;
                return dataInstances;
            }

            try
            {
                foreach (string file in files)
                {
                    dataInstances.Add(new CastingDataInstances(file));
                }
            }
            catch (Exception ex)
            {
                //error.SetError(directoryPath, "Unable to parse one or more files");
                isValid = false;
                return dataInstances;
            }

            string castingName = dataInstances[0].CastingName;

            foreach (CastingDataInstances instance in dataInstances)
            {
                if (castingName != instance.CastingName)
                {
                    //error.SetError(directoryPath, "Multiple Castings were found in this directory");
                    isValid = false;
                    return dataInstances;
                }
            }

            isValid = true;
            return dataInstances;
        }
    }
}
