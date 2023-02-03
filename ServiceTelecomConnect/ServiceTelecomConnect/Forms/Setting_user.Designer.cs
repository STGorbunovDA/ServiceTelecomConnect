namespace ServiceTelecomConnect
{
    partial class Setting_user
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picB_clear = new System.Windows.Forms.PictureBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.cmB_isAdminPost = new System.Windows.Forms.ComboBox();
            this.txB_pass = new System.Windows.Forms.TextBox();
            this.txB_login = new System.Windows.Forms.TextBox();
            this.txB_id = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel1.Controls.Add(this.label36);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 64);
            this.panel1.TabIndex = 1;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(179, 21);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(439, 22);
            this.label36.TabIndex = 61;
            this.label36.Text = "Панель управления доступа пользователей";
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
            this.dataGridView1.Location = new System.Drawing.Point(5, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(800, 287);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellClick);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel2.Controls.Add(this.picB_clear);
            this.panel2.Controls.Add(this.btn_add);
            this.panel2.Controls.Add(this.btn_change);
            this.panel2.Controls.Add(this.btn_delete);
            this.panel2.Controls.Add(this.btn_update);
            this.panel2.Controls.Add(this.cmB_isAdminPost);
            this.panel2.Controls.Add(this.txB_pass);
            this.panel2.Controls.Add(this.txB_login);
            this.panel2.Controls.Add(this.txB_id);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 363);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(809, 98);
            this.panel2.TabIndex = 2;
            // 
            // picB_clear
            // 
            this.picB_clear.BackColor = System.Drawing.Color.Transparent;
            this.picB_clear.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.picB_clear.Location = new System.Drawing.Point(764, 16);
            this.picB_clear.Name = "picB_clear";
            this.picB_clear.Size = new System.Drawing.Size(33, 30);
            this.picB_clear.TabIndex = 9;
            this.picB_clear.TabStop = false;
            this.picB_clear.Click += new System.EventHandler(this.ClearControlFormClick);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add.Location = new System.Drawing.Point(183, 57);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(83, 29);
            this.btn_add.TabIndex = 61;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // btn_change
            // 
            this.btn_change.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_change.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_change.Location = new System.Drawing.Point(481, 57);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(83, 29);
            this.btn_change.TabIndex = 60;
            this.btn_change.Text = "Изменить";
            this.btn_change.UseVisualStyleBackColor = false;
            this.btn_change.Click += new System.EventHandler(this.BtnChangeClick);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_delete.Location = new System.Drawing.Point(382, 57);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(83, 29);
            this.btn_delete.TabIndex = 59;
            this.btn_delete.Text = "Удалить";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.ButtonDeleteClick);
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_update.Location = new System.Drawing.Point(282, 57);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(83, 29);
            this.btn_update.TabIndex = 58;
            this.btn_update.Text = "Обновить";
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.ButtonUpdateClick);
            // 
            // cmB_is_admin_post
            // 
            this.cmB_isAdminPost.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmB_isAdminPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_isAdminPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_isAdminPost.FormattingEnabled = true;
            this.cmB_isAdminPost.Items.AddRange(new object[] {
            "Инженер",
            "Начальник участка",
            "Куратор",
            "Руководитель",
            "Дирекция связи",
            "Admin"});
            this.cmB_isAdminPost.Location = new System.Drawing.Point(570, 18);
            this.cmB_isAdminPost.Name = "cmB_is_admin_post";
            this.cmB_isAdminPost.Size = new System.Drawing.Size(188, 28);
            this.cmB_isAdminPost.TabIndex = 57;
            // 
            // txB_pass
            // 
            this.txB_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_pass.Location = new System.Drawing.Point(180, 20);
            this.txB_pass.Name = "txB_pass";
            this.txB_pass.Size = new System.Drawing.Size(384, 26);
            this.txB_pass.TabIndex = 21;
            // 
            // txB_login
            // 
            this.txB_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_login.Location = new System.Drawing.Point(12, 20);
            this.txB_login.Name = "txB_login";
            this.txB_login.Size = new System.Drawing.Size(162, 26);
            this.txB_login.TabIndex = 20;
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(5, 69);
            this.txB_id.Name = "txB_id";
            this.txB_id.ReadOnly = true;
            this.txB_id.Size = new System.Drawing.Size(50, 26);
            this.txB_id.TabIndex = 19;
            this.txB_id.Visible = false;
            // 
            // Setting_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(809, 461);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(825, 500);
            this.MinimumSize = new System.Drawing.Size(825, 500);
            this.Name = "Setting_user";
            this.Text = "Setting_user";
            this.Load += new System.EventHandler(this.SettingUserLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txB_id;
        private System.Windows.Forms.TextBox txB_pass;
        private System.Windows.Forms.TextBox txB_login;
        private System.Windows.Forms.ComboBox cmB_isAdminPost;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.PictureBox picB_clear;
    }
}