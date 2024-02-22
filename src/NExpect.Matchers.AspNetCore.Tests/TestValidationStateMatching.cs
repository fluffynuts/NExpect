using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NExpect.Exceptions;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestValidationStateMatching
{
    [Test]
    public void ShouldBeAbleToAssertEmpty()
    {
        // Arrange
        var empty = new ValidationStateDictionary();
        var notEmpty = new ValidationStateDictionary();
        notEmpty.Add("foo", new ValidationStateEntry());
        notEmpty["foo"].SuppressValidation = true;
        // Act
        Assert.That(
            () =>
            {
                Expect(empty)
                    .To.Be.Empty();
                Expect(notEmpty)
                    .Not.To.Be.Empty();
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(notEmpty)
                    .To.Be.Empty();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        Assert.That(
            () =>
            {
                Expect(empty)
                    .Not.To.Be.Empty();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}