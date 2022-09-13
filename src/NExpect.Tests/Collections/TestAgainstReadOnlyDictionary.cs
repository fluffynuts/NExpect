using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections;

[TestFixture]
public class TestAgainstReadOnlyDictionary
{
    [Test]
    public void ShouldBeAbleToAssertEmptiness()
    {
        // Arrange
        var dict = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>()
        );
        // Act
        Assert.That(() =>
        {
            Expect(dict)
                .To.Be.Empty();
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(dict)
                .Not.To.Be.Empty();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldBeAbleToAssertKeysAndValues()
    {
        // Arrange
        var dict = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>()
            {
                ["foo"] = "bar"
            }
        );
        // Act
        Assert.That(() =>
        {
            Expect(dict)
                .To.Contain.Key("foo")
                .With.Value("bar");
        }, Throws.Nothing);

        Assert.That(() =>
        {
            Expect(dict)
                .To.Contain.Key("bar")
                .With.Value("foo");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());

        Assert.That(() =>
        {
            Expect(dict)
                .To.Contain.Key("foo")
                .With.Value("foo");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }
}