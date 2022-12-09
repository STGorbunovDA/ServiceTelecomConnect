namespace ServiceTelecomConnect.Forms
{
    partial class AddChangeModelRST
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
            this.cmB_model = new System.Windows.Forms.ComboBox();
            this.btn_add_modelRST = new System.Windows.Forms.Button();
            this.btn_change_modelRST = new System.Windows.Forms.Button();
            this.btn_delete_modelRST = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmB_model
            // 
            this.cmB_model.BackColor = System.Drawing.SystemColors.Window;
            this.cmB_model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmB_model.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmB_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmB_model.FormattingEnabled = true;
            this.cmB_model.Location = new System.Drawing.Point(31, 31);
            this.cmB_model.Name = "cmB_model";
            this.cmB_model.Size = new System.Drawing.Size(264, 228);
            this.cmB_model.TabIndex = 53;
            this.cmB_model.SelectionChangeCommitted += new System.EventHandler(this.CmB_model_SelectionChangeCommitted);
            // 
            // btn_add_modelRST
            // 
            this.btn_add_modelRST.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_add_modelRST.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_add_modelRST.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add_modelRST.Location = new System.Drawing.Point(317, 157);
            this.btn_add_modelRST.Name = "btn_add_modelRST";
            this.btn_add_modelRST.Size = new System.Drawing.Size(119, 30);
            this.btn_add_modelRST.TabIndex = 54;
            this.btn_add_modelRST.Text = "Добавить";
            this.btn_add_modelRST.UseVisualStyleBackColor = false;
            this.btn_add_modelRST.Click += new System.EventHandler(this.Btn_add_modelRST_Click);
            // 
            // btn_change_modelRST
            // 
            this.btn_change_modelRST.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_change_modelRST.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_change_modelRST.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_change_modelRST.Location = new System.Drawing.Point(317, 193);
            this.btn_change_modelRST.Name = "btn_change_modelRST";
            this.btn_change_modelRST.Size = new System.Drawing.Size(119, 30);
            this.btn_change_modelRST.TabIndex = 55;
            this.btn_change_modelRST.Text = "Изменить";
            this.btn_change_modelRST.UseVisualStyleBackColor = false;
            this.btn_change_modelRST.Click += new System.EventHandler(this.Btn_change_modelRST_Click);
            // 
            // btn_delete_modelRST
            // 
            this.btn_delete_modelRST.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete_modelRST.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_modelRST.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_delete_modelRST.Location = new System.Drawing.Point(317, 229);
            this.btn_delete_modelRST.Name = "btn_delete_modelRST";
            this.btn_delete_modelRST.Size = new System.Drawing.Size(119, 30);
            this.btn_delete_modelRST.TabIndex = 56;
            this.btn_delete_modelRST.Text = "Удалить";
            this.btn_delete_modelRST.UseVisualStyleBackColor = false;
            this.btn_delete_modelRST.Click += new System.EventHandler(this.Btn_delete_modelRST_Click);
            // 
            // AddChangeModelRST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ServiceTelecomConnect.Properties.Resources.Untitled_6;
            this.ClientSize = new System.Drawing.Size(461, 287);
            this.Controls.Add(this.btn_delete_modelRST);
            this.Controls.Add(this.btn_change_modelRST);
            this.Controls.Add(this.btn_add_modelRST);
            this.Controls.Add(this.cmB_model);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddChangeModelRST";
            this.Text = "Добавить/Изменить модель";
            this.Load += new System.EventHandler(this.AddChangeModelRST_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmB_model;
        private System.Windows.Forms.Button btn_add_modelRST;
        private System.Windows.Forms.Button btn_change_modelRST;
        private System.Windows.Forms.Button btn_delete_modelRST;
    }
}