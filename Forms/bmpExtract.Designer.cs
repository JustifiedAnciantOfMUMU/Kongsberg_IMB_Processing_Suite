namespace IMB_Data_Processing
{
    partial class bmpExtract
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bmpExtract));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.SourceButton = new System.Windows.Forms.Button();
            this.DestButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DestLable = new System.Windows.Forms.Label();
            this.RunButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = ".bmp Extract";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(703, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tool to extract individual .bmp files from single .imb file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "IMB file";
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(241, 238);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(23, 32);
            this.SourceLabel.TabIndex = 3;
            this.SourceLabel.Text = "-";
            // 
            // SourceButton
            // 
            this.SourceButton.Location = new System.Drawing.Point(45, 227);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(149, 46);
            this.SourceButton.TabIndex = 4;
            this.SourceButton.Text = "Browse";
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // DestButton
            // 
            this.DestButton.Location = new System.Drawing.Point(45, 390);
            this.DestButton.Name = "DestButton";
            this.DestButton.Size = new System.Drawing.Size(149, 46);
            this.DestButton.TabIndex = 5;
            this.DestButton.Text = "Browse";
            this.DestButton.UseVisualStyleBackColor = true;
            this.DestButton.Click += new System.EventHandler(this.DestButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(274, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "Destination Location";
            // 
            // DestLable
            // 
            this.DestLable.AutoSize = true;
            this.DestLable.Location = new System.Drawing.Point(225, 398);
            this.DestLable.Name = "DestLable";
            this.DestLable.Size = new System.Drawing.Size(23, 32);
            this.DestLable.TabIndex = 7;
            this.DestLable.Text = "-";
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(45, 479);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(128, 54);
            this.RunButton.TabIndex = 8;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1137, 54);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 30;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(250, 38);
            this.toolStripProgressBar1.Step = 5;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(30, 41);
            this.toolStripStatusLabel1.Text = "-";
            // 
            // bmpExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 623);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.DestLable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DestButton);
            this.Controls.Add(this.SourceButton);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "bmpExtract";
            this.Text = "IMB Processing Suite - .bmp Extract";
            this.Load += new System.EventHandler(this.bmpExtract_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Button SourceButton;
        private System.Windows.Forms.Button DestButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DestLable;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}