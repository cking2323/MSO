using Assert = Xunit.Assert;

namespace MSO.Tests
{
    public class ExampleSelectorTests 
    {
        private readonly TextWriter originalOut = Console.Out;
        private readonly TextReader originalIn = Console.In;

        public ExampleSelectorTests()
        {
            // Redirect console output to prevent cluttering test output runners
            Console.SetOut(new StringWriter());
        }



        [Fact]
        public void ChooseExamplePathTest()
        {
            string simulatedInput = "1\n";
            Console.SetIn(new StringReader(simulatedInput));

            string path = ExampleSelector.ChoosePath();

            Assert.Contains("basic.txt", path);
        }

        [Fact]
        public void ChooseCustomPathTest()
        {
            string simulatedInput = "4\nC:\\custom\\path\\program.txt\n";
            Console.SetIn(new StringReader(simulatedInput));

            string path = ExampleSelector.ChoosePath();

            Assert.Equal("C:\\custom\\path\\program.txt", path);
        }
    }
}