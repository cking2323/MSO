using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSO
{
    internal interface Visitor
    {
        void Visit(MoveCommand mc);
        void Visit(TurnCommand tc);

        void Visit(CompositeCommand cc);
    }

    internal class ButtonCreationVisitor : Visitor
    {
        private int x_pos;
        private int y_pos;
        int buttonHeight; // or distance in other terms
        int indent;
        Control control;
        Action<Button> action;
        Point point;

        public ButtonCreationVisitor(Control con, Action<Button> act, Point p, int buttonHeight, int indent)
        {
            this.control = con;
            this.action = act;
            this.point = p;
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
            foreach (ICommand command in commands)
            {
                command.Accept(this);
            }
        }

        public void Visit(MoveCommand mc)
        {
            CodeBox.CreateButton(mc, point, control, action);
            point = PointMutation.LinearCommand(point, buttonHeight);
        }
        public void Visit(TurnCommand tc)
        {
            CodeBox.CreateButton(tc, point, control, action);
            point = PointMutation.LinearCommand(point, buttonHeight);
        }
        public void Visit(CompositeCommand cc)
        {

            CodeBox.CreateButton(cc, point, control, action);
            point = PointMutation.CompositeCommand(point, buttonHeight, indent);
            CreateButtonList(cc.GetCommands());
            point = PointMutation.Revert(point, buttonHeight, indent);

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



