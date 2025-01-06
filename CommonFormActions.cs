using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMB_Data_Processing
{
    internal class CommonFormActions
    {
        public (String, long) browse_for_file()
        {
            string s_filepath = "";
            long l_filesize = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IMB files (*.IMB)|*.IMB|All files (*.*)|*.*";
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    s_filepath = openFileDialog.FileName;
                    l_filesize = get_file_size(s_filepath);
                }
                catch (IOException){}
            }

            return (s_filepath, l_filesize) ;
        }

        private long get_file_size(string s_filepath) {
            long size = 0;

            FileInfo fileInfo = new FileInfo(s_filepath);
            size = fileInfo.Length;

            return size;
        }

        public String browse_for_folder()
        {
            string s_folderpath = "";

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                s_folderpath = folderBrowserDialog.SelectedPath;
            }

            return s_folderpath;
        }
    }
}
