
namespace FileSystemVisitor
{
    partial class Form1
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
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FilesFoundLabel = new System.Windows.Forms.Label();
            this.stopSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.excludeItemsCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(471, 12);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(26, 20);
            this.selectFolderButton.TabIndex = 0;
            this.selectFolderButton.Text = "...";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.Location = new System.Drawing.Point(12, 141);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(438, 186);
            this.listBox.TabIndex = 1;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(56, 12);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(394, 20);
            this.pathTextBox.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 359);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(505, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(9, 55);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(41, 13);
            this.label.TabIndex = 4;
            this.label.Text = "Search";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(56, 52);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(394, 20);
            this.searchTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Path";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(11, 333);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 7;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FilesFoundLabel
            // 
            this.FilesFoundLabel.AutoSize = true;
            this.FilesFoundLabel.Location = new System.Drawing.Point(12, 125);
            this.FilesFoundLabel.Name = "FilesFoundLabel";
            this.FilesFoundLabel.Size = new System.Drawing.Size(0, 13);
            this.FilesFoundLabel.TabIndex = 8;
            // 
            // stopSearchCheckBox
            // 
            this.stopSearchCheckBox.AutoSize = true;
            this.stopSearchCheckBox.Location = new System.Drawing.Point(56, 81);
            this.stopSearchCheckBox.Name = "stopSearchCheckBox";
            this.stopSearchCheckBox.Size = new System.Drawing.Size(182, 17);
            this.stopSearchCheckBox.TabIndex = 9;
            this.stopSearchCheckBox.Text = "Stop search once match is found";
            this.stopSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // excludeItemsCheckBox
            // 
            this.excludeItemsCheckBox.AutoSize = true;
            this.excludeItemsCheckBox.Location = new System.Drawing.Point(56, 104);
            this.excludeItemsCheckBox.Name = "excludeItemsCheckBox";
            this.excludeItemsCheckBox.Size = new System.Drawing.Size(179, 17);
            this.excludeItemsCheckBox.TabIndex = 10;
            this.excludeItemsCheckBox.Text = "Remove found items from the list";
            this.excludeItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 381);
            this.Controls.Add(this.excludeItemsCheckBox);
            this.Controls.Add(this.stopSearchCheckBox);
            this.Controls.Add(this.FilesFoundLabel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.label);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.selectFolderButton);
            this.Name = "Form1";
            this.Text = "File Visitor";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label FilesFoundLabel;
        private System.Windows.Forms.CheckBox stopSearchCheckBox;
        private System.Windows.Forms.CheckBox excludeItemsCheckBox;
    }
}

