using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMB_Data_Processing
{
    public partial class bmpExtract : Form
    {
        private CommonFormActions cfa = new CommonFormActions();
        private string s_source_filepath = string.Empty;
        private long   l_source_size     = 0;
        private string s_dest_folderpath = string.Empty;

        public bmpExtract()
        {
            InitializeComponent();
        }

        private void bmpExtract_Load(object sender, EventArgs e)
        {
            SourceLabel.Text = s_source_filepath;
            DestLable.Text = s_dest_folderpath;

            RunButton.Enabled = true;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.MarqueeAnimationSpeed = 30;
            toolStripProgressBar1.Enabled = false;
            toolStripProgressBar1.Visible = true;

            toolStripStatusLabel1.Text = "Setup";
            toolStripStatusLabel1.Visible = true;
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            (s_source_filepath, l_source_size) = cfa.browse_for_file();
            SourceLabel.Text = s_source_filepath;

            toolStripStatusLabel1.Text = "Setup";
        }

        private void DestButton_Click(object sender, EventArgs e)
        {
            s_dest_folderpath = cfa.browse_for_folder();
            DestLable.Text = s_dest_folderpath;

            toolStripStatusLabel1.Text = "Setup";
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Enabled = true;
            toolStripStatusLabel1.Text = "Working";

            RunButton.Enabled = false;

            await Task.Run(() =>
            {
                Program.ProcessIMBFileToFrames( s_source_filepath, s_dest_folderpath);
            });
                
           MessageBox.Show("Extraction Completed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripStatusLabel1.Text = "Completed";
            toolStripProgressBar1.Visible = false;
            RunButton.Enabled = true;
        }
    }
}
