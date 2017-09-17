namespace Mush_Casting_Analyzer
{
    partial class MasterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.directorySelectBTN = new System.Windows.Forms.Button();
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.analyzeFilesBTN = new System.Windows.Forms.Button();
            this.analyzeByDaysRadio = new System.Windows.Forms.RadioButton();
            this.analyzeByIntervalRadio = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // directorySelectBTN
            // 
            this.directorySelectBTN.Location = new System.Drawing.Point(150, 59);
            this.directorySelectBTN.Name = "directorySelectBTN";
            this.directorySelectBTN.Size = new System.Drawing.Size(136, 23);
            this.directorySelectBTN.TabIndex = 0;
            this.directorySelectBTN.Text = "Select a Directory";
            this.directorySelectBTN.UseVisualStyleBackColor = true;
            this.directorySelectBTN.Click += new System.EventHandler(this.directorySelectBTN_Click);
            // 
            // directoryPath
            // 
            this.directoryPath.Enabled = false;
            this.directoryPath.Location = new System.Drawing.Point(13, 59);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(131, 20);
            this.directoryPath.TabIndex = 1;
            // 
            // analyzeFilesBTN
            // 
            this.analyzeFilesBTN.Location = new System.Drawing.Point(150, 226);
            this.analyzeFilesBTN.Name = "analyzeFilesBTN";
            this.analyzeFilesBTN.Size = new System.Drawing.Size(136, 23);
            this.analyzeFilesBTN.TabIndex = 2;
            this.analyzeFilesBTN.Text = "Analyze Files";
            this.analyzeFilesBTN.UseVisualStyleBackColor = true;
            this.analyzeFilesBTN.Click += new System.EventHandler(this.analyzeFilesBTN_Click);
            // 
            // analyzeByDaysRadio
            // 
            this.analyzeByDaysRadio.AutoSize = true;
            this.analyzeByDaysRadio.Checked = true;
            this.analyzeByDaysRadio.Location = new System.Drawing.Point(13, 100);
            this.analyzeByDaysRadio.Name = "analyzeByDaysRadio";
            this.analyzeByDaysRadio.Size = new System.Drawing.Size(156, 17);
            this.analyzeByDaysRadio.TabIndex = 3;
            this.analyzeByDaysRadio.TabStop = true;
            this.analyzeByDaysRadio.Text = "Analyze By Number of Days";
            this.analyzeByDaysRadio.UseVisualStyleBackColor = true;
            // 
            // analyzeByIntervalRadio
            // 
            this.analyzeByIntervalRadio.AutoSize = true;
            this.analyzeByIntervalRadio.Location = new System.Drawing.Point(13, 203);
            this.analyzeByIntervalRadio.Name = "analyzeByIntervalRadio";
            this.analyzeByIntervalRadio.Size = new System.Drawing.Size(141, 17);
            this.analyzeByIntervalRadio.TabIndex = 4;
            this.analyzeByIntervalRadio.Text = "Analyze From an Interval";
            this.analyzeByIntervalRadio.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 261);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.analyzeByIntervalRadio);
            this.Controls.Add(this.analyzeByDaysRadio);
            this.Controls.Add(this.analyzeFilesBTN);
            this.Controls.Add(this.directoryPath);
            this.Controls.Add(this.directorySelectBTN);
            this.Name = "MasterForm";
            this.Text = "Mush Casting Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button directorySelectBTN;
        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button analyzeFilesBTN;
        private System.Windows.Forms.RadioButton analyzeByDaysRadio;
        private System.Windows.Forms.RadioButton analyzeByIntervalRadio;
        private System.Windows.Forms.TextBox textBox1;
    }
}

