using System;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.DanglingPrepositions;

[TestFixture]
public class Matched
{
    [Test]
    public void ShouldAllowArbitraryMatchingForPropertyFetcher()
    {
        // Arrange
        var dog = new Dog() { Name = "Fluffy" };
        
        // Act
        Assert.That(() =>
        {
            Expect(dog)
                .To.Be.Matched.By(o => o.Name == "Fluffy");
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(dog)
                .To.Be.Matched.By(o => o.Name == "Rex");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldAllowMatchingOnExceptionPropertyGrabbers()
    {
        // Arrange
        // Act
        Assert.That(() =>
        {
            Expect(() => throw new Exception("moo"))
                .To.Throw<Exception>()
                .With.Property(o => o)
                .Matched.By(e => e.Message == "moo");
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(() => throw new Exception("moo"))
                .To.Throw<Exception>()
                .With.Property(o => o)
                .Matched.By(e => e.Message == "cow");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

}