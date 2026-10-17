using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    class MetricsResult
    {
        public int TotalCommands { get; set; }
        public int TotalRepeats { get; set; }
        public int MaxNesting { get; set; }
    }

    class MetricsAnalyzer
    {
        public static MetricsResult Analyze(ICommand command)
        {
            // -1 total commands because the commands are all put into 1 composite command at the start
            int totalCommands = -1;
            int totalRepeats = 0;
            int maxNesting = 0;

            void Traverse(ICommand cmd, int currentDepth)
            {
                if (cmd is CompositeCommand composite)
                {
                    if (currentDepth > maxNesting)
                    {
                        maxNesting = currentDepth;
                    }

                    // If it's not the root composite count it as a repeat
                    if (currentDepth > 0)
                    {
                        totalRepeats++;
                    }

                    // Traverse children
                    foreach (var subCommand in composite.GetCommands())
                    {
                        Traverse(subCommand, currentDepth + 1);
                    }
                }
                totalCommands++;
            }

            Traverse(command, 0);

            return new MetricsResult
            {
                TotalCommands = totalCommands,
                TotalRepeats = totalRepeats,
                MaxNesting = maxNesting
            };
        }
    }
}
