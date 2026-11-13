using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    public enum CellType
    {
        Open,
        Closed,
        Goal
    }
    internal class Grid
    {
        public CellType[,] cells; 
        public Grid(int rows, int cols) 
        {
            cells = new CellType[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    cells[i, j] = CellType.Open;
                }
            }
        }

        public void LoadGrid(GridLoadStrategy strategy)
        {
            cells = strategy.LoadGrid();
        }

        public CellType GetCell(int x, int y) 
        {
            return cells[cells.GetLength(0) - 1 - y, x]; //convert matrix axis so [0,0] is leftBottom
        }

        public bool CheckInBounds(int x, int y)
        {
            return x >= 0 && x < cells.GetLength(0) &&
                   y >= 0 && y < cells.GetLength(1);
        }
    }
}
