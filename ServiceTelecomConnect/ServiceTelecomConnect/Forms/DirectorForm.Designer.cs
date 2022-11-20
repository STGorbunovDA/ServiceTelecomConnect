namespace ServiceTelecomConnect.Forms
{
    partial class DirectorForm
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
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.cmB_login_FIO = new System.Windows.Forms.ComboBox();
            this.cmB_road = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(205, 9);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(382, 31);
            this.AuthorizationLabel.TabIndex = 20;
            this.AuthorizationLabel.Text = "Регистрация сотрудников";
            this.AuthorizationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmB_login_FIO
            // 
            this.cmB_login_FIO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_login_FIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_login_FIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_login_FIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_login_FIO.FormattingEnabled = true;
            this.cmB_login_FIO.Location = new System.Drawing.Point(12, 79);
            this.cmB_login_FIO.Name = "cmB_login_FIO";
            this.cmB_login_FIO.Size = new System.Drawing.Size(234, 28);
            this.cmB_login_FIO.TabIndex = 58;
            // 
            // cmB_road
            // 
            this.cmB_road.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_road.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_road.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_road.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_road.FormattingEnabled = true;
            this.cmB_road.Items.AddRange(new object[] {
            "Восточно-Сибирская ЖД",
            "Горьковская ЖД",
            "Дальневосточная ЖД",
            "Забайкальская ЖД",
            "Западно-Сибирская ЖД",
            "Калининградская ЖД",
            "Красноярская ЖД",
            "Куйбышевская ЖД",
            "Московская ЖД",
            "Октябрьская ЖД",
            "Приволжская ЖД",
            "Сахалинская ЖД",
            "Свердловская ЖД",
            "Северная ЖД",
            "Северо-Кавказская ЖД",
            "Юго-Восточная ЖД",
            "Южно-Уральская ЖД"});
            this.cmB_road.Location = new System.Drawing.Point(270, 79);
            this.cmB_road.Name = "cmB_road";
            this.cmB_road.Size = new System.Drawing.Size(211, 28);
            this.cmB_road.TabIndex = 59;
            // 
            // DirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmB_road);
            this.Controls.Add(this.cmB_login_FIO);
            this.Controls.Add(this.AuthorizationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DirectorForm";
            this.Text = "Главная руководитель";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthorizationLabel;
        private System.Windows.Forms.ComboBox cmB_login_FIO;
        private System.Windows.Forms.ComboBox cmB_road;
    }
}