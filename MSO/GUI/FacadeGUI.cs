using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    internal class FacadeGUI
    {
        public ICommand StartCommand;
        public string RunCommands() 
        {
            if (StartCommand != null)
            {
                EntityManager entity = new EntityManager();
                StartCommand.Run(entity);
                return entity.Player.path + "\n" + entity.Player.PosDirToString();
            }

            return "";
        }
        public string RunMetrics() 
        {
            if (StartCommand != null)
            {
                MetricsResult metrics = MetricsAnalyzer.Analyze(StartCommand);
                return "Total Commands: " + metrics.TotalCommands + "\n" +
                       "Total Repeats: " + metrics.TotalRepeats + "\n" +
                       "Max Nesting: " + metrics.MaxNesting + "\n";
            }

            return "";
        }
        public void LoadCommandFromPath(string path)
        {
            FileStrategy a = FileFactory.ReturnFileStrategy(path);
            StartCommand = a.Import();
        }


    }
}
