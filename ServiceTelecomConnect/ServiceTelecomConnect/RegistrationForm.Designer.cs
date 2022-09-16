namespace ServiceTelecomConnect
{
    partial class RegistrationForm
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
            this.passField = new System.Windows.Forms.TextBox();
            this.enterButtonLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.loginField = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Label();
            this.hidePassword = new System.Windows.Forms.Label();
            this.openPassword = new System.Windows.Forms.Label();
            this.comboBox_post = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // passField
            // 
            this.passField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passField.Location = new System.Drawing.Point(420, 252);
            this.passField.Multiline = true;
            this.passField.Name = "passField";
            this.passField.Size = new System.Drawing.Size(162, 31);
            this.passField.TabIndex = 26;
            // 
            // enterButtonLogin
            // 
            this.enterButtonLogin.BackColor = System.Drawing.Color.White;
            this.enterButtonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enterButtonLogin.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enterButtonLogin.ForeColor = System.Drawing.Color.Black;
            this.enterButtonLogin.Location = new System.Drawing.Point(335, 368);
            this.enterButtonLogin.Name = "enterButtonLogin";
            this.enterButtonLogin.Size = new System.Drawing.Size(160, 48);
            this.enterButtonLogin.TabIndex = 25;
            this.enterButtonLogin.Text = "Enter";
            this.enterButtonLogin.UseVisualStyleBackColor = false;
            this.enterButtonLogin.Click += new System.EventHandler(this.EnterButtonLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label4.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(484, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 22);
            this.label4.TabIndex = 24;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label3.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(252, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 22);
            this.label3.TabIndex = 23;
            this.label3.Text = "Login";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.pictureBox2.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.lock_64;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(588, 217);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 66);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // loginField
            // 
            this.loginField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginField.Location = new System.Drawing.Point(252, 252);
            this.loginField.Multiline = true;
            this.loginField.Name = "loginField";
            this.loginField.Size = new System.Drawing.Size(162, 31);
            this.loginField.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.pictureBox1.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.user_64;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(188, 222);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(282, 144);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(260, 41);
            this.AuthorizationLabel.TabIndex = 19;
            this.AuthorizationLabel.Text = "Регистрация";
            this.AuthorizationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // clear
            // 
            this.clear.AutoSize = true;
            this.clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clear.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear.Location = new System.Drawing.Point(383, 286);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(63, 14);
            this.clear.TabIndex = 27;
            this.clear.Text = "очистить";
            this.clear.Click += new System.EventHandler(this.Clear_Click);
            this.clear.MouseEnter += new System.EventHandler(this.Clear_MouseEnter);
            this.clear.MouseLeave += new System.EventHandler(this.Clear_MouseLeave);
            // 
            // hidePassword
            // 
            this.hidePassword.AutoSize = true;
            this.hidePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.hidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hidePassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hidePassword.Location = new System.Drawing.Point(484, 286);
            this.hidePassword.Name = "hidePassword";
            this.hidePassword.Size = new System.Drawing.Size(98, 14);
            this.hidePassword.TabIndex = 29;
            this.hidePassword.Text = "скрыть пароль";
            this.hidePassword.Click += new System.EventHandler(this.HidePassword_Click);
            this.hidePassword.MouseEnter += new System.EventHandler(this.HidePassword_MouseEnter);
            this.hidePassword.MouseLeave += new System.EventHandler(this.HidePassword_MouseLeave);
            // 
            // openPassword
            // 
            this.openPassword.AutoSize = true;
            this.openPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.openPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openPassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openPassword.Location = new System.Drawing.Point(470, 286);
            this.openPassword.Name = "openPassword";
            this.openPassword.Size = new System.Drawing.Size(112, 14);
            this.openPassword.TabIndex = 28;
            this.openPassword.Text = "показать пароль";
            this.openPassword.Click += new System.EventHandler(this.OpenPassword_Click);
            this.openPassword.MouseEnter += new System.EventHandler(this.OpenPassword_MouseEnter);
            this.openPassword.MouseLeave += new System.EventHandler(this.OpenPassword_MouseLeave);
            // 
            // comboBox_post
            // 
            this.comboBox_post.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_post.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_post.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_post.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_post.FormattingEnabled = true;
            this.comboBox_post.Items.AddRange(new object[] {
            "Инженер",
            "Начальник участка",
            "Куратор",
            "Руководитель",
            "Дирекция связи"});
            this.comboBox_post.Location = new System.Drawing.Point(311, 315);
            this.comboBox_post.Name = "comboBox_post";
            this.comboBox_post.Size = new System.Drawing.Size(211, 28);
            this.comboBox_post.TabIndex = 57;
            // 
            // RegistrationForm
            // 
            this.AcceptButton = this.enterButtonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.comboBox_post);
            this.Controls.Add(this.hidePassword);
            this.Controls.Add(this.openPassword);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.passField);
            this.Controls.Add(this.enterButtonLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.loginField);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AuthorizationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(850, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "RegistrationForm";
            this.ShowIcon = false;
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.RegistrationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox passField;
        private System.Windows.Forms.Button enterButtonLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox loginField;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AuthorizationLabel;
        private System.Windows.Forms.Label clear;
        private System.Windows.Forms.Label hidePassword;
        private System.Windows.Forms.Label openPassword;
        private System.Windows.Forms.ComboBox comboBox_post;
    }
}