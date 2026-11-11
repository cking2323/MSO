using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{


    public class CodeBoxControl : UserControl
    {
        private Point mouseStartPosition;
        private Point buttonStartPosition;
        private Point cursorPosition;
        private Point offSet;
        private bool mouseDown = false;
        Button currentButton;

        ICommand commands;
        ICommand currentCommand;

        List<Button> cmButtons;
        ButtonCreationVisitor bcv;

        internal CodeBoxControl(ICommand commands)
        {
            //codeBox = new CodeBox();
            this.Size = new Size(350, 450);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
        }

        internal void ClearButtons()
        {
            this.Controls.Clear();
        }
        internal void RenderButtons(ICommand commands)
        {
            ClearButtons();
            // ICommand visitor
            this.bcv = new ButtonCreationVisitor(this, AddEventhandlers, new Point(0, 0), 30, 30);
            bcv.CreateButtons(commands);
        }



        private void AddEventhandlers(Button b)
        {
            b.MouseDown += ButtonMouseDown;
            b.MouseMove += ButtonMouseDrag;
            b.MouseUp += ButtonMouseUp;
        }
        private void RemoveCommand()
        {

        }
        private void AddCommand()
        {

        }
        private void AddButtonBackToList(List<Button> buttons, Button b, int position)
        {

        }
        private void ButtonMouseDown(object sender, MouseEventArgs mea)
        {
            Button clickedButton = sender as Button;
            currentButton = clickedButton;

            if (currentButton != null)
            {
                currentButton.BringToFront();
            }
            mouseStartPosition = currentButton.Parent.PointToClient(Cursor.Position);
            buttonStartPosition = currentButton.Location;
            mouseDown = true;
        }
        private void ButtonMouseDrag(object sender, MouseEventArgs mea)
        {
            if (mouseDown)
            {
                
                //bcv.CreateButtons(commands);

                //ICommands.remove(sender.
                if (currentButton != null)
                {
                    Point p = currentButton.Parent.PointToClient(Cursor.Position);
                    cursorPosition = p;
                    Point offSet = new Point(mouseStartPosition.X - buttonStartPosition.X, mouseStartPosition.Y - buttonStartPosition.Y);
                    //currentButton.Location = new Point(p.X - offSet.X, p.Y - offSet.Y);
                    int boundXpos = Math.Max(Math.Min(p.X - offSet.X, currentButton.Parent.Width - currentButton.Width), 0);
                    int boundYpos = Math.Max(Math.Min(p.Y - offSet.Y, currentButton.Parent.Height - currentButton.Height), 0);
                    currentButton.Location = new Point(boundXpos, boundYpos);
                }


            }
        }
        private void ButtonMouseUp(object sender, MouseEventArgs mea)
        {
            mouseDown = false;
            //currentButton.Location = new Point(buttonStartPosition.X + (cursorPosition.X - mouseStartPosition.X), buttonStartPosition.Y + (cursorPosition.Y - mouseStartPosition.Y));
        }



    }





    //Creeert alle command buttons in het codeer vak
    public static class CodeBox 
    {
        static internal void CreateButton(ICommand command, Point p, Control con, Action<Button> AddEventHandlers) //voeg functie voor het creeren van eventhandlers in de constructor toe
        {
            Button but = new Button();
            but.Size = new Size(200, 30);

            switch (command) //kleur op basis van command type
            {
                case CompositeCommand:
                    but.BackColor = Color.FromArgb(150, 250, 125, 0);
                    break;
                case MoveCommand:
                    but.BackColor = Color.FromArgb(200, 30, 141, 252);
                    break;
                case TurnCommand:
                    but.BackColor = Color.FromArgb(200, 47, 161, 109);
                    break;

            }

            but.FlatStyle = FlatStyle.Flat;
            but.Font = new Font("verdana", 12f, FontStyle.Bold);
            but.ForeColor = Color.White;
            but.Text = command.ToString();
            but.Location = p;

            con.Controls.Add(but);
            AddEventHandlers(but);
        }
    }

}
