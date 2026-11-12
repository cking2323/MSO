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
        private CellType[,] cells; 
        public Grid(int rows, int cols) 
        {
            cells = new CellType[rows, cols];
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
