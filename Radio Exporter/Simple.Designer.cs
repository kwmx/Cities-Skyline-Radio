namespace Radio_Exporter
{
    partial class Simple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simple));
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtUrl = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtTitle = new MetroFramework.Controls.MetroTextBox();
            this.btnAdd = new MetroFramework.Controls.MetroButton();
            this.txtList = new MetroFramework.Controls.MetroTextBox();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.txtName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(1, 102);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(35, 19);
            this.metroLabel2.TabIndex = 8;
            this.metroLabel2.Text = "URL:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(42, 102);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(302, 23);
            this.txtUrl.TabIndex = 7;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(0, 77);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(36, 19);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(42, 73);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(302, 23);
            this.txtTitle.TabIndex = 5;
            this.txtTitle.Text = "Unknown";
            this.metroToolTip1.SetToolTip(this.txtTitle, "Title of a channel");
            this.txtTitle.Click += new System.EventHandler(this.metroTextBox1_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(42, 131);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(302, 34);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.metroToolTip1.SetToolTip(this.btnAdd, "Add title and url to data");
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtList
            // 
            this.txtList.Location = new System.Drawing.Point(42, 172);
            this.txtList.Multiline = true;
            this.txtList.Name = "txtList";
            this.txtList.Size = new System.Drawing.Size(302, 144);
            this.txtList.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(42, 351);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(302, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export";
            this.metroToolTip1.SetToolTip(this.btnExport, "Save all the data to rxm file");
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(55, 322);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(289, 23);
            this.txtName.TabIndex = 12;
            this.txtName.Text = "radio";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(1, 322);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(48, 19);
            this.metroLabel3.TabIndex = 13;
            this.metroLabel3.Text = "Name:";
            // 
            // Simple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtList);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Simple";
            this.Resizable = false;
            this.Text = "Simple Add";
            this.Load += new System.EventHandler(this.Simple_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtUrl;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtTitle;
        private MetroFramework.Controls.MetroButton btnAdd;
        private MetroFramework.Controls.MetroTextBox txtList;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroTextBox txtName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}