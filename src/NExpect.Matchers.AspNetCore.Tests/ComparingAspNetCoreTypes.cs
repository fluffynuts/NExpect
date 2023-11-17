using Microsoft.AspNetCore.Http;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class ComparingAspNetCoreTypes
{
    [Test]
    public void ShouldBeAbleToPerformDeepEqualityTesting()
    {
        // Arrange
        var item1 = new
        {
            Path = new PathString("/moo")
        };
        var item2 = new
        {
            Path = new PathString("/moo")
        };
        var item3 = new
        {
            Path = new PathString("/cow")
        };
        // Act
        Assert.That(
            () =>
            {
                Expectations.Expect(item1)
                    .Not.To.Deep.Equal(item3);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expectations.Expect(item1)
                    .Not.To.Deep.Equal(item3);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expectations.Expect(item1)
                    .To.Deep.Equal(item3);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        Assert.That(
            () =>
            {
                Expectations.Expect(item1)
                    .Not.To.Deep.Equal(item2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}