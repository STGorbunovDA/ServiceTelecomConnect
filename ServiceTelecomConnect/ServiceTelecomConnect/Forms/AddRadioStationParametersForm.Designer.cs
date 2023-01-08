namespace ServiceTelecomConnect.Forms
{
    partial class AddRadioStationParametersForm
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
            this.lbL_serialNumber = new System.Windows.Forms.Label();
            this.lbL_model = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txB_dateTO = new System.Windows.Forms.TextBox();
            this.lbL_numberAct = new System.Windows.Forms.Label();
            this.lbL_AKB = new System.Windows.Forms.Label();
            this.txB_AKB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbL_serialNumber
            // 
            this.lbL_serialNumber.AutoSize = true;
            this.lbL_serialNumber.BackColor = System.Drawing.Color.Transparent;
            this.lbL_serialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_serialNumber.Location = new System.Drawing.Point(326, 49);
            this.lbL_serialNumber.Name = "lbL_serialNumber";
            this.lbL_serialNumber.Size = new System.Drawing.Size(113, 20);
            this.lbL_serialNumber.TabIndex = 67;
            this.lbL_serialNumber.Text = "672TTTM099";
            // 
            // lbL_model
            // 
            this.lbL_model.AutoSize = true;
            this.lbL_model.BackColor = System.Drawing.Color.Transparent;
            this.lbL_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_model.Location = new System.Drawing.Point(175, 49);
            this.lbL_model.Name = "lbL_model";
            this.lbL_model.Size = new System.Drawing.Size(145, 20);
            this.lbL_model.TabIndex = 68;
            this.lbL_model.Text = "Motorola GP-340";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(445, 46);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 71;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar1_DateSelected);
            // 
            // txB_dateTO
            // 
            this.txB_dateTO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_dateTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTO.Location = new System.Drawing.Point(445, 46);
            this.txB_dateTO.MaxLength = 19;
            this.txB_dateTO.Name = "txB_dateTO";
            this.txB_dateTO.Size = new System.Drawing.Size(114, 26);
            this.txB_dateTO.TabIndex = 70;
            this.txB_dateTO.TabStop = false;
            this.txB_dateTO.Click += new System.EventHandler(this.TxB_dateTO_Click);
            // 
            // lbL_numberAct
            // 
            this.lbL_numberAct.AutoSize = true;
            this.lbL_numberAct.BackColor = System.Drawing.Color.Transparent;
            this.lbL_numberAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_numberAct.Location = new System.Drawing.Point(567, 49);
            this.lbL_numberAct.Name = "lbL_numberAct";
            this.lbL_numberAct.Size = new System.Drawing.Size(80, 20);
            this.lbL_numberAct.TabIndex = 72;
            this.lbL_numberAct.Text = "№53/250";
            // 
            // lbL_AKB
            // 
            this.lbL_AKB.AutoSize = true;
            this.lbL_AKB.BackColor = System.Drawing.Color.Transparent;
            this.lbL_AKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_AKB.Location = new System.Drawing.Point(531, 308);
            this.lbL_AKB.Name = "lbL_AKB";
            this.lbL_AKB.Size = new System.Drawing.Size(97, 20);
            this.lbL_AKB.TabIndex = 73;
            this.lbL_AKB.Text = "1815 AKL7";
            // 
            // txB_AKB
            // 
            this.txB_AKB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_AKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_AKB.Location = new System.Drawing.Point(535, 331);
            this.txB_AKB.MaxLength = 19;
            this.txB_AKB.Name = "txB_AKB";
            this.txB_AKB.Size = new System.Drawing.Size(93, 26);
            this.txB_AKB.TabIndex = 74;
            this.txB_AKB.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(238, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Параметры радиостанции";
            // 
            // AddRadioStationParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(761, 380);
            this.Controls.Add(this.lbL_AKB);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txB_AKB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbL_numberAct);
            this.Controls.Add(this.txB_dateTO);
            this.Controls.Add(this.lbL_serialNumber);
            this.Controls.Add(this.lbL_model);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRadioStationParametersForm";
            this.Text = "Добавление параметров радиостанции";
            this.Load += new System.EventHandler(this.AddRadioStationParametersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        internal System.Windows.Forms.TextBox txB_dateTO;
        internal System.Windows.Forms.Label lbL_serialNumber;
        internal System.Windows.Forms.Label lbL_model;
        internal System.Windows.Forms.Label lbL_numberAct;
        internal System.Windows.Forms.Label lbL_AKB;
        internal System.Windows.Forms.TextBox txB_AKB;
        private System.Windows.Forms.Label label1;
    }
}