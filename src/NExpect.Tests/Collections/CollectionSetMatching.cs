using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Collections;

[TestFixture]
public class CollectionSetMatching
{
    [Test]
    public void PositiveCase()
    {
        // Arrange
        var sub = new[]
        {
            1,
            2,
            3
        };
        var collection = new[]
        {
            10,
            1,
            3,
            7,
            2
        };
        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Be.A.Superset.Of(sub);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(sub)
                    .To.Be.A.Subset.Of(collection);
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void NegatedPositiveCase()
    {
        // Arrange
        var sub = new[]
        {
            1,
            2,
            3
        };
        var collection = new[]
        {
            10,
            1,
            3,
            7,
            2
        };
        // Act
        Assert.That(
            () =>
            {
                Expect(sub)
                    .Not.To.Be.A.Superset.Of(collection);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(collection)
                    .Not.To.Be.A.Subset.Of(sub);
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void FailingCases()
    {
        // Arrange
        var sub = new[]
        {
            1,
            2,
            3
        };
        var collection = new[]
        {
            10,
            1,
            3,
            7,
            2
        };
        // Act
        Assert.That(
            () =>
            {
                Expect(sub)
                    .To.Be.A.Superset.Of(collection);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Be.A.Subset.Of(sub);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }

    [TestFixture]
    public class MutualSets
    {
        [TestFixture]
        public class Subsets
        {
            [Test]
            [Explicit("WIP: I don't have the language 100% down yet - need to think of pretty, readable code")]
            public void ShouldBeAbleToAssertThatTwoSetsAreMutualSubsets()
            {
                // see https://www.mathsisfun.com/sets/sets-introduction.html
                //   for an intro to the notation:
                //   ⊂ == "is a proper subset of" (ie, contains all of)
                //   ⊆ == "is a subset of" (ie contains 1 or more items from the other set, but not all)
                //   - both notations are here to provide full context, but for our purposes,
                //     we can treat them the same: we're interested in 1 or more, up to N
                // ie, given 2 sets, A and B,
                //   A ⊂ B or A ⊆ B
                //   and
                //   B ⊂ A or B ⊆ A
                // Arrange
                var a = new[] { 1, 1, 3, 2, 2, 1, 3 };
                var b = new[] { 3, 2, 3, 1 };
                // Act
                // Expect(a)
                //     .To.Mutally.Subset(b);
                // Assert
            }
        }
    }
}
