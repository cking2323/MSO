using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSO.GUI
{
    internal class ButtonCreation
    {
        private int x_pos;
        private int y_pos;
        int buttonHeight; // or distance in other terms
        int indent;
        Control control;
        Action<Button> action;
        Point point;

        public ButtonCreation(Control con, Action<Button> act, Point p, int buttonHeight, int indent)
        {
            control = con;
            action = act;
            point = p;
            this.buttonHeight = buttonHeight;
            this.indent = indent;
        }


        public void CreateButtons(ICommand commands) // filter the first composite commands that holds the entire program
        {
            if (commands is CompositeCommand composite)
            {
                CreateButtonList(composite.GetCommands());
            }

            else { return; }
        }


        public void CreateButtonList(List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                if (command is CompositeCommand cc)
                {
                    CodeBox.CreateButton(cc, point, control, action);
                    point = PointMutation.CompositeCommand(point, buttonHeight, indent);
                    CreateButtonList(cc.GetCommands());
                    point = PointMutation.Revert(point, buttonHeight, indent);
                }

                else
                {
                    CodeBox.CreateButton(command, point, control, action);
                    point = PointMutation.LinearCommand(point, buttonHeight);
                }
            }
        }
    }

    public static class PointMutation
    {
        public static Point LinearCommand(Point p, int height) //Linear is anything that is not composite
        {
            return new Point(p.X, p.Y + height);
        }
        public static Point CompositeCommand(Point p, int height, int indent)
        {
            return new Point(p.X + indent, p.Y + height);
        }
        public static Point Revert(Point p, int height, int indent)
        {
            return new Point(p.X - indent, p.Y);
        }

    }

}



