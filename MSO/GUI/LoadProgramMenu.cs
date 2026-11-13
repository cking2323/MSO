using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    // Drop down button to load a program example or from file
    internal static class LoadProgramMenu
    {
        internal static void SelectFileButtons(Control host, ICommandLoadTarget target)
        {
            ToolStrip ts = new ToolStrip();
            ts.Location = new Point(20, 20);
            ToolStripDropDownButton programButton = new ToolStripDropDownButton();
            ToolStripMenuItem basic = new ToolStripMenuItem();
            ToolStripMenuItem advanced = new ToolStripMenuItem();
            ToolStripMenuItem expert = new ToolStripMenuItem();
            ToolStripMenuItem chooseFile = new ToolStripMenuItem();

            programButton.Text = "load program";
            programButton.Size = new Size(40, 30);

            basic.Text = "Basic";
            advanced.Text = "Advanced";
            expert.Text = "Expert";
            chooseFile.Text = "Open file";

            programButton.DropDownItems.Add(basic);
            programButton.DropDownItems.Add(advanced);
            programButton.DropDownItems.Add(expert);
            programButton.DropDownItems.Add(new ToolStripSeparator());
            programButton.DropDownItems.Add(chooseFile);


            ts.Items.Add(programButton);
            host.Controls.Add(ts);

            AddFileEvents(programButton, target);
        }

        private static void AddFileEvents(ToolStripDropDownButton programButton, ICommandLoadTarget target)
        {
            ToolStripMenuItem basic = programButton.DropDownItems[0] as ToolStripMenuItem;
            ToolStripMenuItem advanced = programButton.DropDownItems[1] as ToolStripMenuItem;
            ToolStripMenuItem expert = programButton.DropDownItems[2] as ToolStripMenuItem;
            ToolStripMenuItem chooseFile = programButton.DropDownItems[4] as ToolStripMenuItem;

            basic.Click += ClickBasic;
            advanced.MouseDown += ClickAdvanced;
            expert.MouseDown += ClickExpert;
            chooseFile.MouseDown += ClickFile;


            void ClickBasic(object sender, EventArgs ea)
            {
                PickExample("basic");
            }
            void ClickAdvanced(object sender, EventArgs ea)
            {
                PickExample("advanced");
            }
            void ClickExpert(object sender, EventArgs ea)
            {
                PickExample("expert");
            }
            void ClickFile(object sender, EventArgs ea)
            {
                PickFile();
            }



            void PickExample(string mode) // choose example program
            {
                string path = ExampleSelector.ChoosePath(mode);
                target.LoadCommands(path);
            }

            void PickFile() // open file in dialog
            {
                try
                {
                    programButton.HideDropDown();

                    string path = null;
                    OpenFileDialog openFileD = new OpenFileDialog();
                    openFileD.DefaultExt = "txt";
                    openFileD.Title = "Code file...";
                    openFileD.Filter = "text file(*.txt)|*.txt";

                    openFileD.CheckFileExists = true;
                    openFileD.CheckPathExists = true;
                    openFileD.RestoreDirectory = true;

                    if (openFileD.ShowDialog() == DialogResult.OK)
                    {
                        path = openFileD.FileName;
                        FileStrategy a = FileFactory.ReturnFileStrategy(path);
                        target.LoadCommands(path);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show($"Something went wrong\n{ex.Message}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
