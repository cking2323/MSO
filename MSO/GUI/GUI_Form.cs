using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Panel panel;
        char[][] route;

        FacadeGUI FacadeGUI;

        public GUI_Form()
        {
            FacadeGUI = new FacadeGUI();

            panel = new Panel();
            this.BackColor = Color.LightGray;
            this.ClientSize = new Size(1000, 700);

            LoadProgramMenu.SelectFileButtons(this, this);

            gridControl = new GridControl();
            gridControl.Location = new Point(650, 50);
            this.Controls.Add(gridControl);

            codeBoxControl = new CodeBoxControl();
            codeBoxControl.Location = new Point(200, 50);
            this.Controls.Add(codeBoxControl);

            // Sets outputControl below codeBoxControl
            outputControl = new OutputControl(FacadeGUI);
            outputControl.Location = new Point(codeBoxControl.Location.X, codeBoxControl.Bottom + 5);

            // outputControl.Location = new Point(codeBoxControl.Location.X, codeBoxControl.Location.Y + codeBoxControl.Height + 20);
            outputControl.Height = this.Height - codeBoxControl.Height - 20;
            outputControl.Width = codeBoxControl.Width;
            this.Controls.Add(outputControl);

            Button loadGridButton = new Button();
            loadGridButton.Text = "Load Grid";
            loadGridButton.Location = new Point(650, 460);
            loadGridButton.Click += LoadGrid;
            this.Controls.Add(loadGridButton);

            Button runButton = outputControl.flowLayoutPanel.Controls["run"] as Button;
            Button metricsButton = outputControl.flowLayoutPanel.Controls["met"] as Button;
            runButton.Click += RunProgram;
            metricsButton.Click += MetricsProgram;

            int[] PlayerPos = FacadeGUI.GetPlayerPos();
            gridControl.LoadGrid(FacadeGUI.GetGrid(), PlayerPos[0], PlayerPos[1]);
        }

        // Run the program or run Metrics
        private void RunProgram(object sender, EventArgs ea)
        {
            string text = codeBoxControl.GetText();
            string path = @"..\..\output.txt";

            File.WriteAllText(path, text); //create a temporary file
            string fullPath = Path.GetFullPath(path);

            FacadeGUI.LoadCommandFromPath(path);
            string result = FacadeGUI.RunCommands();
            outputControl.SetText(result);

            int[] PlayerPos = FacadeGUI.GetPlayerPos();
            gridControl.LoadGrid(FacadeGUI.GetGrid(), PlayerPos[0], PlayerPos[1]);
        }

        private void MetricsProgram(object sender, EventArgs ea)
        {

            string result = FacadeGUI.RunMetrics();
            outputControl.SetText(result);
        }

        private void LoadGrid(object sender, EventArgs ea)
        {
            string path = PathFileWindow();
            FacadeGUI.LoadGridFromPath(path);
            int[] PlayerPos = FacadeGUI.GetPlayerPos();
            gridControl.LoadGrid(FacadeGUI.GetGrid(), PlayerPos[0], PlayerPos[1]);
        }
        public void LoadCommands(string path)
        {

            string text = FacadeGUI.LoadCommandFromPath(path);
            codeBoxControl.SetText(text);
        }

        private string PathFileWindow()
        {
            string path = null;

            try
            {
                OpenFileDialog openFileD = new OpenFileDialog();
                openFileD.DefaultExt = "txt";
                openFileD.Title = "Code file...";
                openFileD.Filter = "text file(*.txt)|*.txt";

                openFileD.CheckFileExists = true;
                openFileD.CheckPathExists = true;
                openFileD.RestoreDirectory = true;

                if (openFileD.ShowDialog() == DialogResult.OK)
                    path = openFileD.FileName;
            }
            catch (Exception ex)
            { MessageBox.Show($"Something went wrong\n{ex.Message}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            return path;
        }
    }
}
