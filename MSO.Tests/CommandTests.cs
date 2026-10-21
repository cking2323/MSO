using Assert = Xunit.Assert;
using MSO;

namespace MSO.Tests
{
    [TestClass]

    public class CommandTests
    {
        [Fact]
        public void MoveCommandTest()
        {
            var entities = new EntityManager();
            var moveCommand = new MoveCommand(5);

            moveCommand.Run(entities);

            Assert.Equal("(5, 0)", entities.Player.PositionToString());
        }

        [Fact]
        public void TurnCommandTest()
        {
            var entities = new EntityManager();
            var turnCommand = new TurnCommand("right");

            turnCommand.Run(entities);

            Assert.Equal("down", entities.Player.DirectionToString());
        }

        [Fact]
        public void CompositeCommandTest()
        {
            var entities = new EntityManager();
            var composite = new CompositeCommand(3);
            composite.AddCommand(new MoveCommand(2));

            composite.Run(entities);

            Assert.Equal("(6, 0)", entities.Player.PositionToString());
        }

        [Fact]
        public void CompositeCommandNestedTest()
        {
            var entities = new EntityManager();
        
            var outerComposite = new CompositeCommand(2);
        
            var innerComposite = new CompositeCommand(2);
            innerComposite.AddCommand(new MoveCommand(1));
        
            outerComposite.AddCommand(innerComposite);

            outerComposite.Run(entities);

            Assert.Equal("(4, 0)", entities.Player.PositionToString());
        }
    }
}
