using System.Numerics;
using Assert = Xunit.Assert;
using x = Xunit;


namespace MSO.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [Fact]
        public void InitialPlayer()
        {
            Player player = new Player();
            Assert.Equal("right", player.DirectionToString());
            Assert.Equal("(0, 0)", player.PositionToString());
        }
        [Fact]
        public void PlayerMove()
        {
            Player player = new Player();
            player.MoveSteps(5);
            Assert.Equal("(5, 0)", player.PositionToString());
        }
        [Fact]
        public void PlayerTurnRight()
        {
            Player player = new Player();

            player.TurnDirection("right");
            Assert.Equal("down", player.DirectionToString());

            player.TurnDirection("right");
            Assert.Equal("left", player.DirectionToString());

            player.TurnDirection("right");
            Assert.Equal("up", player.DirectionToString());

            player.TurnDirection("right");
            Assert.Equal("right", player.DirectionToString());
        }

        [Fact]
        public void PlayerTurnLeft()
        {
            Player player = new Player();

            player.TurnDirection("left");
            Assert.Equal("up", player.DirectionToString());

            player.TurnDirection("left");
            Assert.Equal("left", player.DirectionToString());

            player.TurnDirection("left");
            Assert.Equal("down", player.DirectionToString());

            player.TurnDirection("left");
            Assert.Equal("right", player.DirectionToString());
        }
        [Fact]
        public void PlayerMoveAndTurn()
        {
            Player player = new Player();
            player.TurnDirection("right");
            player.MoveSteps(5);
            Assert.Equal("(0, -5)", player.PositionToString());
        }
    }
}