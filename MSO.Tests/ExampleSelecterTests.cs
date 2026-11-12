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
    }
}