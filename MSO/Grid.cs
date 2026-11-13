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
            return cells[x, y];
        }
    }
}
