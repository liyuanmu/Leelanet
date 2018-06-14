using Leelanet.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Leelanet.Engine
{

    public class ProcessManager
    {
        private Process proc;
        public delegate void LineReaded(String line);
        public LineReaded OnLineReaded = null;
        private StreamWriter writer;
        private StreamReader reader;
        public Process PROC
        {
            get { return proc; }
        }
        public ProcessManager()
        {
            proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = LeelaConfig.LeelaLoction,
                    FileName = LeelaConfig.LeelaLoction + "leelaz.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    Arguments = LeelaConfig.Arguments
                }
            };
            proc.Start();
            reader = proc.StandardOutput;
            writer = proc.StandardInput;
            new Thread(this.read).Start();
        }
        private void read()
        {
            try
            {
                int c;
                StringBuilder line = new StringBuilder();
                while ((c = reader.Read()) != -1)
                {
                    line.Append((char)c);
                    if ((c == '\n'))
                    {
                        if( OnLineReaded != null)
                        {
                            OnLineReaded(line.ToString());
                        }
                        Console.WriteLine(line);
                        line = new StringBuilder();
                    }
                }


                Console.WriteLine("Leelaz process ended.");
                proc.Dispose();

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
