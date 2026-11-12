using Assert = Xunit.Assert;

namespace MSO.Tests
{
    public class GridTests
    {
        [Fact]
        public void TestGridLoading()
        {
            GridTextFileStrategy gridLoader = new GridTextFileStrategy("");

            CellType[,] grid = gridLoader.InitializeGrid(["oo++x", "++ooo"]);

            Assert.Equal(CellType.Open, grid[0, 0]);
            Assert.Equal(CellType.Closed, grid[0, 3]);
            Assert.Equal(CellType.Goal, grid[0, 4]);
        }

        [Theory]
        [InlineData("ooa")]
        [InlineData("123")]
        [InlineData("abc")]
        public void TestGridLoading_InvalidCharacters(string line)
        {
            var gridLoader = new GridTextFileStrategy("");

            Assert.Throws<FormatException>(() =>
                gridLoader.InitializeGrid([line])
            );
        }

        [Theory]
        [InlineData("oox\noooo")]
        [InlineData("ooox\nooo")]
        public void TestGridLoading_InvalidDimensions(string line)
        {
            var gridLoader = new GridTextFileStrategy("");

            Assert.Throws<FormatException>(() =>
                gridLoader.InitializeGrid(line.Split('\n'))
            );
        }

        [Theory]
        [InlineData("oox\noox")]
        [InlineData("ooo\nooo")]
        public void TestGridLoading_InvalidGoals(string line)
        {
            var gridLoader = new GridTextFileStrategy("");

            Assert.Throws<FormatException>(() =>
                gridLoader.VerifyGoal(gridLoader.InitializeGrid(line.Split('\n')))
            );
        }
    }
}
