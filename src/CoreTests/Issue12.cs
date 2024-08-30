using System;
using NUnit.Framework;
using static NExpect.Expectations;
using NExpect;
using NExpect.Implementations;

namespace CoreTests;

[TestFixture]
public class Issue12
{
    [Test]
    public void ComparingTypes_ShouldNotStall()
    {
        // Arrange

        // Pre-Assert

        // Act
        Assert.That(() =>
        {
            Expect(new Object().GetType()).To.Equal(typeof(Object));
        }, Throws.Nothing);

        // Assert
    }

    [Test]
    public void StringifyObject()
    {
        // Arrange

        // Pre-Assert

        // Act
        var result = (new object()).Stringify();

        // Assert
        Console.WriteLine(result);
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void StringifyType()
    {
        // Arrange

        // Pre-Assert

        // Act
        var result = typeof(object).Stringify();

        // Assert
        Assert.That(result, Is.Not.Null);
    }
}