using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NExpect;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests;

[TestFixture]
public class CatchingIncompleteExpectations
{
    [Test]
    public void ShouldCatchSimpleExpect()
    {
        // Arrange
        // Act
        Assert.That(
            RunAndVerify(
                () =>
                {
                    Expect(2);
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );

        Assert.That(
            RunAndVerify(
                () =>
                {
                    Expect(2)
                        .To.Equal(2);
                }
            ),
            Throws.Nothing
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    // contrived - property access is required
                    // to pass compilation, but still
                    var moo = Expect(2)
                        .To.Equal(2)
                        .And;
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );
        // Assert
    }

    [Test]
    public void ShouldCatchIncompleteCollectionMatcher()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var objects = new[]
        {
            new { Id = 1, Name = "Bob" },
            new { Id = 2, Name = "Mary" }
        };


        // Act
        Assert.That(
            RunAndVerify(
                () =>
                {
                    // this is valid collection shorthand
                    Expect(collection)
                        .To.Contain(2);
                }
            ),
            Throws.Nothing
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    Expect(collection)
                        .To.Contain.At.Least(2);
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    Expect(collection)
                        .To.Contain.At.Least(2).Items();
                }
            ),
            Throws.Nothing
        );

        Assert.That(
            RunAndVerify(
                () =>
                {
                    var _ = Expect(objects)
                        .To.Contain.Exactly(1);
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    var _ = Expect(objects)
                        .To.Contain.Exactly(1)
                        .Deep;
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    var _ = Expect(objects)
                        .To.Contain.Exactly(1)
                        .Deep.Equal;
                }
            ),
            Throws.Exception.InstanceOf<IncompleteExpectationException>()
        );
        Assert.That(
            RunAndVerify(
                () =>
                {
                    var _ = Expect(objects)
                        .To.Contain.Exactly(1)
                        .Deep.Equal.To(
                            new { Id = 1, Name = "Bob" }
                        );
                }
            ),
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void ShouldNotRequireUserInterventionForCustomMatchers()
    {
        // Arrange
        // Act
        Assert.That(
            () =>
            {
                Expect(1)
                    .To.Be.One();
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void ExceptionChains1()
    {
        // Arrange
        var ex = new AggregateException("moo", new[] { new Exception("1"), new Exception("2") });
        // Act
        try
        {
            Expect(() => throw ex)
                .To.Throw<AggregateException>()
                .With.CollectionProperty(e => e.InnerExceptions)
                .Containing.Exactly(1)
                .Matched.By(e => e.Message == "1");
        }
        catch
        {
            // suppress
        }

        ExpectationTracker.AssertNoIncompleteExpectations();
        // Assert
    }

    [Test]
    public void ExceptionChains2()
    {
        // Arrange
        var ex = new AggregateException("moo", new[] { new Exception("1"), new Exception("2") });
        // Act
        try
        {
            Expect(() => throw ex)
                .To.Throw<AggregateException>()
                .With.CollectionProperty(e => e.InnerExceptions)
                .Not.Containing.Any
                .Matched.By(e => e.Message == "3");
        }
        catch
        {
            // suppress
        }

        ExpectationTracker.AssertNoIncompleteExpectations();
        // Assert
    }

    [Test]
    public void MessageNotContains1()
    {
        // Arrange
        var str = "moo, cow";
        // Pre-assert
        // Act
        try
        {
            Expect(() => throw new Exception(str))
                .To.Throw<Exception>()
                .With.Message.Containing("beef");
        }
        catch
        {
            // suppress
        }

        // Assert
        ExpectationTracker.AssertNoIncompleteExpectations();
    }

    [Test]
    public void MessageNotContains2()
    {
        // Arrange
        var str = "moo, cow";
        // Pre-assert
        // Act
        try
        {
            Expect(() => throw new Exception(str))
                .To.Throw<Exception>()
                .With.Message.Not.Containing("cow");
        }
        catch
        {
            // suppress
        }

        // Assert
        ExpectationTracker.AssertNoIncompleteExpectations();
    }

    [Test]
    public void MessageNotContains3()
    {
        // Arrange
        var str = "moo, cow";
        // Pre-assert
        // Act
        try
        {
            Expect(() => throw new Exception(str))
                .To.Throw<Exception>()
                .With.Message.Containing("beef");
        }
        catch
        {
            // suppress
        }

        // Assert
        ExpectationTracker.AssertNoIncompleteExpectations();
    }

    [Test]
    public Task AndExtension()
    {
        // Arrange
        // Act
        Expect("foo the ant")
            .To.Have.A.Space()
            .And.Have.A.Foo()
            .And.An.Ant();
        // Assert
        ExpectationTracker.AssertNoIncompleteExpectations();
        return Task.CompletedTask;
    }

    [Test]
    public void ContainInOrderAfterAnd()
    {
        // Arrange
        var left = GetRandomString(72, 128);
        var right = GetRandomString(72, 128);
        // Pre-Assert
        // Act
        try
        {
            var ex = Assert.Throws<UnmetExpectationException>(
                () => Expect(left)
                    .To.Equal(right)
            );
            Expect(ex.Message)
                .To.Start.With("Expected")
                .And.To.Contain.In.Order(
                    "\n",
                    right,
                    "\n",
                    "but got",
                    "\n",
                    left
                );
        }
        catch
        {
            // suppress
        }
        // Assert
        ExpectationTracker.AssertNoIncompleteExpectations();
    }

    [Test]
    [Explicit("this is a visual test for the error text")]
    public void EyeballingTheResult()
    {
        // Arrange
        var numbers = new[] { 1, 2, 3 };
        // Act
        Expect(numbers).To.Contain.Exactly(1);
        Expect(1);
        ExpectationTracker.WarnOfIncompleteExpectations();
        // Assert
    }

    private Action RunAndVerify(Action action)
    {
        return () =>
        {
            action();
            ExpectationTracker.AssertNoIncompleteExpectations();
        };
    }
}

public static class IntMatchers
{
    public static IMore<long> One(
        this IBe<long> be
    )
    {
        return be.AddMatcher(
            actual =>
            {
                var passed = actual == 1;
                return new MatcherResult(
                    passed,
                    () => $"Expected {passed.AsNot()}to get 1, but got {actual}"
                );
            }
        );
    }
}