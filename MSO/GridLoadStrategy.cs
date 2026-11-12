using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal abstract class GridLoadStrategy
    {
        protected string path;
        public abstract CellType[,] LoadGrid();

        public GridLoadStrategy(string _path) { path = _path; }
    }

    internal class GridTextFileStrategy : GridLoadStrategy
    {
        public GridTextFileStrategy(string _path) : base(_path) { }

        public override CellType[,] LoadGrid()
        {
            string[] lines = File.ReadAllLines(path);

            CellType[,] grid = this.InitializeGrid(lines);

            this.VerifyGoal(grid);

            return grid;
        }

        public CellType[,] InitializeGrid(string[] lines)
        {
            CellType[,] grid = new CellType[lines.Length, lines[0].Length]; //Initalises the grid by length of lines, and length of first line

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y].Length != lines[0].Length)
                        throw new FormatException("Line " + (y + 1).ToString() + ": should contain " + lines[0].Length.ToString() + " cells.");

                    CellType type = CellType.Open; //Initialise cells as Open

                    if (lines[y][x] == 'o') { type = CellType.Open; }
                    else if (lines[y][x] == '+') { type = CellType.Closed; }
                    else if (lines[y][x] == 'x') { type = CellType.Goal; }
                    else throw new FormatException(lines[y][x].ToString() + "is unsupported when loading grids");

                    grid[y, x] = type;
                }
            }

            return grid;
        }

        public void VerifyGoal(CellType[,] grid) 
        {
            int goalCount = 0;

            foreach (CellType cell in grid)
                if (cell == CellType.Goal)
                    goalCount++;

            if (goalCount != 1)
                throw new FormatException("Found invalid amount of goals: there should be exactly 1 goal");
        }
    }
}
