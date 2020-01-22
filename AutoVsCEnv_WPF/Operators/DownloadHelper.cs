using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutoVsCEnv_WPF.Operators
{
    class DownloadHelper
    {
        const string aria2Path = @"Libs\aria2c.exe";

        public delegate void OnProgressChangedHandler(string percent, string speed, string eta);
        public event OnProgressChangedHandler OnProgressChanged;

        public void Download(string url, string saveDirectory)
        {
            Console.WriteLine(url);
            string args = "-d " + saveDirectory + " -c " + 
                "--header=\"accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9\" " +
                "--header=\"dnt: 1\" " +
                "--header=\"accept-language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6\" " +
                "-U \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 Edg/79.0.309.71\" " +
                url;

            Process p = new Process();
            p.StartInfo.FileName = aria2Path;
            p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            p.StartInfo.Arguments = args;

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.OutputDataReceived += ReceivedOutput;
            p.ErrorDataReceived += ReceivedOutput;

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();
        }

        private void ReceivedOutput(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            Regex regex = new Regex(@"\[#.*\((.+%)\) CN:1 DL:(.*) ETA:(.*)\]");
            if(e.Data != null)
            {
                Match match = regex.Match(e.Data);
                if (match.Success)
                {
                    string percent = match.Groups[1].Value;
                    string speed = match.Groups[2].Value.Replace("i", "");
                    string eta = match.Groups[3].Value;

                    OnProgressChanged(percent, speed, eta);
                }
            }        
        }
    }
}
