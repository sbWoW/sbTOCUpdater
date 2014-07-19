﻿namespace sbTOCUpdater
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnScan = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFolderSelection = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DGPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGInterfaceVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbInterfaceNumber = new System.Windows.Forms.TextBox();
            this.lblInterfaceNumber = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(692, 4);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "te";
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(67, 6);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(548, 20);
            this.tbFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Directory";
            // 
            // btnFolderSelection
            // 
            this.btnFolderSelection.Location = new System.Drawing.Point(621, 4);
            this.btnFolderSelection.Name = "btnFolderSelection";
            this.btnFolderSelection.Size = new System.Drawing.Size(32, 23);
            this.btnFolderSelection.TabIndex = 3;
            this.btnFolderSelection.Text = "...";
            this.btnFolderSelection.UseVisualStyleBackColor = true;
            this.btnFolderSelection.Click += new System.EventHandler(this.btnFolderSelection_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGPath,
            this.DGFileName,
            this.DGInterfaceVersion,
            this.Status});
            this.dataGridView1.Location = new System.Drawing.Point(13, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(763, 400);
            this.dataGridView1.TabIndex = 4;
            // 
            // DGPath
            // 
            this.DGPath.DataPropertyName = "ParentFolder";
            this.DGPath.HeaderText = "Path";
            this.DGPath.Name = "DGPath";
            this.DGPath.ReadOnly = true;
            this.DGPath.Width = 300;
            // 
            // DGFileName
            // 
            this.DGFileName.DataPropertyName = "TocName";
            this.DGFileName.HeaderText = "FileName";
            this.DGFileName.Name = "DGFileName";
            this.DGFileName.ReadOnly = true;
            this.DGFileName.Width = 200;
            // 
            // DGInterfaceVersion
            // 
            this.DGInterfaceVersion.DataPropertyName = "InterfaceVersion";
            this.DGInterfaceVersion.HeaderText = "Interface Version";
            this.DGInterfaceVersion.Name = "DGInterfaceVersion";
            this.DGInterfaceVersion.ReadOnly = true;
            this.DGInterfaceVersion.Width = 120;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // tbInterfaceNumber
            // 
            this.tbInterfaceNumber.Location = new System.Drawing.Point(110, 455);
            this.tbInterfaceNumber.Name = "tbInterfaceNumber";
            this.tbInterfaceNumber.Size = new System.Drawing.Size(128, 20);
            this.tbInterfaceNumber.TabIndex = 5;
            this.tbInterfaceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInterfaceNumber_KeyPress);
            // 
            // lblInterfaceNumber
            // 
            this.lblInterfaceNumber.AutoSize = true;
            this.lblInterfaceNumber.Location = new System.Drawing.Point(15, 458);
            this.lblInterfaceNumber.Name = "lblInterfaceNumber";
            this.lblInterfaceNumber.Size = new System.Drawing.Size(89, 13);
            this.lblInterfaceNumber.TabIndex = 6;
            this.lblInterfaceNumber.Text = "Interface Number";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(244, 453);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(95, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "update all";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(504, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "v0.1, 19.07.2014, Steffen \'smb\' Buehl <sb@sbuehl.com>";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ForeColor = System.Drawing.Color.LightGray;
            this.linkLabel1.LinkColor = System.Drawing.Color.LightGray;
            this.linkLabel1.Location = new System.Drawing.Point(504, 449);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(220, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/sbWoW/sbTOCUpdater/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 484);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblInterfaceNumber);
            this.Controls.Add(this.tbInterfaceNumber);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnFolderSelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.btnScan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "sbTOCUpdater - v0.1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFolderSelection;
        private System.Windows.Forms.TextBox tbInterfaceNumber;
        private System.Windows.Forms.Label lblInterfaceNumber;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGInterfaceVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

