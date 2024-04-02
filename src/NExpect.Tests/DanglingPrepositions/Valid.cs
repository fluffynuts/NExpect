using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect.Tests.DanglingPrepositions;

[TestFixture]
public class Valid
{
    [Test]
    public void DanglingOnA()
    {
        // Arrange
        // Act
        Assert.That(
            () =>
            {
                Expect(2)
                    .To.Be.A.Valid.Integer();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(2)
                    .Not.To.Be.A.Valid.Integer();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect("abc")
                    .Not.To.Be.A.Valid.Integer();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect("abc")
                    .To.Not.Be.A.Valid.Integer();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect("abc")
                    .To.Be.A.Valid.Integer();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}

public static class ValidIntegerMatchers
{
    public static IMore<T> Integer<T>(
        this IValid<T> valid
    )
    {
        return valid.AddMatcher(
            actual =>
            {
                var passed = int.TryParse($"{actual}", out _);
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} {passed.AsNot()}to be an integer value"
                );
            }
        );
    }
}