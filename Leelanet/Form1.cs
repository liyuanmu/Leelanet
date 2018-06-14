using Leelanet.Config;
using Leelanet.Engine;
using Leelanet.infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Leelanet
{
    public partial class frmMain : Form
    {
        private LeeBoard leeBoard;
        private Leelaz leelaz = null;
        
        public frmMain()
        {
            InitializeComponent();
            LeelaConfig.Init();
            leeBoard = new LeeBoard(picBoard.Width);
        }

       

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            SetPanPosition();
        }

        private void SetPanPosition()
        {
            int picPanSize = (panel1.Width > panel1.Height ? panel1.Height : panel1.Width) - 5;
            picPanSize = picPanSize / 19 * 19;
            picBoard.Size = new Size(picPanSize, picPanSize);
            picBoard.Top = (panel1.Height - picBoard.Height) / 2;
            picBoard.Left = (panel1.Width - picBoard.Width) / 2;
        }

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {

            leeBoard.SetSize(picBoard.Width);

            picBoard.Image = leeBoard.RenderBoard();
            label1.Text = leelaz!=null && leelaz.HasProc()? "Leela引擎已启动" : "Leela引擎未启动";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(500, 500);
            SetPanPosition();
            leelaz = new Leelaz();
            leelaz.notifyListener += new Leelaz.Listener(OnNotifyLeela);
            
        }

       

        private void picBoard_MouseUp(object sender, MouseEventArgs e)
        {
            
            int[] coord = leeBoard.Click(e.X, e.Y);
            if (coord != null)
            {
                if (leelaz != null)
                {
                    //leelaz.playMove(leeBoard.GetCurrent == POINTSTATUS.BLACK ? "W" : "B", alphabet[coord[0]].ToString()+(coord[1]+1) );
                    leelaz.playMove(leeBoard.GetCurrent == POINTSTATUS.BLACK ? "W" : "B", leeBoard.convertToNameCoord(coord[0],coord[1]));
                }
            }
        }

        private void btnStartLeela_Click(object sender, EventArgs e)
        {
            if (!leelaz.HasProc())
            {
                ProcessManager pm = new ProcessManager();
                leelaz.Attach(pm);
                leeBoard.GetHistoryMoves().ForEach(delegate (MoveNode mn)
                {
                    leelaz.playMove(mn.color == POINTSTATUS.BLACK ? "B" : "W", leeBoard.convertToNameCoord(mn.X,mn.Y));
                });
                
                leelaz.togglePonder();
            }
        }

        private void OnNotifyLeela()
        {
            //throw new NotImplementedException();
            leeBoard.bestMoves = leelaz.GetBestMoves();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if( leelaz != null)
            {
                leelaz.ShutDown();
            }
        }

        private void BtnBackMove_Click(object sender, EventArgs e)
        {
            leelaz.BackMove();
            leeBoard.BackMove();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if( leelaz!= null)
            {
                leelaz.CloseEngine();
            }
        }

        private void picBoard_MouseMove(object sender, MouseEventArgs e)
        {
            int[] coord = leeBoard.MouseMove(e.X, e.Y);
            if( coord != null)
            {
                leeBoard.CrossCoord = coord;
            }
        }
    }
}
