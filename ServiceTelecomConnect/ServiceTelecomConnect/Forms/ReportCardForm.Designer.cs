namespace ServiceTelecomConnect.Forms
{
    partial class ReportCardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txB_timeCount = new System.Windows.Forms.TextBox();
            this.txB_dateTimeExit = new System.Windows.Forms.TextBox();
            this.txB_dateTimeInput = new System.Windows.Forms.TextBox();
            this.txB_user = new System.Windows.Forms.TextBox();
            this.txB_id = new System.Windows.Forms.TextBox();
            this.picB_Update = new System.Windows.Forms.PictureBox();
            this.btn_save_excel = new System.Windows.Forms.Button();
            this.cmB_dateTimeInput = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_Update)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(796, 505);
            this.dataGridView1.TabIndex = 91;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txB_timeCount);
            this.panel1.Controls.Add(this.txB_dateTimeExit);
            this.panel1.Controls.Add(this.txB_dateTimeInput);
            this.panel1.Controls.Add(this.txB_user);
            this.panel1.Controls.Add(this.txB_id);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 35);
            this.panel1.TabIndex = 92;
            this.panel1.Visible = false;
            // 
            // txB_timeCount
            // 
            this.txB_timeCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_timeCount.Location = new System.Drawing.Point(147, 3);
            this.txB_timeCount.MaxLength = 49;
            this.txB_timeCount.Multiline = true;
            this.txB_timeCount.Name = "txB_timeCount";
            this.txB_timeCount.Size = new System.Drawing.Size(33, 28);
            this.txB_timeCount.TabIndex = 101;
            // 
            // txB_dateTimeExit
            // 
            this.txB_dateTimeExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTimeExit.Location = new System.Drawing.Point(108, 3);
            this.txB_dateTimeExit.MaxLength = 49;
            this.txB_dateTimeExit.Multiline = true;
            this.txB_dateTimeExit.Name = "txB_dateTimeExit";
            this.txB_dateTimeExit.Size = new System.Drawing.Size(33, 28);
            this.txB_dateTimeExit.TabIndex = 100;
            // 
            // txB_dateTimeInput
            // 
            this.txB_dateTimeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTimeInput.Location = new System.Drawing.Point(72, 3);
            this.txB_dateTimeInput.MaxLength = 49;
            this.txB_dateTimeInput.Multiline = true;
            this.txB_dateTimeInput.Name = "txB_dateTimeInput";
            this.txB_dateTimeInput.Size = new System.Drawing.Size(30, 28);
            this.txB_dateTimeInput.TabIndex = 99;
            // 
            // txB_user
            // 
            this.txB_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_user.Location = new System.Drawing.Point(36, 3);
            this.txB_user.MaxLength = 49;
            this.txB_user.Multiline = true;
            this.txB_user.Name = "txB_user";
            this.txB_user.Size = new System.Drawing.Size(30, 28);
            this.txB_user.TabIndex = 98;
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(3, 3);
            this.txB_id.MaxLength = 49;
            this.txB_id.Multiline = true;
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(27, 28);
            this.txB_id.TabIndex = 97;
            // 
            // picB_Update
            // 
            this.picB_Update.BackColor = System.Drawing.Color.Transparent;
            this.picB_Update.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.icons8_синхронизация_подключения_321;
            this.picB_Update.Location = new System.Drawing.Point(775, 12);
            this.picB_Update.Name = "picB_Update";
            this.picB_Update.Size = new System.Drawing.Size(33, 30);
            this.picB_Update.TabIndex = 99;
            this.picB_Update.TabStop = false;
            this.picB_Update.Click += new System.EventHandler(this.PicB_Update_Click);
            // 
            // btn_save_excel
            // 
            this.btn_save_excel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_save_excel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save_excel.Location = new System.Drawing.Point(618, 11);
            this.btn_save_excel.Name = "btn_save_excel";
            this.btn_save_excel.Size = new System.Drawing.Size(151, 31);
            this.btn_save_excel.TabIndex = 100;
            this.btn_save_excel.Text = "Сохранить в excel";
            this.btn_save_excel.UseVisualStyleBackColor = false;
            this.btn_save_excel.Click += new System.EventHandler(this.Btn_save_excel_Click);
            // 
            // cmB_dateTimeInput
            // 
            this.cmB_dateTimeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_dateTimeInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_dateTimeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_dateTimeInput.FormattingEnabled = true;
            this.cmB_dateTimeInput.Items.AddRange(new object[] {
            "Модель",
            "Неисправность",
            "Автор",
            "Описание неисправности"});
            this.cmB_dateTimeInput.Location = new System.Drawing.Point(384, 10);
            this.cmB_dateTimeInput.Name = "cmB_dateTimeInput";
            this.cmB_dateTimeInput.Size = new System.Drawing.Size(228, 28);
            this.cmB_dateTimeInput.TabIndex = 101;
            this.cmB_dateTimeInput.SelectionChangeCommitted += new System.EventHandler(this.CmB_dateTimeInput_SelectionChangeCommitted);
            // 
            // ReportCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(820, 565);
            this.Controls.Add(this.cmB_dateTimeInput);
            this.Controls.Add(this.btn_save_excel);
            this.Controls.Add(this.picB_Update);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(836, 604);
            this.MinimumSize = new System.Drawing.Size(836, 604);
            this.Name = "ReportCardForm";
            this.Text = "Табель";
            this.Load += new System.EventHandler(this.ReportCardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_Update)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txB_id;
        internal System.Windows.Forms.TextBox txB_user;
        internal System.Windows.Forms.TextBox txB_dateTimeInput;
        internal System.Windows.Forms.TextBox txB_dateTimeExit;
        internal System.Windows.Forms.TextBox txB_timeCount;
        private System.Windows.Forms.PictureBox picB_Update;
        private System.Windows.Forms.Button btn_save_excel;
        private System.Windows.Forms.ComboBox cmB_dateTimeInput;
    }
}