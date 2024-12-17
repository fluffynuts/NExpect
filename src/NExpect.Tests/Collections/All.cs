using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.Collections;

[TestFixture]
public class All
{
    [Test]
    public void WhenAllMatch_ShouldNotThrow()
    {
        // Arrange
        var search = GetRandomString(4);
        var collection = PyLike.Range(GetRandomInt(3, 5)).Select(i => search);

        // Pre-Assert

        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.All
                    .Equal.To(search);
            },
            Throws.Nothing
        );
        var hashSet = collection.AsHashSet();
        Assert.That(() =>
        {
            Expect(hashSet)
                .To.Contain.All.Of([search]);
        }, Throws.Nothing);
        // Assert
    }

    [Test]
    public void WhenNotAllMatch_ShouldNotThrow()
    {
        // Arrange
        var search = GetRandomString(4);
        var collection = PyLike.Range(GetRandomInt(3, 5))
            .Select(i => search)
            .Union(
                new[]
                {
                    GetAnother(search)
                }
            );

        // Pre-Assert

        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Contain.All
                    .Equal.To(search);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }

    [Test]
    public void Negated_WhenAllMatch_ShouldThrow()
    {
        // Arrange
        var search = GetRandomString(4);
        var collection = PyLike.Range(GetRandomInt(3, 5)).Select(i => search);

        // Pre-Assert

        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .Not.To.Contain.All
                    .Equal.To(search);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        // Assert
    }

    [Test]
    public void NegatedAlt_WhenAllMatch_ShouldThrow()
    {
        // Arrange
        var search = GetRandomString(4);
        var collection = PyLike.Range(GetRandomInt(3, 5)).Select(i => search);

        // Pre-Assert

        // Act
        Assert.That(
            () =>
            {
                Expect(collection)
                    .To.Not.Contain.All
                    .Equal.To(search);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        // Assert
    }

    [TestFixture]
    public class CollectionItemReferenceEquality
    {
        [Test]
        public void ShouldBeAbleToAssertCollectionItemsAreReferenceEqual()
        {
            // Arrange
            var collection1 = new[]
            {
                new object(),
                new object()
            };
            var collection2 = collection1.ToArray();
            var outOfOrder = new[]
            {
                collection1[1],
                collection1[0]
            };
            // Act
            // this is the use-case: when the collection
            // is cloned but the items are not
            Assert.That(
                () =>
                {
                    Expect(collection1)
                        .To.Be(collection2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // positive test
            Assert.That(
                () =>
                {
                    Expect(collection1)
                        .Items.To.Be(collection2);
                },
                Throws.Nothing
            );

            // negative test
            Assert.That(
                () =>
                {
                    Expect(collection1)
                        .Items.Not.To.Be(collection2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // negative test
            Assert.That(
                () =>
                {
                    Expect(collection1)
                        .Items.To.Not.Be(collection2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // equality != equivalence
            Assert.That(
                () =>
                {
                    Expect(collection1)
                        .Items.To.Be(outOfOrder);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // Assert
        }
    }
}