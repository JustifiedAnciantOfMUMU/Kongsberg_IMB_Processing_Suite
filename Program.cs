using IMB_Data_Processing.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IMB_Data_Processing.M3TcpTestClient;

namespace IMB_Data_Processing
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Application.Run(new bmpExtract());
            Application.Run(new SonarSettingsExtract());

            //M3TcpTestClient IMBClient = new M3TcpTestClient();
            
            //string inputPath = "E:\\Wrasbury_2024_Data\\RAW Data\\day_2\\Sonar Data\\Deploying PABLO Lander\\imb\\deployingPabloLander.imb";
            //string outputPath = "C:\\AJFTemp";

            //if (args.Length > 0 && System.IO.File.Exists(args[0]))
            //{
            //    inputPath = args[0];
            //}
            //if (args.Length > 1 && System.IO.Directory.Exists(args[1]))
            //{
            //    outputPath = args[1];
            //}
            //ProcessIMBFileToFrames(IMBClient, inputPath, outputPath);
        }


        public static void ProcessIMBFileToFrames(string input_path, string output_path)
        {
            M3TcpTestClient m3TcpTestClient = new M3TcpTestClient();
            m3TcpTestClient.StartIMBfileread(input_path, output_path);
        }


        public static Dictionary<string, string> ExtractSonarSettings(string input_path)
        {
            M3TcpTestClient m3TcpTestClient = new M3TcpTestClient();
            IMBPacket packet = new IMBPacket();
            packet = m3TcpTestClient.return_single_packet(input_path);

            Dictionary<string, string> sonar_info = new Dictionary<string, string>();
            sonar_info.Add("mode_id", packet.dwModeID.ToString());
            sonar_info.Add("dist_min", packet.fNearRange.ToString());
            sonar_info.Add("dist_max", packet.fFarRange.ToString());
            sonar_info.Add("sonar_freq", packet.dwSonarFreq.ToString());
            sonar_info.Add("pulse_length", packet.dwPulseLength.ToString());

            return sonar_info;

        }






    }
}
