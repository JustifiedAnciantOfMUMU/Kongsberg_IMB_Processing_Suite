using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMB_Data_Processing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            M3TcpTestClient IMBClient = new M3TcpTestClient();
            
            string inputPath = "E:\\Wrasbury_2024_Data\\RAW Data\\day_2\\Sonar Data\\Deploying PABLO Lander\\imb\\deployingPabloLander.imb";
            string outputPath = string.Empty;

            if (args.Length > 0 && System.IO.File.Exists(args[0]))
            {
                inputPath = args[0];
            }
            if (args.Length > 1 && System.IO.Directory.Exists(args[1]))
            {
                outputPath = args[1];
            }
            ProcessIMBFileToFrames(IMBClient, inputPath, "");
        }


        static void ProcessIMBFileToFrames(M3TcpTestClient IMBclient, string input_path, string output_path)
        {
            M3TcpTestClient m3TcpTestClient = new M3TcpTestClient();
            m3TcpTestClient.StartIMBfileread(input_path);
        }









    }
}
