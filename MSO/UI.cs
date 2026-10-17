using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal static class UI
    {
        public static void PrintLine(string s) {
            Console.WriteLine(s);
        }
        public static void Print(string s) { Console.Write(s); }

        public static string GetMode()
        {
            UI.PrintLine("Do you want to execute the program (type e) or calculate all metrics (type m)");
            return Console.ReadLine().ToLower();
        }


        private static readonly List<(string Name, string Path)> PredefinedExamples = new List<(string, string)>
        {
            ("Basic Example", @"C:\Users\laptop\Documents\GitHub\MSO\MSO\examples\basic.txt"),
            ("Advanced Example", @"C:\Users\laptop\Documents\GitHub\MSO\MSO\examples\advanced.txt"),
            ("Expert Example", @"C:\Users\laptop\Documents\GitHub\MSO\MSO\examples\expert.txt")
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
            else { 
                UI.PrintLine("Choose a real path");
                ChoosePath();
            }
            return "no path L bozo";
        }
    }
}
