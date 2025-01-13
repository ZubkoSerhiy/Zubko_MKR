using System.IO;
using Xunit;

public class ProgramTests
{
    [Fact]
    public void CalculatePosition_ValidInput_ReturnsCorrectResult()
    {
        int n = 4;
        int k = 3;
        int[] arrangement = { 2, 3, 4 };
        long expected = 10;

        long result = Program.CalculatePosition(n, k, arrangement);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculatePosition_InvalidInput_ThrowsException()
    {
        int n = 3;
        int k = 4;
        int[] arrangement = { 1, 2, 3, 4 };

        Assert.Throws<InvalidOperationException>(() => Program.CalculatePosition(n, k, arrangement));
    }

    [Fact]
    public void CalculatePosition_ZeroK_ReturnsOne()
    {
        int n = 5;
        int k = 0;
        int[] arrangement = { };
        long expected = 1;

        long result = Program.CalculatePosition(n, k, arrangement);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculatePosition_DuplicateValuesInArrangement_ThrowsException()
    {
        int n = 4;
        int k = 3;
        int[] arrangement = { 2, 2, 3 };

        Assert.Throws<InvalidOperationException>(() => Program.CalculatePosition(n, k, arrangement));
    }

    [Fact]
    public void CalculatePosition_ValueExceedingN_ThrowsException()
    {
        int n = 4;
        int k = 2;
        int[] arrangement = { 5, 3 };

        Assert.Throws<InvalidOperationException>(() => Program.CalculatePosition(n, k, arrangement));
    }

    [Fact]
    public void CalculatePosition_NEqualsK_ReturnsCorrectResult()
    {
        int n = 3;
        int k = 3;
        int[] arrangement = { 1, 2, 3 };
        long expected = 1;

        long result = Program.CalculatePosition(n, k, arrangement);

        Assert.Equal(expected, result);
    }
}
