using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Leelanet.infrastructure
{
    class LeeBitmap
    {
        private Graphics g;
        private int PanSize;
        private int _Height;
        private Bitmap bp;
        private void DrawLine(int startX, int startY, int endX, int endY)
        {
            Pen p = new Pen(Color.Black, 1);
            g.DrawLine(p, startX, startY, endX, endY);
        }
        public void DrawCircleLine(int x, int y, int rad, Color? color = null)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color _color = color ?? Color.Black;
            Pen pen = new Pen(_color,2f);


            g.DrawEllipse(pen,x, y, rad, rad);
        }
        public void DrawCircle( int x, int y,int rad, Color? color = null)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color _color = color ?? Color.Black;
            Brush bush = new SolidBrush(_color);


            g.FillEllipse(bush, x, y, rad,rad);
        }
        public void DrawPiece(int x, int y, int rad, Color? color = null)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;// .AntiAlias;

            Color _color = color ?? Color.Black;
            //Brush bush = new SolidBrush(Color.Gray);
           // Color _color = color ?? Color.Black;
            Image image = Resource.black0;

            int colorarg = 100;

            Rectangle myRectangle = new Rectangle(x+1, y+1, rad + 3, rad + 3);

            // 创建渐变画刷，颜色水平从左到右由蓝变到绿  
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(
                            myRectangle,
                            Color.FromArgb(0,0,0,0),
                            Color.FromArgb(colorarg, colorarg, colorarg, 0),
                            LinearGradientMode.ForwardDiagonal);
            // 使用渐变画刷填充椭圆  
            //myGraphics.FillEllipse(myLinearGradientBrush, myRectangle);
            g.FillEllipse(myLinearGradientBrush, x+1, y+1, rad + 3, rad +3);
            if ( _color == Color.White)
            {
                image = Resource.white0;
            }
            g.DrawImage(image, x, y, rad,rad);
        }
        

        
        public void DrawArc(int x, int y,int width,int height,Color color)
        {
            Pen pen = new Pen(color, 1);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawArc(pen, x, y, width, height, 180, 80)
;        }  
        public void DrawHorLine(int startX, int startY, int lineLength)
        {
            DrawLine(startX, startY, startX + lineLength, startY);
        }
        public void DrawVerLine(int startX, int startY, int lineLength)
        {
            DrawLine(startX, startY, startX, startY + lineLength);
        }
        public LeeBitmap(int panSize)
        {
            PanSize = panSize;
            bp = new Bitmap(PanSize, PanSize);
            g = Graphics.FromImage(bp);
        }
        public void RenderBackGround()
        {
            Image myImage = Resource.bg;
            //Image myImage = Image.FromFile("bg.gif");
            // 创建纹理画刷  
            TextureBrush myTextureBrush = new TextureBrush(myImage);
            g.FillRectangle(myTextureBrush, 0, 0, PanSize, PanSize);
        }

        internal void DrawCross(int x, int y, int rad)
        {
            Image image = Resource.cross;
            g.DrawImage(image, x, y, rad, rad);
        }

        public Bitmap BP {
            get { return bp; }
        }

        internal void DrawString(float x, float y, string word,float maximumFontHeight, float maximumFontWidth)
        {

            if (word.Length > 4)
            {
                word = word.Substring(0, 4);
            }
            y++;
            x++;
            // Create font and brush.
            Font drawFont = new Font("Arial", 16,FontStyle.Bold);
            
            drawFont = new Font("Arial",(float)(drawFont.Size * maximumFontWidth / g.MeasureString(word,drawFont).Width), FontStyle.Bold);
            
            
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create rectangle for drawing.
            
            
            //RectangleF drawRect = new RectangleF(x, y, width, height);

            // Draw rectangle to screen.
            Pen blackPen = new Pen(Color.Black);
            //g.DrawRectangle(blackPen, x, y, width, height);

            // Draw string to screen.
            
            //g.ScaleTransform(0.5f, 0.5f);
            g.DrawString(word, drawFont, drawBrush,x,y);
            
        }
    }
    
}
