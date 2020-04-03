using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Any
    {
        [Test]
        public void WhenHave1MatchInActual_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString();
            var actual = GetRandomCollection<string>(2, 4).Union(search.AsArray());

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(actual)
                        .To.Contain.Any
                        .Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void WhenHave0MatchInActual_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString();
            var actual = GetRandomCollection<string>(2, 4);

            // Pre-Assert
            Assert.That(actual, Does.Not.Contain(search), "Should not find search before test");

            // Act
            Assert.That(() =>
                {
                    Expect(actual)
                        .To.Contain.Any
                        .Equal.To(search);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void WhenHaveActualAllMatchingSearch_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString();
            var actual = PyLike.Range(GetRandomInt(2, 4)).Select(i => search);

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(actual)
                        .To.Contain.Any
                        .Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }
    }
}