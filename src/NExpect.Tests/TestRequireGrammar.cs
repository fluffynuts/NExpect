using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;

namespace NExpect.Tests;

[TestFixture]
public class TestRequireGrammar
{
    [Test]
    public void ShouldBeAbleToUseRequireAsDangler()
    {
        // Arrange
        var dog = new Dog();
        var rock = new Rock();
        // Act
        Assert.That(
            () =>
            {
                Expect(dog)
                    .To.Require.Food();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(dog)
                    .Not.To.Require.Food();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(dog)
                    .To.Not.Require.Food();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        Assert.That(
            () =>
            {
                Expect(rock)
                    .To.Require.Food();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(rock)
                    .Not.To.Require.Food();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(rock)
                    .To.Not.Require.Food();
            },
            Throws.Nothing
        );
        // Assert
    }

    public abstract class Animal
    {
    }

    public class Dog : Animal
    {
    }

    public class Rock
    {
    }
}

public static class RequireTestExtensions
{
    public static IMore<T> Food<T>(
        this IRequire<T> require
    )
    {
        return require.AddMatcher(
            actual =>
            {
                var passed = actual is TestRequireGrammar.Animal;
                return new MatcherResult(
                    passed,
                    () => $"Expected {typeof(T)} {passed.AsNot()}to require food"
                );
            }
        );
    }
}