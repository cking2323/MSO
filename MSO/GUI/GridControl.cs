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

        public GridControl()
        {
            this.Size = new Size(300, 300);
            this.gritDisplay = new GridDisplay();
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackgroundImage = gritDisplay.bitMap;
        }

        public void LoadGrid(CellType[,] grid)
        {
            this.BackgroundImage = gritDisplay.CreateGraphics(300, grid);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Invalidate();
        }
    }

    // Creates an image with the player grid and coloured squares
    public class GridDisplay
    {
        public Bitmap bitMap;

        public Bitmap CreateGraphics(int size, CellType[,] grid)
        {
            Bitmap bm = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bm);
            GenerateSquares(grid, g, bm.Width);
            GenerateGrid(grid.GetLength(0), grid.GetLength(1), g, bm.Width);
            return bm;
        }

        private void GenerateSquares(CellType[,] grid, Graphics gr, int bitmapSize)
        {
            SolidBrush brush = new SolidBrush(Color.IndianRed);
            SolidBrush brush1 = new SolidBrush(Color.Green);
            SolidBrush brush2 = new SolidBrush(Color.White);

            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int boxWidth = bitmapSize / cols;
            int boxHeight = bitmapSize / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    CellType c = grid[i, j];

                    if (c == CellType.Closed)
                    {
                        gr.FillRectangle(brush, j * boxWidth, i * boxHeight, boxWidth, boxHeight);
                    }
                    else if (c == CellType.Open)
                    {
                        gr.FillRectangle(brush2, j * boxWidth, i * boxHeight, boxWidth, boxHeight);
                    }
                    else if (c == CellType.Goal)
                    {
                        gr.FillRectangle(brush1, j * boxWidth, i * boxHeight, boxWidth, boxHeight);
                    }
                }
            }
        }

        // Generates grid with black lines
        private void GenerateGrid(int rows, int cols, Graphics gr, int bitmapSize)
        {
            Pen pen = new Pen(Color.Black, 2);

            int boxWidth = bitmapSize / cols;
            int boxHeight = bitmapSize / rows;

            for (int i = 0; i < rows + 1; i++)
            {
                gr.DrawLine(pen, 0, i * boxHeight, bitmapSize, i * boxHeight);
            }

            for (int i = 0; i < cols + 1; i++)
            {
                gr.DrawLine(pen, i * boxWidth, 0, i * boxWidth, bitmapSize);
            }
        }
    }
}