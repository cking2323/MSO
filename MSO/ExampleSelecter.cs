using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO 
{
    public class ExampleSelector
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly List<(string Name, string Path)> PredefinedExamples = new List<(string, string)>
        {
            ("Basic Example", Path.Combine(BaseDirectory,@"..\..\..\examples\basic.txt")),
            ("Advanced Example", Path.Combine(BaseDirectory,@"..\..\..\examples\advanced.txt")),
            ("Expert Example", Path.Combine(BaseDirectory, @"..\..\..\examples\expert.txt"))
        };
        public static string ChoosePath()
        {
            UI.PrintLine($"Select a program");
            for (int i = 0; i < PredefinedExamples.Count; i++)
            {
                UI.PrintLine($"{i + 1}. {PredefinedExamples[i].Name}");
            }
            UI.PrintLine($"{PredefinedExamples.Count + 1}. Import from text file");

            int choice = Int32.Parse(Console.ReadLine());
            if (choice <= PredefinedExamples.Count && PredefinedExamples.Count > 0)
            {
                return PredefinedExamples[choice - 1].Path;
            }
            if (choice == PredefinedExamples.Count + 1)
            {
                UI.PrintLine("Enter the full path of the program");
                string path = Console.ReadLine();
                return path;
            }
            else
            {
                UI.PrintLine($"Choose a path between from 1-{PredefinedExamples.Count + 1}");
                ChoosePath();
            }
            return "no path L bozo";
        }
    }
}

