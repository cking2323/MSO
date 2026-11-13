using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal class EntityManager
    {
        public int size = 7;
        public Player Player { get; private set; }
        public Grid Grid { get; private set; }
        public EntityManager() 
        {
            Grid = new Grid(size, size); //default size
            Player = new Player();
        }
    }
    internal class Player
    {
        private int pos_x;
        private int pos_y;

        public bool wallAhead;
        public bool gridBorderAhead;

        private int direction_x;
        private int direction_y;

        public bool repeatMode;

        public string path;

        private enum Facing { right, down, left, up }
        Facing facing = Facing.right;

        public Player() 
        {
            pos_x = 0;
            pos_y = 0;

            direction_x = 1;
            direction_y = 0;

            path = "";

            repeatMode = false;
        }

        public void PathClear()
        {
            path = "";
        }

        public void ResetPosition()
        {
            pos_x = 0;
            pos_y = 0;
        }

        public void MoveSteps(int steps, Grid Grid)
        {
            path += "Player went from " + PositionToString();

            for (int i = 0; i < steps; i++)
            {
                if (CheckMoveStep(Grid))
                {
                    pos_x += direction_x;
                    pos_y += direction_y;
                }
                else
                {
                    break;
                }
            }

            path += " to " + PositionToString() + ".\n";
        }

        public bool CheckMoveStep(Grid grid)
        {
            int nextX = pos_x + direction_x;
            int nextY = pos_y + direction_y;

            if (!grid.CheckInBounds(nextX, nextY))
            {
                if (repeatMode == false)
                    throw new Exception("Move illegal: Out of bounds");

                gridBorderAhead = true;
                return false;
            }
                
            if (! (grid.GetCell(nextX, nextY) != CellType.Closed) )
            {
                if (repeatMode == false)
                    throw new Exception("Move illegal: blocked cell");

                wallAhead = true;
                return false;
            }

            return true;
        }

        public void TurnDirection (string direction)
        {
            path += "Player faced from " + DirectionToString();

            if (direction == "left")
            {
                int newX = -direction_y;
                int newY = direction_x;

                direction_x = newX;
                direction_y = newY;
                facing = (Facing)(((int)facing + 3) % 4);
            }

            if (direction == "right")
            {
                int newX = direction_y;
                int newY = -direction_x;

                direction_x = newX;
                direction_y = newY;
                facing = (Facing)(((int)facing + 1) % 4);
            }

            path += " to " + DirectionToString() + ".\n";
        }

        public void ClearPath() { path = ""; }

        public int[] GetPosition()
        {
            return [this.pos_x, this.pos_y];
        }

        public string PositionToString()
        {
            return "(" + pos_x.ToString() +  ", " + pos_y.ToString() + ")";
        }

        public string DirectionToString() 
        {
            return facing.ToString();
        }

        public string PosDirToString()
        {
            return "Player ends at position: " + PositionToString() + " facing " + DirectionToString();
        }
    }
}
