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
        private int _numberOfDays;
        private DateTime _startDate;
        private DateTime _endDate;

        public string NumberOfDays { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

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

            masterGrid.DataContext = this;
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
                bool isValid;
                List<string> errors;
                List<CastingDataInstances> data = Validate(out isValid, out errors);

                if (!isValid)
                {
                    MessageBox.Show(String.Join("\n", errors), "Error");
                    return;
                }

                Results frm = new Results((TimeZoneInfo)TimeZoneDropDown.SelectedItem, TimeFormatCheckbox.IsChecked.Value);
                if (analyzeByDaysRadio.IsChecked.Value)
                {
                    frm.AnalyzeData(Int32.Parse(NumberOfDays), ref data);
                }

                else
                {
                    frm.AnalyzeData
                        (
                        _startDate,
                        _endDate,
                        ref data
                        );
                }                   
                    frm.ShowDialog();
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

        private List<CastingDataInstances> LoadData(ref bool isValid, ref List<string> errors)
        {
            string[] files;
            List<CastingDataInstances> dataInstances = new List<CastingDataInstances>();

            try
            {
                files = Directory.GetFiles(directoryPath.Text, "*", SearchOption.AllDirectories);
            }

            catch (Exception ex)
            {
                errors.Add(ex.Message);
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
                errors.Add("Unable to parse one or more files");
                isValid = false;
                return dataInstances;
            }

            string castingName = dataInstances[0].CastingName;

            foreach (CastingDataInstances instance in dataInstances)
            {
                if (castingName != instance.CastingName)
                {
                    errors.Add("Multiple Castings were found in this directory");
                    isValid = false;
                    return dataInstances;
                }
            }
            
            return dataInstances;
        }

        private List<CastingDataInstances> Validate(out bool isValid, out List<string> errors)
        {
            isValid = true;
            errors = new List<string>();

            if (analyzeByDaysRadio.IsChecked.Value)
            {
                try
                {
                    _numberOfDays = Int32.Parse(NumberOfDays);
                    if (_numberOfDays < 1)
                    {
                        errors.Add("NumberOfDays must be greater than zero.");
                        isValid = false;
                    }
                }

                catch
                {
                    errors.Add("Number Of Days must be an integer.");
                    isValid = false;
                }
            }
            
            else
            {
                try
                {
                    if (StartDate == null || EndDate == null)
                    {
                        throw new Exception("Date(s) cannot be null");
                    }
                    _startDate = Convert.ToDateTime(StartDate);
                    _endDate = Convert.ToDateTime(EndDate);

                    if (endDatePicker.SelectedDate.Value < startDatePicker.SelectedDate.Value)
                    {
                        errors.Add("End Date must be after start date.");
                        isValid = false;
                    }
                }

                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    isValid = false;
                }
            }
            
            List<CastingDataInstances> data = LoadData(ref isValid, ref errors);
            return data;
        }
    }
}
