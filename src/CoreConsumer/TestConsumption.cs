using NUnit.Framework;
using NExpect;
using static NExpect.Expectations;

namespace CoreConsumer;

[TestFixture]
public class TestConsumption
{
    [Test]
    public void ShouldBeAbleToExpect_Pass()
    {
        // Arrange
        // Pre-Assert
        // Act
        PerformExpectation(1, 1);
        // Assert
    }

    [Test]
    [Ignore("Run to see stack trace")]
    public void ShouldBeAbleToExpect_Fail()
    {
        // Arrange
        // Pre-Assert
        // Act
        PerformExpectation(1, 2);
        // Assert
    }

    private static void PerformExpectation(int left, int right)
    {
        Expect(left).To.Equal(right);
    }
}