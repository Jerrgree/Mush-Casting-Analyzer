namespace Mush_Casting_Analyzer
{
    partial class OptionsForm
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TimeZoneDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.directorySelectBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 226);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(188, 226);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // TimeZoneDropDown
            // 
            this.TimeZoneDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeZoneDropDown.FormattingEnabled = true;
            this.TimeZoneDropDown.Location = new System.Drawing.Point(13, 34);
            this.TimeZoneDropDown.Name = "TimeZoneDropDown";
            this.TimeZoneDropDown.Size = new System.Drawing.Size(240, 21);
            this.TimeZoneDropDown.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Default Time Zone";
            // 
            // directoryPath
            // 
            this.directoryPath.Enabled = false;
            this.directoryPath.Location = new System.Drawing.Point(113, 121);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(166, 20);
            this.directoryPath.TabIndex = 5;
            // 
            // directorySelectBTN
            // 
            this.directorySelectBTN.Location = new System.Drawing.Point(6, 119);
            this.directorySelectBTN.Name = "directorySelectBTN";
            this.directorySelectBTN.Size = new System.Drawing.Size(101, 23);
            this.directorySelectBTN.TabIndex = 4;
            this.directorySelectBTN.Text = "Default Directory";
            this.directorySelectBTN.UseVisualStyleBackColor = true;
            this.directorySelectBTN.Click += new System.EventHandler(this.directorySelectBTN_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.directoryPath);
            this.Controls.Add(this.directorySelectBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeZoneDropDown);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ComboBox TimeZoneDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button directorySelectBTN;
    }
}