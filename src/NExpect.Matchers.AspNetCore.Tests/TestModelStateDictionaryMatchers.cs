using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Exceptions;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestModelStateDictionaryMatchers
{
    [Test]
    public void ShouldBeAbleToTestEmpty()
    {
        // Arrange
        var empty = new ModelStateDictionary();
        var notEmpty = new ModelStateDictionary();
        notEmpty.AddModelError(
            GetRandomString(),
            GetRandomString()
        );
        // Act
        Assert.That(() =>
        {
            Expect(empty)
                .To.Be.Empty();
            Expect(notEmpty)
                .Not.To.Be.Empty();
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(empty)
                .Not.To.Be.Empty();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        Assert.That(() =>
        {
            Expect(notEmpty)
                .To.Be.Empty();
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }

    [Test]
    public void ShouldBeAbleToTestNull()
    {
        // Arrange
        // Act
        Assert.That(() =>
        {
            Expect(null as ModelStateDictionary)
                .To.Be.Null();
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldBeAbleToFindExistingError()
    {
        // Arrange
        var key = GetRandomString();
        var message = GetRandomString();
        var dict = new ModelStateDictionary();
        dict.AddModelError(key, message);
        // Act
        Expect(dict).To.Contain.Key(key)
            .With.Value.Matched.By(e => e.Errors.Single().ErrorMessage == message);
        // Assert
    }
}