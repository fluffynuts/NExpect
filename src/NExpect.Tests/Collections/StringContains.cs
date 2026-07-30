using System;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Collections;

[TestFixture]
public class StringContains
{
    [Test]
    public void ShouldBeAbleToAssertMultipleSubstringsInParentString()
    {
        // Arrange
        var input = "hello, world! How goes?";

        // Act
        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.All.Of("hello", "world");
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.All.Of(["hello", "world"]);
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.All.Of(["hello", "how"], StringComparison.OrdinalIgnoreCase);
            }
        ).Not.To.Throw();

        Expect(
                () =>
                {
                    Expect(input)
                        .To.Contain.All.Of(
                            "foo",
                            "bar",
                            "hello"
                        );
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing(
                "to contain all of"
            ).Then("foo")
            .Then("bar")
            .Then("hello")
            .Then("missing value")
            .Then("- foo")
            .Then("- bar")
            .And.Matching(
                str =>
                {
                    var parts = str.Split("missing value");
                    return !parts[1].Contains("- hello");
                }
            );
        // Assert
    }

    [Test]
    public void ShouldBeAbleToAssertMultipleSubstringsAllNotInActual()
    {
        // Arrange
        var input = "foo, bar, quux!";
        // Act
        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.None.Of("hello", "world");
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.None.Of("Foo", "Bar");
            }
        ).Not.To.Throw();

        Expect(
                () =>
                {
                    Expect(input)
                        .To.Contain.None.Of("foo", "bar");
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing(
                "to contain none of"
            ).Then("foo")
            .Then("bar")
            .Then("found values")
            .Then("foo")
            .Then("bar");

        Expect(
            () =>
            {
                Expect(input)
                    .To.Contain.None.Of(
                        ["Foo", "Bar"],
                        StringComparison.OrdinalIgnoreCase
                    );
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }
}