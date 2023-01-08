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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txB_dateTO = new System.Windows.Forms.TextBox();
            this.lbL_AKB = new System.Windows.Forms.Label();
            this.txB_AKB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txB_model = new System.Windows.Forms.TextBox();
            this.txB_serialNumber = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txB_numberAct = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.pnl_transmitter = new System.Windows.Forms.Panel();
            this.pnl_frequencies = new System.Windows.Forms.Panel();
            this.pnl_transmitter.SuspendLayout();
            this.pnl_frequencies.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(388, 397);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 71;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar1_DateSelected);
            // 
            // txB_dateTO
            // 
            this.txB_dateTO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_dateTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTO.Location = new System.Drawing.Point(61, 63);
            this.txB_dateTO.MaxLength = 19;
            this.txB_dateTO.Name = "txB_dateTO";
            this.txB_dateTO.ReadOnly = true;
            this.txB_dateTO.Size = new System.Drawing.Size(113, 26);
            this.txB_dateTO.TabIndex = 70;
            this.txB_dateTO.TabStop = false;
            this.txB_dateTO.Click += new System.EventHandler(this.TxB_dateTO_Click);
            // 
            // lbL_AKB
            // 
            this.lbL_AKB.AutoSize = true;
            this.lbL_AKB.BackColor = System.Drawing.Color.Transparent;
            this.lbL_AKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_AKB.Location = new System.Drawing.Point(46, 489);
            this.lbL_AKB.Name = "lbL_AKB";
            this.lbL_AKB.Size = new System.Drawing.Size(97, 20);
            this.lbL_AKB.TabIndex = 73;
            this.lbL_AKB.Text = "1815 AKL7";
            // 
            // txB_AKB
            // 
            this.txB_AKB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_AKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_AKB.Location = new System.Drawing.Point(50, 512);
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
            this.label1.Location = new System.Drawing.Point(274, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Параметры радиостанции";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(8, 67);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 18);
            this.label16.TabIndex = 148;
            this.label16.Text = "Дата:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(180, 67);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 18);
            this.label21.TabIndex = 156;
            this.label21.Text = "Модель:";
            // 
            // txB_model
            // 
            this.txB_model.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_model.Location = new System.Drawing.Point(254, 63);
            this.txB_model.Multiline = true;
            this.txB_model.Name = "txB_model";
            this.txB_model.ReadOnly = true;
            this.txB_model.Size = new System.Drawing.Size(170, 28);
            this.txB_model.TabIndex = 160;
            this.txB_model.TabStop = false;
            // 
            // txB_serialNumber
            // 
            this.txB_serialNumber.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_serialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_serialNumber.Location = new System.Drawing.Point(501, 63);
            this.txB_serialNumber.Multiline = true;
            this.txB_serialNumber.Name = "txB_serialNumber";
            this.txB_serialNumber.ReadOnly = true;
            this.txB_serialNumber.Size = new System.Drawing.Size(175, 28);
            this.txB_serialNumber.TabIndex = 162;
            this.txB_serialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(430, 66);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 20);
            this.label22.TabIndex = 161;
            this.label22.Text = "Зав. №:";
            // 
            // txB_numberAct
            // 
            this.txB_numberAct.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_numberAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberAct.Location = new System.Drawing.Point(751, 63);
            this.txB_numberAct.MaxLength = 31;
            this.txB_numberAct.Name = "txB_numberAct";
            this.txB_numberAct.ReadOnly = true;
            this.txB_numberAct.Size = new System.Drawing.Size(86, 26);
            this.txB_numberAct.TabIndex = 164;
            this.txB_numberAct.TabStop = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label40.Location = new System.Drawing.Point(682, 67);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(63, 18);
            this.label40.TabIndex = 163;
            this.label40.Text = "№ Акта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(257, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 165;
            this.label2.Text = "Передатчик";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 166;
            this.label3.Text = "Низкий, Вт:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(107, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(72, 28);
            this.textBox1.TabIndex = 167;
            this.textBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 18);
            this.label4.TabIndex = 168;
            this.label4.Text = "Высокий, Вт:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(107, 57);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(72, 28);
            this.textBox2.TabIndex = 169;
            this.textBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(185, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 18);
            this.label5.TabIndex = 170;
            this.label5.Text = "Отклоние, Гц:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(359, 23);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(72, 28);
            this.textBox3.TabIndex = 171;
            this.textBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(184, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 18);
            this.label6.TabIndex = 172;
            this.label6.Text = "Чувствительность, мВ:";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(359, 57);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(72, 28);
            this.textBox4.TabIndex = 173;
            this.textBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(488, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 18);
            this.label7.TabIndex = 174;
            this.label7.Text = "КНИ, %:";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(559, 23);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(72, 28);
            this.textBox5.TabIndex = 175;
            this.textBox5.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(437, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 18);
            this.label8.TabIndex = 176;
            this.label8.Text = "Девиация, кГЦ:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(559, 57);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(72, 28);
            this.textBox6.TabIndex = 177;
            this.textBox6.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(668, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 27);
            this.label9.TabIndex = 178;
            this.label9.Text = "Частоты(МГц)";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(3, 3);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(175, 226);
            this.textBox7.TabIndex = 179;
            this.textBox7.TabStop = false;
            // 
            // pnl_transmitter
            // 
            this.pnl_transmitter.BackColor = System.Drawing.Color.Transparent;
            this.pnl_transmitter.Controls.Add(this.textBox6);
            this.pnl_transmitter.Controls.Add(this.label3);
            this.pnl_transmitter.Controls.Add(this.textBox1);
            this.pnl_transmitter.Controls.Add(this.label4);
            this.pnl_transmitter.Controls.Add(this.label8);
            this.pnl_transmitter.Controls.Add(this.textBox2);
            this.pnl_transmitter.Controls.Add(this.textBox5);
            this.pnl_transmitter.Controls.Add(this.label5);
            this.pnl_transmitter.Controls.Add(this.label7);
            this.pnl_transmitter.Controls.Add(this.textBox3);
            this.pnl_transmitter.Controls.Add(this.textBox4);
            this.pnl_transmitter.Controls.Add(this.label6);
            this.pnl_transmitter.Location = new System.Drawing.Point(14, 142);
            this.pnl_transmitter.Name = "pnl_transmitter";
            this.pnl_transmitter.Size = new System.Drawing.Size(644, 119);
            this.pnl_transmitter.TabIndex = 180;
            // 
            // pnl_frequencies
            // 
            this.pnl_frequencies.BackColor = System.Drawing.Color.Transparent;
            this.pnl_frequencies.Controls.Add(this.textBox7);
            this.pnl_frequencies.Location = new System.Drawing.Point(664, 142);
            this.pnl_frequencies.Name = "pnl_frequencies";
            this.pnl_frequencies.Size = new System.Drawing.Size(186, 232);
            this.pnl_frequencies.TabIndex = 181;
            // 
            // AddRadioStationParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(849, 729);
            this.Controls.Add(this.pnl_frequencies);
            this.Controls.Add(this.pnl_transmitter);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txB_numberAct);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.txB_serialNumber);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txB_model);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lbL_AKB);
            this.Controls.Add(this.txB_AKB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txB_dateTO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRadioStationParametersForm";
            this.Text = "Добавление параметров радиостанции";
            this.Load += new System.EventHandler(this.AddRadioStationParametersForm_Load);
            this.pnl_transmitter.ResumeLayout(false);
            this.pnl_transmitter.PerformLayout();
            this.pnl_frequencies.ResumeLayout(false);
            this.pnl_frequencies.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        internal System.Windows.Forms.TextBox txB_dateTO;
        internal System.Windows.Forms.Label lbL_AKB;
        internal System.Windows.Forms.TextBox txB_AKB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label21;
        internal System.Windows.Forms.TextBox txB_model;
        internal System.Windows.Forms.TextBox txB_serialNumber;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox txB_numberAct;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel pnl_transmitter;
        private System.Windows.Forms.Panel pnl_frequencies;
    }
}