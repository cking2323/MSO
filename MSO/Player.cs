using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal class EntityManager
    {
        public Player Player { get; private set; }
        public EntityManager() 
        {
            Player = new Player();
        }
    }
    internal class Player
    {
        private int pos_x;
        private int pos_y;

        private int direction_x;
        private int direction_y;

        private enum Facing { right, down, left, up }
        Facing facing = Facing.right;

        public Player() 
        {
            pos_x = 0;
            pos_y = 0;

            direction_x = 1;
            direction_y = 0;
        }

        public void MoveSteps(int steps)
        {
            pos_x += direction_x * steps;
            pos_y += direction_y * steps;
        }

        public void TurnDirection (string direction)
        {
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
        }

        public string PositionToString()
        {
            return "(" + pos_x.ToString() +  ", " + pos_y.ToString() + ")";
        }

        public string DirectionToString() 
        {
            return facing.ToString();
            //return "(" + direction_x.ToString() + ", " + direction_y.ToString() + ")";
        }
    }
}
