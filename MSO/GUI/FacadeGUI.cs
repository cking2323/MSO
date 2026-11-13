using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MSO.GUI
{
    internal class FacadeGUI
    {
        public ICommand StartCommand;

        EntityManager entity = new EntityManager();
        MetricsAnalyzer metrics = new MetricsAnalyzer();
        public string RunCommands() 
        {
            if (StartCommand != null)
            {
                entity.Player.ClearPath();
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
        public string LoadCommandFromPath(string path)
        {
            FileStrategy a = FileFactory.ReturnFileStrategy(path);
            StartCommand = a.Import();

            return File.ReadAllText(path);
        }

        public void LoadGridFromPath(string path)
        {
            GridTextFileStrategy strat = new GridTextFileStrategy(path);
            entity.Player.ResetPosition();
            entity.Grid.LoadGrid(strat);
        }
        public CellType[,] GetGrid()
        {
            return entity.Grid.cells;
        }

        public int[] GetPlayerPos()
        {
            return entity.Player.GetPosition();
        }
    }
}
