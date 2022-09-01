using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SimpleTwitchChat
{
    internal class PyPoints
    {
        string cmd = "";
        string args = "";
        public PyPoints(string cmd, string args)
        {
            this.cmd = cmd;
            this.args = args;
        }

        public string GetPoints()
        {
            return run_cmd(cmd, args);
        }
        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Oleg\AppData\Local\Programs\Python\Python310\python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
    
}
