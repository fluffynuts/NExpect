﻿using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections;

[TestFixture]
public class PartialCollectionMatching
{
    [Test]
    public void ShouldAssertContainsAnyOf()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var other = new[] { 4, 5, 6 };
        var sub = new[] { 1, 8, 9 };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .Not.To.Contain.Any.Of(other);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(other)
                    .To.Contain.Any.Of(sub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        Assert.That(
            () =>
            {
                Expect(collection)
                    .Not.To.Contain.Any.Of(sub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
    }

    [Test]
    public void ShouldAssertContainsNoneOf()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var sub = new[] { 1, 8, 9 };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.None.Of(sub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.None.Of(sub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
    }

    [Test]
    public void ShouldAssertContainsAllOf()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var subset = new[] { 1, 3 };
        var superset = new[] { 1, 2, 3, 4 };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.All.Of(subset);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.All.Of(superset);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains("missing:\n[ 4 ]")
        );
        // Assert
    }

    [Test]
    public void ShouldAssertContainsAllOfViaParams()
    {
        // Arrange
        // Act
        Assert.That(
            () =>
            {
                Expect(1, 2, 3)
                    .To.Contain.All.Of(1, 3);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(1, 2, 3)
                    .To.Contain.All.Of(1, 2, 3, 4);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains("missing:\n[ 4 ]")
        );
        // Assert
    }

    [Test]
    public void ShouldBeAbleToTestThatLargerRepeatedCollectionItemsAreAllInSmallerCollection()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var actual = new[] { 1, 2, 3, 2, 3, 1, 3, 2, 1, 3, 1, 3, 2, 2, 3, 1, 1 };
        // Act
        Assert.That(() =>
        {
            Expect(collection)
                .To.Contain.All.Of(actual);
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldBeAbleToAssertAllOfOnSubSetOfRepeatedCollection()
    {
        // Arrange
        var collection = new[] { 1, 1, 1 };
        var actual = new[] { 1 };
        // Act
        Assert.That(() =>
        {
            Expect(collection)
                .To.Contain.All.Of(actual);
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(actual)
                .To.Contain.All.Of(collection);
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldBeAbleToTestThatLargerRepeatedCollectionItemsAreAllNotInSmallerCollection()
    {
        // Arrange
        var collection = new[] { 4, 5, 6 };
        var actual = new[] { 1, 2, 3, 2, 3, 1, 3, 2, 1, 3, 1, 3, 2, 2, 3, 1, 1 };
        // Act
        Assert.That(() =>
        {
            Expect(collection)
                .To.Contain.None.Of(actual);
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void ShouldAssertContainsExactNumberOf()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var sub = new[] { 1, 8, 9 };
        var otherSub = new[] { 8, 10, 9 };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.Exactly(1)
                    .Of(sub);
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(sub)
                    .To.Contain.Exactly(1)
                    .Of(otherSub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
    }

    [Test]
    public void ShouldAssertContainsAtLeastNumberOf()
    {
        // Arrange
        var collection = new[] { 1, 2, 3 };
        var has2 = new[] { 1, 3, 7, 9 };
        var has1 = new[] { 1, 5, 9, 10 };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.At.Least(1).Of(has2);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.At.Least(1).Of(has1);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.At.Least(2).Of(has1);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}