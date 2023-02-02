namespace ServiceTelecomConnect
{
    partial class Menu
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
            this.lbL_sectionForeman = new System.Windows.Forms.Label();
            this.lbL_TutorialEngineers = new System.Windows.Forms.Label();
            this.lbL_сomparison = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbL_director = new System.Windows.Forms.Label();
            this.picB_setting = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_setting)).BeginInit();
            this.SuspendLayout();
            // 
            // lbL_section_foreman
            // 
            this.lbL_sectionForeman.AutoSize = true;
            this.lbL_sectionForeman.BackColor = System.Drawing.Color.Transparent;
            this.lbL_sectionForeman.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_sectionForeman.Location = new System.Drawing.Point(386, 212);
            this.lbL_sectionForeman.Name = "lbL_section_foreman";
            this.lbL_sectionForeman.Size = new System.Drawing.Size(106, 41);
            this.lbL_sectionForeman.TabIndex = 1;
            this.lbL_sectionForeman.Text = "База";
            this.lbL_sectionForeman.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbL_sectionForeman.Click += new System.EventHandler(this.LblBazaClick);
            this.lbL_sectionForeman.MouseEnter += new System.EventHandler(this.LblSectionForemanMouseEnter);
            this.lbL_sectionForeman.MouseLeave += new System.EventHandler(this.LblSectionForemanMouseLeave);
            // 
            // lbL_TutorialEngineers
            // 
            this.lbL_TutorialEngineers.AutoSize = true;
            this.lbL_TutorialEngineers.BackColor = System.Drawing.Color.Transparent;
            this.lbL_TutorialEngineers.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_TutorialEngineers.Location = new System.Drawing.Point(343, 134);
            this.lbL_TutorialEngineers.Name = "lbL_TutorialEngineers";
            this.lbL_TutorialEngineers.Size = new System.Drawing.Size(194, 41);
            this.lbL_TutorialEngineers.TabIndex = 2;
            this.lbL_TutorialEngineers.Text = "Обучалка";
            this.lbL_TutorialEngineers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbL_TutorialEngineers.Click += new System.EventHandler(this.LblTutorialEngineersClick);
            this.lbL_TutorialEngineers.MouseEnter += new System.EventHandler(this.LblTutorialEngineersMouseEnter);
            this.lbL_TutorialEngineers.MouseLeave += new System.EventHandler(this.LblTutorialEngineersMouseLeave);
            // 
            // lbL_сomparison
            // 
            this.lbL_сomparison.AutoSize = true;
            this.lbL_сomparison.BackColor = System.Drawing.Color.Transparent;
            this.lbL_сomparison.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_сomparison.Location = new System.Drawing.Point(303, 291);
            this.lbL_сomparison.Name = "lbL_сomparison";
            this.lbL_сomparison.Size = new System.Drawing.Size(282, 41);
            this.lbL_сomparison.TabIndex = 3;
            this.lbL_сomparison.Text = "Для куратора";
            this.lbL_сomparison.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbL_сomparison.Click += new System.EventHandler(this.LblComparisonFormClick);
            this.lbL_сomparison.MouseEnter += new System.EventHandler(this.LblComparisonMouseEnter);
            this.lbL_сomparison.MouseLeave += new System.EventHandler(this.LblComparisonMouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbL_director);
            this.panel1.Controls.Add(this.picB_setting);
            this.panel1.Controls.Add(this.lbL_сomparison);
            this.panel1.Controls.Add(this.lbL_TutorialEngineers);
            this.panel1.Controls.Add(this.lbL_sectionForeman);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 461);
            this.panel1.TabIndex = 2;
            // 
            // lbL_director
            // 
            this.lbL_director.AutoSize = true;
            this.lbL_director.BackColor = System.Drawing.Color.Transparent;
            this.lbL_director.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_director.Location = new System.Drawing.Point(303, 375);
            this.lbL_director.Name = "lbL_director";
            this.lbL_director.Size = new System.Drawing.Size(282, 41);
            this.lbL_director.TabIndex = 5;
            this.lbL_director.Text = "Руководитель";
            this.lbL_director.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbL_director.Visible = false;
            this.lbL_director.Click += new System.EventHandler(this.LbLDirectorClick);
            this.lbL_director.MouseEnter += new System.EventHandler(this.LbLDirectorMouseEnter);
            this.lbL_director.MouseLeave += new System.EventHandler(this.LbLDirectorMouseLeave);
            // 
            // picB_setting
            // 
            this.picB_setting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picB_setting.BackColor = System.Drawing.Color.Transparent;
            this.picB_setting.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.data_integrated_data_management_data_processing_setting_configuration_icon_1906481;
            this.picB_setting.Location = new System.Drawing.Point(12, 12);
            this.picB_setting.Name = "picB_setting";
            this.picB_setting.Size = new System.Drawing.Size(66, 66);
            this.picB_setting.TabIndex = 4;
            this.picB_setting.TabStop = false;
            this.picB_setting.Visible = false;
            this.picB_setting.Click += new System.EventHandler(this.SettingClick);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_5;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "Menu";
            this.ShowIcon = false;
            this.Text = "Меню";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuFormClosed);
            this.Load += new System.EventHandler(this.MenuLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_setting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbL_sectionForeman;
        private System.Windows.Forms.Label lbL_TutorialEngineers;
        private System.Windows.Forms.Label lbL_сomparison;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picB_setting;
        private System.Windows.Forms.Label lbL_director;
    }
}