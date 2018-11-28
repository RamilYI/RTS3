using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RTS3
{
    public partial class SRV3 : Form
    {
        private static StringBuilder cmdOutput;
        private Process cmdProcess;
        private StreamWriter cmdStreamWriter;

        public SRV3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmdStreamWriter.WriteLine(textBox1.Text);
            textBox2.Text = cmdOutput.ToString();
        }

        void run()
        {
            cmdOutput = new StringBuilder("");
            cmdProcess = new Process();

            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            cmdProcess.OutputDataReceived += OutputDataReceived;
            cmdProcess.Start();

            cmdStreamWriter = cmdProcess.StandardInput;
            cmdProcess.BeginOutputReadLine();
        }

        private void SRV3_Load(object sender, EventArgs e) => run();

        private static void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                cmdOutput.Append(Environment.NewLine + e.Data);
            }
        }

        private void button2_Click(object sender, EventArgs e) => cmdProcess.Kill();
    }
}
