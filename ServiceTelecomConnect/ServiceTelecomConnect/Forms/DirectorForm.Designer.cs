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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.cmB_section_foreman_FIO = new System.Windows.Forms.ComboBox();
            this.cmB_road = new System.Windows.Forms.ComboBox();
            this.cmB_engineers_FIO = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txB_attorney = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_add_registrationEmployees = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txB_id = new System.Windows.Forms.TextBox();
            this.btn_change_registrationEmployees = new System.Windows.Forms.Button();
            this.btn_delete_registrationEmployees = new System.Windows.Forms.Button();
            this.picB_clear = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txB_numberPrintDocument = new System.Windows.Forms.TextBox();
            this.picB_Update = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmB_departmentCommunications = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmB_curator = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_Update)).BeginInit();
            this.SuspendLayout();
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(322, 9);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(382, 31);
            this.AuthorizationLabel.TabIndex = 20;
            this.AuthorizationLabel.Text = "Регистрация сотрудников";
            this.AuthorizationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmB_section_foreman_FIO
            // 
            this.cmB_section_foreman_FIO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_section_foreman_FIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_section_foreman_FIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_section_foreman_FIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_section_foreman_FIO.FormattingEnabled = true;
            this.cmB_section_foreman_FIO.Location = new System.Drawing.Point(20, 344);
            this.cmB_section_foreman_FIO.Name = "cmB_section_foreman_FIO";
            this.cmB_section_foreman_FIO.Size = new System.Drawing.Size(223, 28);
            this.cmB_section_foreman_FIO.TabIndex = 58;
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
            this.cmB_road.Location = new System.Drawing.Point(494, 344);
            this.cmB_road.Name = "cmB_road";
            this.cmB_road.Size = new System.Drawing.Size(223, 28);
            this.cmB_road.TabIndex = 59;
            // 
            // cmB_engineers_FIO
            // 
            this.cmB_engineers_FIO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_engineers_FIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_engineers_FIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_engineers_FIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_engineers_FIO.FormattingEnabled = true;
            this.cmB_engineers_FIO.Location = new System.Drawing.Point(256, 344);
            this.cmB_engineers_FIO.Name = "cmB_engineers_FIO";
            this.cmB_engineers_FIO.Size = new System.Drawing.Size(223, 28);
            this.cmB_engineers_FIO.TabIndex = 60;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(16, 317);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(97, 20);
            this.label30.TabIndex = 84;
            this.label30.Text = "Начальник:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(252, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 85;
            this.label1.Text = "Инженер:";
            // 
            // txB_attorney
            // 
            this.txB_attorney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_attorney.Location = new System.Drawing.Point(256, 407);
            this.txB_attorney.MaxLength = 49;
            this.txB_attorney.Multiline = true;
            this.txB_attorney.Name = "txB_attorney";
            this.txB_attorney.Size = new System.Drawing.Size(223, 28);
            this.txB_attorney.TabIndex = 86;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(252, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 87;
            this.label2.Text = "Довереность";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(490, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 88;
            this.label3.Text = "Дорога:";
            // 
            // btn_add_registrationEmployees
            // 
            this.btn_add_registrationEmployees.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_add_registrationEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_add_registrationEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add_registrationEmployees.Location = new System.Drawing.Point(732, 405);
            this.btn_add_registrationEmployees.Name = "btn_add_registrationEmployees";
            this.btn_add_registrationEmployees.Size = new System.Drawing.Size(78, 30);
            this.btn_add_registrationEmployees.TabIndex = 89;
            this.btn_add_registrationEmployees.Text = "Добавить";
            this.btn_add_registrationEmployees.UseVisualStyleBackColor = false;
            this.btn_add_registrationEmployees.Click += new System.EventHandler(this.Btn_add_registrationEmployeess_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(1007, 253);
            this.dataGridView1.TabIndex = 90;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(12, 9);
            this.txB_id.MaxLength = 49;
            this.txB_id.Multiline = true;
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(34, 28);
            this.txB_id.TabIndex = 91;
            this.txB_id.Visible = false;
            // 
            // btn_change_registrationEmployees
            // 
            this.btn_change_registrationEmployees.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_change_registrationEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_change_registrationEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_change_registrationEmployees.Location = new System.Drawing.Point(821, 405);
            this.btn_change_registrationEmployees.Name = "btn_change_registrationEmployees";
            this.btn_change_registrationEmployees.Size = new System.Drawing.Size(78, 30);
            this.btn_change_registrationEmployees.TabIndex = 92;
            this.btn_change_registrationEmployees.Text = "Изменить";
            this.btn_change_registrationEmployees.UseVisualStyleBackColor = false;
            this.btn_change_registrationEmployees.Click += new System.EventHandler(this.Btn_change_registrationEmployees_Click);
            // 
            // btn_delete_registrationEmployees
            // 
            this.btn_delete_registrationEmployees.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete_registrationEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_registrationEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_delete_registrationEmployees.Location = new System.Drawing.Point(909, 405);
            this.btn_delete_registrationEmployees.Name = "btn_delete_registrationEmployees";
            this.btn_delete_registrationEmployees.Size = new System.Drawing.Size(78, 30);
            this.btn_delete_registrationEmployees.TabIndex = 93;
            this.btn_delete_registrationEmployees.Text = "Удалить";
            this.btn_delete_registrationEmployees.UseVisualStyleBackColor = false;
            this.btn_delete_registrationEmployees.Click += new System.EventHandler(this.Btn_delete_registrationEmployees_Click);
            // 
            // picB_clear
            // 
            this.picB_clear.BackColor = System.Drawing.Color.Transparent;
            this.picB_clear.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.picB_clear.Location = new System.Drawing.Point(986, 10);
            this.picB_clear.Name = "picB_clear";
            this.picB_clear.Size = new System.Drawing.Size(33, 30);
            this.picB_clear.TabIndex = 95;
            this.picB_clear.TabStop = false;
            this.picB_clear.Click += new System.EventHandler(this.PicB_clear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(490, 384);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 97;
            this.label4.Text = "№ печати";
            // 
            // txB_numberPrintDocument
            // 
            this.txB_numberPrintDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberPrintDocument.Location = new System.Drawing.Point(494, 407);
            this.txB_numberPrintDocument.MaxLength = 49;
            this.txB_numberPrintDocument.Multiline = true;
            this.txB_numberPrintDocument.Name = "txB_numberPrintDocument";
            this.txB_numberPrintDocument.Size = new System.Drawing.Size(223, 28);
            this.txB_numberPrintDocument.TabIndex = 96;
            // 
            // picB_Update
            // 
            this.picB_Update.BackColor = System.Drawing.Color.Transparent;
            this.picB_Update.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.icons8_синхронизация_подключения_321;
            this.picB_Update.Location = new System.Drawing.Point(947, 9);
            this.picB_Update.Name = "picB_Update";
            this.picB_Update.Size = new System.Drawing.Size(33, 30);
            this.picB_Update.TabIndex = 98;
            this.picB_Update.TabStop = false;
            this.picB_Update.Click += new System.EventHandler(this.PicB_Update_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(728, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 20);
            this.label5.TabIndex = 100;
            this.label5.Text = "Представитель дирекция связи:";
            // 
            // cmB_departmentCommunications
            // 
            this.cmB_departmentCommunications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_departmentCommunications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_departmentCommunications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_departmentCommunications.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_departmentCommunications.FormattingEnabled = true;
            this.cmB_departmentCommunications.Location = new System.Drawing.Point(732, 344);
            this.cmB_departmentCommunications.Name = "cmB_departmentCommunications";
            this.cmB_departmentCommunications.Size = new System.Drawing.Size(255, 28);
            this.cmB_departmentCommunications.TabIndex = 99;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(16, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 102;
            this.label6.Text = "Куратор:";
            // 
            // cmB_curator
            // 
            this.cmB_curator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmB_curator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_curator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_curator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_curator.FormattingEnabled = true;
            this.cmB_curator.Location = new System.Drawing.Point(20, 407);
            this.cmB_curator.Name = "cmB_curator";
            this.cmB_curator.Size = new System.Drawing.Size(223, 28);
            this.cmB_curator.TabIndex = 101;
            // 
            // DirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(1031, 466);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmB_curator);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmB_departmentCommunications);
            this.Controls.Add(this.picB_Update);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txB_numberPrintDocument);
            this.Controls.Add(this.picB_clear);
            this.Controls.Add(this.btn_delete_registrationEmployees);
            this.Controls.Add(this.btn_change_registrationEmployees);
            this.Controls.Add(this.txB_id);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_add_registrationEmployees);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txB_attorney);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cmB_engineers_FIO);
            this.Controls.Add(this.cmB_road);
            this.Controls.Add(this.cmB_section_foreman_FIO);
            this.Controls.Add(this.AuthorizationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(1047, 505);
            this.MinimumSize = new System.Drawing.Size(1047, 505);
            this.Name = "DirectorForm";
            this.Text = "Главная руководитель";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DirectorForm_FormClosed);
            this.Load += new System.EventHandler(this.DirectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picB_Update)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthorizationLabel;
        private System.Windows.Forms.ComboBox cmB_section_foreman_FIO;
        private System.Windows.Forms.ComboBox cmB_road;
        private System.Windows.Forms.ComboBox cmB_engineers_FIO;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txB_attorney;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_add_registrationEmployees;
        private System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.TextBox txB_id;
        private System.Windows.Forms.Button btn_change_registrationEmployees;
        private System.Windows.Forms.Button btn_delete_registrationEmployees;
        private System.Windows.Forms.PictureBox picB_clear;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txB_numberPrintDocument;
        private System.Windows.Forms.PictureBox picB_Update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmB_departmentCommunications;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmB_curator;
    }
}