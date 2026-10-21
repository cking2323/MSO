using Assert = Xunit.Assert;

namespace MSO.Tests
{
    [TestClass]
    public class FileStrategyTests
    {
        [Fact]
        public void FileFactoryTxtExtensionTest()
        {
            FileStrategy strategy = FileFactory.ReturnFileStrategy("test.txt");

            Assert.NotNull(strategy);
            Assert.IsType<TextFileStrategy>(strategy);
        }

        [Fact]
        public void FileFactoryInvalidExtensionTest()
        {
            var ex = Assert.Throws<Exception>(() => FileFactory.ReturnFileStrategy("test.json"));
            Assert.Contains("Path fout", ex.Message);
        }

        [Fact]
        public void TextFileStrategyImportTest()
        {
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "Move 5\nTurn left\n");

            try
            {
                FileStrategy strategy = new TextFileStrategy(tempFile);

                ICommand rootCommand = strategy.Import();

                var metrics = MetricsAnalyzer.Analyze(rootCommand);

                Assert.Equal(2, metrics.TotalCommands);
                Assert.Equal(0, metrics.TotalRepeats);
                Assert.Equal(0, metrics.MaxNesting);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TextFileStrategyImportRepeatTest()
        {
            string tempFile = Path.GetTempFileName();
            string fileContent = "Repeat 3\n\tMove 10\n\tTurn right\n";
            File.WriteAllText(tempFile, fileContent);

            try
            {
                FileStrategy strategy = new TextFileStrategy(tempFile);

                ICommand rootCommand = strategy.Import();
                var metrics = MetricsAnalyzer.Analyze(rootCommand);

                Assert.Equal(3, metrics.TotalCommands); 
                Assert.Equal(1, metrics.TotalRepeats);  
                Assert.Equal(1, metrics.MaxNesting);    
            }
            finally
            {
                // Cleanup temp file
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
