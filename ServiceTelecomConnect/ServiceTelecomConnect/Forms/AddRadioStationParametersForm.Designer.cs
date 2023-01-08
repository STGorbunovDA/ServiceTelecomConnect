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
            this.label1 = new System.Windows.Forms.Label();
            this.txB_serialNumber = new System.Windows.Forms.Label();
            this.txB_model = new System.Windows.Forms.Label();
            this.picB_clear_dataTO = new System.Windows.Forms.PictureBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txB_dateTO = new System.Windows.Forms.TextBox();
            this.txB_numberAct = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear_dataTO)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(217, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Параметры радиостанции";
            // 
            // txB_serialNumber
            // 
            this.txB_serialNumber.AutoSize = true;
            this.txB_serialNumber.BackColor = System.Drawing.Color.Transparent;
            this.txB_serialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_serialNumber.Location = new System.Drawing.Point(393, 66);
            this.txB_serialNumber.Name = "txB_serialNumber";
            this.txB_serialNumber.Size = new System.Drawing.Size(113, 20);
            this.txB_serialNumber.TabIndex = 67;
            this.txB_serialNumber.Text = "672TTTM099";
            // 
            // txB_model
            // 
            this.txB_model.AutoSize = true;
            this.txB_model.BackColor = System.Drawing.Color.Transparent;
            this.txB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_model.Location = new System.Drawing.Point(32, 66);
            this.txB_model.Name = "txB_model";
            this.txB_model.Size = new System.Drawing.Size(145, 20);
            this.txB_model.TabIndex = 68;
            this.txB_model.Text = "Motorola GP-340";
            // 
            // picB_clear_dataTO
            // 
            this.picB_clear_dataTO.BackColor = System.Drawing.Color.Transparent;
            this.picB_clear_dataTO.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.gui_eraser_icon_157160__1_;
            this.picB_clear_dataTO.Location = new System.Drawing.Point(743, 59);
            this.picB_clear_dataTO.Name = "picB_clear_dataTO";
            this.picB_clear_dataTO.Size = new System.Drawing.Size(30, 30);
            this.picB_clear_dataTO.TabIndex = 69;
            this.picB_clear_dataTO.TabStop = false;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(573, 89);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 71;
            // 
            // txB_dateTO
            // 
            this.txB_dateTO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txB_dateTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_dateTO.Location = new System.Drawing.Point(573, 63);
            this.txB_dateTO.MaxLength = 19;
            this.txB_dateTO.Name = "txB_dateTO";
            this.txB_dateTO.Size = new System.Drawing.Size(164, 26);
            this.txB_dateTO.TabIndex = 70;
            // 
            // txB_numberAct
            // 
            this.txB_numberAct.AutoSize = true;
            this.txB_numberAct.BackColor = System.Drawing.Color.Transparent;
            this.txB_numberAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txB_numberAct.Location = new System.Drawing.Point(246, 66);
            this.txB_numberAct.Name = "txB_numberAct";
            this.txB_numberAct.Size = new System.Drawing.Size(80, 20);
            this.txB_numberAct.TabIndex = 72;
            this.txB_numberAct.Text = "№53/250";
            // 
            // AddRadioStationParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txB_numberAct);
            this.Controls.Add(this.picB_clear_dataTO);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txB_dateTO);
            this.Controls.Add(this.txB_model);
            this.Controls.Add(this.txB_serialNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRadioStationParametersForm";
            this.Text = "Добавление параметров радиостанции";
            this.Load += new System.EventHandler(this.AddRadioStationParametersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picB_clear_dataTO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picB_clear_dataTO;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        internal System.Windows.Forms.TextBox txB_dateTO;
        internal System.Windows.Forms.Label txB_serialNumber;
        internal System.Windows.Forms.Label txB_model;
        internal System.Windows.Forms.Label txB_numberAct;
    }
}