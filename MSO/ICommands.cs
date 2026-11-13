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
            entities.Player.MoveSteps(steps, entities.Grid);
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
            entities.Player.TurnDirection(turnDirection);
        }
    }

    internal abstract class NestedCommand : ICommand
    {
        protected List<ICommand> commands;

        public abstract void Run(EntityManager entities);

        public void AddCommand(ICommand Icommand) { commands.Add(Icommand); }

        public List<ICommand> GetCommands() { return commands; }
    }

    internal class CompositeCommand : NestedCommand 
    {
        public int repeatAmount;

        public CompositeCommand(int repeat_amount) { commands = new List<ICommand>(); this.repeatAmount = repeat_amount; }

        public override void Run(EntityManager entities) 
        {
            for (int i = 0; i < repeatAmount; i++) 
            {
                foreach (ICommand command in commands) { command.Run(entities); }
            }
        }
    }


    internal class RepeatCommand : NestedCommand
    {

        public string repeatType;

        public RepeatCommand(string repeatType) 
        { 
            commands = new List<ICommand>();
            this.repeatType = repeatType; 
        }

        public override void Run(EntityManager entities)
        {
            entities.Player.repeatMode = true;

            for (int i = 0; i < 1000; i++)
            {
                if (i == 999)
                    throw new Exception("9999 iterations reached. Infinite loop likely detected");

                if (repeatType == "WallAhead" && entities.Player.wallAhead)
                {
                    entities.Player.wallAhead = false;
                    break;
                }

                if (repeatType == "GridEdge" && entities.Player.gridBorderAhead)
                {
                    entities.Player.gridBorderAhead = false;
                    break;
                }

                commands[i % commands.Count].Run(entities);
            }

            entities.Player.repeatMode = false;
        }
    }
}
