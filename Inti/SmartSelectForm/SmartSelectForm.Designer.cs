namespace Inti.SmartSelectUtils
{
    partial class SmartSelectForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSmartSelect = new System.Windows.Forms.Panel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.smartSelect = new Inti.UserControls.SmartSelect();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelSmartSelect.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelSmartSelect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelButton, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(437, 302);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelSmartSelect
            // 
            this.panelSmartSelect.AutoSize = true;
            this.panelSmartSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSmartSelect.Controls.Add(this.smartSelect);
            this.panelSmartSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSmartSelect.Location = new System.Drawing.Point(3, 3);
            this.panelSmartSelect.Name = "panelSmartSelect";
            this.panelSmartSelect.Size = new System.Drawing.Size(431, 250);
            this.panelSmartSelect.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButton.Controls.Add(this.buttonCancel);
            this.panelButton.Controls.Add(this.buttonSubmit);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButton.Location = new System.Drawing.Point(3, 259);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(431, 40);
            this.panelButton.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(347, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(9, 8);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 0;
            this.buttonSubmit.Text = "Aceptar";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            // 
            // smartSelect
            // 
            this.smartSelect.DataFunction = null;
            this.smartSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSelect.ItemChangedCallback = null;
            this.smartSelect.Location = new System.Drawing.Point(0, 0);
            this.smartSelect.Name = "smartSelect";
            this.smartSelect.Size = new System.Drawing.Size(431, 250);
            this.smartSelect.TabIndex = 0;
            // 
            // SmartSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(437, 302);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SmartSelectForm";
            this.Text = "SmartSelectForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelSmartSelect.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelSmartSelect;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSubmit;
        private UserControls.SmartSelect smartSelect;
    }
}