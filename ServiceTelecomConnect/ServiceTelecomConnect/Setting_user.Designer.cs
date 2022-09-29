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
            this.PicB_clear = new System.Windows.Forms.PictureBox();
            this.Btn_add = new System.Windows.Forms.Button();
            this.Btn_change = new System.Windows.Forms.Button();
            this.Btn_delete = new System.Windows.Forms.Button();
            this.Btn_update = new System.Windows.Forms.Button();
            this.comboBox_is_admin_post = new System.Windows.Forms.ComboBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicB_clear)).BeginInit();
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
            this.dataGridView1.Size = new System.Drawing.Size(800, 276);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.panel2.Controls.Add(this.PicB_clear);
            this.panel2.Controls.Add(this.Btn_add);
            this.panel2.Controls.Add(this.Btn_change);
            this.panel2.Controls.Add(this.Btn_delete);
            this.panel2.Controls.Add(this.Btn_update);
            this.panel2.Controls.Add(this.comboBox_is_admin_post);
            this.panel2.Controls.Add(this.textBox_pass);
            this.panel2.Controls.Add(this.textBox_login);
            this.panel2.Controls.Add(this.textBox_id);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 363);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(809, 98);
            this.panel2.TabIndex = 2;
            // 
            // PicB_clear
            // 
            this.PicB_clear.BackColor = System.Drawing.Color.Transparent;
            this.PicB_clear.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.PicB_clear.Location = new System.Drawing.Point(764, 16);
            this.PicB_clear.Name = "PicB_clear";
            this.PicB_clear.Size = new System.Drawing.Size(33, 30);
            this.PicB_clear.TabIndex = 9;
            this.PicB_clear.TabStop = false;
            this.PicB_clear.Click += new System.EventHandler(this.PicB_clear_Click);
            // 
            // Btn_add
            // 
            this.Btn_add.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Btn_add.Location = new System.Drawing.Point(183, 57);
            this.Btn_add.Name = "Btn_add";
            this.Btn_add.Size = new System.Drawing.Size(83, 29);
            this.Btn_add.TabIndex = 61;
            this.Btn_add.Text = "Добавить";
            this.Btn_add.UseVisualStyleBackColor = false;
            this.Btn_add.Click += new System.EventHandler(this.Btn_add_Click);
            // 
            // Btn_change
            // 
            this.Btn_change.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_change.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Btn_change.Location = new System.Drawing.Point(481, 57);
            this.Btn_change.Name = "Btn_change";
            this.Btn_change.Size = new System.Drawing.Size(83, 29);
            this.Btn_change.TabIndex = 60;
            this.Btn_change.Text = "Изменить";
            this.Btn_change.UseVisualStyleBackColor = false;
            this.Btn_change.Click += new System.EventHandler(this.Button_change_Click);
            // 
            // Btn_delete
            // 
            this.Btn_delete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Btn_delete.Location = new System.Drawing.Point(382, 57);
            this.Btn_delete.Name = "Btn_delete";
            this.Btn_delete.Size = new System.Drawing.Size(83, 29);
            this.Btn_delete.TabIndex = 59;
            this.Btn_delete.Text = "Удалить";
            this.Btn_delete.UseVisualStyleBackColor = false;
            this.Btn_delete.Click += new System.EventHandler(this.Button_delete_Click);
            // 
            // Btn_update
            // 
            this.Btn_update.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Btn_update.Location = new System.Drawing.Point(282, 57);
            this.Btn_update.Name = "Btn_update";
            this.Btn_update.Size = new System.Drawing.Size(83, 29);
            this.Btn_update.TabIndex = 58;
            this.Btn_update.Text = "Обновить";
            this.Btn_update.UseVisualStyleBackColor = false;
            this.Btn_update.Click += new System.EventHandler(this.Button_update_Click);
            // 
            // comboBox_is_admin_post
            // 
            this.comboBox_is_admin_post.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.comboBox_is_admin_post.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_is_admin_post.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_is_admin_post.FormattingEnabled = true;
            this.comboBox_is_admin_post.Items.AddRange(new object[] {
            "Инженер",
            "Начальник участка",
            "Куратор",
            "Руководитель",
            "Дирекция связи",
            "Admin"});
            this.comboBox_is_admin_post.Location = new System.Drawing.Point(570, 18);
            this.comboBox_is_admin_post.Name = "comboBox_is_admin_post";
            this.comboBox_is_admin_post.Size = new System.Drawing.Size(188, 28);
            this.comboBox_is_admin_post.TabIndex = 57;
            // 
            // textBox_pass
            // 
            this.textBox_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_pass.Location = new System.Drawing.Point(180, 20);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.Size = new System.Drawing.Size(384, 26);
            this.textBox_pass.TabIndex = 21;
            // 
            // textBox_login
            // 
            this.textBox_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_login.Location = new System.Drawing.Point(12, 20);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(162, 26);
            this.textBox_login.TabIndex = 20;
            // 
            // textBox_id
            // 
            this.textBox_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_id.Location = new System.Drawing.Point(5, 69);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.ReadOnly = true;
            this.textBox_id.Size = new System.Drawing.Size(50, 26);
            this.textBox_id.TabIndex = 19;
            this.textBox_id.Visible = false;
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
            this.Load += new System.EventHandler(this.Setting_user_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicB_clear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.ComboBox comboBox_is_admin_post;
        private System.Windows.Forms.Button Btn_update;
        private System.Windows.Forms.Button Btn_delete;
        private System.Windows.Forms.Button Btn_change;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button Btn_add;
        private System.Windows.Forms.PictureBox PicB_clear;
    }
}