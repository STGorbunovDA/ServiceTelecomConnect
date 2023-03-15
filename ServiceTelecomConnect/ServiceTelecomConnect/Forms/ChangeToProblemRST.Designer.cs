namespace ServiceTelecomConnect
{
    partial class ChangeToProblemRST
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
            this.picB_clearControl = new System.Windows.Forms.PictureBox();
            this.cmB_model = new System.Windows.Forms.ComboBox();
            this.txB_info = new System.Windows.Forms.TextBox();
            this.txB_actions = new System.Windows.Forms.TextBox();
            this.btn_ChageRadiostantionProblem = new System.Windows.Forms.Button();
            this.cmB_problem = new System.Windows.Forms.ComboBox();
            this.chB_problem_Enable = new System.Windows.Forms.CheckBox();
            this.txB_id = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clearControl)).BeginInit();
            this.SuspendLayout();
            // 
            // txB_problem
            // 
            this.txB_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_problem.Location = new System.Drawing.Point(16, 114);
            this.txB_problem.Multiline = true;
            this.txB_problem.Name = "txB_problem";
            this.txB_problem.Size = new System.Drawing.Size(377, 114);
            this.txB_problem.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(519, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 20);
            this.label4.TabIndex = 49;
            this.label4.Text = "Описание дефекта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "Виды работ по устраненнию дефекта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Неисп.:";
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
            // picB_clearControl
            // 
            this.picB_clearControl.BackColor = System.Drawing.Color.Transparent;
            this.picB_clearControl.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.picB_clearControl.Location = new System.Drawing.Point(759, 12);
            this.picB_clearControl.Name = "picB_clearControl";
            this.picB_clearControl.Size = new System.Drawing.Size(33, 32);
            this.picB_clearControl.TabIndex = 53;
            this.picB_clearControl.TabStop = false;
            this.picB_clearControl.Click += new System.EventHandler(this.ClearControlForm);
            // 
            // cmB_model
            // 
            this.cmB_model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_model.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_model.FormattingEnabled = true;
            this.cmB_model.Location = new System.Drawing.Point(100, 16);
            this.cmB_model.Name = "cmB_model";
            this.cmB_model.Size = new System.Drawing.Size(293, 28);
            this.cmB_model.TabIndex = 84;
            this.cmB_model.Click += new System.EventHandler(this.CmbModelClick);
            // 
            // txB_info
            // 
            this.txB_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_info.Location = new System.Drawing.Point(411, 114);
            this.txB_info.Multiline = true;
            this.txB_info.Name = "txB_info";
            this.txB_info.Size = new System.Drawing.Size(369, 321);
            this.txB_info.TabIndex = 86;
            // 
            // txB_actions
            // 
            this.txB_actions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_actions.Location = new System.Drawing.Point(16, 272);
            this.txB_actions.Multiline = true;
            this.txB_actions.Name = "txB_actions";
            this.txB_actions.Size = new System.Drawing.Size(377, 198);
            this.txB_actions.TabIndex = 87;
            // 
            // btn_ChageRadiostantionProblem
            // 
            this.btn_ChageRadiostantionProblem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_ChageRadiostantionProblem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ChageRadiostantionProblem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_ChageRadiostantionProblem.Location = new System.Drawing.Point(661, 441);
            this.btn_ChageRadiostantionProblem.Name = "btn_ChageRadiostantionProblem";
            this.btn_ChageRadiostantionProblem.Size = new System.Drawing.Size(119, 30);
            this.btn_ChageRadiostantionProblem.TabIndex = 88;
            this.btn_ChageRadiostantionProblem.Text = "Изменить";
            this.btn_ChageRadiostantionProblem.UseVisualStyleBackColor = false;
            this.btn_ChageRadiostantionProblem.Click += new System.EventHandler(this.BtnChangeRadiostantionProblemClick);
            // 
            // cmB_problem
            // 
            this.cmB_problem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_problem.Enabled = false;
            this.cmB_problem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_problem.FormattingEnabled = true;
            this.cmB_problem.Items.AddRange(new object[] {
            "Не выключается",
            "Не включается",
            "Произвольно включается",
            "Нет приёма",
            "Нет передачи",
            "Регулятор/Переключатель",
            "Корпус"});
            this.cmB_problem.Location = new System.Drawing.Point(100, 77);
            this.cmB_problem.Name = "cmB_problem";
            this.cmB_problem.Size = new System.Drawing.Size(293, 28);
            this.cmB_problem.TabIndex = 95;
            // 
            // chB_problem_Enable
            // 
            this.chB_problem_Enable.AutoSize = true;
            this.chB_problem_Enable.BackColor = System.Drawing.Color.Transparent;
            this.chB_problem_Enable.Location = new System.Drawing.Point(79, 85);
            this.chB_problem_Enable.Name = "chB_problem_Enable";
            this.chB_problem_Enable.Size = new System.Drawing.Size(15, 14);
            this.chB_problem_Enable.TabIndex = 117;
            this.chB_problem_Enable.UseVisualStyleBackColor = false;
            this.chB_problem_Enable.Click += new System.EventHandler(this.ChbProblemEnableClick);
            // 
            // txB_id
            // 
            this.txB_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_id.Location = new System.Drawing.Point(496, 19);
            this.txB_id.Multiline = true;
            this.txB_id.Name = "txB_id";
            this.txB_id.Size = new System.Drawing.Size(57, 24);
            this.txB_id.TabIndex = 118;
            this.txB_id.Visible = false;
            // 
            // ChangeToProblemRST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources._999;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.txB_id);
            this.Controls.Add(this.chB_problem_Enable);
            this.Controls.Add(this.cmB_problem);
            this.Controls.Add(this.btn_ChageRadiostantionProblem);
            this.Controls.Add(this.txB_actions);
            this.Controls.Add(this.txB_info);
            this.Controls.Add(this.cmB_model);
            this.Controls.Add(this.picB_clearControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbL_Author);
            this.Controls.Add(this.txB_problem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(816, 521);
            this.MinimumSize = new System.Drawing.Size(816, 521);
            this.Name = "ChangeToProblemRST";
            this.ShowIcon = false;
            this.Text = "Изменение неисправности радиостанции";
            this.Load += new System.EventHandler(this.AddToProblemRadiostantionLoad);
            ((System.ComponentModel.ISupportInitialize)(this.picB_clearControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox picB_clearControl;
        private System.Windows.Forms.Button btn_ChageRadiostantionProblem;
        private System.Windows.Forms.CheckBox chB_problem_Enable;
        internal System.Windows.Forms.TextBox txB_problem;
        internal System.Windows.Forms.ComboBox cmB_model;
        internal System.Windows.Forms.TextBox txB_info;
        internal System.Windows.Forms.TextBox txB_actions;
        internal System.Windows.Forms.ComboBox cmB_problem;
        internal System.Windows.Forms.Label lbL_Author;
        internal System.Windows.Forms.TextBox txB_id;
    }
}