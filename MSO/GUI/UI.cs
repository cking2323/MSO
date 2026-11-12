using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MSO.GUI
{
    internal static class UI
    {

        public static void PrintLine(string s, TextBox tb) 
        {
            tb.AppendText(s + Environment.NewLine);
            //Console.WriteLine(s);
        }
        public static void Print(string s, TextBox tb) 
        {
            tb.AppendText(s);
            //Console.Write(s);
        }

        //public static string GetMode()
        //{
        //    UI.PrintLine("Do you want to execute the program (type e) or calculate all metrics (type m)");
        //    return Console.ReadLine().ToLower();
        //}
    }




}
