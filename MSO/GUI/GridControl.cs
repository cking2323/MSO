using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSO
{
    public class GridControl : UserControl
    {
        public GridDisplay gritDisplay;

        public GridControl(char[][] route)
        {
            this.Size = new Size(300, 300);
            this.gritDisplay = new GridDisplay(Size.Width, route);
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackgroundImage = gritDisplay.bitMap;

        }

    }
    
    // Creates an image with the player grid and coloured squares
    public class GridDisplay
    {
        public Bitmap bitMap;

        public GridDisplay(int size, char[][] route)
        {
            //this.bitMap = new Bitmap(300,300);
            this.bitMap = CreateGraphics(size, route);
        }
        public Bitmap CreateGraphics(int size, char[][] route) //creates the rendered bitmap    
        {
            Bitmap bm = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bm);
            GenerateSquares(route, g, bm.Width);
            GenerateGrid(5, g, bm.Width);
            return bm;
        }

        // colours the squares based on the provided pathfinding exercises
        private void GenerateSquares(char[][] route, Graphics gr, int bitmapSize)
        {
            SolidBrush brush = new SolidBrush(Color.IndianRed);
            SolidBrush brush1 = new SolidBrush(Color.Green);
            SolidBrush brush2 = new SolidBrush(Color.White);
            int boxSize = bitmapSize / route.Length;
            for (int i = 0; i < route.Length; i++)
            {
                char[] ch = route[i];
                for (int j = 0; j < ch.Length; j++)
                {
                    char c = ch[j];

                    if (c == '+')
                    {
                        gr.FillRectangle(brush, j * boxSize, i * boxSize, (j + 1) * boxSize, (i + 1) * boxSize);
                    }
                    else if (c == 'o')
                    {
                        gr.FillRectangle(brush2, j * boxSize, i * boxSize, (j + 1) * boxSize, (i + 1) * boxSize);
                    }
                    else if (c == 'x')
                    {
                        gr.FillRectangle(brush1, j * boxSize, i * boxSize, (j + 1) * boxSize, (i + 1) * boxSize);
                    }

                }
            }

        }

        // Generates grid with black lines
        private void GenerateGrid(int dimension, Graphics gr, int bitmapSize)
        {
            Pen pen = new Pen(Color.Black, 2);
            int step = bitmapSize / dimension;

            for (int i = 0; i < dimension + 1; i++) // horizontal lines
            {
                if (i == dimension)
                {
                    gr.DrawLine(pen, 0, i * step - 4, bitmapSize, i * step - 4);
                }
                else
                {
                    gr.DrawLine(pen, 0, i * step, bitmapSize, i * step);
                }
            }
            for (int i = 0; i < dimension + 1; i++) // vertical lines
            {
                if (i == dimension)
                {
                    gr.DrawLine(pen, bitmapSize - 4, 0, bitmapSize - 4, bitmapSize);
                }
                else
                {
                    gr.DrawLine(pen, i * step, 0, i * step, bitmapSize);
                }
            }
        }
    }

}


