using MSO;
using System.Collections.Generic;
using System.Reflection;

abstract class FileStrategy
{
    protected string path;
    protected CompositeCommand command;
    public FileStrategy(string _path) { this.path = _path; this.command = new CompositeCommand(1); }
    public abstract ICommand Import();
}

class TextFileStrategy : FileStrategy 
{
    public TextFileStrategy(string _path) : base(_path) { }
    public override ICommand Import()
    {
        List<CompositeCommand> nestedComposites = new List<CompositeCommand>();
        
        nestedComposites.Add(this.command);

        int currentNesting = 0;

        foreach (string line in File.ReadLines(this.path))
        {
            currentNesting = line.Length - line.TrimStart('\t').Length;
            
            for (int i = 0; i < nestedComposites.Count - (currentNesting + 1); i++)
                nestedComposites.RemoveAt(nestedComposites.Count - 1);

            string[] args = line.TrimStart('\t').Split(" ");

            if (args[0] == "Move")
            {
                nestedComposites[currentNesting].AddCommand(new MoveCommand(int.Parse(args[1])));
            }

            if (args[0] == "Turn")
            {
                nestedComposites[currentNesting].AddCommand(new TurnCommand(args[1]));
            }

            if (args[0] == "Repeat")
            {
                CompositeCommand repeat = new CompositeCommand(int.Parse(args[1]));
                nestedComposites.Add(repeat);
                nestedComposites[currentNesting].AddCommand(repeat);
            }
        }

        return command;
    }
}

class FileFactory
{
    public static FileStrategy ReturnFileStrategy(string path)
    {
        if (Path.GetExtension(path) == ".txt")
            return new TextFileStrategy(path);

        throw new Exception("FileFactory: Path fout");
    }
}

class Program
{
    static void Main()
    {
        Run();
    }
    private static void Run()
    {
        string path = UI.ChoosePath();

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