using System;
using Xunit;

public class ProgramTests
{
    [Fact]
    public void ProcessData_ValidInput_ReturnsCorrectResult()
    {
        int n = 3;
        string[] input = { "3", "10:15:30", "12:30:45", "14:45:00" };

        long expected = 36930;

        long result = Program.ProcessData(n, input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ProcessData_InvalidInput_HandlesIncorrectFormat()
    {
        int n = 3;
        string[] input = { "3", "invalid_time", "12:34:00", "25:00:00" };

        long result = Program.ProcessData(n, input);

        Assert.True(result > 0);
    }

    [Fact]
    public void ProcessData_EmptyInput_ThrowsArgumentException()
    {
        int n = 0;
        string[] input = { "0" };

        Assert.Throws<ArgumentException>(() => Program.ProcessData(n, input));
    }
}