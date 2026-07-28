using System;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Collections;

[TestFixture]
public class Intersection
{
    [Test]
    public void ShouldBeAbleToAssertThatTwoCollectionsIntersect()
    {
        // Arrange
        var a1 = new[]
        {
            1,
            2,
            3
        };
        var a2 = new[]
        {
            2,
            3,
            4,
            5
        };
        var a3 = new[]
        {
            5,
            6,
            7
        };

        // Act
        Expect(
            () =>
            {
                Expect(a1)
                    .To.Intersect(a2);
            }
        ).Not.To.Throw();
        Expect(
                () =>
                {
                    Expect(a1)
                        .Not.To.Intersect(a2);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("Expected no intersection")
            .Then("2").Then("3");
        Expect(
                () =>
                {
                    Expect(a1)
                        .To.Intersect(a3);
                }
            ).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("Expected intersection")
            .Then("1").Then("2").Then("3")
            .Then("with")
            .Then("5").Then("6").Then("7")
            .Then("no matching elements");
        Expect(
            () =>
            {
                Expect(a1)
                    .Not.To.Intersect(a3);
            }
        ).Not.To.Throw();

        // Assert
    }
}