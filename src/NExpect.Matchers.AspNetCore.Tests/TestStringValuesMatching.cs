using Microsoft.Extensions.Primitives;
using NExpect.Exceptions;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestStringValuesMatching
{
    [Test]
    public void ShouldWorkAsOnArrayOfString()
    {
        // Arrange
        var s1 = GetRandomString();
        var s2 = GetAnother(s1);
        var s3 = GetAnother<string>(
            new[]
            {
                s1,
                s2
            }
        );
        var sv = new StringValues([s1, s2, s3]);
        // Act
        Assert.That(
            () =>
            {
                Expect(sv)
                    .To.Contain(s1);
                Expect(sv)
                    .To.Contain.Exactly(1)
                    .Equal.To(s1);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(sv)
                    .Not.To.Contain(s2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        
        Assert.That(
            () =>
            {
                Expect(sv)
                    .Not.To.Contain.Any
                    .Equal.To(s2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(sv)
                    .To.Not.Contain(s2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        
        Assert.That(
            () =>
            {
                Expect(sv)
                    .To.Not.Contain.Any
                    .Equal.To(s2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}