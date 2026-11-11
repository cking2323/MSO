using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    internal interface ICommand
    {
        public void Run(EntityManager entities, TextBox tb);

        abstract void Accept(Visitor v);

    }

    internal class MoveCommand : ICommand 
    {
        private int steps;
        public MoveCommand(int _steps) { this.steps = _steps;}

        public void Accept(Visitor v)
        {
            v.Visit(this);
        }

        public void Run(EntityManager entities, TextBox tb) 
        { 
            UI.Print("Player went from " + entities.Player.PositionToString(), tb);
            entities.Player.MoveSteps(steps);
            UI.PrintLine(" to " + entities.Player.PositionToString(), tb);
        }

        public override string ToString()
        {
            return "Move " + steps.ToString();
        }
    }
    internal class TurnCommand : ICommand
    {
        public string turnDirection; // "left" or "right"
        public TurnCommand(string _turnDirection) 
        {
            this.turnDirection = _turnDirection;
        }
        public void Accept(Visitor v)
        {
            v.Visit(this);
        }
        public void Run(EntityManager entities, TextBox tb)
        {
            UI.Print("Player faced from " + entities.Player.DirectionToString(), tb);
            entities.Player.TurnDirection(turnDirection);
            UI.PrintLine(" to " + entities.Player.DirectionToString(), tb);
        }
        public override string ToString()
        {
            return "Turn " + turnDirection;
        }
    }

    internal class CompositeCommand : ICommand 
    {
        private List<ICommand> commands;

        public int repeatAmount;

        public CompositeCommand(int repeat_amount) { commands = new List<ICommand>(); this.repeatAmount = repeat_amount; }

        public void AddCommand(ICommand Icommand) { commands.Add(Icommand); }

        public List<ICommand> GetCommands() { return commands; }

        public void Accept(Visitor v)
        {
            v.Visit(this);
        }
        public void Run(EntityManager entities, TextBox tb) 
        {
            UI.PrintLine("COMPOSITE: " + repeatAmount + " TIMES", tb);
            for (int i = 0; i < repeatAmount; i++) 
            {
                foreach (ICommand command in commands) { command.Run(entities, tb); }
            }
        }
        public override string ToString()
        {
            return "Repeat " + repeatAmount.ToString();
        }
    }
}
