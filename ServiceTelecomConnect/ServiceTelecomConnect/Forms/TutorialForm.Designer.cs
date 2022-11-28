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
            this.btn_delete_problem = new System.Windows.Forms.Button();
            this.btn_change_problem = new System.Windows.Forms.Button();
            this.btn_brief_info = new System.Windows.Forms.Button();
            this.cmb_unique = new System.Windows.Forms.ComboBox();
            this.cmB_seach = new System.Windows.Forms.ComboBox();
            this.btn_new_rst_problem = new System.Windows.Forms.Button();
            this.picB_update = new System.Windows.Forms.PictureBox();
            this.txB_search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txB_author = new System.Windows.Forms.TextBox();
            this.txB_actions = new System.Windows.Forms.TextBox();
            this.txB_info = new System.Windows.Forms.TextBox();
            this.txB_problem = new System.Windows.Forms.TextBox();
            this.txB_model = new System.Windows.Forms.TextBox();
            this.txB_id = new System.Windows.Forms.TextBox();
            this.btn_save_excel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel1.Controls.Add(this.btn_save_excel);
            this.panel1.Controls.Add(this.btn_delete_problem);
            this.panel1.Controls.Add(this.btn_change_problem);
            this.panel1.Controls.Add(this.btn_brief_info);
            this.panel1.Controls.Add(this.cmb_unique);
            this.panel1.Controls.Add(this.cmB_seach);
            this.panel1.Controls.Add(this.btn_new_rst_problem);
            this.panel1.Controls.Add(this.picB_update);
            this.panel1.Controls.Add(this.txB_search);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1349, 92);
            this.panel1.TabIndex = 0;
            // 
            // btn_delete_problem
            // 
            this.btn_delete_problem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_delete_problem.Location = new System.Drawing.Point(480, 45);
            this.btn_delete_problem.Name = "btn_delete_problem";
            this.btn_delete_problem.Size = new System.Drawing.Size(229, 31);
            this.btn_delete_problem.TabIndex = 98;
            this.btn_delete_problem.Text = "Удалить иформацию";
            this.btn_delete_problem.UseVisualStyleBackColor = false;
            this.btn_delete_problem.Click += new System.EventHandler(this.Btn_delete_problem_Click);
            // 
            // btn_change_problem
            // 
            this.btn_change_problem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_change_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_change_problem.Location = new System.Drawing.Point(245, 45);
            this.btn_change_problem.Name = "btn_change_problem";
            this.btn_change_problem.Size = new System.Drawing.Size(229, 31);
            this.btn_change_problem.TabIndex = 97;
            this.btn_change_problem.Text = "Изменить неисправность";
            this.btn_change_problem.UseVisualStyleBackColor = false;
            this.btn_change_problem.Click += new System.EventHandler(this.Btn_change_problem_Click);
            // 
            // btn_brief_info
            // 
            this.btn_brief_info.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_brief_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_brief_info.Location = new System.Drawing.Point(1051, 28);
            this.btn_brief_info.Name = "btn_brief_info";
            this.btn_brief_info.Size = new System.Drawing.Size(229, 31);
            this.btn_brief_info.TabIndex = 96;
            this.btn_brief_info.Text = "Краткая иформация";
            this.btn_brief_info.UseVisualStyleBackColor = false;
            this.btn_brief_info.Click += new System.EventHandler(this.Btn_brief_info_Click);
            // 
            // cmb_unique
            // 
            this.cmb_unique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_unique.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_unique.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmb_unique.FormattingEnabled = true;
            this.cmb_unique.Location = new System.Drawing.Point(245, 10);
            this.cmb_unique.Name = "cmb_unique";
            this.cmb_unique.Size = new System.Drawing.Size(229, 28);
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
            "Описание неисправности"});
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
            this.btn_new_rst_problem.Location = new System.Drawing.Point(11, 44);
            this.btn_new_rst_problem.Name = "btn_new_rst_problem";
            this.btn_new_rst_problem.Size = new System.Drawing.Size(229, 31);
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
            this.picB_update.Location = new System.Drawing.Point(1286, 19);
            this.picB_update.Name = "picB_update";
            this.picB_update.Size = new System.Drawing.Size(50, 48);
            this.picB_update.TabIndex = 92;
            this.picB_update.TabStop = false;
            this.picB_update.Click += new System.EventHandler(this.PicB_update_Click);
            // 
            // txB_search
            // 
            this.txB_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txB_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_search.Location = new System.Drawing.Point(245, 10);
            this.txB_search.Name = "txB_search";
            this.txB_search.Size = new System.Drawing.Size(229, 29);
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
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel2.Controls.Add(this.txB_author);
            this.panel2.Controls.Add(this.txB_actions);
            this.panel2.Controls.Add(this.txB_info);
            this.panel2.Controls.Add(this.txB_problem);
            this.panel2.Controls.Add(this.txB_model);
            this.panel2.Controls.Add(this.txB_id);
            this.panel2.Location = new System.Drawing.Point(1, 557);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1349, 92);
            this.panel2.TabIndex = 98;
            this.panel2.Visible = false;
            // 
            // txB_author
            // 
            this.txB_author.Location = new System.Drawing.Point(366, 13);
            this.txB_author.Name = "txB_author";
            this.txB_author.Size = new System.Drawing.Size(65, 20);
            this.txB_author.TabIndex = 5;
            // 
            // txB_actions
            // 
            this.txB_actions.Location = new System.Drawing.Point(295, 13);
            this.txB_actions.Name = "txB_actions";
            this.txB_actions.Size = new System.Drawing.Size(65, 20);
            this.txB_actions.TabIndex = 4;
            // 
            // txB_info
            // 
            this.txB_info.Location = new System.Drawing.Point(224, 13);
            this.txB_info.Name = "txB_info";
            this.txB_info.Size = new System.Drawing.Size(65, 20);
            this.txB_info.TabIndex = 3;
            // 
            // txB_problem
            // 
            this.txB_problem.Location = new System.Drawing.Point(153, 13);
            this.txB_problem.Name = "txB_problem";
            this.txB_problem.Size = new System.Drawing.Size(65, 20);
            this.txB_problem.TabIndex = 2;
            // 
            // txB_model
            // 
            this.txB_model.Location = new System.Drawing.Point(82, 13);
            this.txB_model.Name = "txB_model";
            this.txB_model.Size = new System.Drawing.Size(65, 20);
            this.txB_model.TabIndex = 1;
            // 
            // txB_id
            // 
            this.txB_id.Location = new System.Drawing.Point(11, 13);
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(65, 20);
            this.txB_id.TabIndex = 0;
            // 
            // btn_save_excel
            // 
            this.btn_save_excel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_save_excel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save_excel.Location = new System.Drawing.Point(480, 9);
            this.btn_save_excel.Name = "btn_save_excel";
            this.btn_save_excel.Size = new System.Drawing.Size(229, 31);
            this.btn_save_excel.TabIndex = 99;
            this.btn_save_excel.Text = "Сохранить в excel";
            this.btn_save_excel.UseVisualStyleBackColor = false;
            this.btn_save_excel.Click += new System.EventHandler(this.Btn_save_excel_Click);
            // 
            // TutorialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1352, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(1368, 700);
            this.MinimumSize = new System.Drawing.Size(1368, 700);
            this.Name = "TutorialForm";
            this.Text = "Обучалка";
            this.Load += new System.EventHandler(this.TutorialForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txB_search;
        private System.Windows.Forms.PictureBox picB_update;
        private System.Windows.Forms.Button btn_new_rst_problem;
        private System.Windows.Forms.ComboBox cmB_seach;
        private System.Windows.Forms.ComboBox cmb_unique;
        private System.Windows.Forms.Button btn_brief_info;
        private System.Windows.Forms.Button btn_change_problem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txB_id;
        private System.Windows.Forms.TextBox txB_author;
        private System.Windows.Forms.TextBox txB_actions;
        private System.Windows.Forms.TextBox txB_info;
        private System.Windows.Forms.TextBox txB_problem;
        private System.Windows.Forms.TextBox txB_model;
        private System.Windows.Forms.Button btn_delete_problem;
        private System.Windows.Forms.Button btn_save_excel;
    }
}