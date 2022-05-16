using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
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
            Assert.That(() =>
                {
                    Expect(collection)
                        .To.Contain.All
                        .Equal.To(search);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void WhenNotAllMatch_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(4);
            var collection = PyLike.Range(GetRandomInt(3, 5))
                .Select(i => search)
                .Union(new[] {GetAnother(search)});

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection)
                        .To.Contain.All
                        .Equal.To(search);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
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
            Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Contain.All
                        .Equal.To(search);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

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
            Assert.That(() =>
                {
                    Expect(collection)
                        .To.Not.Contain.All
                        .Equal.To(search);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

    }
}