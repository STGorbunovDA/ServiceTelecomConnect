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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.cmB_section_foreman_FIO = new System.Windows.Forms.ComboBox();
            this.cmB_road = new System.Windows.Forms.ComboBox();
            this.cmB_engineers_FIO = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txB_attorney = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save_add_rst = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txB_id = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(126, 9);
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
            this.cmB_section_foreman_FIO.Size = new System.Drawing.Size(262, 28);
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
            this.cmB_road.Location = new System.Drawing.Point(328, 344);
            this.cmB_road.Name = "cmB_road";
            this.cmB_road.Size = new System.Drawing.Size(262, 28);
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
            this.cmB_engineers_FIO.Location = new System.Drawing.Point(20, 401);
            this.cmB_engineers_FIO.Name = "cmB_engineers_FIO";
            this.cmB_engineers_FIO.Size = new System.Drawing.Size(262, 28);
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
            this.label1.Location = new System.Drawing.Point(16, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 85;
            this.label1.Text = "Инженер:";
            // 
            // txB_attorney
            // 
            this.txB_attorney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_attorney.Location = new System.Drawing.Point(328, 401);
            this.txB_attorney.MaxLength = 49;
            this.txB_attorney.Multiline = true;
            this.txB_attorney.Name = "txB_attorney";
            this.txB_attorney.Size = new System.Drawing.Size(262, 28);
            this.txB_attorney.TabIndex = 86;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(324, 378);
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
            this.label3.Location = new System.Drawing.Point(324, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 88;
            this.label3.Text = "Дорога:";
            // 
            // btn_save_add_rst
            // 
            this.btn_save_add_rst.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_save_add_rst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save_add_rst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save_add_rst.Location = new System.Drawing.Point(471, 448);
            this.btn_save_add_rst.Name = "btn_save_add_rst";
            this.btn_save_add_rst.Size = new System.Drawing.Size(119, 30);
            this.btn_save_add_rst.TabIndex = 89;
            this.btn_save_add_rst.Text = "Добавить";
            this.btn_save_add_rst.UseVisualStyleBackColor = false;
            this.btn_save_add_rst.Click += new System.EventHandler(this.Btn_save_add_rst_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(621, 253);
            this.dataGridView1.TabIndex = 90;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(20, 448);
            this.txB_id.MaxLength = 49;
            this.txB_id.Multiline = true;
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(34, 28);
            this.txB_id.TabIndex = 91;
            this.txB_id.Visible = false;
            // 
            // DirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(645, 490);
            this.Controls.Add(this.txB_id);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_save_add_rst);
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
            this.Name = "DirectorForm";
            this.Text = "Главная руководитель";
            this.Load += new System.EventHandler(this.DirectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button btn_save_add_rst;
        private System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.TextBox txB_id;
    }
}