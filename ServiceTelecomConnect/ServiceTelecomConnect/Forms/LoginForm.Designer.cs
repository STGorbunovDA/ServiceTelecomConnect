﻿namespace ServiceTelecomConnect
{
    partial class LoginForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.AuthorizationLabel = new System.Windows.Forms.Label();
            this.btnAuthorization = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.lbL_registrationLoginForm = new System.Windows.Forms.Label();
            this.openPassword = new System.Windows.Forms.Label();
            this.hidePassword = new System.Windows.Forms.Label();
            this.lbL_clear = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AuthorizationLabel
            // 
            this.AuthorizationLabel.AutoSize = true;
            this.AuthorizationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationLabel.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorizationLabel.Location = new System.Drawing.Point(284, 146);
            this.AuthorizationLabel.Name = "AuthorizationLabel";
            this.AuthorizationLabel.Size = new System.Drawing.Size(260, 41);
            this.AuthorizationLabel.TabIndex = 0;
            this.AuthorizationLabel.Text = "Авторизация";
            this.AuthorizationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAuthorization
            // 
            this.btnAuthorization.BackColor = System.Drawing.Color.White;
            this.btnAuthorization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuthorization.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAuthorization.ForeColor = System.Drawing.Color.Black;
            this.btnAuthorization.Location = new System.Drawing.Point(337, 321);
            this.btnAuthorization.Name = "btnAuthorization";
            this.btnAuthorization.Size = new System.Drawing.Size(160, 48);
            this.btnAuthorization.TabIndex = 16;
            this.btnAuthorization.Text = "Enter";
            this.btnAuthorization.UseVisualStyleBackColor = false;
            this.btnAuthorization.Click += new System.EventHandler(this.EnterButtonLoginClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label4.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(518, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 22);
            this.label4.TabIndex = 15;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.label3.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(214, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 22);
            this.label3.TabIndex = 14;
            this.label3.Text = "Login";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.pictureBox2.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.lock_64;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(622, 220);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 66);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // txbLogin
            // 
            this.txbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbLogin.Location = new System.Drawing.Point(214, 255);
            this.txbLogin.Multiline = true;
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(198, 31);
            this.txbLogin.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.pictureBox1.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.user_64;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(150, 225);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // txbPassword
            // 
            this.txbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbPassword.Location = new System.Drawing.Point(418, 255);
            this.txbPassword.Multiline = true;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(198, 31);
            this.txbPassword.TabIndex = 17;
            // 
            // lbL_registrationLoginForm
            // 
            this.lbL_registrationLoginForm.AutoSize = true;
            this.lbL_registrationLoginForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.lbL_registrationLoginForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbL_registrationLoginForm.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_registrationLoginForm.Location = new System.Drawing.Point(344, 393);
            this.lbL_registrationLoginForm.Name = "lbL_registrationLoginForm";
            this.lbL_registrationLoginForm.Size = new System.Drawing.Size(143, 16);
            this.lbL_registrationLoginForm.TabIndex = 18;
            this.lbL_registrationLoginForm.Text = "Еще нет аккаунта?";
            this.lbL_registrationLoginForm.Visible = false;
            this.lbL_registrationLoginForm.Click += new System.EventHandler(this.RegistrationLoginFormClick);
            this.lbL_registrationLoginForm.MouseEnter += new System.EventHandler(this.RegistrationLoginFormMouseEnter);
            this.lbL_registrationLoginForm.MouseLeave += new System.EventHandler(this.RegistrationLoginFormMouseLeave);
            // 
            // openPassword
            // 
            this.openPassword.AutoSize = true;
            this.openPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.openPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openPassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openPassword.Location = new System.Drawing.Point(504, 289);
            this.openPassword.Name = "openPassword";
            this.openPassword.Size = new System.Drawing.Size(112, 14);
            this.openPassword.TabIndex = 19;
            this.openPassword.Text = "показать пароль";
            this.openPassword.Click += new System.EventHandler(this.OpenPasswordClick);
            this.openPassword.MouseEnter += new System.EventHandler(this.OpenPasswordMouseEnter);
            this.openPassword.MouseLeave += new System.EventHandler(this.OpenPasswordMouseLeave);
            // 
            // hidePassword
            // 
            this.hidePassword.AutoSize = true;
            this.hidePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.hidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hidePassword.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hidePassword.Location = new System.Drawing.Point(503, 304);
            this.hidePassword.Name = "hidePassword";
            this.hidePassword.Size = new System.Drawing.Size(98, 14);
            this.hidePassword.TabIndex = 20;
            this.hidePassword.Text = "скрыть пароль";
            this.hidePassword.Click += new System.EventHandler(this.HidePasswordClick);
            this.hidePassword.MouseEnter += new System.EventHandler(this.HidePasswordMouseEnter);
            this.hidePassword.MouseLeave += new System.EventHandler(this.HidePasswordMouseLeave);
            // 
            // lbL_clear
            // 
            this.lbL_clear.AutoSize = true;
            this.lbL_clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(219)))), ((int)(((byte)(94)))));
            this.lbL_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbL_clear.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_clear.Location = new System.Drawing.Point(383, 289);
            this.lbL_clear.Name = "lbL_clear";
            this.lbL_clear.Size = new System.Drawing.Size(63, 14);
            this.lbL_clear.TabIndex = 28;
            this.lbL_clear.Text = "очистить";
            this.lbL_clear.Click += new System.EventHandler(this.ClearClick);
            this.lbL_clear.MouseEnter += new System.EventHandler(this.ClearMouseEnter);
            this.lbL_clear.MouseLeave += new System.EventHandler(this.ClearMouseLeave);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnAuthorization;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_2;
            this.ClientSize = new System.Drawing.Size(799, 461);
            this.Controls.Add(this.lbL_clear);
            this.Controls.Add(this.hidePassword);
            this.Controls.Add(this.openPassword);
            this.Controls.Add(this.lbL_registrationLoginForm);
            this.Controls.Add(this.btnAuthorization);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AuthorizationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(815, 500);
            this.MinimumSize = new System.Drawing.Size(815, 500);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.LoginFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthorizationLabel;
        private System.Windows.Forms.Button btnAuthorization;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label lbL_registrationLoginForm;
        private System.Windows.Forms.Label openPassword;
        private System.Windows.Forms.Label hidePassword;
        private System.Windows.Forms.Label lbL_clear;
    }
}

