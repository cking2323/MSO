using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    internal class GUI_Form : Form
    {
        
        GridControl gritControl;
        CodeBoxControl codeBoxControl;
        Panel panel;
        char[][] route;
        List<string> commands = new List<string>();
        ICommand selectedCommand;

        public GUI_Form()
        {
            string path = ExampleSelector.ChoosePath();

            FileStrategy a = FileFactory.ReturnFileStrategy(path);

            string mode = UI.GetMode();

            EntityManager entities = new EntityManager();
            ICommand command = a.Import();


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

        }
    }
}
