using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using NExpect.Exceptions;
using NExpect.Matchers.AspNet.Tests.Implementations;
using static NExpect.AspNetCoreExpectations;
// leave this in: without it, the need to cast as IQueryCollection is dropped
// ReSharper disable once RedundantUsingDirective
using static NExpect.Expectations;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestQueryCollectionMatchers
{
    [Test]
    public void ShouldBeAbleToAssertAgainstQueryLikeADictionary()
    {
        // Arrange
        var query = new FakeQueryCollection
        {
            ["foo"] = "bar"
        } as IQueryCollection;
        // Act
        Assert.That(() =>
        {
            Expect(query)
                .To.Contain.Key("foo")
                .With.Value("bar");
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(query)
                .To.Contain.Key("foo1")
                .With.Value("bar");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        Assert.That(() =>
        {
            Expect(query)
                .To.Contain.Key("foo")
                .With.Value("bar1");
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }
}

[TestFixture]
public class ComparingAspNetCoreTypes
{
    [Test]
    public void ShouldBeAbleToPerformDeepEqualityTesting()
    {
        // Arrange
        var item1 = new { Path = new PathString("/moo") };
        var item2 = new { Path = new PathString("/moo") };
        var item3 = new { Path = new PathString("/cow") };
        // Act
        Assert.That(() =>
        {
            Expect(item1)
                .Not.To.Deep.Equal(item3);
        }, Throws.Nothing);
        
        Assert.That(() =>
        {
            Expect(item1)
                .Not.To.Deep.Equal(item3);
        }, Throws.Nothing);

        Assert.That(() =>
        {
            Expect(item1)
                .To.Deep.Equal(item3);
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        
        Assert.That(() =>
        {
            Expect(item1)
                .Not.To.Deep.Equal(item2);
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }
}