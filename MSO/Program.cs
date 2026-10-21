using MSO;
using System.Collections.Generic;
using System.Reflection;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MSO.Tests")]

class Program
{
    static void Main()
    {
        Run();
    }
    private static void Run()
    {
        string path = ExampleSelector.ChoosePath();

        FileStrategy a = FileFactory.ReturnFileStrategy(path);

        string mode = UI.GetMode();

        EntityManager entities = new EntityManager();
        ICommand command = a.Import();

        if (mode == "m")
        {
            MetricsResult metrics = MetricsAnalyzer.Analyze(command);

            UI.PrintLine("total commands: " + metrics.TotalCommands);
            UI.PrintLine("total repeats: " + metrics.TotalRepeats);
            UI.PrintLine("max nesting: " + metrics.MaxNesting);
            // get metrics and print them out
        }
        else if (mode == "e")
        {
            command.Run(entities);
        }
        else { UI.PrintLine("please enter m or e"); }
        Run();
    }
}