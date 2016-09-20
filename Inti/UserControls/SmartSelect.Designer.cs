namespace Inti.UserControls
{
    partial class SmartSelect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.removeEntityButton = new System.Windows.Forms.Button();
            this.addEntityButton = new System.Windows.Forms.Button();
            this.selectionListPanel = new System.Windows.Forms.Panel();
            this.selectionList = new System.Windows.Forms.ListBox();
            this.entityListPanel = new System.Windows.Forms.Panel();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.entityList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.selectionListPanel.SuspendLayout();
            this.entityListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.buttonPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectionListPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.entityListPanel, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(378, 186);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.removeEntityButton);
            this.buttonPanel.Controls.Add(this.addEntityButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(173, 3);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(31, 180);
            this.buttonPanel.TabIndex = 0;
            // 
            // removeEntityButton
            // 
            this.removeEntityButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.removeEntityButton.Location = new System.Drawing.Point(4, 92);
            this.removeEntityButton.Name = "removeEntityButton";
            this.removeEntityButton.Size = new System.Drawing.Size(24, 23);
            this.removeEntityButton.TabIndex = 1;
            this.removeEntityButton.Text = ">";
            this.removeEntityButton.UseVisualStyleBackColor = true;
            this.removeEntityButton.Click += new System.EventHandler(this.removeEntityButton_Click);
            // 
            // addEntityButton
            // 
            this.addEntityButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addEntityButton.Location = new System.Drawing.Point(4, 63);
            this.addEntityButton.Name = "addEntityButton";
            this.addEntityButton.Size = new System.Drawing.Size(24, 23);
            this.addEntityButton.TabIndex = 0;
            this.addEntityButton.Text = "<";
            this.addEntityButton.UseVisualStyleBackColor = true;
            this.addEntityButton.Click += new System.EventHandler(this.addEntityButton_Click);
            // 
            // selectionListPanel
            // 
            this.selectionListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectionListPanel.Controls.Add(this.selectionList);
            this.selectionListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectionListPanel.Location = new System.Drawing.Point(3, 3);
            this.selectionListPanel.Name = "selectionListPanel";
            this.selectionListPanel.Size = new System.Drawing.Size(164, 180);
            this.selectionListPanel.TabIndex = 1;
            // 
            // selectionList
            // 
            this.selectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectionList.FormattingEnabled = true;
            this.selectionList.HorizontalScrollbar = true;
            this.selectionList.Location = new System.Drawing.Point(0, 0);
            this.selectionList.Name = "selectionList";
            this.selectionList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectionList.Size = new System.Drawing.Size(164, 180);
            this.selectionList.TabIndex = 0;
            this.selectionList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectedItemDoubleClicked);
            // 
            // entityListPanel
            // 
            this.entityListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.entityListPanel.Controls.Add(this.loadingLabel);
            this.entityListPanel.Controls.Add(this.entityList);
            this.entityListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityListPanel.Location = new System.Drawing.Point(210, 3);
            this.entityListPanel.Name = "entityListPanel";
            this.entityListPanel.Size = new System.Drawing.Size(165, 180);
            this.entityListPanel.TabIndex = 2;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(49, 73);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(62, 13);
            this.loadingLabel.TabIndex = 1;
            this.loadingLabel.Text = "Cargando...";
            // 
            // entityList
            // 
            this.entityList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityList.FormattingEnabled = true;
            this.entityList.HorizontalScrollbar = true;
            this.entityList.Location = new System.Drawing.Point(0, 0);
            this.entityList.Name = "entityList";
            this.entityList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.entityList.Size = new System.Drawing.Size(165, 180);
            this.entityList.TabIndex = 0;
            this.entityList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.entityDoubleClicked);
            // 
            // SmartSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SmartSelect";
            this.Size = new System.Drawing.Size(378, 186);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.selectionListPanel.ResumeLayout(false);
            this.entityListPanel.ResumeLayout(false);
            this.entityListPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button removeEntityButton;
        private System.Windows.Forms.Button addEntityButton;
        private System.Windows.Forms.Panel selectionListPanel;
        private System.Windows.Forms.ListBox selectionList;
        private System.Windows.Forms.Panel entityListPanel;
        private System.Windows.Forms.ListBox entityList;
        private System.Windows.Forms.Label loadingLabel;
    }
}
