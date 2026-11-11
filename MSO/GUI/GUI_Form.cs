using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    internal interface ICommandLoadTarget
    {
        void LoadCommands(ICommand command);
    }

    internal class GUI_Form : Form, ICommandLoadTarget
    {
        
        GridControl gritControl;
        CodeBoxControl codeBoxControl;
        Panel panel;
        char[][] route;


        EntityManager entities;
        ICommand commands;
        ICommand selectedCommand;

        public GUI_Form()
        {

            entities = new EntityManager();


            panel = new Panel();
            this.BackColor = Color.LightGray;
            this.ClientSize = new Size(800, 600);
            this.route = new char[][] {
                new[] { 'o', 'o', 'o', '+', '+' },
                new[] { '+', '+', 'o', '+', '+' },
                new[] { '+', '+', 'o', '+', '+' },
                new[] { '+', '+', 'o', 'o', '+' },
                new[] { '+', '+', '+', 'o', 'x' }
            };
            gritControl = new GridControl(route);
            gritControl.Location = new Point(450, 50);
            this.Controls.Add(gritControl);



            codeBoxControl = new CodeBoxControl(commands);
            codeBoxControl.Location = new Point(100, 50);
            this.Controls.Add(codeBoxControl);

            LoadProgramMenu.SelectFileButtons(this, this);

        }

        public void LoadCommands(ICommand command)
        {
            this.commands = command;
            codeBoxControl.RenderButtons(commands);
        }














    }


}
