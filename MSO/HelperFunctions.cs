using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal static class HelperFunctions
    {

        internal static int[] GetNestings(ICommand command)
        {
            
            MetricsResult ms = MetricsAnalyzer.Analyze(command);

            CompositeCommand rootCommand = command as CompositeCommand;

            int[] nestingArray = new int[ms.TotalCommands];
            int currentDepth = 0;
            int currentCommandNumber = 0;


            Traverse(rootCommand.GetCommands());

            void Traverse(List<ICommand> commands)
            {
                foreach (ICommand command in commands)
                {
                    nestingArray[currentCommandNumber] = currentDepth;
                    currentCommandNumber++;

                    if (command is CompositeCommand)
                    {
                        CompositeCommand cc = command as CompositeCommand;
                        currentDepth++;
                        Traverse(cc.GetCommands());
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            return nestingArray;
        }


        public static List<Button> GetChildButtons(Control con, CompositeCommand command)
        {
            List<Button> childButtons = new List<Button>();

            foreach (ICommand child in command.GetCommands())
            {
                Button button = con.GetButton(child);
                childButtons.Add(button);

                if (child is CompositeCommand)
                {
                    CompositeCommand cc = child as CompositeCommand;
                    childButtons.AddRange(GetChildButtons(con, cc));
                }
            }
            return childButtons;

        }

        public static Point GetDropPoint(Point point, int[] nestingArray, int buttonHeight, int indent)
        {
            int index = Math.Max((int)Math.Round((double)point.Y / buttonHeight), 0);
            int nestingIndex = Math.Max((int)Math.Round((double)point.X / indent), 0); 
            int pos_y;
            int pos_x;
            int maxNesting;

            if (nestingArray == null || nestingArray.Length == 0) return new Point(0, 0);

            if (index >= nestingArray.Length)
            {
                pos_y = nestingArray.Length * buttonHeight;
                maxNesting = nestingArray[nestingArray.Length - 1];
            }
            else
            {
                pos_y = index * buttonHeight;
                maxNesting = nestingArray[index];
            }

            int clampedNesting = Math.Min(maxNesting, nestingIndex);
            pos_x = clampedNesting * indent;

            return new Point(pos_x, pos_y);

        }


        public static void RemoveCommand(ICommand list, ICommand target)
        {
            if (list is CompositeCommand)
            {
                CompositeCommand cc = list as CompositeCommand;
                try
                {
                    cc.GetCommands().Remove(target);
                }
                catch
                {
                    foreach (ICommand command in cc.GetCommands())
                    {
                        RemoveCommand(command, target);
                    }
                }
            }
            else { return; }
        }

    }












    public static class ControlExtension
    {
        public static Button GetButton(this Control parent, object tagToFind)
        {
            if (tagToFind == null)
            {
                return null;
            }

            foreach (Control c in parent.Controls)
            {
                if (c is Button button && Equals(button.Tag, tagToFind))
                {
                    return button;
                }
                if (c.HasChildren)
                {
                    Button found = c.GetButton(tagToFind);

                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        
        
        }

    }





















}
