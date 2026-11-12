using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    internal class EditCommandPanel : Panel
    {
        TableLayoutPanel tlp;
        FacadeGUI facadeGUI;
        public EditCommandPanel(Control control, FacadeGUI facadeGUI)
        {
            
            this.facadeGUI = facadeGUI;
            //this.Dock = DockStyle.Left;
            tlp = new TableLayoutPanel();
            tlp.ColumnCount = 1;
            tlp.RowCount = 5;
            tlp.Dock = DockStyle.Left;

            Button addCommand = new Button();
            Button chooseCommand = new Button();
            Button removeCommand = new Button();
            Button direction = new Button();
            ContextMenuStrip menuStrip = new ContextMenuStrip();
            ContextMenuStrip facing = new ContextMenuStrip();
            TextBox textBox = new TextBox();
            textBox.TextAlign = HorizontalAlignment.Center;
            addCommand.Height = 35;
            removeCommand.Height = 35;

            menuStrip.Items.Add("Composite Command", null, SetComposite);
            menuStrip.Items.Add("Move Command", null, SetMove);
            menuStrip.Items.Add("Turn Command", null, SetTurn);
            menuStrip.Items.Add("Conditional Command", null, SetConditional);

            facing.Items.Add("North", null, SetNorth);
            facing.Items.Add("East", null, SetEast);
            facing.Items.Add("South", null, SetSouth);
            facing.Items.Add("West", null, SetWest);

            addCommand.Text = "Add Command";
            direction.Text = "Direction";
            chooseCommand.Text = "Choose Command";
            removeCommand.Text = "Remove Command";
            addCommand.AutoSize = true;
            chooseCommand.AutoSize = true;
            removeCommand.AutoSize = true;
            direction.AutoSize = true;
            addCommand.BackColor = Color.White;
            direction.BackColor = Color.White;
            chooseCommand.BackColor = Color.White;
            removeCommand.BackColor = Color.White;

            chooseCommand.Click += ShowCommandMenu;
            direction.Click += ShowDirectionMenu;

            chooseCommand.Dock = DockStyle.Top;
            textBox.Dock = DockStyle.Top;
            direction.Dock = DockStyle.Top;
            removeCommand.Dock = DockStyle.Top;
            addCommand.Dock = DockStyle.Top;

            tlp.Controls.Add(chooseCommand, 0, 0);
            tlp.Controls.Add(textBox, 0, 1);
            tlp.Controls.Add(direction, 0, 2);
            tlp.Controls.Add(removeCommand, 0, 3);
            tlp.Controls.Add(addCommand, 0, 4);
            this.Controls.Add(tlp);


            void ShowCommandMenu(object sender, EventArgs e)
            {
                menuStrip.Show(chooseCommand, new Point(0, chooseCommand.Height));

            }
            void ShowDirectionMenu(object sender, EventArgs e)
            {
                facing.Show(direction, new Point(0, direction.Height));
            }


            void SetComposite(object sender, EventArgs e)
            {
                chooseCommand.Text = "Composite";
            }
            void SetMove(object sender, EventArgs e)
            {
                chooseCommand.Text = "Move";
            }
            void SetTurn(object sender, EventArgs e)
            {
                chooseCommand.Text = "Turn";
            }
            void SetConditional(object sender, EventArgs e)
            {
                chooseCommand.Text = "Conditional";
            }

            void SetNorth(object sender, EventArgs e)
            {
                direction.Text = "North";
            }
            void SetEast(object sender, EventArgs e)
            {
                direction.Text = "East";
            }
            void SetSouth(object sender, EventArgs e)
            {
                direction.Text = "South";
            }
            void SetWest(object sender, EventArgs e)
            {
                direction.Text = "West";
            }


        }








    }
}
