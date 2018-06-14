using Leelanet.Config;
using Leelanet.infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Leelanet.Engine
{
    public class Leelaz
    {
        public delegate void Listener();
        public Listener notifyListener=null;
        private StreamWriter writer;
        private StreamReader reader;
        private Process proc;
        private List<MoveData> bestMoves;
        private List<MoveData> bestMovesTemp;
        private List<LeelazListener> listeners;
        private bool isPondering;
        private long startPonderTime;
        private long maxAnalyzeTimeMillis; //, maxThinkingTimeMillis;
        private int cmdNumber;
        private int currentCmdNum;
        private bool printCommunication;
        // fixed_handicap
        public bool isSettingHandicap = false;

        // genmove
        public bool isThinking = false;

        private bool isLoaded = false;
        private bool isCheckingVersion;

        public Leelaz()
        {
            /*

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
            //proc = Process.Start(@"d:\lizzie\leelaz.exe", @"--gtp --lagbuffer 0 --weights d:\lizzie\network.gz --threads 2");
            reader = proc.StandardOutput;
            writer = proc.StandardInput;
            sendCommand("version");
            //togglePonder();
            new Thread(this.read).Start();
            //proc.StandardInput.WriteLine("Test");
            */
        }
        public void Attach( ProcessManager pm)
        {
            this.proc = pm.PROC;
            pm.OnLineReaded += new ProcessManager.LineReaded(OnLineReaded);
            writer = proc.StandardInput;
            isPondering = false;
        }

        private void OnLineReaded(String line)
        {
            parseLine(line.ToString());
        }

        internal bool HasProc()
        {
            try
            {
                return proc != null && !proc.HasExited;
            }
            catch
            {
                return false;
            }
        }

        

        internal List<MoveData> GetBestMoves()
        {
            return bestMoves;
        }

        internal void ShutDown()
        {

            if (HasProc())
            {
                proc.Kill();
            }
            
        }

        public virtual void sendCommand(string command)
        {
            command = cmdNumber + " " + command;
            cmdNumber++;
            if (printCommunication)
            {
                Console.Write("> {0}\n", command);
            }
            if (command.StartsWith("fixed_handicap", StringComparison.Ordinal))
            {
                isSettingHandicap = true;
            }
            try
            {
                writer.WriteLine(command);
                writer.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        internal void BackMove()
        {

            lock (this)
            {
                sendCommand("undo");
                if (bestMoves != null)
                {
                    bestMoves.Clear();
                }
                
                if (isPondering)
                    ponder();
            }

        }

        internal void CloseEngine()
        {
            if (proc != null && !proc.HasExited)
            {
                proc.Kill();
            }
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
                        parseLine(line.ToString());
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
            Application.Exit();
        }
        private void parseLine(String line)
        {
            lock (this)
            {
               if (line.Equals("\n"))
                {
                    // End of response
                }
                else if (line.StartsWith("info", StringComparison.Ordinal))
                {
                    isLoaded = true;
                    if (currentCmdNum == cmdNumber - 1)
                    {
                        // This should not be stale data when the command number match
                        parseInfo(line.Substring(5));
                        notifyBestMoveListeners();
                        //if (Lizzie.frame != null)
                        //{
                        //    Lizzie.frame.repaint();
                        //}
                        // don't follow the maxAnalyzeTime rule if we are in analysis mode
                        //if (DateTimeHelperClass.CurrentUnixTimeMillis() - startPonderTime > maxAnalyzeTimeMillis && !Lizzie.board.inAnalysisMode())
                        //{
                        //    togglePonder();
                        //}
                    }
                }
                /*else if (line.StartsWith("play", StringComparison.Ordinal))
                {
                    // In lz-genmove_analyze
                    if (Lizzie.frame.isPlayingAgainstLeelaz)
                    {
                        Lizzie.board.place(line.Substring(5).Trim());
                    }
                    isThinking = false;

                }*/
                //else if (Lizzie.frame != null && line.StartsWith("=", StringComparison.Ordinal))
                else if (line.StartsWith("=", StringComparison.Ordinal))
                {
                    if (printCommunication)
                    {
                        Console.Write(line);
                    }
                    string[] @params = line.Trim().Split(' ');
                    currentCmdNum = int.Parse(@params[0].Substring(1).Trim());

                    if (@params.Length == 1)
                    {
                        return;
                    }


                    //if (isSettingHandicap)
                    //{
                    //    for (int i = 2; i < @params.Length; i++)
                    //    {
                    //        int[] coordinates = featurecat.lizzie.rules.Board.convertNameToCoordinates(@params[i]);
                    //        Lizzie.board.History.setStone(coordinates, Stone.BLACK);
                    //    }
                    //    isSettingHandicap = false;
                    //}
                    //else if (isThinking && !isPondering)
                    //{
                    //    if (Lizzie.frame.isPlayingAgainstLeelaz)
                    //    {
                    //        Lizzie.board.place(@params[1]);
                    //        isThinking = false;
                    //    }
                    //}
                    //else if (isCheckingVersion)
                    //{
                    //    string[] ver = @params[1].Split(new String[] { "\\." }, StringSplitOptions.RemoveEmptyEntries);
                    //    int minor = int.Parse(ver[1]);
                    //    // Gtp support added in version 15
                    //    if (minor < 15)
                    //    {
                    //        MessageBox.Show("Lizzie requires version 0.15 or later of Leela Zero for analysis (found " + @params[1] + ")");
                    //    }
                    //    isCheckingVersion = false;
                    //}
                    
                }
            }
        }
        private void ponder()
        {
            isPondering = true;
            //startPonderTime = DateTimeHelperClass.CurrentUnixTimeMillis();
            //sendCommand("lz-analyze " + Lizzie.config.config.getJSONObject("leelaz").getInt("analyze-update-interval-centisec")); // until it responds to this, incoming ponder results are obsolete
            sendCommand("lz-analyze 10");
        }

        public virtual void togglePonder()
        {
            isPondering = !isPondering;
            if (isPondering)
            {
                ponder();
            }
            else
            {
                sendCommand("name"); // ends pondering
            }
        }
        private void parseInfo(string line)
        {

            bestMoves = new List<MoveData>();
            string[] variations = line.Split(new String[] { " info " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string @var in variations)
            {
                bestMoves.Add(new MoveData(@var));
            }
            // Not actually necessary to sort with current version of LZ (0.15)
            // but not guaranteed to be ordered in the future
            bestMoves.Sort();
            Console.WriteLine("*********bestmoves***********=" + bestMoves.Count);
        }
       

        private void notifyBestMoveListeners()
        {
            lock (this)
            {
                //foreach (LeelazListener listener in listeners)
                //{
                //    listener.bestMoveNotification(bestMoves);
                //}
                if (notifyListener != null)
                {
                    notifyListener();
                }
            }
        }
        public void playMove(String colorString, String move)
        {
            lock(this)
            {
                sendCommand("play " + colorString + " " + move);
                if (bestMoves != null)
                {
                    bestMoves.Clear();
                }
                if (isPondering)
                {
                    ponder();
                }
            }
        }
        //internal void playMove(POINTSTATUS color, int x, int y)
        //{
        //    lock (this)
        //    {
        //        sendCommand("play " + color == POINTSTATUS.BLACK ? "B" : "W" + " " + alphabet[coord[0]].ToString() + (coord[1] + 1));
        //        bestMoves.Clear();

        //        if (isPondering)
        //        {
        //            ponder();
        //        }
        //    }
        //}
        public virtual bool Loaded
        {
            get
            {
                return isLoaded;
            }
        }
    }
}
