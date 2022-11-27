namespace ServiceTelecomConnect
{
    partial class AddToProblemRST
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
            this.txB_problem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbL_Author = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.cmB_model = new System.Windows.Forms.ComboBox();
            this.txB_info = new System.Windows.Forms.TextBox();
            this.txB_actions = new System.Windows.Forms.TextBox();
            this.btn_save_add_rst_problem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // txB_problem
            // 
            this.txB_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_problem.Location = new System.Drawing.Point(16, 114);
            this.txB_problem.Multiline = true;
            this.txB_problem.Name = "txB_problem";
            this.txB_problem.Size = new System.Drawing.Size(352, 114);
            this.txB_problem.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 20);
            this.label4.TabIndex = 49;
            this.label4.Text = "Описание дефекта:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(463, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(299, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "Виды работ по устраненнию дефекта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Неисправность:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(12, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.TabIndex = 46;
            this.label11.Text = "Модель:";
            // 
            // lbL_Author
            // 
            this.lbL_Author.AutoSize = true;
            this.lbL_Author.BackColor = System.Drawing.Color.Transparent;
            this.lbL_Author.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_Author.Location = new System.Drawing.Point(559, 17);
            this.lbL_Author.Name = "lbL_Author";
            this.lbL_Author.Size = new System.Drawing.Size(194, 27);
            this.lbL_Author.TabIndex = 45;
            this.lbL_Author.Text = "Горбунов Д.А.";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.pictureBox4.Location = new System.Drawing.Point(759, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(33, 32);
            this.pictureBox4.TabIndex = 53;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.PictureBox4_Click);
            // 
            // cmB_model
            // 
            this.cmB_model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_model.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_model.FormattingEnabled = true;
            this.cmB_model.Items.AddRange(new object[] {
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
            this.cmB_model.Location = new System.Drawing.Point(100, 16);
            this.cmB_model.Name = "cmB_model";
            this.cmB_model.Size = new System.Drawing.Size(268, 28);
            this.cmB_model.TabIndex = 84;
            // 
            // txB_info
            // 
            this.txB_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_info.Location = new System.Drawing.Point(16, 276);
            this.txB_info.Multiline = true;
            this.txB_info.Name = "txB_info";
            this.txB_info.Size = new System.Drawing.Size(348, 149);
            this.txB_info.TabIndex = 86;
            // 
            // txB_actions
            // 
            this.txB_actions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_actions.Location = new System.Drawing.Point(426, 114);
            this.txB_actions.Multiline = true;
            this.txB_actions.Name = "txB_actions";
            this.txB_actions.Size = new System.Drawing.Size(366, 311);
            this.txB_actions.TabIndex = 87;
            // 
            // btn_save_add_rst_problem
            // 
            this.btn_save_add_rst_problem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_save_add_rst_problem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save_add_rst_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save_add_rst_problem.Location = new System.Drawing.Point(673, 441);
            this.btn_save_add_rst_problem.Name = "btn_save_add_rst_problem";
            this.btn_save_add_rst_problem.Size = new System.Drawing.Size(119, 30);
            this.btn_save_add_rst_problem.TabIndex = 88;
            this.btn_save_add_rst_problem.Text = "Добавить";
            this.btn_save_add_rst_problem.UseVisualStyleBackColor = false;
            this.btn_save_add_rst_problem.Click += new System.EventHandler(this.Btn_save_add_rst_problem_Click);
            // 
            // AddToProblemRST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.btn_save_add_rst_problem);
            this.Controls.Add(this.txB_actions);
            this.Controls.Add(this.txB_info);
            this.Controls.Add(this.cmB_model);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbL_Author);
            this.Controls.Add(this.txB_problem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddToProblemRST";
            this.ShowIcon = false;
            this.Text = "Добавление неисправности радиостанции";
            this.Load += new System.EventHandler(this.AddToProblemRST_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txB_problem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbL_Author;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ComboBox cmB_model;
        private System.Windows.Forms.TextBox txB_info;
        private System.Windows.Forms.TextBox txB_actions;
        private System.Windows.Forms.Button btn_save_add_rst_problem;
    }
}