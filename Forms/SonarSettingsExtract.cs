using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMB_Data_Processing.Forms
{
    public partial class SonarSettingsExtract : Form
    {
        private CommonFormActions cfa = new CommonFormActions();
        private string s_source_filepath = string.Empty;
        private long l_source_size = 0;
        Dictionary<string, string> sonar_info = new Dictionary<string, string>();

        public SonarSettingsExtract()
        {
            InitializeComponent();
        }

        private void SonarSettingsExtract_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "No File Selected";
        }

        private void Browse_Button_Click(object sender, EventArgs e)
        {
            (s_source_filepath, l_source_size) = cfa.browse_for_file();
            sourceLabel.Text = s_source_filepath;
            toolStripStatusLabel1.Text = "No File Selected";

            sonar_info = Program.ExtractSonarSettings(s_source_filepath);

            mode_label.Text = "Mode - " + sonar_info["mode_id"];
            min_dist_label.Text = "Min Dist - " + sonar_info["dist_min"] + " m";
            max_dist_label.Text = "Max Dist - " + sonar_info["dist_max"] + " m";
            SonarFreqLabel.Text = "Sonar Freq - " + sonar_info["sonar_freq"] + " Hz";
            PulseLengthLabel.Text = "Pulse Length - " + sonar_info["pulse_length"] + " µs";

            toolStripStatusLabel1.Text = "Values Updates";
        }
    }
}
