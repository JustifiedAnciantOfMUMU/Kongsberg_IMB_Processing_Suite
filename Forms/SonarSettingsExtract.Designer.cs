namespace IMB_Data_Processing.Forms
{
    partial class SonarSettingsExtract
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SonarSettingsExtract));
            this.title = new System.Windows.Forms.Label();
            this.description_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FileBrowseButton = new System.Windows.Forms.Button();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mode_label = new System.Windows.Forms.Label();
            this.min_dist_label = new System.Windows.Forms.Label();
            this.max_dist_label = new System.Windows.Forms.Label();
            this.SonarFreqLabel = new System.Windows.Forms.Label();
            this.PulseLengthLabel = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(31, 34);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(321, 32);
            this.title.TabIndex = 0;
            this.title.Text = "Sonar Setting Extraction";
            // 
            // description_label
            // 
            this.description_label.AutoSize = true;
            this.description_label.Location = new System.Drawing.Point(31, 78);
            this.description_label.Name = "description_label";
            this.description_label.Size = new System.Drawing.Size(930, 32);
            this.description_label.TabIndex = 1;
            this.description_label.Text = "Will extract the sonar settings used for an sonar recording ina .imb format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = ".imb file";
            // 
            // FileBrowseButton
            // 
            this.FileBrowseButton.Location = new System.Drawing.Point(37, 201);
            this.FileBrowseButton.Name = "FileBrowseButton";
            this.FileBrowseButton.Size = new System.Drawing.Size(149, 55);
            this.FileBrowseButton.TabIndex = 3;
            this.FileBrowseButton.Text = "Browse";
            this.FileBrowseButton.UseVisualStyleBackColor = true;
            this.FileBrowseButton.Click += new System.EventHandler(this.Browse_Button_Click);
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(207, 213);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(23, 32);
            this.sourceLabel.TabIndex = 4;
            this.sourceLabel.Text = "-";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 745);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1293, 54);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(232, 41);
            this.toolStripStatusLabel1.Text = "No File Selected";
            // 
            // mode_label
            // 
            this.mode_label.AutoSize = true;
            this.mode_label.Location = new System.Drawing.Point(46, 302);
            this.mode_label.Name = "mode_label";
            this.mode_label.Size = new System.Drawing.Size(108, 32);
            this.mode_label.TabIndex = 6;
            this.mode_label.Text = "Mode - ";
            // 
            // min_dist_label
            // 
            this.min_dist_label.AutoSize = true;
            this.min_dist_label.Location = new System.Drawing.Point(285, 302);
            this.min_dist_label.Name = "min_dist_label";
            this.min_dist_label.Size = new System.Drawing.Size(139, 32);
            this.min_dist_label.TabIndex = 7;
            this.min_dist_label.Text = "Min Dist - ";
            // 
            // max_dist_label
            // 
            this.max_dist_label.AutoSize = true;
            this.max_dist_label.Location = new System.Drawing.Point(518, 302);
            this.max_dist_label.Name = "max_dist_label";
            this.max_dist_label.Size = new System.Drawing.Size(139, 32);
            this.max_dist_label.TabIndex = 8;
            this.max_dist_label.Text = "Max Dist -";
            // 
            // SonarFreqLabel
            // 
            this.SonarFreqLabel.AutoSize = true;
            this.SonarFreqLabel.Location = new System.Drawing.Point(46, 410);
            this.SonarFreqLabel.Name = "SonarFreqLabel";
            this.SonarFreqLabel.Size = new System.Drawing.Size(171, 32);
            this.SonarFreqLabel.TabIndex = 9;
            this.SonarFreqLabel.Text = "Sonar Freq -";
            // 
            // PulseLengthLabel
            // 
            this.PulseLengthLabel.AutoSize = true;
            this.PulseLengthLabel.Location = new System.Drawing.Point(415, 409);
            this.PulseLengthLabel.Name = "PulseLengthLabel";
            this.PulseLengthLabel.Size = new System.Drawing.Size(197, 32);
            this.PulseLengthLabel.TabIndex = 10;
            this.PulseLengthLabel.Text = "Pulse Length -";
            // 
            // SonarSettingsExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 799);
            this.Controls.Add(this.PulseLengthLabel);
            this.Controls.Add(this.SonarFreqLabel);
            this.Controls.Add(this.max_dist_label);
            this.Controls.Add(this.min_dist_label);
            this.Controls.Add(this.mode_label);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.FileBrowseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.description_label);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SonarSettingsExtract";
            this.Text = "SonarSettingsExtract";
            this.Load += new System.EventHandler(this.SonarSettingsExtract_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label description_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FileBrowseButton;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label mode_label;
        private System.Windows.Forms.Label min_dist_label;
        private System.Windows.Forms.Label max_dist_label;
        private System.Windows.Forms.Label SonarFreqLabel;
        private System.Windows.Forms.Label PulseLengthLabel;
    }
}