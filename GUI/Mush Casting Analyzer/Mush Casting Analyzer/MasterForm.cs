using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace Mush_Casting_Analyzer
{
    public partial class MasterForm : Form
    {
        private ErrorProvider error = new ErrorProvider();
        private int NumberOfDays;
        private FolderBrowserDialog fbd = new FolderBrowserDialog();

        public MasterForm()
        {
            InitializeComponent();

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

            TimeZoneDropDown.DataSource = timeZones;

            TimeZoneDropDown.SelectedItem = TimeZoneInfo.FindSystemTimeZoneById(Properties.Settings.Default.DefaultTimeZone);

            if (Properties.Settings.Default.DefaultPath == "")
            {
                directoryPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            else
            {
                directoryPath.Text = Properties.Settings.Default.DefaultPath;
            }
        }

        private void directorySelectBTN_Click(object sender, EventArgs e)
        {
            error.SetError(directoryPath, "");

            fbd.SelectedPath = directoryPath.Text;

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                directoryPath.Text = fbd.SelectedPath;
            }
        }

        private void analyzeFilesBTN_Click(object sender, EventArgs e)
        {
            error.SetError(endDatePicker, "");
            error.SetError(NumberOfDaysEntry, "");

            if (!validate())
            {
                return;
            }

            bool isValid = true;

            List<CastingDataInstances> DataInstances = LoadData(out isValid);

            if (!isValid)
            {
                return;
            }

            Results frm = new Results((TimeZoneInfo)TimeZoneDropDown.SelectedItem);

            if (analyzeByDaysRadio.Checked)
            {
                frm.AnalyzeData(NumberOfDays, ref DataInstances);
            }

            else
            {
                frm.AnalyzeData(
                    (long)(startDatePicker.Value - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    (long)(endDatePicker.Value - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    ref DataInstances
                    );
            }

            frm.ShowDialog();
        }

        private void analyzeByDaysRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (analyzeByDaysRadio.Checked)
            {
                NumberOfDaysPanel.Enabled = true;
                IntervalPanel.Enabled = false;
            }

            else
            {
                NumberOfDaysPanel.Enabled = false;
                IntervalPanel.Enabled = true;
            }
        }

        private bool validate()
        {
            bool isValid = true;
            if (directoryPath.Text == "")
            {
                error.SetError(directoryPath, "Required");
                isValid = false;
            }

            if (analyzeByDaysRadio.Checked)
            {
                if (NumberOfDaysEntry.Value < 1)
                {
                    error.SetError(NumberOfDaysEntry, "Number of Days must be positive");
                    isValid = false;
                }

                if (!Int32.TryParse(NumberOfDaysEntry.Value.ToString(), out NumberOfDays))
                {
                    error.SetError(NumberOfDaysEntry, "Number of Days must be an integer");
                    isValid = false;
                }
            }

            else
            {
                if (startDatePicker.Value > endDatePicker.Value)
                {
                    error.SetError(endDatePicker, "End date cannot be before the start date");
                    isValid = false;
                }
            }

            return isValid;
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
                error.SetError(directoryPath, ex.Message);
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
                error.SetError(directoryPath, "Unable to parse one or more files");
                isValid = false;
                return dataInstances;
            }

            string castingName = dataInstances[0].CastingName;

            foreach (CastingDataInstances instance in dataInstances)
            {
                if (castingName != instance.CastingName)
                {
                    error.SetError(directoryPath, "Multiple Castings were found in this directory");
                    isValid = false;
                    return dataInstances;
                }
            }

            isValid = true;
            return dataInstances;
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            OptionsForm frm = new OptionsForm();
            frm.ShowDialog();
        }
    }
}
