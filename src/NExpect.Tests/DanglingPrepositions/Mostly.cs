using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.DanglingPrepositions;

[TestFixture]
public class Mostly
{
    [Test]
    public void ShouldBeAbleToTestMostlyDistinct()
    {
        // Arrange
        var numbers = new[] { 1, 2, 3, 1, 2 };
        // Act
        Assert.That(() =>
        {
            Expect(numbers)
                .To.Be.Mostly.Distinct();
        }, Throws.Nothing);


        Assert.That(() =>
        {
            Expect(numbers)
                .Not.To.Be.Mostly.Distinct();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldBeAbleToApplyCustomRatio()
    {
        // Arrange
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 8, 8, 8, 10 };
        // Act
        Assert.That(() =>
        {
            Expect(numbers)
                .To.Be.Mostly.Distinct(
                    minimumRequiredRatio: 0.82
                );
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }
}