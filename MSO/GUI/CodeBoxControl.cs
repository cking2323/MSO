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

        List<ICommand> commands;
        ICommand currentCommand;

        Button firstButton;
        Button secondButton;

        List<Button> cmButtons;
        ButtonCreationVisitor bcv;


        public CodeBoxControl(List<string> strings)
        {
            //codeBox = new CodeBox();
            this.Size = new Size(300, 500);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // ICommand visitor
            this.bcv = new ButtonCreationVisitor(this, AddEventhandlers, new Point(0, 0), 30, 30);
            bcv.CreateButtons(commands);


            //this.firstButton = codeBox.MakeButton("Move 10", new Point(10, 10), this);
            //this.secondButton = codeBox.MakeButton("Left 5", new Point(100, 10), this);
            //this.firstButton.Visible = false;
            //this.secondButton.Visible = false;
            this.cmButtons = new List<Button>();
            cmButtons.Add(this.firstButton);
            cmButtons.Add(this.secondButton);
            CreateCommandButtons(cmButtons);
        }



        //creert de buttonLijst zonder te tonen
        private void CreateCommandButtons(List<Button> bl) // dit moet zijn met commands als input en dan wordt de make button van codebox aangeroepen 
        {
            int x_pos = 0;
            int y_pos = 0;

            foreach (Button button in bl)
            {
                AddEventhandlers(button);
                button.Location = new Point(x_pos, y_pos);
                button.Visible = true;
                y_pos += button.Height;
            }
        }
        // toont de button lijst in de window 
        private void RenderButtons(List<Button> bl)
        {
            int x_pos = 0;
            int y_pos = 0;

            foreach (Button button in bl)
            {
                button.Location = new Point(x_pos, y_pos);
                button.Visible = true;
                y_pos += button.Height;
            }
 
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
            mouseStartPosition = currentButton.Parent.PointToClient(Cursor.Position);
            buttonStartPosition = currentButton.Location;
            mouseDown = true;
        }
        private void ButtonMouseDrag(object sender, MouseEventArgs mea)
        {
            if (mouseDown)
            {
                cmButtons.Remove(currentButton);
                RenderButtons(cmButtons);

                //ICommands.remove(sender.

                Point p = currentButton.Parent.PointToClient(Cursor.Position);
                cursorPosition = p;
                Point offSet = new Point(mouseStartPosition.X - buttonStartPosition.X, mouseStartPosition.Y - buttonStartPosition.Y);
                //currentButton.Location = new Point(p.X - offSet.X, p.Y - offSet.Y);
                int boundXpos = Math.Max(Math.Min(p.X - offSet.X, currentButton.Parent.Width - currentButton.Width), 0);
                int boundYpos = Math.Max(Math.Min(p.Y - offSet.Y, currentButton.Parent.Height - currentButton.Height), 0);
                currentButton.Location = new Point(boundXpos, boundYpos);

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
            but.Size = new Size(200, 30); but.BackColor = Color.LightSeaGreen; //kleur op basis van command type
            but.Text = command.ToString();
            but.Location = p;
            con.Controls.Add(but);
            AddEventHandlers(but);
        }
    }

}
