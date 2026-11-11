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
        
        GridControl gridControl;
        CodeBoxControl codeBoxControl;
        OutputControl outputControl;
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
            this.ClientSize = new Size(1000, 700);
            this.route = new char[][] {
                new[] { 'o', 'o', 'o', '+', '+' },
                new[] { '+', '+', 'o', '+', '+' },
                new[] { '+', '+', 'o', '+', '+' },
                new[] { '+', '+', 'o', 'o', '+' },
                new[] { '+', '+', '+', 'o', 'x' }
            };

            LoadProgramMenu.SelectFileButtons(this, this);

            gridControl = new GridControl(route);
            gridControl.Location = new Point(650, 50);
            this.Controls.Add(gridControl);

            codeBoxControl = new CodeBoxControl(commands);
            codeBoxControl.Location = new Point(200, 50);
            this.Controls.Add(codeBoxControl);

            // Sets outputControl below codeBoxControl
            outputControl = new OutputControl();
            outputControl.Location = new Point(codeBoxControl.Location.X, codeBoxControl.Bottom + 5);
           // outputControl.Location = new Point(codeBoxControl.Location.X, codeBoxControl.Location.Y + codeBoxControl.Height + 20);
            outputControl.Height = this.Height - codeBoxControl.Height - 20;
            outputControl.Width = codeBoxControl.Width;
            this.Controls.Add(outputControl);

            Button runButton = outputControl.flowLayoutPanel.Controls["run"] as Button;
            Button metricsButton = outputControl.flowLayoutPanel.Controls["met"] as Button;
            runButton.Click += RunProgram;
            metricsButton.Click += MetricsProgram;



        }

        // Run the program or run Metrics
        private void RunProgram(object sender, EventArgs ea)
        {
            if (commands != null)
            {
                outputControl.RunEvent(commands, entities);
            }
            else { return; }

        }

        private void MetricsProgram(object sender, EventArgs ea)
        {
            if (commands != null)
            {
                outputControl.MetricsEvent(commands);
            }
            else { return; }
        }


        public void LoadCommands(ICommand command)
        {
            this.commands = command;
            codeBoxControl.RenderButtons(commands);
        }














    }


}
