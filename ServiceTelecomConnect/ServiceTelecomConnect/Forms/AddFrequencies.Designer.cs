namespace ServiceTelecomConnect.Forms
{
    partial class AddFrequencies
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
            this.cmB_Frequencies = new System.Windows.Forms.ComboBox();
            this.btn_add_Frequencies = new System.Windows.Forms.Button();
            this.btn_change_Frequencies = new System.Windows.Forms.Button();
            this.btn_delete_Frequencies = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmB_Frequencies
            // 
            this.cmB_Frequencies.BackColor = System.Drawing.SystemColors.Window;
            this.cmB_Frequencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmB_Frequencies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_Frequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_Frequencies.FormattingEnabled = true;
            this.cmB_Frequencies.Location = new System.Drawing.Point(31, 31);
            this.cmB_Frequencies.Name = "cmB_Frequencies";
            this.cmB_Frequencies.Size = new System.Drawing.Size(264, 228);
            this.cmB_Frequencies.TabIndex = 53;
            this.cmB_Frequencies.SelectionChangeCommitted += new System.EventHandler(this.CmB_frequencies_SelectionChangeCommitted);
            // 
            // btn_add_Frequencies
            // 
            this.btn_add_Frequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_add_Frequencies.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_add_Frequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add_Frequencies.Location = new System.Drawing.Point(317, 157);
            this.btn_add_Frequencies.Name = "btn_add_Frequencies";
            this.btn_add_Frequencies.Size = new System.Drawing.Size(119, 30);
            this.btn_add_Frequencies.TabIndex = 54;
            this.btn_add_Frequencies.Text = "Добавить";
            this.btn_add_Frequencies.UseVisualStyleBackColor = false;
            this.btn_add_Frequencies.Click += new System.EventHandler(this.Btn_add_Frequencies_Click);
            // 
            // btn_change_Frequencies
            // 
            this.btn_change_Frequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_change_Frequencies.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_change_Frequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_change_Frequencies.Location = new System.Drawing.Point(317, 193);
            this.btn_change_Frequencies.Name = "btn_change_Frequencies";
            this.btn_change_Frequencies.Size = new System.Drawing.Size(119, 30);
            this.btn_change_Frequencies.TabIndex = 55;
            this.btn_change_Frequencies.Text = "Изменить";
            this.btn_change_Frequencies.UseVisualStyleBackColor = false;
            this.btn_change_Frequencies.Click += new System.EventHandler(this.Btn_change_Frequencies_Click);
            // 
            // btn_delete_Frequencies
            // 
            this.btn_delete_Frequencies.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete_Frequencies.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_Frequencies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_delete_Frequencies.Location = new System.Drawing.Point(317, 229);
            this.btn_delete_Frequencies.Name = "btn_delete_Frequencies";
            this.btn_delete_Frequencies.Size = new System.Drawing.Size(119, 30);
            this.btn_delete_Frequencies.TabIndex = 56;
            this.btn_delete_Frequencies.Text = "Удалить";
            this.btn_delete_Frequencies.UseVisualStyleBackColor = false;
            this.btn_delete_Frequencies.Click += new System.EventHandler(this.Btn_delete_Frequencies_Click);
            // 
            // AddFrequencies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(461, 287);
            this.Controls.Add(this.btn_delete_Frequencies);
            this.Controls.Add(this.btn_change_Frequencies);
            this.Controls.Add(this.btn_add_Frequencies);
            this.Controls.Add(this.cmB_Frequencies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddFrequencies";
            this.Text = "Добавить/Изменить модель";
            this.Load += new System.EventHandler(this.AddFrequencies_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmB_Frequencies;
        private System.Windows.Forms.Button btn_add_Frequencies;
        private System.Windows.Forms.Button btn_change_Frequencies;
        private System.Windows.Forms.Button btn_delete_Frequencies;
    }
}