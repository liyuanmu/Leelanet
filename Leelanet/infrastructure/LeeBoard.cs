using Leelanet.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Leelanet.infrastructure
{
    class LeeBoard
    {
       
        private int interval;
        private int lineLength;
        private int startX;
        private int startY;
        private int starOffset;
        private int starRad;
        private int pieceSize;
        private GoLogical goLogical;
        private List<MoveNode> HistoryMoves = new List<MoveNode>();
        private String alphabet = "ABCDEFGHJKLMNOPQRST";
        public int BoardSize { get; set; }
        public List<MoveData> bestMoves { set; get; }
        public int[] CrossCoord { set; get; }
        public POINTSTATUS GetCurrent
        {
            get
            {
                return goLogical.CURRENTCOLOR;
            }
        }
        public LeeBoard( int boardSize)
        {
            goLogical = new GoLogical();

            SetSize(boardSize);
        }

        public  void SetSize(int panSize)
        {
            
            interval = panSize / 19;
            lineLength = interval * 18;
            BoardSize = panSize;
            startX = interval / 2;
            startY = interval / 2;
            starRad = interval / 4;
            //pieceSize = interval - 8;
            pieceSize = interval-2;
            if (pieceSize <= 0)
            {
                pieceSize = 2;
            }
            starOffset = starRad / 2;
            if (starRad <= 2)
            {
                starRad = 6;
                starOffset = 1;
            }
        }

        public Image RenderBoard()
        {
            LeeBitmap leeBitmap = new LeeBitmap(BoardSize);
            leeBitmap.RenderBackGround();
            for (int i = 0; i < 19; i++)
            {

                leeBitmap.DrawVerLine(i * interval + startX, startY, lineLength);
                leeBitmap.DrawHorLine(startX, i * interval + startY, lineLength);
                if (i == 3 || i == 9 || i == 15)
                {
                    leeBitmap.DrawCircle(startX + i * interval - starOffset, startY + 3 * interval - starOffset, interval / 4);
                    leeBitmap.DrawCircle(startX + i * interval - starOffset, startY + 9 * interval - starOffset, interval / 4);
                    leeBitmap.DrawCircle(startX + i * interval - starOffset, startY + 15 * interval - starOffset, interval / 4);
                }
            }
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (goLogical.allPoints[i, j] != POINTSTATUS.EMPTY)
                    {
                        DrawPiece(leeBitmap, i, j, goLogical.allPoints[i, j]);
                    }
                }
            }
            if (bestMoves != null)
            {
                int index = 0;
                bestMoves.ForEach(delegate (MoveData md)
                {
                DrawBestMove(leeBitmap, md.coordinate, md.winrate, index);
                    index++;
                });
            }
            if( CrossCoord != null)
            {
                DrawCross(leeBitmap, startX - pieceSize / 2 + CrossCoord[0] * interval, startY - pieceSize / 2 + CrossCoord[1] * interval ,interval);
            }
            return leeBitmap.BP;
        }

        private void DrawCross(LeeBitmap lp, int x, int y, int rad)
        {
            lp.DrawCross(x, y,rad);
        }

        internal List<MoveNode> GetHistoryMoves()
        {
            return HistoryMoves;
        }

        private void DrawBestMove(LeeBitmap leeBitmap, string coordinate, double winrate,int index)
        {
            int[] coord = convertNameToCoordinates(coordinate);
            if (coord != null)
            {
                Color _colorEdge = Color.Blue;
                Color _colorContent = Color.MediumSpringGreen;
                if ( index == 0)
                {
                     _colorEdge = Color.Red;
                     _colorContent = Color.DarkOrange;
                }
                else if( index == 1)
                {
                    _colorEdge = Color.DeepPink;
                    _colorContent = Color.GreenYellow;
                }
                leeBitmap.DrawCircleLine(startX - pieceSize / 2 + coord[0] * interval, startY - pieceSize / 2 + coord[1] * interval, pieceSize+1, _colorEdge);
                leeBitmap.DrawCircle(startX - pieceSize / 2 + coord[0] * interval, startY - pieceSize / 2 + coord[1] * interval, pieceSize, _colorContent);
                leeBitmap.DrawString(startX - pieceSize / 2 + coord[0] * interval, startY - pieceSize / 2 + coord[1] * interval, winrate.ToString(),interval/2, interval/2*1.5f);
            }
        }

        internal void BackMove()
        {
            if(HistoryMoves != null&&HistoryMoves.Count>0)
            {
                MoveNode mn = HistoryMoves.Last();
                goLogical.CURRENTCOLOR = mn.color;
                HistoryMoves.RemoveAt(HistoryMoves.Count - 1);
                if (HistoryMoves.Count > 0) {
                    
                    goLogical.allPoints = (POINTSTATUS[,])HistoryMoves.Last().allPoints.Clone();

                }
                else
                {
                    goLogical.CURRENTCOLOR = POINTSTATUS.BLACK;
                    goLogical.allPoints[mn.X, mn.Y] = POINTSTATUS.EMPTY;
                }
            }
            
        }

        public int[] convertNameToCoordinates(String namedCoordinate)
        {
            namedCoordinate = namedCoordinate.Trim();
            if (namedCoordinate.ToLower().Equals("pass"))
            {
                return null;
            }
            // coordinates take the form C16 A19 Q5 K10 etc. I is not used.
            int x = alphabet.IndexOf(namedCoordinate.ToCharArray()[0]);
            int y = Convert.ToInt32(namedCoordinate.Substring(1)) - 1;
            return new int[] { x, y };
        }
        public String convertToCoordinates(String namedCoordinate)
        {
            namedCoordinate = namedCoordinate.Trim();
            if (namedCoordinate.ToLower().Equals("pass"))
            {
                return null;
            }
            // coordinates take the form C16 A19 Q5 K10 etc. I is not used.
            int x = alphabet.IndexOf(namedCoordinate.ToCharArray()[0]);
            int y = Convert.ToInt32(namedCoordinate.Substring(1));
            return x.ToString() + y;
        }
        public String convertToNameCoord( int x, int y)
        {
            return alphabet[x].ToString() + (y + 1);
        }
        internal int[] MouseMove( int x, int y)
        {
            if ((x > startX && x < (startX + BoardSize))
                && y > startY && y < (startY + BoardSize))
            {
                x = x / interval;
                y = y / interval;
                return  new int[] { x, y };
                
               
            }
            return null;
        }
        internal int[] Click(int x, int y)
        {
            if(( x > startX && x< (startX + BoardSize))
                &&y>startY&&y<(startY + BoardSize))
            {
                x = x / interval;
                y = y / interval;
                int[] ret= goLogical.Go(x,y);
                if( ret != null)
                {
                    HistoryMoves.Add(
                        new MoveNode()
                        {
                            X = x,
                            Y = y,
                            allPoints = (POINTSTATUS[,])goLogical.allPoints.Clone(),
                            color = goLogical.CURRENTCOLOR == POINTSTATUS.BLACK ? POINTSTATUS.WHITE : POINTSTATUS.BLACK
                        });
                    
                   
                    
                }
                return ret;   
            }
            return null;
        }

        public void DrawPiece(LeeBitmap leeBitmap, int x, int y,POINTSTATUS pIECECOLOR = POINTSTATUS.BLACK)
        {
            leeBitmap.DrawPiece(startX-pieceSize/2+x*interval, startY-pieceSize/2+y*interval, pieceSize, pIECECOLOR== POINTSTATUS.BLACK?Color.Black: Color.White);
            //leeBitmap.DrawArc(startX - pieceSize / 2, startY - pieceSize / 2, pieceSize - 2, pieceSize - 2, Color.White);
        }
       
    }
}
