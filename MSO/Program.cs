using System.Collections.Generic;
using System.Reflection;
using MSO;
using MSO.GUI;

//[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("MSO.Tests")]

class Program
{
    [STAThread]
    static void Main()
    {
        Run();
    }

    static void Run()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new GUI_Form());
    }

    //private static void Run()
    //{
    //    string path = ExampleSelector.ChoosePath();

    //    FileStrategy a = FileFactory.ReturnFileStrategy(path);

    //    string mode = UI.GetMode();

    //    EntityManager entities = new EntityManager();
    //    ICommand command = a.Import();

    //    if (mode == "m")
    //    {
    //        MetricsResult metrics = MetricsAnalyzer.Analyze(command);

    //        UI.PrintLine("total commands: " + metrics.TotalCommands);
    //        UI.PrintLine("total repeats: " + metrics.TotalRepeats);
    //        UI.PrintLine("max nesting: " + metrics.MaxNesting);
    //        // get metrics and print them out
    //    }
    //    else if (mode == "e")
    //    {
    //        command.Run(entities);
    //    }
    //    else { UI.PrintLine("please enter m or e"); }
    //    Run();
    //}

}