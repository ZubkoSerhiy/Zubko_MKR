using Xunit;

namespace Lab3.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void ProcessData_WithValidPath_ReturnsCorrectDistance()
        {
            string[] input = {
                "3 3 3",
                "1..",
                "oo.",
                "oo.",
                "ooo",
                "..o",
                "oo.",
                "ooo",
                "ooo",
                "o.2"
            };

            int expected = 30;
            int result = Program.ProcessData(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProcessData_WithNoPath_ReturnsMaxValue()
        {
            string[] input = {
                "3 3 3",
                "1..",
                "ooo",
                "oo.",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "o.2"
            };

            int expected = -1;
            int result = Program.ProcessData(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ProcessData_WithOnlyWalls_ReturnsMaxValue()
        {
            string[] input = {
                "3 3 3",
                "1oo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "ooo",
                "oo2"
            };

            int expected = -1;
            int result = Program.ProcessData(input);

            Assert.Equal(expected, result);
        }
    }
}