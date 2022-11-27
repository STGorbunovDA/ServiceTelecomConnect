namespace ServiceTelecomConnect.Forms
{
    partial class TutorialForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_unique = new System.Windows.Forms.ComboBox();
            this.cmB_seach = new System.Windows.Forms.ComboBox();
            this.btn_new_rst_problem = new System.Windows.Forms.Button();
            this.picB_update = new System.Windows.Forms.PictureBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txB_search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmb_unique);
            this.panel1.Controls.Add(this.cmB_seach);
            this.panel1.Controls.Add(this.btn_new_rst_problem);
            this.panel1.Controls.Add(this.picB_update);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.txB_search);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1349, 92);
            this.panel1.TabIndex = 0;
            // 
            // cmb_unique
            // 
            this.cmb_unique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_unique.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_unique.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmb_unique.FormattingEnabled = true;
            this.cmb_unique.Items.AddRange(new object[] {
            "Модель",
            "Неисправность",
            "Автор"});
            this.cmb_unique.Location = new System.Drawing.Point(245, 10);
            this.cmb_unique.Name = "cmb_unique";
            this.cmb_unique.Size = new System.Drawing.Size(292, 28);
            this.cmb_unique.TabIndex = 95;
            this.cmb_unique.Visible = false;
            this.cmb_unique.SelectedIndexChanged += new System.EventHandler(this.Cmb_unique_SelectedIndexChanged);
            // 
            // cmB_seach
            // 
            this.cmB_seach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_seach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_seach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_seach.FormattingEnabled = true;
            this.cmB_seach.Items.AddRange(new object[] {
            "Модель",
            "Неисправность",
            "Автор",
            "Виды работ"});
            this.cmB_seach.Location = new System.Drawing.Point(11, 10);
            this.cmB_seach.Name = "cmB_seach";
            this.cmB_seach.Size = new System.Drawing.Size(228, 28);
            this.cmB_seach.TabIndex = 94;
            this.cmB_seach.SelectionChangeCommitted += new System.EventHandler(this.Cmb_seach_SelectionChangeCommitted);
            // 
            // btn_new_rst_problem
            // 
            this.btn_new_rst_problem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_new_rst_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_new_rst_problem.Location = new System.Drawing.Point(245, 44);
            this.btn_new_rst_problem.Name = "btn_new_rst_problem";
            this.btn_new_rst_problem.Size = new System.Drawing.Size(292, 31);
            this.btn_new_rst_problem.TabIndex = 93;
            this.btn_new_rst_problem.Text = "Добавить новую неисправность";
            this.btn_new_rst_problem.UseVisualStyleBackColor = false;
            this.btn_new_rst_problem.Click += new System.EventHandler(this.Btn_new_rst_problem_Click);
            // 
            // picB_update
            // 
            this.picB_update.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picB_update.BackColor = System.Drawing.Color.Transparent;
            this.picB_update.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.system_software_update_22485__1_;
            this.picB_update.Location = new System.Drawing.Point(592, 10);
            this.picB_update.Name = "picB_update";
            this.picB_update.Size = new System.Drawing.Size(50, 48);
            this.picB_update.TabIndex = 92;
            this.picB_update.TabStop = false;
            this.picB_update.Click += new System.EventHandler(this.PicB_update_Click);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_search.Location = new System.Drawing.Point(543, 10);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(29, 29);
            this.btn_search.TabIndex = 91;
            this.btn_search.Text = "...";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.Btn_search_Click);
            // 
            // txB_search
            // 
            this.txB_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txB_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_search.Location = new System.Drawing.Point(245, 10);
            this.txB_search.Name = "txB_search";
            this.txB_search.Size = new System.Drawing.Size(292, 29);
            this.txB_search.TabIndex = 89;
            this.txB_search.DoubleClick += new System.EventHandler(this.TxB_search_DoubleClick);
            this.txB_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_search_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(1, 101);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(1349, 548);
            this.dataGridView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(11, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 31);
            this.button1.TabIndex = 96;
            this.button1.Text = "Краткая иформация";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // TutorialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1352, 661);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TutorialForm";
            this.Text = "Обучалка";
            this.Load += new System.EventHandler(this.TutorialForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txB_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.PictureBox picB_update;
        private System.Windows.Forms.Button btn_new_rst_problem;
        private System.Windows.Forms.ComboBox cmB_seach;
        private System.Windows.Forms.ComboBox cmb_unique;
        private System.Windows.Forms.Button button1;
    }
}