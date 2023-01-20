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
            this.lbL_nameAKB = new System.Windows.Forms.Label();
            this.txB_percentAKB = new System.Windows.Forms.TextBox();
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
            this.txB_LowPowerLevelTransmitter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txB_HighPowerLevelTransmitter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txB_FrequencyDeviationTransmitter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txB_SensitivityTransmitter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txB_KNITransmitter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txB_DeviationTransmitter = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txB_TransmitterFrequencies = new System.Windows.Forms.TextBox();
            this.pnl_transmitter = new System.Windows.Forms.Panel();
            this.pnl_frequencies = new System.Windows.Forms.Panel();
            this.txB_ReceiverFrequencies = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnl_Receiver = new System.Windows.Forms.Panel();
            this.txB_SuppressorReceiver = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txB_SelectivityReceiver = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txB_OutputPowerWattReceiver = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txB_OutputPowerVoltReceiver = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txB_SensitivityReceiver = new System.Windows.Forms.TextBox();
            this.txB_KNIReceiver = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnl_CurrentConsumption = new System.Windows.Forms.Panel();
            this.txB_BatteryDischargeAlarmCurrentConsumption = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txB_TransmissionModeCurrentConsumption = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txB_ReceptionModeCurrentConsumption = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txB_StandbyModeCurrentConsumption = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.pnl_Accessories = new System.Windows.Forms.Panel();
            this.lbL_ManipulatorAccessories = new System.Windows.Forms.Label();
            this.lbL_BatteryChargerAccessories = new System.Windows.Forms.Label();
            this.cmB_ManipulatorAccessories = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmB_BatteryChargerAccessories = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.pnl_info_rst = new System.Windows.Forms.Panel();
            this.pnl_AKB = new System.Windows.Forms.Panel();
            this.btn_Save_add_rst_remont = new System.Windows.Forms.Button();
            this.pnl_NoteRadioStationParameters = new System.Windows.Forms.Panel();
            this.txB_NoteRadioStationParameters = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cmB_frequency = new System.Windows.Forms.ComboBox();
            this.lbL_city = new System.Windows.Forms.Label();
            this.lbL_road = new System.Windows.Forms.Label();
            this.btn_Frequencies = new System.Windows.Forms.Button();
            this.pnl_transmitter.SuspendLayout();
            this.pnl_frequencies.SuspendLayout();
            this.pnl_Receiver.SuspendLayout();
            this.pnl_CurrentConsumption.SuspendLayout();
            this.pnl_Accessories.SuspendLayout();
            this.pnl_info_rst.SuspendLayout();
            this.pnl_AKB.SuspendLayout();
            this.pnl_NoteRadioStationParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(140, 60);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 71;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar1_DateSelected);
            // 
            // txB_dateTO
            // 
            this.txB_dateTO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_dateTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTO.Location = new System.Drawing.Point(61, 12);
            this.txB_dateTO.MaxLength = 19;
            this.txB_dateTO.Name = "txB_dateTO";
            this.txB_dateTO.ReadOnly = true;
            this.txB_dateTO.Size = new System.Drawing.Size(113, 26);
            this.txB_dateTO.TabIndex = 70;
            this.txB_dateTO.TabStop = false;
            this.txB_dateTO.Click += new System.EventHandler(this.TxB_dateTO_Click);
            // 
            // lbL_nameAKB
            // 
            this.lbL_nameAKB.AutoSize = true;
            this.lbL_nameAKB.BackColor = System.Drawing.Color.Transparent;
            this.lbL_nameAKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_nameAKB.Location = new System.Drawing.Point(48, 7);
            this.lbL_nameAKB.Name = "lbL_nameAKB";
            this.lbL_nameAKB.Size = new System.Drawing.Size(97, 20);
            this.lbL_nameAKB.TabIndex = 73;
            this.lbL_nameAKB.Text = "1815 AKL7";
            // 
            // txB_percentAKB
            // 
            this.txB_percentAKB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_percentAKB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_percentAKB.Location = new System.Drawing.Point(49, 30);
            this.txB_percentAKB.MaxLength = 19;
            this.txB_percentAKB.Name = "txB_percentAKB";
            this.txB_percentAKB.Size = new System.Drawing.Size(93, 26);
            this.txB_percentAKB.TabIndex = 74;
            this.txB_percentAKB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_AKB_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(394, 9);
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
            this.label16.Location = new System.Drawing.Point(8, 16);
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
            this.label21.Location = new System.Drawing.Point(180, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 18);
            this.label21.TabIndex = 156;
            this.label21.Text = "Модель:";
            // 
            // txB_model
            // 
            this.txB_model.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_model.Location = new System.Drawing.Point(254, 12);
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
            this.txB_serialNumber.Location = new System.Drawing.Point(501, 12);
            this.txB_serialNumber.Multiline = true;
            this.txB_serialNumber.Name = "txB_serialNumber";
            this.txB_serialNumber.ReadOnly = true;
            this.txB_serialNumber.Size = new System.Drawing.Size(153, 28);
            this.txB_serialNumber.TabIndex = 162;
            this.txB_serialNumber.TabStop = false;
            this.txB_serialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(430, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 20);
            this.label22.TabIndex = 161;
            this.label22.Text = "Зав. №:";
            // 
            // txB_numberAct
            // 
            this.txB_numberAct.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_numberAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberAct.Location = new System.Drawing.Point(728, 12);
            this.txB_numberAct.MaxLength = 31;
            this.txB_numberAct.Name = "txB_numberAct";
            this.txB_numberAct.ReadOnly = true;
            this.txB_numberAct.Size = new System.Drawing.Size(94, 26);
            this.txB_numberAct.TabIndex = 164;
            this.txB_numberAct.TabStop = false;
            this.txB_numberAct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label40.Location = new System.Drawing.Point(659, 16);
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
            this.label2.Location = new System.Drawing.Point(253, 114);
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
            // txB_LowPowerLevelTransmitter
            // 
            this.txB_LowPowerLevelTransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_LowPowerLevelTransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_LowPowerLevelTransmitter.Location = new System.Drawing.Point(107, 23);
            this.txB_LowPowerLevelTransmitter.Name = "txB_LowPowerLevelTransmitter";
            this.txB_LowPowerLevelTransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_LowPowerLevelTransmitter.TabIndex = 167;
            this.txB_LowPowerLevelTransmitter.TabStop = false;
            this.txB_LowPowerLevelTransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_LowPowerLevelTransmitter_KeyPress);
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
            // txB_HighPowerLevelTransmitter
            // 
            this.txB_HighPowerLevelTransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_HighPowerLevelTransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_HighPowerLevelTransmitter.Location = new System.Drawing.Point(107, 57);
            this.txB_HighPowerLevelTransmitter.Name = "txB_HighPowerLevelTransmitter";
            this.txB_HighPowerLevelTransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_HighPowerLevelTransmitter.TabIndex = 169;
            this.txB_HighPowerLevelTransmitter.TabStop = false;
            this.txB_HighPowerLevelTransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_HighPowerLevelTransmitter_KeyPress);
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
            // txB_FrequencyDeviationTransmitter
            // 
            this.txB_FrequencyDeviationTransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_FrequencyDeviationTransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_FrequencyDeviationTransmitter.Location = new System.Drawing.Point(359, 23);
            this.txB_FrequencyDeviationTransmitter.Name = "txB_FrequencyDeviationTransmitter";
            this.txB_FrequencyDeviationTransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_FrequencyDeviationTransmitter.TabIndex = 171;
            this.txB_FrequencyDeviationTransmitter.TabStop = false;
            this.txB_FrequencyDeviationTransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_FrequencyDeviationTransmitter_KeyPress);
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
            // txB_SensitivityTransmitter
            // 
            this.txB_SensitivityTransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_SensitivityTransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_SensitivityTransmitter.Location = new System.Drawing.Point(359, 57);
            this.txB_SensitivityTransmitter.Name = "txB_SensitivityTransmitter";
            this.txB_SensitivityTransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_SensitivityTransmitter.TabIndex = 173;
            this.txB_SensitivityTransmitter.TabStop = false;
            this.txB_SensitivityTransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_SensitivityTransmitter_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(496, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 18);
            this.label7.TabIndex = 174;
            this.label7.Text = "КНИ, %:";
            // 
            // txB_KNITransmitter
            // 
            this.txB_KNITransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_KNITransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_KNITransmitter.Location = new System.Drawing.Point(567, 23);
            this.txB_KNITransmitter.Name = "txB_KNITransmitter";
            this.txB_KNITransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_KNITransmitter.TabIndex = 175;
            this.txB_KNITransmitter.TabStop = false;
            this.txB_KNITransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_KNITransmitter_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(445, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 18);
            this.label8.TabIndex = 176;
            this.label8.Text = "Девиация, кГЦ:";
            // 
            // txB_DeviationTransmitter
            // 
            this.txB_DeviationTransmitter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_DeviationTransmitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_DeviationTransmitter.Location = new System.Drawing.Point(567, 57);
            this.txB_DeviationTransmitter.Name = "txB_DeviationTransmitter";
            this.txB_DeviationTransmitter.Size = new System.Drawing.Size(72, 26);
            this.txB_DeviationTransmitter.TabIndex = 177;
            this.txB_DeviationTransmitter.TabStop = false;
            this.txB_DeviationTransmitter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_DeviationTransmitter_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(759, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(194, 27);
            this.label9.TabIndex = 178;
            this.label9.Text = "Частоты (МГц)";
            // 
            // txB_TransmitterFrequencies
            // 
            this.txB_TransmitterFrequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_TransmitterFrequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_TransmitterFrequencies.Location = new System.Drawing.Point(15, 23);
            this.txB_TransmitterFrequencies.Multiline = true;
            this.txB_TransmitterFrequencies.Name = "txB_TransmitterFrequencies";
            this.txB_TransmitterFrequencies.Size = new System.Drawing.Size(163, 215);
            this.txB_TransmitterFrequencies.TabIndex = 179;
            this.txB_TransmitterFrequencies.TabStop = false;
            this.txB_TransmitterFrequencies.Click += new System.EventHandler(this.TxB_TransmitterFrequencies_Click);
            this.txB_TransmitterFrequencies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_TransmitterFrequencies_KeyPress);
            // 
            // pnl_transmitter
            // 
            this.pnl_transmitter.BackColor = System.Drawing.Color.Transparent;
            this.pnl_transmitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_transmitter.Controls.Add(this.txB_DeviationTransmitter);
            this.pnl_transmitter.Controls.Add(this.label3);
            this.pnl_transmitter.Controls.Add(this.txB_LowPowerLevelTransmitter);
            this.pnl_transmitter.Controls.Add(this.label4);
            this.pnl_transmitter.Controls.Add(this.label8);
            this.pnl_transmitter.Controls.Add(this.txB_HighPowerLevelTransmitter);
            this.pnl_transmitter.Controls.Add(this.txB_KNITransmitter);
            this.pnl_transmitter.Controls.Add(this.label5);
            this.pnl_transmitter.Controls.Add(this.label7);
            this.pnl_transmitter.Controls.Add(this.txB_FrequencyDeviationTransmitter);
            this.pnl_transmitter.Controls.Add(this.txB_SensitivityTransmitter);
            this.pnl_transmitter.Controls.Add(this.label6);
            this.pnl_transmitter.Location = new System.Drawing.Point(10, 144);
            this.pnl_transmitter.Name = "pnl_transmitter";
            this.pnl_transmitter.Size = new System.Drawing.Size(644, 119);
            this.pnl_transmitter.TabIndex = 180;
            // 
            // pnl_frequencies
            // 
            this.pnl_frequencies.BackColor = System.Drawing.Color.Transparent;
            this.pnl_frequencies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_frequencies.Controls.Add(this.txB_ReceiverFrequencies);
            this.pnl_frequencies.Controls.Add(this.label26);
            this.pnl_frequencies.Controls.Add(this.label25);
            this.pnl_frequencies.Controls.Add(this.txB_TransmitterFrequencies);
            this.pnl_frequencies.Location = new System.Drawing.Point(663, 144);
            this.pnl_frequencies.Name = "pnl_frequencies";
            this.pnl_frequencies.Size = new System.Drawing.Size(380, 256);
            this.pnl_frequencies.TabIndex = 181;
            // 
            // txB_ReceiverFrequencies
            // 
            this.txB_ReceiverFrequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_ReceiverFrequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_ReceiverFrequencies.Location = new System.Drawing.Point(201, 23);
            this.txB_ReceiverFrequencies.Multiline = true;
            this.txB_ReceiverFrequencies.Name = "txB_ReceiverFrequencies";
            this.txB_ReceiverFrequencies.Size = new System.Drawing.Size(163, 215);
            this.txB_ReceiverFrequencies.TabIndex = 181;
            this.txB_ReceiverFrequencies.TabStop = false;
            this.txB_ReceiverFrequencies.Click += new System.EventHandler(this.TxB_ReceiverFrequencies_Click);
            this.txB_ReceiverFrequencies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_ReceiverFrequencies_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.Location = new System.Drawing.Point(255, 2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(54, 18);
            this.label26.TabIndex = 180;
            this.label26.Text = "Приём";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(60, 2);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(76, 18);
            this.label25.TabIndex = 178;
            this.label25.Text = "Передача";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(270, 266);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 27);
            this.label10.TabIndex = 182;
            this.label10.Text = "Приёмник";
            // 
            // pnl_Receiver
            // 
            this.pnl_Receiver.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Receiver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Receiver.Controls.Add(this.txB_SuppressorReceiver);
            this.pnl_Receiver.Controls.Add(this.label17);
            this.pnl_Receiver.Controls.Add(this.txB_SelectivityReceiver);
            this.pnl_Receiver.Controls.Add(this.label14);
            this.pnl_Receiver.Controls.Add(this.txB_OutputPowerWattReceiver);
            this.pnl_Receiver.Controls.Add(this.label13);
            this.pnl_Receiver.Controls.Add(this.label11);
            this.pnl_Receiver.Controls.Add(this.txB_OutputPowerVoltReceiver);
            this.pnl_Receiver.Controls.Add(this.label12);
            this.pnl_Receiver.Controls.Add(this.txB_SensitivityReceiver);
            this.pnl_Receiver.Controls.Add(this.txB_KNIReceiver);
            this.pnl_Receiver.Controls.Add(this.label15);
            this.pnl_Receiver.Location = new System.Drawing.Point(13, 296);
            this.pnl_Receiver.Name = "pnl_Receiver";
            this.pnl_Receiver.Size = new System.Drawing.Size(644, 104);
            this.pnl_Receiver.TabIndex = 181;
            // 
            // txB_SuppressorReceiver
            // 
            this.txB_SuppressorReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_SuppressorReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_SuppressorReceiver.Location = new System.Drawing.Point(566, 53);
            this.txB_SuppressorReceiver.Name = "txB_SuppressorReceiver";
            this.txB_SuppressorReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_SuppressorReceiver.TabIndex = 181;
            this.txB_SuppressorReceiver.TabStop = false;
            this.txB_SuppressorReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_SuppressorReceiver_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(483, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 18);
            this.label17.TabIndex = 180;
            this.label17.Text = "ШУМ, мкВ:";
            // 
            // txB_SelectivityReceiver
            // 
            this.txB_SelectivityReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_SelectivityReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_SelectivityReceiver.Location = new System.Drawing.Point(405, 19);
            this.txB_SelectivityReceiver.Name = "txB_SelectivityReceiver";
            this.txB_SelectivityReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_SelectivityReceiver.TabIndex = 179;
            this.txB_SelectivityReceiver.TabStop = false;
            this.txB_SelectivityReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_SelectivityReceiver_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(250, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(135, 18);
            this.label14.TabIndex = 178;
            this.label14.Text = "Избирательн., дБ:";
            // 
            // txB_OutputPowerWattReceiver
            // 
            this.txB_OutputPowerWattReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_OutputPowerWattReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_OutputPowerWattReceiver.Location = new System.Drawing.Point(164, 53);
            this.txB_OutputPowerWattReceiver.Name = "txB_OutputPowerWattReceiver";
            this.txB_OutputPowerWattReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_OutputPowerWattReceiver.TabIndex = 177;
            this.txB_OutputPowerWattReceiver.TabStop = false;
            this.txB_OutputPowerWattReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_OutputPowerWattReceiver_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(14, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(144, 18);
            this.label13.TabIndex = 176;
            this.label13.Text = "Вых. мощность, Вт:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(14, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 18);
            this.label11.TabIndex = 166;
            this.label11.Text = "Вых. мощность, В:";
            // 
            // txB_OutputPowerVoltReceiver
            // 
            this.txB_OutputPowerVoltReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_OutputPowerVoltReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_OutputPowerVoltReceiver.Location = new System.Drawing.Point(164, 19);
            this.txB_OutputPowerVoltReceiver.Name = "txB_OutputPowerVoltReceiver";
            this.txB_OutputPowerVoltReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_OutputPowerVoltReceiver.TabIndex = 167;
            this.txB_OutputPowerVoltReceiver.TabStop = false;
            this.txB_OutputPowerVoltReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_OutputPowerVoltReceiver_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(250, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 18);
            this.label12.TabIndex = 168;
            this.label12.Text = "Чувствительн., мкВ:";
            // 
            // txB_SensitivityReceiver
            // 
            this.txB_SensitivityReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_SensitivityReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_SensitivityReceiver.Location = new System.Drawing.Point(405, 53);
            this.txB_SensitivityReceiver.Name = "txB_SensitivityReceiver";
            this.txB_SensitivityReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_SensitivityReceiver.TabIndex = 169;
            this.txB_SensitivityReceiver.TabStop = false;
            this.txB_SensitivityReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_SensitivityReceiver_KeyPress);
            // 
            // txB_KNIReceiver
            // 
            this.txB_KNIReceiver.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_KNIReceiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_KNIReceiver.Location = new System.Drawing.Point(566, 19);
            this.txB_KNIReceiver.Name = "txB_KNIReceiver";
            this.txB_KNIReceiver.Size = new System.Drawing.Size(72, 26);
            this.txB_KNIReceiver.TabIndex = 175;
            this.txB_KNIReceiver.TabStop = false;
            this.txB_KNIReceiver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_KNIReceiver_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(495, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 18);
            this.label15.TabIndex = 174;
            this.label15.Text = "КНИ, %:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(215, 403);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(236, 27);
            this.label18.TabIndex = 183;
            this.label18.Text = "Потребляемый ток";
            // 
            // pnl_CurrentConsumption
            // 
            this.pnl_CurrentConsumption.BackColor = System.Drawing.Color.Transparent;
            this.pnl_CurrentConsumption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_CurrentConsumption.Controls.Add(this.txB_BatteryDischargeAlarmCurrentConsumption);
            this.pnl_CurrentConsumption.Controls.Add(this.label24);
            this.pnl_CurrentConsumption.Controls.Add(this.txB_TransmissionModeCurrentConsumption);
            this.pnl_CurrentConsumption.Controls.Add(this.label23);
            this.pnl_CurrentConsumption.Controls.Add(this.txB_ReceptionModeCurrentConsumption);
            this.pnl_CurrentConsumption.Controls.Add(this.label20);
            this.pnl_CurrentConsumption.Controls.Add(this.txB_StandbyModeCurrentConsumption);
            this.pnl_CurrentConsumption.Controls.Add(this.label19);
            this.pnl_CurrentConsumption.Location = new System.Drawing.Point(13, 432);
            this.pnl_CurrentConsumption.Name = "pnl_CurrentConsumption";
            this.pnl_CurrentConsumption.Size = new System.Drawing.Size(644, 104);
            this.pnl_CurrentConsumption.TabIndex = 184;
            // 
            // txB_BatteryDischargeAlarmCurrentConsumption
            // 
            this.txB_BatteryDischargeAlarmCurrentConsumption.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_BatteryDischargeAlarmCurrentConsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_BatteryDischargeAlarmCurrentConsumption.Location = new System.Drawing.Point(566, 57);
            this.txB_BatteryDischargeAlarmCurrentConsumption.Name = "txB_BatteryDischargeAlarmCurrentConsumption";
            this.txB_BatteryDischargeAlarmCurrentConsumption.Size = new System.Drawing.Size(72, 26);
            this.txB_BatteryDischargeAlarmCurrentConsumption.TabIndex = 184;
            this.txB_BatteryDischargeAlarmCurrentConsumption.TabStop = false;
            this.txB_BatteryDischargeAlarmCurrentConsumption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_BatteryDischargeAlarmCurrentConsumption_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(332, 61);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(222, 18);
            this.label24.TabIndex = 183;
            this.label24.Text = "Сигнализация разряда АКБ, В:";
            // 
            // txB_TransmissionModeCurrentConsumption
            // 
            this.txB_TransmissionModeCurrentConsumption.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_TransmissionModeCurrentConsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_TransmissionModeCurrentConsumption.Location = new System.Drawing.Point(566, 23);
            this.txB_TransmissionModeCurrentConsumption.Name = "txB_TransmissionModeCurrentConsumption";
            this.txB_TransmissionModeCurrentConsumption.Size = new System.Drawing.Size(72, 26);
            this.txB_TransmissionModeCurrentConsumption.TabIndex = 182;
            this.txB_TransmissionModeCurrentConsumption.TabStop = false;
            this.txB_TransmissionModeCurrentConsumption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_TransmissionModeCurrentConsumption_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(259, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(295, 18);
            this.label23.TabIndex = 181;
            this.label23.Text = "Режим передачи (высокая мощность), А:";
            // 
            // txB_ReceptionModeCurrentConsumption
            // 
            this.txB_ReceptionModeCurrentConsumption.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_ReceptionModeCurrentConsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_ReceptionModeCurrentConsumption.Location = new System.Drawing.Point(172, 57);
            this.txB_ReceptionModeCurrentConsumption.Name = "txB_ReceptionModeCurrentConsumption";
            this.txB_ReceptionModeCurrentConsumption.Size = new System.Drawing.Size(72, 26);
            this.txB_ReceptionModeCurrentConsumption.TabIndex = 179;
            this.txB_ReceptionModeCurrentConsumption.TabStop = false;
            this.txB_ReceptionModeCurrentConsumption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_ReceptionModeCurrentConsumption_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(3, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 18);
            this.label20.TabIndex = 180;
            this.label20.Text = "Режим приём, мА:";
            // 
            // txB_StandbyModeCurrentConsumption
            // 
            this.txB_StandbyModeCurrentConsumption.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_StandbyModeCurrentConsumption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_StandbyModeCurrentConsumption.Location = new System.Drawing.Point(172, 23);
            this.txB_StandbyModeCurrentConsumption.Name = "txB_StandbyModeCurrentConsumption";
            this.txB_StandbyModeCurrentConsumption.Size = new System.Drawing.Size(72, 26);
            this.txB_StandbyModeCurrentConsumption.TabIndex = 178;
            this.txB_StandbyModeCurrentConsumption.TabStop = false;
            this.txB_StandbyModeCurrentConsumption.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxB_StandbyModeCurrentConsumption_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(3, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(163, 18);
            this.label19.TabIndex = 178;
            this.label19.Text = "Дежурный режим, мА:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(679, 403);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(348, 27);
            this.label27.TabIndex = 185;
            this.label27.Text = "Аксессуары (при наличии)";
            // 
            // pnl_Accessories
            // 
            this.pnl_Accessories.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Accessories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Accessories.Controls.Add(this.lbL_ManipulatorAccessories);
            this.pnl_Accessories.Controls.Add(this.lbL_BatteryChargerAccessories);
            this.pnl_Accessories.Controls.Add(this.cmB_ManipulatorAccessories);
            this.pnl_Accessories.Controls.Add(this.label29);
            this.pnl_Accessories.Controls.Add(this.cmB_BatteryChargerAccessories);
            this.pnl_Accessories.Controls.Add(this.label28);
            this.pnl_Accessories.Location = new System.Drawing.Point(663, 432);
            this.pnl_Accessories.Name = "pnl_Accessories";
            this.pnl_Accessories.Size = new System.Drawing.Size(380, 104);
            this.pnl_Accessories.TabIndex = 186;
            // 
            // lbL_ManipulatorAccessories
            // 
            this.lbL_ManipulatorAccessories.AutoSize = true;
            this.lbL_ManipulatorAccessories.BackColor = System.Drawing.Color.Transparent;
            this.lbL_ManipulatorAccessories.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_ManipulatorAccessories.Location = new System.Drawing.Point(332, 61);
            this.lbL_ManipulatorAccessories.Name = "lbL_ManipulatorAccessories";
            this.lbL_ManipulatorAccessories.Size = new System.Drawing.Size(16, 18);
            this.lbL_ManipulatorAccessories.TabIndex = 190;
            this.lbL_ManipulatorAccessories.Text = "2";
            this.lbL_ManipulatorAccessories.Visible = false;
            // 
            // lbL_BatteryChargerAccessories
            // 
            this.lbL_BatteryChargerAccessories.AutoSize = true;
            this.lbL_BatteryChargerAccessories.BackColor = System.Drawing.Color.Transparent;
            this.lbL_BatteryChargerAccessories.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_BatteryChargerAccessories.Location = new System.Drawing.Point(332, 27);
            this.lbL_BatteryChargerAccessories.Name = "lbL_BatteryChargerAccessories";
            this.lbL_BatteryChargerAccessories.Size = new System.Drawing.Size(16, 18);
            this.lbL_BatteryChargerAccessories.TabIndex = 189;
            this.lbL_BatteryChargerAccessories.Text = "1";
            this.lbL_BatteryChargerAccessories.Visible = false;
            // 
            // cmB_ManipulatorAccessories
            // 
            this.cmB_ManipulatorAccessories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_ManipulatorAccessories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_ManipulatorAccessories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_ManipulatorAccessories.FormattingEnabled = true;
            this.cmB_ManipulatorAccessories.Items.AddRange(new object[] {
            "испр.",
            "неиспр."});
            this.cmB_ManipulatorAccessories.Location = new System.Drawing.Point(248, 57);
            this.cmB_ManipulatorAccessories.Name = "cmB_ManipulatorAccessories";
            this.cmB_ManipulatorAccessories.Size = new System.Drawing.Size(78, 28);
            this.cmB_ManipulatorAccessories.TabIndex = 188;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(37, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(205, 18);
            this.label29.TabIndex = 187;
            this.label29.Text = "Манипулятор: испр./неиспр.:";
            // 
            // cmB_BatteryChargerAccessories
            // 
            this.cmB_BatteryChargerAccessories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_BatteryChargerAccessories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_BatteryChargerAccessories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_BatteryChargerAccessories.FormattingEnabled = true;
            this.cmB_BatteryChargerAccessories.Items.AddRange(new object[] {
            "испр.",
            "неиспр."});
            this.cmB_BatteryChargerAccessories.Location = new System.Drawing.Point(248, 23);
            this.cmB_BatteryChargerAccessories.Name = "cmB_BatteryChargerAccessories";
            this.cmB_BatteryChargerAccessories.Size = new System.Drawing.Size(78, 28);
            this.cmB_BatteryChargerAccessories.TabIndex = 186;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(98, 27);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(128, 18);
            this.label28.TabIndex = 185;
            this.label28.Text = "ЗУ испр./неиспр.:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(2, 34);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 18);
            this.label30.TabIndex = 187;
            this.label30.Text = "АКБ:";
            // 
            // pnl_info_rst
            // 
            this.pnl_info_rst.BackColor = System.Drawing.Color.Transparent;
            this.pnl_info_rst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_info_rst.Controls.Add(this.txB_serialNumber);
            this.pnl_info_rst.Controls.Add(this.txB_dateTO);
            this.pnl_info_rst.Controls.Add(this.label16);
            this.pnl_info_rst.Controls.Add(this.label21);
            this.pnl_info_rst.Controls.Add(this.txB_model);
            this.pnl_info_rst.Controls.Add(this.label22);
            this.pnl_info_rst.Controls.Add(this.label40);
            this.pnl_info_rst.Controls.Add(this.txB_numberAct);
            this.pnl_info_rst.Location = new System.Drawing.Point(83, 48);
            this.pnl_info_rst.Name = "pnl_info_rst";
            this.pnl_info_rst.Size = new System.Drawing.Size(846, 52);
            this.pnl_info_rst.TabIndex = 188;
            // 
            // pnl_AKB
            // 
            this.pnl_AKB.BackColor = System.Drawing.Color.Transparent;
            this.pnl_AKB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_AKB.Controls.Add(this.txB_percentAKB);
            this.pnl_AKB.Controls.Add(this.lbL_nameAKB);
            this.pnl_AKB.Controls.Add(this.label30);
            this.pnl_AKB.Location = new System.Drawing.Point(663, 542);
            this.pnl_AKB.Name = "pnl_AKB";
            this.pnl_AKB.Size = new System.Drawing.Size(161, 77);
            this.pnl_AKB.TabIndex = 189;
            // 
            // btn_Save_add_rst_remont
            // 
            this.btn_Save_add_rst_remont.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Save_add_rst_remont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save_add_rst_remont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Save_add_rst_remont.Location = new System.Drawing.Point(883, 589);
            this.btn_Save_add_rst_remont.Name = "btn_Save_add_rst_remont";
            this.btn_Save_add_rst_remont.Size = new System.Drawing.Size(129, 30);
            this.btn_Save_add_rst_remont.TabIndex = 190;
            this.btn_Save_add_rst_remont.Text = "Добавить";
            this.btn_Save_add_rst_remont.UseVisualStyleBackColor = false;
            this.btn_Save_add_rst_remont.Click += new System.EventHandler(this.Btn_save_add_rst_remont_Click);
            // 
            // pnl_NoteRadioStationParameters
            // 
            this.pnl_NoteRadioStationParameters.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NoteRadioStationParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_NoteRadioStationParameters.Controls.Add(this.txB_NoteRadioStationParameters);
            this.pnl_NoteRadioStationParameters.Controls.Add(this.label31);
            this.pnl_NoteRadioStationParameters.Location = new System.Drawing.Point(13, 546);
            this.pnl_NoteRadioStationParameters.Name = "pnl_NoteRadioStationParameters";
            this.pnl_NoteRadioStationParameters.Size = new System.Drawing.Size(644, 73);
            this.pnl_NoteRadioStationParameters.TabIndex = 191;
            // 
            // txB_NoteRadioStationParameters
            // 
            this.txB_NoteRadioStationParameters.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_NoteRadioStationParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_NoteRadioStationParameters.Location = new System.Drawing.Point(106, 10);
            this.txB_NoteRadioStationParameters.Multiline = true;
            this.txB_NoteRadioStationParameters.Name = "txB_NoteRadioStationParameters";
            this.txB_NoteRadioStationParameters.Size = new System.Drawing.Size(532, 54);
            this.txB_NoteRadioStationParameters.TabIndex = 185;
            this.txB_NoteRadioStationParameters.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.Location = new System.Drawing.Point(4, 30);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(98, 18);
            this.label31.TabIndex = 185;
            this.label31.Text = "Примечание:";
            // 
            // cmB_frequency
            // 
            this.cmB_frequency.BackColor = System.Drawing.SystemColors.Window;
            this.cmB_frequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmB_frequency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_frequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_frequency.FormattingEnabled = true;
            this.cmB_frequency.Location = new System.Drawing.Point(716, 385);
            this.cmB_frequency.Name = "cmB_frequency";
            this.cmB_frequency.Size = new System.Drawing.Size(264, 228);
            this.cmB_frequency.TabIndex = 193;
            this.cmB_frequency.Visible = false;
            this.cmB_frequency.SelectionChangeCommitted += new System.EventHandler(this.CmB_frequency_SelectionChangeCommitted);
            this.cmB_frequency.MouseLeave += new System.EventHandler(this.CmB_frequency_MouseLeave);
            // 
            // lbL_city
            // 
            this.lbL_city.AutoSize = true;
            this.lbL_city.BackColor = System.Drawing.Color.Transparent;
            this.lbL_city.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_city.Location = new System.Drawing.Point(975, 14);
            this.lbL_city.Name = "lbL_city";
            this.lbL_city.Size = new System.Drawing.Size(52, 18);
            this.lbL_city.TabIndex = 182;
            this.lbL_city.Text = "Город";
            this.lbL_city.Visible = false;
            // 
            // lbL_road
            // 
            this.lbL_road.AutoSize = true;
            this.lbL_road.BackColor = System.Drawing.Color.Transparent;
            this.lbL_road.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_road.Location = new System.Drawing.Point(975, 48);
            this.lbL_road.Name = "lbL_road";
            this.lbL_road.Size = new System.Drawing.Size(60, 18);
            this.lbL_road.TabIndex = 194;
            this.lbL_road.Text = "Дорога";
            this.lbL_road.Visible = false;
            // 
            // btn_Frequencies
            // 
            this.btn_Frequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Frequencies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Frequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Frequencies.Location = new System.Drawing.Point(883, 553);
            this.btn_Frequencies.Name = "btn_Frequencies";
            this.btn_Frequencies.Size = new System.Drawing.Size(129, 30);
            this.btn_Frequencies.TabIndex = 195;
            this.btn_Frequencies.Text = "Добавить Частоты";
            this.btn_Frequencies.UseVisualStyleBackColor = false;
            this.btn_Frequencies.Click += new System.EventHandler(this.Btn_Frequencies_Click);
            // 
            // AddRadioStationParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(1055, 631);
            this.Controls.Add(this.pnl_transmitter);
            this.Controls.Add(this.lbL_road);
            this.Controls.Add(this.lbL_city);
            this.Controls.Add(this.cmB_frequency);
            this.Controls.Add(this.btn_Frequencies);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.pnl_NoteRadioStationParameters);
            this.Controls.Add(this.btn_Save_add_rst_remont);
            this.Controls.Add(this.pnl_AKB);
            this.Controls.Add(this.pnl_info_rst);
            this.Controls.Add(this.pnl_Accessories);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.pnl_CurrentConsumption);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pnl_Receiver);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pnl_frequencies);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRadioStationParametersForm";
            this.Text = "Добавление параметров радиостанции";
            this.Load += new System.EventHandler(this.AddRadioStationParametersForm_Load);
            this.pnl_transmitter.ResumeLayout(false);
            this.pnl_transmitter.PerformLayout();
            this.pnl_frequencies.ResumeLayout(false);
            this.pnl_frequencies.PerformLayout();
            this.pnl_Receiver.ResumeLayout(false);
            this.pnl_Receiver.PerformLayout();
            this.pnl_CurrentConsumption.ResumeLayout(false);
            this.pnl_CurrentConsumption.PerformLayout();
            this.pnl_Accessories.ResumeLayout(false);
            this.pnl_Accessories.PerformLayout();
            this.pnl_info_rst.ResumeLayout(false);
            this.pnl_info_rst.PerformLayout();
            this.pnl_AKB.ResumeLayout(false);
            this.pnl_AKB.PerformLayout();
            this.pnl_NoteRadioStationParameters.ResumeLayout(false);
            this.pnl_NoteRadioStationParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        internal System.Windows.Forms.TextBox txB_dateTO;
        internal System.Windows.Forms.Label lbL_nameAKB;
        internal System.Windows.Forms.TextBox txB_percentAKB;
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
        internal System.Windows.Forms.TextBox txB_LowPowerLevelTransmitter;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txB_HighPowerLevelTransmitter;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txB_FrequencyDeviationTransmitter;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txB_SensitivityTransmitter;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txB_KNITransmitter;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txB_DeviationTransmitter;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txB_TransmitterFrequencies;
        private System.Windows.Forms.Panel pnl_transmitter;
        private System.Windows.Forms.Panel pnl_frequencies;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnl_Receiver;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txB_OutputPowerVoltReceiver;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox txB_SensitivityReceiver;
        internal System.Windows.Forms.TextBox txB_KNIReceiver;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.TextBox txB_SuppressorReceiver;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.TextBox txB_SelectivityReceiver;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox txB_OutputPowerWattReceiver;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnl_CurrentConsumption;
        internal System.Windows.Forms.TextBox txB_StandbyModeCurrentConsumption;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.TextBox txB_TransmissionModeCurrentConsumption;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.TextBox txB_ReceptionModeCurrentConsumption;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox txB_ReceiverFrequencies;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.TextBox txB_BatteryDischargeAlarmCurrentConsumption;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel pnl_Accessories;
        private System.Windows.Forms.Label label28;
        internal System.Windows.Forms.ComboBox cmB_ManipulatorAccessories;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.ComboBox cmB_BatteryChargerAccessories;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel pnl_info_rst;
        private System.Windows.Forms.Panel pnl_AKB;
        private System.Windows.Forms.Button btn_Save_add_rst_remont;
        private System.Windows.Forms.Panel pnl_NoteRadioStationParameters;
        internal System.Windows.Forms.TextBox txB_NoteRadioStationParameters;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label lbL_ManipulatorAccessories;
        internal System.Windows.Forms.Label lbL_BatteryChargerAccessories;
        internal System.Windows.Forms.ComboBox cmB_frequency;
        internal System.Windows.Forms.Label lbL_road;
        internal System.Windows.Forms.Label lbL_city;
        private System.Windows.Forms.Button btn_Frequencies;
    }
}