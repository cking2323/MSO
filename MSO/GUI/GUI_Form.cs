using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = MSO.ICommand;

namespace MSO.GUI
{
    internal interface ICommandLoadTarget
    {
        void LoadCommands(string path);
    }

    internal class GUI_Form : Form, ICommandLoadTarget
    {
        
        GridControl gridControl;
        CodeBoxControl codeBoxControl;
        OutputControl outputControl;
        EditCommandPanel acp;
        char[][] route;

        ICommand command;
        FacadeGUI facadeGUI;

        public GUI_Form()
        {
            facadeGUI = new FacadeGUI();

            this.acp = new EditCommandPanel(this, facadeGUI);
            this.acp.Location = new Point(5, 50);
            this.acp.Size = new Size(200, 600);
            this.Controls.Add(acp);

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

            codeBoxControl = new CodeBoxControl(facadeGUI);
            codeBoxControl.Location = new Point(210, 50);
            this.Controls.Add(codeBoxControl);

            // Sets outputControl below codeBoxControl
            outputControl = new OutputControl(facadeGUI);
            outputControl.Location = new Point(codeBoxControl.Location.X, codeBoxControl.Bottom + 5);
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
            string result = facadeGUI.RunCommands();
            outputControl.SetText(result);
        }

        private void MetricsProgram(object sender, EventArgs ea)
        {
            string result = facadeGUI.RunMetrics();
            outputControl.SetText(result);
        }


        public void LoadCommands(string path)
        {
            facadeGUI.LoadCommandFromPath(path);
            codeBoxControl.RenderButtons(facadeGUI.StartCommand);
        }


    }


}
