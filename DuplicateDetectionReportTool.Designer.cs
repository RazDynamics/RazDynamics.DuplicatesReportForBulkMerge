using System;
using System.Windows.Forms;

namespace CRMConsultants.DuplicateDetectionReport
{
    partial class DuplicateDetectionReportTool
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuplicateDetectionReportTool));
            this.tcReport = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbDuplicateDetection = new System.Windows.Forms.TabPage();
            this.gbDuplicateDetectionJob = new System.Windows.Forms.GroupBox();
            this.cmbDuplicateDetectionJobs = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.gbEntity = new System.Windows.Forms.GroupBox();
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblOutputFilePath = new System.Windows.Forms.Label();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGenerate = new System.Windows.Forms.ToolStripButton();
            this.tbMergeDuplicates = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbnNoOfCompletedFields = new System.Windows.Forms.RadioButton();
            this.rbModifiedOn = new System.Windows.Forms.RadioButton();
            this.rbNoOfActivities = new System.Windows.Forms.RadioButton();
            this.rbCreatedOn = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMergeDuplicates = new System.Windows.Forms.ToolStripButton();
            this.tcReport.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tbDuplicateDetection.SuspendLayout();
            this.gbDuplicateDetectionJob.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbEntity.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.tbMergeDuplicates.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcReport
            // 
            this.tcReport.Controls.Add(this.tabPage1);
            this.tcReport.Controls.Add(this.tbDuplicateDetection);
            this.tcReport.Controls.Add(this.tbMergeDuplicates);
            this.tcReport.Location = new System.Drawing.Point(3, 12);
            this.tcReport.Name = "tcReport";
            this.tcReport.SelectedIndex = 0;
            this.tcReport.Size = new System.Drawing.Size(743, 514);
            this.tcReport.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(735, 488);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Instruction ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(16, 35);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(676, 246);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tbDuplicateDetection
            // 
            this.tbDuplicateDetection.Controls.Add(this.gbDuplicateDetectionJob);
            this.tbDuplicateDetection.Controls.Add(this.groupBox2);
            this.tbDuplicateDetection.Controls.Add(this.gbEntity);
            this.tbDuplicateDetection.Controls.Add(this.groupBox1);
            this.tbDuplicateDetection.Controls.Add(this.toolStripMenu);
            this.tbDuplicateDetection.Location = new System.Drawing.Point(4, 22);
            this.tbDuplicateDetection.Name = "tbDuplicateDetection";
            this.tbDuplicateDetection.Padding = new System.Windows.Forms.Padding(3);
            this.tbDuplicateDetection.Size = new System.Drawing.Size(735, 488);
            this.tbDuplicateDetection.TabIndex = 0;
            this.tbDuplicateDetection.Text = "Duplicate Detection Report";
            this.tbDuplicateDetection.UseVisualStyleBackColor = true;
            // 
            // gbDuplicateDetectionJob
            // 
            this.gbDuplicateDetectionJob.Controls.Add(this.cmbDuplicateDetectionJobs);
            this.gbDuplicateDetectionJob.Location = new System.Drawing.Point(9, 115);
            this.gbDuplicateDetectionJob.Name = "gbDuplicateDetectionJob";
            this.gbDuplicateDetectionJob.Size = new System.Drawing.Size(320, 59);
            this.gbDuplicateDetectionJob.TabIndex = 98;
            this.gbDuplicateDetectionJob.TabStop = false;
            this.gbDuplicateDetectionJob.Text = "Select Duplicate Detection Job To Analyze";
            this.gbDuplicateDetectionJob.Visible = false;
            // 
            // cmbDuplicateDetectionJobs
            // 
            this.cmbDuplicateDetectionJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuplicateDetectionJobs.Enabled = false;
            this.cmbDuplicateDetectionJobs.FormattingEnabled = true;
            this.cmbDuplicateDetectionJobs.Location = new System.Drawing.Point(10, 25);
            this.cmbDuplicateDetectionJobs.Name = "cmbDuplicateDetectionJobs";
            this.cmbDuplicateDetectionJobs.Size = new System.Drawing.Size(272, 21);
            this.cmbDuplicateDetectionJobs.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvAttributes);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.btnCheckAll);
            this.groupBox2.Location = new System.Drawing.Point(9, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 300);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attributes";
            // 
            // lvAttributes
            // 
            this.lvAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttributes.CheckBoxes = true;
            this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader1});
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.GridLines = true;
            this.lvAttributes.Location = new System.Drawing.Point(6, 48);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(468, 228);
            this.lvAttributes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAttributes.TabIndex = 85;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Display Name";
            this.columnHeader8.Width = 211;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Schema Name";
            this.columnHeader1.Width = 251;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(315, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Clear";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(220, 19);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 0;
            this.btnCheckAll.Text = "Select All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // gbEntity
            // 
            this.gbEntity.Controls.Add(this.cmbEntities);
            this.gbEntity.Location = new System.Drawing.Point(9, 40);
            this.gbEntity.Name = "gbEntity";
            this.gbEntity.Size = new System.Drawing.Size(320, 59);
            this.gbEntity.TabIndex = 95;
            this.gbEntity.TabStop = false;
            this.gbEntity.Text = "Entity";
            // 
            // cmbEntities
            // 
            this.cmbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntities.Enabled = false;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(10, 25);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(272, 21);
            this.cmbEntities.TabIndex = 9;
            this.cmbEntities.SelectedIndexChanged += new System.EventHandler(this.cmbEntities_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.lblOutputFilePath);
            this.groupBox1.Location = new System.Drawing.Point(346, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 59);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select File Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(306, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(43, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(71, 28);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(229, 20);
            this.txtFilePath.TabIndex = 6;
            // 
            // lblOutputFilePath
            // 
            this.lblOutputFilePath.AutoSize = true;
            this.lblOutputFilePath.Location = new System.Drawing.Point(6, 34);
            this.lblOutputFilePath.Name = "lblOutputFilePath";
            this.lblOutputFilePath.Size = new System.Drawing.Size(47, 13);
            this.lblOutputFilePath.TabIndex = 5;
            this.lblOutputFilePath.Text = "File path";
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbLoadEntities,
            this.toolStripSeparator2,
            this.tsbGenerate});
            this.toolStripMenu.Location = new System.Drawing.Point(3, 3);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(729, 25);
            this.toolStripMenu.TabIndex = 94;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(94, 22);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.tsbLoadEntities_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGenerate
            // 
            this.tsbGenerate.Enabled = false;
            this.tsbGenerate.Image = ((System.Drawing.Image)(resources.GetObject("tsbGenerate.Image")));
            this.tsbGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerate.Name = "tsbGenerate";
            this.tsbGenerate.Size = new System.Drawing.Size(103, 22);
            this.tsbGenerate.Text = "Export to Excel";
            this.tsbGenerate.Click += new System.EventHandler(this.tsbGenerate_Click);
            // 
            // tbMergeDuplicates
            // 
            this.tbMergeDuplicates.Controls.Add(this.groupBox3);
            this.tbMergeDuplicates.Controls.Add(this.toolStrip1);
            this.tbMergeDuplicates.Location = new System.Drawing.Point(4, 22);
            this.tbMergeDuplicates.Name = "tbMergeDuplicates";
            this.tbMergeDuplicates.Padding = new System.Windows.Forms.Padding(3);
            this.tbMergeDuplicates.Size = new System.Drawing.Size(735, 488);
            this.tbMergeDuplicates.TabIndex = 1;
            this.tbMergeDuplicates.Text = "Merge Duplicates";
            this.tbMergeDuplicates.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.rbnNoOfCompletedFields);
            this.groupBox3.Controls.Add(this.rbModifiedOn);
            this.groupBox3.Controls.Add(this.rbNoOfActivities);
            this.groupBox3.Controls.Add(this.rbCreatedOn);
            this.groupBox3.Location = new System.Drawing.Point(15, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(693, 304);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criteria to decide Master";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Note: Current version only allows to select 1 criteria. Next version will allow t" +
    "o select multiple criteria for defining Master.";
            // 
            // rbnNoOfCompletedFields
            // 
            this.rbnNoOfCompletedFields.AutoSize = true;
            this.rbnNoOfCompletedFields.Enabled = false;
            this.rbnNoOfCompletedFields.Location = new System.Drawing.Point(33, 143);
            this.rbnNoOfCompletedFields.Name = "rbnNoOfCompletedFields";
            this.rbnNoOfCompletedFields.Size = new System.Drawing.Size(180, 17);
            this.rbnNoOfCompletedFields.TabIndex = 1;
            this.rbnNoOfCompletedFields.Text = "Max Number of Completed Fields";
            this.rbnNoOfCompletedFields.UseVisualStyleBackColor = true;
            // 
            // rbModifiedOn
            // 
            this.rbModifiedOn.AutoSize = true;
            this.rbModifiedOn.Enabled = false;
            this.rbModifiedOn.Location = new System.Drawing.Point(33, 70);
            this.rbModifiedOn.Name = "rbModifiedOn";
            this.rbModifiedOn.Size = new System.Drawing.Size(192, 17);
            this.rbModifiedOn.TabIndex = 1;
            this.rbModifiedOn.Text = "Most Recently Edited (Modified On)";
            this.rbModifiedOn.UseVisualStyleBackColor = true;
            // 
            // rbNoOfActivities
            // 
            this.rbNoOfActivities.AutoSize = true;
            this.rbNoOfActivities.Enabled = false;
            this.rbNoOfActivities.Location = new System.Drawing.Point(33, 106);
            this.rbNoOfActivities.Name = "rbNoOfActivities";
            this.rbNoOfActivities.Size = new System.Drawing.Size(142, 17);
            this.rbNoOfActivities.TabIndex = 0;
            this.rbNoOfActivities.Text = "Max Number of Activities";
            this.rbNoOfActivities.UseVisualStyleBackColor = true;
            // 
            // rbCreatedOn
            // 
            this.rbCreatedOn.AutoSize = true;
            this.rbCreatedOn.Checked = true;
            this.rbCreatedOn.Enabled = false;
            this.rbCreatedOn.Location = new System.Drawing.Point(33, 37);
            this.rbCreatedOn.Name = "rbCreatedOn";
            this.rbCreatedOn.Size = new System.Drawing.Size(118, 17);
            this.rbCreatedOn.TabIndex = 0;
            this.rbCreatedOn.TabStop = true;
            this.rbCreatedOn.Text = "Oldest (Created On)";
            this.rbCreatedOn.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.btnMergeDuplicates});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(729, 25);
            this.toolStrip1.TabIndex = 95;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMergeDuplicates
            // 
            this.btnMergeDuplicates.Enabled = false;
            this.btnMergeDuplicates.Image = ((System.Drawing.Image)(resources.GetObject("btnMergeDuplicates.Image")));
            this.btnMergeDuplicates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMergeDuplicates.Name = "btnMergeDuplicates";
            this.btnMergeDuplicates.Size = new System.Drawing.Size(119, 22);
            this.btnMergeDuplicates.Text = "Merge Duplicates";
            this.btnMergeDuplicates.Click += new System.EventHandler(this.btnMergeDuplicates_Click);
            // 
            // DuplicateDetectionReportTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcReport);
            this.Name = "DuplicateDetectionReportTool";
            this.Size = new System.Drawing.Size(813, 553);
            this.tcReport.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tbDuplicateDetection.ResumeLayout(false);
            this.tbDuplicateDetection.PerformLayout();
            this.gbDuplicateDetectionJob.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbEntity.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tbMergeDuplicates.ResumeLayout(false);
            this.tbMergeDuplicates.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TabControl tcReport;
        private System.Windows.Forms.TabPage tbDuplicateDetection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.GroupBox gbEntity;
        private System.Windows.Forms.ComboBox cmbEntities;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblOutputFilePath;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGenerate;
        private System.Windows.Forms.TabPage tbMergeDuplicates;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnMergeDuplicates;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbModifiedOn;
        private System.Windows.Forms.RadioButton rbCreatedOn;
        private GroupBox gbDuplicateDetectionJob;
        private ComboBox cmbDuplicateDetectionJobs;
        private RadioButton rbnNoOfCompletedFields;
        private RadioButton rbNoOfActivities;
        private Label label1;
        private TabPage tabPage1;
        private TextBox textBox1;
    }
}
