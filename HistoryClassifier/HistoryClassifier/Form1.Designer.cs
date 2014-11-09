namespace HistoryClassifier
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
            this.lsbxVersions = new System.Windows.Forms.ListBox();
            this.btnBug = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFeature = new System.Windows.Forms.Button();
            this.btnEnhancement = new System.Windows.Forms.Button();
            this.btnIrrelevant = new System.Windows.Forms.Button();
            this.chkbFlag = new System.Windows.Forms.CheckBox();
            this.btnSplit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lsbxHistory = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAd = new System.Windows.Forms.Button();
            this.btnChangeRating = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersionPat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDatePattern = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateFormat = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDesktop = new System.Windows.Forms.RadioButton();
            this.rbMobile = new System.Windows.Forms.RadioButton();
            this.rbSibling = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbxVersions
            // 
            this.lsbxVersions.FormattingEnabled = true;
            this.lsbxVersions.Location = new System.Drawing.Point(12, 37);
            this.lsbxVersions.Name = "lsbxVersions";
            this.lsbxVersions.Size = new System.Drawing.Size(84, 264);
            this.lsbxVersions.TabIndex = 0;
            this.lsbxVersions.SelectedIndexChanged += new System.EventHandler(this.lsbxVersions_SelectedIndexChanged);
            // 
            // btnBug
            // 
            this.btnBug.Location = new System.Drawing.Point(102, 307);
            this.btnBug.Name = "btnBug";
            this.btnBug.Size = new System.Drawing.Size(104, 37);
            this.btnBug.TabIndex = 1;
            this.btnBug.Text = "Bug (Q)";
            this.btnBug.UseVisualStyleBackColor = true;
            this.btnBug.Click += new System.EventHandler(this.btnBug_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(192, 37);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(498, 264);
            this.textBox1.TabIndex = 2;
            // 
            // btnFeature
            // 
            this.btnFeature.Location = new System.Drawing.Point(223, 307);
            this.btnFeature.Name = "btnFeature";
            this.btnFeature.Size = new System.Drawing.Size(104, 37);
            this.btnFeature.TabIndex = 3;
            this.btnFeature.Text = "Feature (W)";
            this.btnFeature.UseVisualStyleBackColor = true;
            this.btnFeature.Click += new System.EventHandler(this.btnFeature_Click);
            // 
            // btnEnhancement
            // 
            this.btnEnhancement.Location = new System.Drawing.Point(344, 307);
            this.btnEnhancement.Name = "btnEnhancement";
            this.btnEnhancement.Size = new System.Drawing.Size(104, 37);
            this.btnEnhancement.TabIndex = 4;
            this.btnEnhancement.Text = "Enhancement (E)";
            this.btnEnhancement.UseVisualStyleBackColor = true;
            this.btnEnhancement.Click += new System.EventHandler(this.btnEnhancement_Click);
            // 
            // btnIrrelevant
            // 
            this.btnIrrelevant.Location = new System.Drawing.Point(465, 307);
            this.btnIrrelevant.Name = "btnIrrelevant";
            this.btnIrrelevant.Size = new System.Drawing.Size(104, 37);
            this.btnIrrelevant.TabIndex = 5;
            this.btnIrrelevant.Text = "Non-Functional (R)";
            this.btnIrrelevant.UseVisualStyleBackColor = true;
            this.btnIrrelevant.Click += new System.EventHandler(this.btnIrrelevant_Click);
            // 
            // chkbFlag
            // 
            this.chkbFlag.AutoSize = true;
            this.chkbFlag.Location = new System.Drawing.Point(12, 318);
            this.chkbFlag.Name = "chkbFlag";
            this.chkbFlag.Size = new System.Drawing.Size(46, 17);
            this.chkbFlag.TabIndex = 6;
            this.chkbFlag.Text = "Flag";
            this.chkbFlag.UseVisualStyleBackColor = true;
            this.chkbFlag.CheckedChanged += new System.EventHandler(this.chkbFlag_CheckedChanged);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(707, 266);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(83, 35);
            this.btnSplit.TabIndex = 7;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(871, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawFileToolStripMenuItem,
            this.savedFileToolStripMenuItem,
            this.patternToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // rawFileToolStripMenuItem
            // 
            this.rawFileToolStripMenuItem.Name = "rawFileToolStripMenuItem";
            this.rawFileToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.rawFileToolStripMenuItem.Text = "Raw File";
            this.rawFileToolStripMenuItem.Click += new System.EventHandler(this.rawFileToolStripMenuItem_Click);
            // 
            // savedFileToolStripMenuItem
            // 
            this.savedFileToolStripMenuItem.Name = "savedFileToolStripMenuItem";
            this.savedFileToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.savedFileToolStripMenuItem.Text = "Saved File";
            this.savedFileToolStripMenuItem.Click += new System.EventHandler(this.savedFileToolStripMenuItem_Click);
            // 
            // patternToolStripMenuItem
            // 
            this.patternToolStripMenuItem.Name = "patternToolStripMenuItem";
            this.patternToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.patternToolStripMenuItem.Text = "Patterns";
            this.patternToolStripMenuItem.Click += new System.EventHandler(this.patternToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // lsbxHistory
            // 
            this.lsbxHistory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lsbxHistory.FormattingEnabled = true;
            this.lsbxHistory.Location = new System.Drawing.Point(102, 37);
            this.lsbxHistory.Name = "lsbxHistory";
            this.lsbxHistory.Size = new System.Drawing.Size(84, 264);
            this.lsbxHistory.TabIndex = 9;
            this.lsbxHistory.SelectedIndexChanged += new System.EventHandler(this.lsbxHistory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(704, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Classified as: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(703, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "      ";
            // 
            // btnAd
            // 
            this.btnAd.Location = new System.Drawing.Point(586, 307);
            this.btnAd.Name = "btnAd";
            this.btnAd.Size = new System.Drawing.Size(104, 37);
            this.btnAd.TabIndex = 12;
            this.btnAd.Text = "Advertisement (U)";
            this.btnAd.UseVisualStyleBackColor = true;
            this.btnAd.Click += new System.EventHandler(this.btnAd_Click);
            // 
            // btnChangeRating
            // 
            this.btnChangeRating.Location = new System.Drawing.Point(707, 307);
            this.btnChangeRating.Name = "btnChangeRating";
            this.btnChangeRating.Size = new System.Drawing.Size(104, 37);
            this.btnChangeRating.TabIndex = 13;
            this.btnChangeRating.Text = "Change Rating Request (I)";
            this.btnChangeRating.UseVisualStyleBackColor = true;
            this.btnChangeRating.Click += new System.EventHandler(this.btnChangeRating_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblVersionPat,
            this.toolStripStatusLabel3,
            this.lblDatePattern,
            this.toolStripStatusLabel5,
            this.lblDateFormat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 349);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(871, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel1.Text = "Version Pattern: ";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // lblVersionPat
            // 
            this.lblVersionPat.Name = "lblVersionPat";
            this.lblVersionPat.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabel3.Text = "Date Pattern:";
            // 
            // lblDatePattern
            // 
            this.lblDatePattern.Name = "lblDatePattern";
            this.lblDatePattern.Size = new System.Drawing.Size(10, 17);
            this.lblDatePattern.Text = " ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabel5.Text = "Date Format:";
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(10, 17);
            this.lblDateFormat.Text = " ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSibling);
            this.groupBox1.Controls.Add(this.rbMobile);
            this.groupBox1.Controls.Add(this.rbDesktop);
            this.groupBox1.Location = new System.Drawing.Point(707, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 92);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Type";
            // 
            // rbDesktop
            // 
            this.rbDesktop.AutoSize = true;
            this.rbDesktop.Location = new System.Drawing.Point(6, 19);
            this.rbDesktop.Name = "rbDesktop";
            this.rbDesktop.Size = new System.Drawing.Size(65, 17);
            this.rbDesktop.TabIndex = 0;
            this.rbDesktop.TabStop = true;
            this.rbDesktop.Text = "Desktop";
            this.rbDesktop.UseVisualStyleBackColor = true;
            this.rbDesktop.CheckedChanged += new System.EventHandler(this.rbDesktop_CheckedChanged);
            // 
            // rbMobile
            // 
            this.rbMobile.AutoSize = true;
            this.rbMobile.Location = new System.Drawing.Point(6, 42);
            this.rbMobile.Name = "rbMobile";
            this.rbMobile.Size = new System.Drawing.Size(56, 17);
            this.rbMobile.TabIndex = 1;
            this.rbMobile.TabStop = true;
            this.rbMobile.Text = "Mobile";
            this.rbMobile.UseVisualStyleBackColor = true;
            this.rbMobile.CheckedChanged += new System.EventHandler(this.rbMobile_CheckedChanged);
            // 
            // rbSibling
            // 
            this.rbSibling.AutoSize = true;
            this.rbSibling.Location = new System.Drawing.Point(6, 65);
            this.rbSibling.Name = "rbSibling";
            this.rbSibling.Size = new System.Drawing.Size(56, 17);
            this.rbSibling.TabIndex = 2;
            this.rbSibling.TabStop = true;
            this.rbSibling.Text = "Sibling";
            this.rbSibling.UseVisualStyleBackColor = true;
            this.rbSibling.CheckedChanged += new System.EventHandler(this.rbSibling_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 371);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnChangeRating);
            this.Controls.Add(this.btnAd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsbxHistory);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.chkbFlag);
            this.Controls.Add(this.btnIrrelevant);
            this.Controls.Add(this.btnEnhancement);
            this.Controls.Add(this.btnFeature);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnBug);
            this.Controls.Add(this.lsbxVersions);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Classifier";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbxVersions;
        private System.Windows.Forms.Button btnBug;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFeature;
        private System.Windows.Forms.Button btnEnhancement;
        private System.Windows.Forms.Button btnIrrelevant;
        private System.Windows.Forms.CheckBox chkbFlag;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ListBox lsbxHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAd;
        private System.Windows.Forms.Button btnChangeRating;
        private System.Windows.Forms.ToolStripMenuItem rawFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patternToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblVersionPat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblDatePattern;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lblDateFormat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSibling;
        private System.Windows.Forms.RadioButton rbMobile;
        private System.Windows.Forms.RadioButton rbDesktop;
    }
}

