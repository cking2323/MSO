using Assert = Xunit.Assert;

namespace MSO.Tests
{
    [TestClass]
    public class MetricsAnalyzerTests
    {
        [Fact]
        public void FlatTreeTest()
        {
            var root = new CompositeCommand(1);
            root.AddCommand(new MoveCommand(5));
            root.AddCommand(new TurnCommand("left"));

            var metrics = MetricsAnalyzer.Analyze(root);

            Assert.Equal(2, metrics.TotalCommands);
            Assert.Equal(0, metrics.TotalRepeats);
            Assert.Equal(0, metrics.MaxNesting);
        }

        [Fact]
        public void NestedCompositeTest()
        {
            var root = new CompositeCommand(1);

            var repeatCommand = new CompositeCommand(3);
            repeatCommand.AddCommand(new MoveCommand(2));

            root.AddCommand(repeatCommand);

            var metrics = MetricsAnalyzer.Analyze(root);

            Assert.Equal(2, metrics.TotalCommands);
            Assert.Equal(1, metrics.TotalRepeats);
            Assert.Equal(1, metrics.MaxNesting);
        }

        [Fact]
        public void DeeplyNestedCommandsTest()
        {
            var root = new CompositeCommand(1);

            var level1 = new CompositeCommand(2);
            var level2 = new CompositeCommand(2);
            level2.AddCommand(new MoveCommand(1));

            level1.AddCommand(level2);
            root.AddCommand(level1);

            var metrics = MetricsAnalyzer.Analyze(root);

            Assert.Equal(2, metrics.TotalRepeats);
            Assert.Equal(2, metrics.MaxNesting);
        }
    }
}
