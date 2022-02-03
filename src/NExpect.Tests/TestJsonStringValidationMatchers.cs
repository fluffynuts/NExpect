using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests;

[TestFixture]
public class TestJsonStringValidationMatchers
{
    [Test]
    public void ShouldBeAbleToAssertAgainstValidJson()
    {
        // Arrange
        var json = @"{ ""id"": 1 }";
        // Act
        Assert.That(() =>
        {
            Expect(json)
                .To.Be.Json();
        }, Throws.Nothing);
        
        Assert.That(() =>
        {
            Expect(json)
                .Not.To.Be.Json();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
            .With.Message.Contains(json));
        // Assert
    }
    
    [Test]
    public void ShouldBeAbleToAssertAgainstInValidJson()
    {
        // Arrange
        var invalid = @"{ ""id"": whee }";
        // Act
        Assert.That(() =>
        {
            Expect(invalid)
                .Not.To.Be.Json();
        }, Throws.Nothing);
        
        Assert.That(() =>
        {
            Expect(invalid)
                .To.Be.Json();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
            .With.Message.Contains(invalid));
        // Assert
    }
}