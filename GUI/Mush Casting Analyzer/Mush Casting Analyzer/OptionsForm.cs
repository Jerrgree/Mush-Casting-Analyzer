using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Mush_Casting_Analyzer
{
    public partial class OptionsForm : Form
    {
        private FolderBrowserDialog fbd = new FolderBrowserDialog();
        private ErrorProvider error = new ErrorProvider();

        public OptionsForm()
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

            TimeFormatCheckbox.Checked = Properties.Settings.Default.ShowTimeIn24hrs;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultTimeZone = ((TimeZoneInfo)TimeZoneDropDown.SelectedItem).Id;
            Properties.Settings.Default.DefaultPath = directoryPath.Text;
            Properties.Settings.Default.ShowTimeIn24hrs = TimeFormatCheckbox.Checked;

            Properties.Settings.Default.Save();

            DialogResult result = MessageBox.Show("Save Succesfull!", "", MessageBoxButtons.OK);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DefaultTimeZone != ((TimeZoneInfo)TimeZoneDropDown.SelectedItem).Id ||
                Properties.Settings.Default.DefaultPath != directoryPath.Text ||
                Properties.Settings.Default.ShowTimeIn24hrs != TimeFormatCheckbox.Checked)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes. Are you certain that you wish to close?", "Unsaved Changes", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            this.Close();
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
    }
}
