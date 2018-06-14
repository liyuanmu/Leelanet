using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leelanet.infrastructure
{
    class GoLogical
    {
        public List<GoPointStatus> GoPoints;
        public POINTSTATUS[,] allPoints = new POINTSTATUS[19, 19];
        private POINTSTATUS CurrentColor = POINTSTATUS.BLACK;
        private POINTSTATUS[,] allTempPoints = new POINTSTATUS[19, 19];
        public POINTSTATUS CURRENTCOLOR { get { return CurrentColor; }  set { CurrentColor = value; } }
        public GoLogical()
        {
            GoPoints = new List<GoPointStatus>();
            //allPoints[0, 0] = POINTSTATUS.black;
            //allPoints[0, 1] = POINTSTATUS.white;
            //allPoints.Initialize(POINTSTATUS.none);
        }
        public void Add(int x, int y, POINTSTATUS color)
        {
            GoPointStatus gp = new GoPointStatus();
            gp.x = x;
            gp.y = y;
            gp.Status = color;

            GoPoints.Add(gp);
            SwitchPlayer();
        }
        private void SwitchPlayer()
        {
            CurrentColor = CurrentColor == POINTSTATUS.BLACK ? POINTSTATUS.WHITE : POINTSTATUS.BLACK;
        }
        internal int[] Go(int x, int y)
        {
            if (IsLegal(x, y))
            {
                allPoints[x, y] = CurrentColor;
                SwitchPlayer();
                return new int[] { x, y };
            }
            return null;
        }
        private bool IsLegal(int x, int y)
        {
            bool bRet = true;
            if (allPoints[x, y] == POINTSTATUS.EMPTY)
            {
                allPoints[x, y] = CurrentColor;
                if (x > 0 && allPoints[x - 1, y] != CurrentColor &&  allPoints[x - 1, y] !=POINTSTATUS.EMPTY)
                {
                    allTempPoints = (POINTSTATUS[,])allPoints.Clone();
                    allTempPoints[x - 1, y] = POINTSTATUS.VISITED;
                    if( !get_adjacent_intersections( x-1,y, allPoints[x - 1, y]))
                    {
                        RemovePiece();
                        allPoints = allTempPoints;
                        
                    }
                }
                if( x<18 && allPoints[x + 1, y] != CurrentColor && allPoints[x + 1, y] != POINTSTATUS.EMPTY)
                {
                    allTempPoints = (POINTSTATUS[,])allPoints.Clone();
                    allTempPoints[x + 1, y] = POINTSTATUS.VISITED;
                    if (!get_adjacent_intersections(x +1, y, allPoints[x + 1, y]))
                    {
                        RemovePiece();
                        allPoints = allTempPoints;

                    }
                }
                if (y < 18 && allPoints[x , y+1] != CurrentColor && allPoints[x , y+1] != POINTSTATUS.EMPTY)
                {
                    allTempPoints = (POINTSTATUS[,])allPoints.Clone();
                    allTempPoints[x , y+1] = POINTSTATUS.VISITED;
                    if (!get_adjacent_intersections(x , y+1, allPoints[x , y+1]))
                    {
                        RemovePiece();
                        allPoints = allTempPoints;

                    }
                }
                if (y > 0 && allPoints[x, y-1] != CurrentColor && allPoints[x, y-1] != POINTSTATUS.EMPTY)
                {
                    allTempPoints = (POINTSTATUS[,])allPoints.Clone();
                    allTempPoints[x , y-1] = POINTSTATUS.VISITED;
                    if (!get_adjacent_intersections(x , y-1, allPoints[x , y-1]))
                    {
                        RemovePiece();
                        allPoints = allTempPoints;

                    }
                }
                if( IsSuiCide(x,y, CurrentColor))
                {
                    allPoints[x, y] = POINTSTATUS.EMPTY;
                    bRet = false;
                }
            }
            else
            {
                bRet = false;
            }
            return bRet;
        }
        private void RemovePiece()
        {
            for( int i = 0; i < 19; i++)
            {
                for( int j = 0; j < 19; j++)
                {
                    if( allTempPoints[i,j] ==POINTSTATUS.VISITED)
                    {
                        allTempPoints[i, j] = POINTSTATUS.EMPTY;
                    }
                }
            }
        }
       private bool get_adjacent_intersections(int i , int j, POINTSTATUS type)
        {
            //allTempPoints = allPoints;
            if (i > 0)
            {
                if (allTempPoints[i - 1, j] == type && allTempPoints[i - 1, j] != POINTSTATUS.VISITED)
                {
                    allTempPoints[i - 1, j] = POINTSTATUS.VISITED;
                    if( get_adjacent_intersections(i - 1, j, type))
                    {
                        return true;
                    }
                }
                else if(allTempPoints[i-1,j] == POINTSTATUS.EMPTY)
                {
                    return true;
                }
            }
            if (j < 18)
            {
                if (allTempPoints[i, j + 1] == type && allTempPoints[i , j+1] != POINTSTATUS.VISITED)
                {
                    allTempPoints[i , j+1] = POINTSTATUS.VISITED;
                    if( get_adjacent_intersections(i, j + 1, type))
                    {
                        return true;
                    }
                }
                else if (allTempPoints[i, j + 1] == POINTSTATUS.EMPTY)
                {
                    return true;
                }
            }
            if (i < 18 )
            {
                if (allTempPoints[i+1, j] == type && allTempPoints[i + 1, j] != POINTSTATUS.VISITED)
                {
                    allTempPoints[i + 1, j] = POINTSTATUS.VISITED;
                    if( get_adjacent_intersections(i+1, j, type))
                    {
                        return true;
                    }
                }
                else if (allTempPoints[i+1, j] == POINTSTATUS.EMPTY)
                {
                    return true;
                }
            }
            if (j > 0)

            {
                if (allTempPoints[i, j - 1] == type && allTempPoints[i , j-1] != POINTSTATUS.VISITED)
                {
                    allTempPoints[i , j-1] = POINTSTATUS.VISITED;
                    if (get_adjacent_intersections(i, j - 1, type))
                    {
                        return true;
                    }
                }
                else if (allTempPoints[i, j - 1] == POINTSTATUS.EMPTY)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsSuiCide( int x, int y , POINTSTATUS type)
        {
            allTempPoints = (POINTSTATUS[,])allPoints.Clone();
            allTempPoints[x, y] = POINTSTATUS.VISITED;
            return !get_adjacent_intersections(x, y, allPoints[x, y]);
            
        }
        
    }
    class GoPointStatus
    {
        public int x { get; set; }
        public int y { get; set; }
        public POINTSTATUS Status { get; set; }


    }
    public enum POINTSTATUS { EMPTY=0, BLACK =1, WHITE=2,
        VISITED = 3
    }
    
}
