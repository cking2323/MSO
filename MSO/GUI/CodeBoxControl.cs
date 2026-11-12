using MSO.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = MSO.ICommand;

namespace MSO
{


    public class CodeBoxControl : UserControl
    {
        FacadeGUI facadeGUI;

        private Point mouseStartPosition;
        private Point buttonStartPosition;
        private Point cursorPosition;
        private Point offSet;
        private bool mouseDown = false;
        private bool draggingButton = false;
        private Panel panelH;
        private Panel panelV;

        ICommand commands;
        ICommand selectCommand;
        Button selectButton;
        List<(Button, Point)> selectButtons;
        int[] nestingArray;
        Point dropPoint;


        ButtonCreation bcv;

        internal CodeBoxControl(FacadeGUI facadeGUI)
        {
            this.facadeGUI = facadeGUI;
            this.Size = new Size(350, 450);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.selectButtons = new List<(Button, Point)>();
            this.dropPoint = new Point(0,0);
            this.panelH = new Panel();
            this.panelV = new Panel();


            panelH.Height = 1;
            panelH.Width = this.Width;
            panelH.Visible = false;
            panelH.BackColor = Color.BlueViolet;
            this.Controls.Add(panelH);

            panelV.Height = this.Height;
            panelV.Width = 1;
            panelV.Visible = false;
            panelV.BackColor = Color.BlueViolet;
            this.Controls.Add(panelV);
        }

        internal void SelectiveClearButtons() //clears all buttons except those being dragged
        {
            for(int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control control = this.Controls[i];
                if (control == panelH ) continue;
                if (control == panelV ) continue;
                if (control is Button button && selectButtons.Any(x => x.Item1 == button))
                {
                    continue;
                }
                this.Controls.RemoveAt(i);
                control.Dispose();
            }
        }
        internal void ClearSelectedButtons()
        {
            foreach ((Button b, Point p) in selectButtons)
            {
                this.Controls.Remove(b);
            }
            selectButtons.Clear();
        }

        internal void RenderButtons(ICommand commands)
        {
            SelectiveClearButtons();
            // ICommand visitor
            this.bcv = new ButtonCreation(this, AddEventhandlers, new Point(0, 0), 30, 30);
            bcv.CreateButtons(commands);
            this.commands = facadeGUI.StartCommand;
            this.nestingArray = HelperFunctions.GetNestings(commands);
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
            selectButton = clickedButton;

            if (selectButton != null)
            {
                selectButtons.Add((selectButton, new Point(0, 0)));
                selectCommand = selectButton.Tag as ICommand;

                selectButton.BringToFront();

                if (selectCommand is CompositeCommand)
                {
                    List<Button> buttons = new List<Button>();

                    buttons = HelperFunctions.GetChildButtons(this, selectCommand as CompositeCommand);

                    foreach (Button button in buttons)
                    {
                        selectButtons.Add((button, new Point(button.Location.X - selectButton.Location.X, button.Location.Y - selectButton.Location.Y)));
                    }

                }

            }

            mouseStartPosition = selectButton.Parent.PointToClient(Cursor.Position);
            buttonStartPosition = selectButton.Location;
            mouseDown = true;
        }
        private void ButtonMouseDrag(object sender, MouseEventArgs mea)
        {


            if (mouseDown)
            {
                //bcv.CreateButtons(commands);
                //ICommands.remove(sender.

                Point p = selectButton.Parent.PointToClient(Cursor.Position);
                cursorPosition = p;

                dropPoint = HelperFunctions.GetDropPoint(cursorPosition, nestingArray, 30, 30);

                if (selectButton != null)
                {

                    if (selectCommand != null)
                    {
                        nestingArray = HelperFunctions.GetNestings(commands);
                        
                        if (draggingButton == false)
                        {
                            HelperFunctions.RemoveCommand(commands, selectCommand);
                            RenderButtons(commands);
                            draggingButton = true;
                        }

                    }

                    if (draggingButton == true)
                    {
                        panelH.Visible = true;
                        panelH.Location = new Point(0, dropPoint.Y);
                        panelV.Visible = true;
                        panelV.Location = new Point(dropPoint.X, 0);
                    }

                    Point offSet = new Point(mouseStartPosition.X - buttonStartPosition.X, mouseStartPosition.Y - buttonStartPosition.Y);

                    int boundXpos = Math.Max(Math.Min(p.X - offSet.X, selectButton.Parent.Width - selectButton.Width), 0);
                    int boundYpos = Math.Max(Math.Min(p.Y - offSet.Y, selectButton.Parent.Height - selectButton.Height), 0);
                    
                    Point reference = new Point(boundXpos, boundYpos);
                    selectButton.Location = reference;
                    
                    if (selectCommand is CompositeCommand)
                    {
                        foreach((Button b,Point po) button in selectButtons)
                        {
                            button.b.Location = new Point(reference.X + button.po.X, reference.Y + button.po.Y);
                        }
                    }
                }


            }
        }
        private void ButtonMouseUp(object sender, MouseEventArgs mea)
        {
            mouseDown = false;
            draggingButton = false;
            panelH.Visible = false;
            panelV.Visible = false;
            ClearSelectedButtons();
            this.Controls.Remove(selectButton);
        }



    }





    //Creeert alle command buttons in het codeer vak
    public static class CodeBox 
    {
        static internal void CreateButton(ICommand command, Point p, Control con, Action<Button> AddEventHandlers) //voeg functie voor het creeren van eventhandlers in de constructor toe
        {
            Button but = new Button();
            but.Size = new Size(200, 30);

            but.Tag = command;

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
