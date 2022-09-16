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
            this.label_section_foreman = new System.Windows.Forms.Label();
            this.label_TutorialEngineers = new System.Windows.Forms.Label();
            this.label_сomparison = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1_setting = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_setting)).BeginInit();
            this.SuspendLayout();
            // 
            // label_section_foreman
            // 
            this.label_section_foreman.AutoSize = true;
            this.label_section_foreman.BackColor = System.Drawing.Color.Transparent;
            this.label_section_foreman.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_section_foreman.Location = new System.Drawing.Point(222, 231);
            this.label_section_foreman.Name = "label_section_foreman";
            this.label_section_foreman.Size = new System.Drawing.Size(392, 41);
            this.label_section_foreman.TabIndex = 1;
            this.label_section_foreman.Text = "Начальник участка";
            this.label_section_foreman.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_section_foreman.Click += new System.EventHandler(this.label_baza_Click);
            this.label_section_foreman.MouseEnter += new System.EventHandler(this.label_section_foreman_MouseEnter);
            this.label_section_foreman.MouseLeave += new System.EventHandler(this.label_section_foreman_MouseLeave);
            // 
            // label_TutorialEngineers
            // 
            this.label_TutorialEngineers.AutoSize = true;
            this.label_TutorialEngineers.BackColor = System.Drawing.Color.Transparent;
            this.label_TutorialEngineers.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_TutorialEngineers.Location = new System.Drawing.Point(333, 161);
            this.label_TutorialEngineers.Name = "label_TutorialEngineers";
            this.label_TutorialEngineers.Size = new System.Drawing.Size(172, 41);
            this.label_TutorialEngineers.TabIndex = 2;
            this.label_TutorialEngineers.Text = "Инженер";
            this.label_TutorialEngineers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_TutorialEngineers.Click += new System.EventHandler(this.label_TutorialEngineers_Click);
            this.label_TutorialEngineers.MouseEnter += new System.EventHandler(this.label_TutorialEngineers_MouseEnter);
            this.label_TutorialEngineers.MouseLeave += new System.EventHandler(this.label_TutorialEngineers_MouseLeave);
            // 
            // label_сomparison
            // 
            this.label_сomparison.AutoSize = true;
            this.label_сomparison.BackColor = System.Drawing.Color.Transparent;
            this.label_сomparison.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_сomparison.Location = new System.Drawing.Point(333, 305);
            this.label_сomparison.Name = "label_сomparison";
            this.label_сomparison.Size = new System.Drawing.Size(172, 41);
            this.label_сomparison.TabIndex = 3;
            this.label_сomparison.Text = "Куратор";
            this.label_сomparison.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_сomparison.Click += new System.EventHandler(this.label1_Click);
            this.label_сomparison.MouseEnter += new System.EventHandler(this.label_сomparison_MouseEnter);
            this.label_сomparison.MouseLeave += new System.EventHandler(this.label_сomparison_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1_setting);
            this.panel1.Controls.Add(this.label_сomparison);
            this.panel1.Controls.Add(this.label_TutorialEngineers);
            this.panel1.Controls.Add(this.label_section_foreman);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 461);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1_setting
            // 
            this.pictureBox1_setting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1_setting.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1_setting.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.data_integrated_data_management_data_processing_setting_configuration_icon_1906481;
            this.pictureBox1_setting.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1_setting.Name = "pictureBox1_setting";
            this.pictureBox1_setting.Size = new System.Drawing.Size(66, 65);
            this.pictureBox1_setting.TabIndex = 4;
            this.pictureBox1_setting.TabStop = false;
            this.pictureBox1_setting.Visible = false;
            this.pictureBox1_setting.Click += new System.EventHandler(this.pictureBox1_setting_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_5;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "Menu";
            this.ShowIcon = false;
            this.Text = "Меню";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_setting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_section_foreman;
        private System.Windows.Forms.Label label_TutorialEngineers;
        private System.Windows.Forms.Label label_сomparison;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1_setting;
    }
}