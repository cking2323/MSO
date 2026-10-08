using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal interface ICommand
    {
        public void Run(EntityManager entities);
    }

    internal class MoveCommand : ICommand 
    {
        private int steps;
        public MoveCommand(int _steps) { this.steps = _steps;}

        public void Run(EntityManager entities) 
        { 
            Console.WriteLine("Player went from " + entities.Player.PositionToString());
            entities.Player.MoveSteps(steps);
            Console.WriteLine("to " + entities.Player.PositionToString());
        }
    }
    internal class TurnCommand : ICommand
    {
        public string turnDirection; // "left" or "right"
        public TurnCommand(string _turnDirection) 
        {
            this.turnDirection = _turnDirection;
        }

        public void Run(EntityManager entities)
        {
            Console.WriteLine("Player faced from " + entities.Player.DirectionToString());
            entities.Player.TurnDirection(turnDirection);
            Console.WriteLine("to " + entities.Player.DirectionToString());
        }
    }

    internal class CompositeCommand : ICommand 
    {
        private List<ICommand> commands;

        private int repeatAmount;

        public CompositeCommand(int repeat_amount) { commands = new List<ICommand>(); this.repeatAmount = repeat_amount; }

        public void AddCommand(ICommand Icommand) { commands.Add(Icommand); }

        public void Run(EntityManager entities) 
        {
            Console.WriteLine("COMPOSITE: " + repeatAmount + " TIMES");
            for (int i = 0; i < repeatAmount; i++) 
            {
                foreach (ICommand command in commands) { command.Run(entities); }
            }
        }
    }
}
