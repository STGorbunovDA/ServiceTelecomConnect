﻿namespace ServiceTelecomConnect
{
    partial class СhangeRSTFormCurator
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txB_dateTO = new System.Windows.Forms.TextBox();
            this.txB_networkNumber = new System.Windows.Forms.TextBox();
            this.txB_inventoryNumber = new System.Windows.Forms.TextBox();
            this.txB_serialNumber = new System.Windows.Forms.TextBox();
            this.txB_company = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbL_Date = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_save_add_rst = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txB_price = new System.Windows.Forms.TextBox();
            this.txB_location = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.cmB_poligon = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txB_numberAct = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txB_city = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cmB_model = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label36 = new System.Windows.Forms.Label();
            this.txB_comment = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txB_numberActRemont = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txB_priceRemont = new System.Windows.Forms.TextBox();
            this.txB_decommission = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmB_сategory = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.cmB_month = new System.Windows.Forms.ComboBox();
            this.lbL_road = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.pictureBox4.Location = new System.Drawing.Point(792, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 36);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.ClearControlForm);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(273, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Изменение радиостанции";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.pictureBox5.Location = new System.Drawing.Point(150, 343);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(30, 30);
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.ClearControlDateTO);
            // 
            // txB_dateTO
            // 
            this.txB_dateTO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_dateTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTO.Location = new System.Drawing.Point(185, 346);
            this.txB_dateTO.MaxLength = 19;
            this.txB_dateTO.Name = "txB_dateTO";
            this.txB_dateTO.ReadOnly = true;
            this.txB_dateTO.Size = new System.Drawing.Size(232, 26);
            this.txB_dateTO.TabIndex = 26;
            this.txB_dateTO.Click += new System.EventHandler(this.TxbDateTOClick);
            // 
            // txB_networkNumber
            // 
            this.txB_networkNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_networkNumber.Location = new System.Drawing.Point(185, 313);
            this.txB_networkNumber.MaxLength = 99;
            this.txB_networkNumber.Name = "txB_networkNumber";
            this.txB_networkNumber.Size = new System.Drawing.Size(232, 26);
            this.txB_networkNumber.TabIndex = 25;
            this.txB_networkNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbNetworkNumberKeyPress);
            this.txB_networkNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbNetworkNumberKeyUp);
            // 
            // txB_inventoryNumber
            // 
            this.txB_inventoryNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_inventoryNumber.Location = new System.Drawing.Point(185, 279);
            this.txB_inventoryNumber.MaxLength = 99;
            this.txB_inventoryNumber.Name = "txB_inventoryNumber";
            this.txB_inventoryNumber.Size = new System.Drawing.Size(232, 26);
            this.txB_inventoryNumber.TabIndex = 24;
            // 
            // txB_serialNumber
            // 
            this.txB_serialNumber.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_serialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_serialNumber.Location = new System.Drawing.Point(185, 247);
            this.txB_serialNumber.MaxLength = 31;
            this.txB_serialNumber.Name = "txB_serialNumber";
            this.txB_serialNumber.Size = new System.Drawing.Size(232, 26);
            this.txB_serialNumber.TabIndex = 23;
            this.txB_serialNumber.Click += new System.EventHandler(this.TxbSerialNumberClick);
            this.txB_serialNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbSerialNumberKeyPress);
            this.txB_serialNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbSerialNumberKeyUp);
            // 
            // txB_company
            // 
            this.txB_company.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_company.Location = new System.Drawing.Point(186, 149);
            this.txB_company.MaxLength = 31;
            this.txB_company.Name = "txB_company";
            this.txB_company.Size = new System.Drawing.Size(232, 26);
            this.txB_company.TabIndex = 22;
            this.txB_company.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbCompanyKeyPress);
            this.txB_company.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbCompanyKeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(10, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Место нахождения:";
            // 
            // lbL_Date
            // 
            this.lbL_Date.AutoSize = true;
            this.lbL_Date.BackColor = System.Drawing.Color.Transparent;
            this.lbL_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_Date.Location = new System.Drawing.Point(12, 350);
            this.lbL_Date.Name = "lbL_Date";
            this.lbL_Date.Size = new System.Drawing.Size(77, 20);
            this.lbL_Date.TabIndex = 20;
            this.lbL_Date.Text = "Дата ТО:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Сетевой номер:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(10, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Инвентарный номер:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Заводской номер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(11, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Предприятие:";
            // 
            // button_save_add_rst
            // 
            this.button_save_add_rst.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_save_add_rst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save_add_rst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_save_add_rst.Location = new System.Drawing.Point(728, 450);
            this.button_save_add_rst.Name = "button_save_add_rst";
            this.button_save_add_rst.Size = new System.Drawing.Size(119, 30);
            this.button_save_add_rst.TabIndex = 28;
            this.button_save_add_rst.Text = "Изменить";
            this.button_save_add_rst.UseVisualStyleBackColor = false;
            this.button_save_add_rst.Click += new System.EventHandler(this.BtnChangeRadiostantionClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(11, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 20);
            this.label8.TabIndex = 31;
            this.label8.Text = "Полигон:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(10, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 35;
            this.label10.Text = "Модель:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(12, 386);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 38;
            this.label9.Text = "Цена ТО:";
            // 
            // txB_price
            // 
            this.txB_price.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_price.Location = new System.Drawing.Point(186, 383);
            this.txB_price.MaxLength = 20;
            this.txB_price.Name = "txB_price";
            this.txB_price.ReadOnly = true;
            this.txB_price.Size = new System.Drawing.Size(232, 26);
            this.txB_price.TabIndex = 39;
            this.txB_price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbPriceKeyPress);
            // 
            // txB_location
            // 
            this.txB_location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_location.Location = new System.Drawing.Point(186, 181);
            this.txB_location.MaxLength = 99;
            this.txB_location.Name = "txB_location";
            this.txB_location.Size = new System.Drawing.Size(232, 26);
            this.txB_location.TabIndex = 53;
            this.txB_location.Click += new System.EventHandler(this.TxbLocationClick);
            this.txB_location.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbLocationKeyPress);
            this.txB_location.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbLocationKeyUp);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(253, 209);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 54;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar1DateSelected);
            // 
            // cmB_poligon
            // 
            this.cmB_poligon.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmB_poligon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_poligon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmB_poligon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_poligon.FormattingEnabled = true;
            this.cmB_poligon.Items.AddRange(new object[] {
            "РЦС-1",
            "РЦС-2",
            "РЦС-3",
            "РЦС-4",
            "РЦС-5",
            "РЦС-6"});
            this.cmB_poligon.Location = new System.Drawing.Point(186, 114);
            this.cmB_poligon.Name = "cmB_poligon";
            this.cmB_poligon.Size = new System.Drawing.Size(232, 28);
            this.cmB_poligon.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.Brown;
            this.label11.Location = new System.Drawing.Point(424, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 24);
            this.label11.TabIndex = 56;
            this.label11.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.Brown;
            this.label12.Location = new System.Drawing.Point(424, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 24);
            this.label12.TabIndex = 57;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.Color.Brown;
            this.label13.Location = new System.Drawing.Point(424, 186);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 24);
            this.label13.TabIndex = 58;
            this.label13.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.Color.Brown;
            this.label14.Location = new System.Drawing.Point(424, 221);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 24);
            this.label14.TabIndex = 59;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.Brown;
            this.label15.Location = new System.Drawing.Point(424, 251);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 24);
            this.label15.TabIndex = 60;
            this.label15.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.ForeColor = System.Drawing.Color.Brown;
            this.label16.Location = new System.Drawing.Point(423, 388);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(18, 24);
            this.label16.TabIndex = 62;
            this.label16.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.ForeColor = System.Drawing.Color.Brown;
            this.label17.Location = new System.Drawing.Point(424, 351);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 24);
            this.label17.TabIndex = 61;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.ForeColor = System.Drawing.Color.Brown;
            this.label18.Location = new System.Drawing.Point(422, 420);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 24);
            this.label18.TabIndex = 65;
            this.label18.Text = "*";
            // 
            // txB_numberAct
            // 
            this.txB_numberAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberAct.Location = new System.Drawing.Point(185, 418);
            this.txB_numberAct.MaxLength = 31;
            this.txB_numberAct.Name = "txB_numberAct";
            this.txB_numberAct.Size = new System.Drawing.Size(232, 26);
            this.txB_numberAct.TabIndex = 64;
            this.txB_numberAct.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbNumberActKeyUp);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(12, 422);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 20);
            this.label19.TabIndex = 63;
            this.label19.Text = "№ Акта:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(12, 82);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 20);
            this.label30.TabIndex = 83;
            this.label30.Text = "Город:";
            // 
            // txB_city
            // 
            this.txB_city.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_city.Location = new System.Drawing.Point(185, 81);
            this.txB_city.MaxLength = 31;
            this.txB_city.Name = "txB_city";
            this.txB_city.Size = new System.Drawing.Size(232, 26);
            this.txB_city.TabIndex = 84;
            this.txB_city.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbCityKeyPress);
            this.txB_city.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxbCityKeyUp);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.ForeColor = System.Drawing.Color.Brown;
            this.label31.Location = new System.Drawing.Point(423, 83);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(18, 24);
            this.label31.TabIndex = 85;
            this.label31.Text = "*";
            // 
            // cmB_model
            // 
            this.cmB_model.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmB_model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_model.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_model.FormattingEnabled = true;
            this.cmB_model.Location = new System.Drawing.Point(186, 213);
            this.cmB_model.Name = "cmB_model";
            this.cmB_model.Size = new System.Drawing.Size(232, 28);
            this.cmB_model.TabIndex = 52;
            this.cmB_model.SelectedIndexChanged += new System.EventHandler(this.CmbModelSelectedIndexChanged);
            this.cmB_model.Click += new System.EventHandler(this.CmbModelClick);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(463, 318);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(108, 20);
            this.label36.TabIndex = 110;
            this.label36.Text = "Примечание:";
            // 
            // txB_comment
            // 
            this.txB_comment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_comment.Location = new System.Drawing.Point(615, 254);
            this.txB_comment.MaxLength = 500;
            this.txB_comment.Multiline = true;
            this.txB_comment.Name = "txB_comment";
            this.txB_comment.Size = new System.Drawing.Size(232, 190);
            this.txB_comment.TabIndex = 109;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(463, 84);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(139, 20);
            this.label20.TabIndex = 111;
            this.label20.Text = "№ Акта Ремонта:";
            // 
            // txB_numberActRemont
            // 
            this.txB_numberActRemont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberActRemont.Location = new System.Drawing.Point(615, 81);
            this.txB_numberActRemont.MaxLength = 31;
            this.txB_numberActRemont.Name = "txB_numberActRemont";
            this.txB_numberActRemont.Size = new System.Drawing.Size(232, 26);
            this.txB_numberActRemont.TabIndex = 112;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(463, 121);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(93, 20);
            this.label21.TabIndex = 113;
            this.label21.Text = "Категория:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(463, 152);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(122, 20);
            this.label22.TabIndex = 115;
            this.label22.Text = "Цена Ремонта:";
            // 
            // txB_priceRemont
            // 
            this.txB_priceRemont.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_priceRemont.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_priceRemont.Location = new System.Drawing.Point(615, 150);
            this.txB_priceRemont.MaxLength = 31;
            this.txB_priceRemont.Name = "txB_priceRemont";
            this.txB_priceRemont.ReadOnly = true;
            this.txB_priceRemont.Size = new System.Drawing.Size(232, 26);
            this.txB_priceRemont.TabIndex = 116;
            // 
            // txB_decommission
            // 
            this.txB_decommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_decommission.Location = new System.Drawing.Point(615, 181);
            this.txB_decommission.MaxLength = 31;
            this.txB_decommission.Name = "txB_decommission";
            this.txB_decommission.Size = new System.Drawing.Size(232, 26);
            this.txB_decommission.TabIndex = 118;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(463, 184);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(146, 20);
            this.label23.TabIndex = 117;
            this.label23.Text = "№ Акта Списания:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(463, 216);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(61, 20);
            this.label25.TabIndex = 121;
            this.label25.Text = "Месяц:";
            // 
            // cmB_сategory
            // 
            this.cmB_сategory.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmB_сategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_сategory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmB_сategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_сategory.FormattingEnabled = true;
            this.cmB_сategory.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6"});
            this.cmB_сategory.Location = new System.Drawing.Point(615, 115);
            this.cmB_сategory.Name = "cmB_сategory";
            this.cmB_сategory.Size = new System.Drawing.Size(232, 28);
            this.cmB_сategory.TabIndex = 145;
            this.cmB_сategory.SelectionChangeCommitted += new System.EventHandler(this.CmbCategorySelectionChangeCommitted);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.ForeColor = System.Drawing.Color.Brown;
            this.label24.Location = new System.Drawing.Point(424, 282);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 24);
            this.label24.TabIndex = 146;
            this.label24.Text = "*";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label40.ForeColor = System.Drawing.Color.Brown;
            this.label40.Location = new System.Drawing.Point(423, 314);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(18, 24);
            this.label40.TabIndex = 147;
            this.label40.Text = "*";
            // 
            // cmB_month
            // 
            this.cmB_month.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmB_month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_month.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmB_month.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_month.FormattingEnabled = true;
            this.cmB_month.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.cmB_month.Location = new System.Drawing.Point(615, 213);
            this.cmB_month.Name = "cmB_month";
            this.cmB_month.Size = new System.Drawing.Size(232, 28);
            this.cmB_month.TabIndex = 148;
            // 
            // lbL_road
            // 
            this.lbL_road.AutoSize = true;
            this.lbL_road.BackColor = System.Drawing.Color.Transparent;
            this.lbL_road.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbL_road.Location = new System.Drawing.Point(11, 19);
            this.lbL_road.Name = "lbL_road";
            this.lbL_road.Size = new System.Drawing.Size(49, 15);
            this.lbL_road.TabIndex = 149;
            this.lbL_road.Text = "Дорога";
            this.lbL_road.Visible = false;
            // 
            // СhangeRSTFormCurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(859, 491);
            this.Controls.Add(this.lbL_road);
            this.Controls.Add(this.cmB_month);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cmB_сategory);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txB_decommission);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txB_priceRemont);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txB_numberActRemont);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txB_comment);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.txB_city);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txB_numberAct);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmB_poligon);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txB_location);
            this.Controls.Add(this.cmB_model);
            this.Controls.Add(this.txB_price);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button_save_add_rst);
            this.Controls.Add(this.txB_dateTO);
            this.Controls.Add(this.txB_networkNumber);
            this.Controls.Add(this.txB_inventoryNumber);
            this.Controls.Add(this.txB_serialNumber);
            this.Controls.Add(this.txB_company);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbL_Date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(875, 530);
            this.MinimumSize = new System.Drawing.Size(875, 530);
            this.Name = "СhangeRSTFormCurator";
            this.ShowIcon = false;
            this.Text = "Изменение РСТ";
            this.Load += new System.EventHandler(this.ChangeRSTFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbL_Date;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_save_add_rst;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.TextBox txB_city;
        internal System.Windows.Forms.ComboBox cmB_poligon;
        internal System.Windows.Forms.TextBox txB_dateTO;
        internal System.Windows.Forms.TextBox txB_networkNumber;
        internal System.Windows.Forms.TextBox txB_inventoryNumber;
        internal System.Windows.Forms.TextBox txB_serialNumber;
        internal System.Windows.Forms.TextBox txB_company;
        internal System.Windows.Forms.TextBox txB_price;
        internal System.Windows.Forms.TextBox txB_location;
        internal System.Windows.Forms.TextBox txB_numberAct;
        internal System.Windows.Forms.ComboBox cmB_model;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.TextBox txB_comment;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox txB_numberActRemont;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox txB_priceRemont;
        internal System.Windows.Forms.TextBox txB_decommission;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.ComboBox cmB_сategory;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label40;
        internal System.Windows.Forms.ComboBox cmB_month;
        internal System.Windows.Forms.Label lbL_road;
    }
}