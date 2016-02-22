namespace Radio_Exporter
{
    partial class Bulk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bulk));
            this.txtChannels = new MetroFramework.Controls.MetroTextBox();
            this.txtFileName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // txtChannels
            // 
            this.txtChannels.Location = new System.Drawing.Point(8, 63);
            this.txtChannels.Multiline = true;
            this.txtChannels.Name = "txtChannels";
            this.txtChannels.Size = new System.Drawing.Size(481, 367);
            this.txtChannels.TabIndex = 0;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(8, 436);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(481, 23);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "radio";
            this.metroToolTip1.SetToolTip(this.txtFileName, "Save file name");
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(8, 462);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(388, 95);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = resources.GetString("metroLabel1.Text");
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(317, 566);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(158, 23);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "Export";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(8, 566);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(158, 23);
            this.metroButton2.TabIndex = 4;
            this.metroButton2.Text = "Import";
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // metroButton3
            // 
            this.metroButton3.Location = new System.Drawing.Point(388, 34);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(101, 23);
            this.metroButton3.TabIndex = 5;
            this.metroButton3.Text = "Add empty line";
            this.metroButton3.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // Bulk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 600);
            this.Controls.Add(this.metroButton3);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.txtChannels);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(498, 600);
            this.MinimumSize = new System.Drawing.Size(498, 600);
            this.Name = "Bulk";
            this.Resizable = false;
            this.Text = "Bulk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtChannels;
        private MetroFramework.Controls.MetroTextBox txtFileName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton metroButton3;
    }
}