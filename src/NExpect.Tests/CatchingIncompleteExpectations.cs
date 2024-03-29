using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Tests;

[TestFixture]
public class CatchingIncompleteExpectations
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Assertions.EnableTracking();
    }

    [OneTimeTearDown]
    public void OneTimeTeardown()
    {
        Assertions.DisableTracking();
    }

    [Test]
    public void ShouldCatchSimpleExpect()
    {
        // // Arrange
        // // Act
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

        Assertions.VerifyNoIncompleteAssertions();
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

        Assertions.VerifyNoIncompleteAssertions();
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
        Assertions.VerifyNoIncompleteAssertions();
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
        Assertions.VerifyNoIncompleteAssertions();
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
        Assertions.VerifyNoIncompleteAssertions();
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
        Assertions.VerifyNoIncompleteAssertions();
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
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldNotThrowForDateComparisons()
    {
        // Arrange
        var earlier = new DateTime(2020, 1, 1);
        var later = new DateTime(2021, 1, 1);
        // Act
        Expect(later)
            .To.Be.Greater.Than(earlier);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(later)
            .To.Be.Greater.Than.Or.Equal.To(earlier);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(earlier)
            .To.Be.Less.Than(later);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(earlier)
            .To.Be.Less.Than.Or.Equal.To(later);
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldNotThrowForIntComparisons()
    {
        // Arrange
        var earlier = 1;
        var later = 2;
        // Act
        Expect(later)
            .To.Be.Greater.Than(earlier);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(later)
            .To.Be.Greater.Than.Or.Equal.To(earlier);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(earlier)
            .To.Be.Less.Than(later);
        Assertions.VerifyNoIncompleteAssertions();
        Expect(earlier)
            .To.Be.Less.Than.Or.Equal.To(later);
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertTypeWithoutViolating()
    {
        // Arrange
        var foo = new Service() as object;
        // Act
        Expect(foo)
            .To.Be.An.Instance.Of<Service>();
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertSingleStringInCollection()
    {
        // Arrange
        var strings = new[] { "a", "b", "c" };
        // Act
        Expect(strings)
            .To.Contain.Exactly(1)
            .Equal.To("b");
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertOnlyTheKeyInADictionary()
    {
        // Arrange
        var dict = new Dictionary<string, string>()
        {
            ["foo"] = "bar"
        };
        // Act
        Expect(dict)
            .To.Contain.Key("foo");
        // Assert

        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertMessageMatchingOnException()
    {
        // Arrange
        // Act
        Expect(() => throw new Exception("nope"))
            .To.Throw<Exception>()
            .With.Message.Matching(
                new Regex("nope")
            );
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertExceptionTypeAlt()
    {
        // Arrange
        // Act
        Expect(() => throw new ArgumentException("foo"))
            .To.Throw()
            .With.Type(typeof(ArgumentException));
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToDoCollectionCasting()
    {
        // Arrange
        var left = new[] { new { id = 1 } };
        var right = new[] { new { id = 1, name = "bob" } };
        // Act
        Expect(left)
            .As.Objects
            .To.Intersection.Equal(right);
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldAllowPropertyValidation()
    {
        // Arrange
        var sut = typeof(Person);
        // Act
        Expect(sut)
            .To.Have.Property("Name")
            .With.Attribute<StringLengthAttribute>(
                a => a.MaximumLength == 123
            );
        Expect(sut)
            .To.Have.Property("Name")
            .With.Type<string>();
        Expect(sut)
            .To.Have.Property("Name")
            .With.Attribute<StringLengthAttribute>(
                a => a.MaximumLength == 123
            )
            .With.Type<string>();
        Expect(sut)
            .To.Have.Property("Name")
            .With.Attribute<StringLengthAttribute>(
                a => a.MaximumLength == 123
            )
            .And
            .To.Have.Property("Name")
            .With.Type<string>();
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    [Test]
    public void ShouldBeAbleToAssertRuntimes()
    {
        // Arrange
        // Act
        Expect(() => Thread.Sleep(500))
            .RunTime
            .To.Be.Greater.Than(TimeSpan.FromMilliseconds(100))
            .And
            .To.Be.Less.Than(TimeSpan.FromMilliseconds(1000));
        // Assert
        Assertions.VerifyNoIncompleteAssertions();
    }

    public class Person
    {
        [StringLength(123)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class Service
    {
    }

    private Action RunAndVerify(Action action)
    {
        return () =>
        {
            action();
            Assertions.VerifyNoIncompleteAssertions();
        };
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
        Assertions.WarnOfIncompleteAssertions();
        // Assert
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