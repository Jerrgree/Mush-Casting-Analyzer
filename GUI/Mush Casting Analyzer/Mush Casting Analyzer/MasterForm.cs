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

namespace Mush_Casting_Analyzer
{
    public partial class MasterForm : Form
    {
        private ErrorProvider error = new ErrorProvider();

        public MasterForm()
        {
            InitializeComponent();
        }

        private void directorySelectBTN_Click(object sender, EventArgs e)
        {
            error.SetError(directoryPath, "");
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                directoryPath.Text = fbd.SelectedPath;
            }
        }

        private void analyzeFilesBTN_Click(object sender, EventArgs e)
        {
            if (directoryPath.Text == "")
            {
                error.SetError(directoryPath, "Required");
                return;
            }
            string[] files;

            try
            {
                files = Directory.GetFiles(directoryPath.Text, "*", SearchOption.AllDirectories);
            }

            catch (Exception ex)
            {
                error.SetError(directoryPath, ex.Message);
                return;
            }

            // UTC Now in milliseconds since epoch
            long now = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;

            List<CastingDataInstances> dataInstances = new List<CastingDataInstances>();

            try
            {
                foreach(string file in files)
                { 
                    dataInstances.Add(new CastingDataInstances(file));
                }
            }
            catch (Exception ex)
            {
                error.SetError(directoryPath, "Unable to parse one or more files");
                return;
            }

            string castingName = dataInstances[0].CastingName;

            foreach(CastingDataInstances instance in dataInstances)
            {
                if (castingName != instance.CastingName)
                {
                    error.SetError(directoryPath, "Multiple Castings were found in this directory");
                    return;
                }
            }
        }
    }
}
