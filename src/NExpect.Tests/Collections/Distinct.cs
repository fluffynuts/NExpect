using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Distinct
    {
        [TestFixture]
        public class OperatingOnEmptyCollection
        {
            [Test]
            public void ShouldNotThrow()
            {
                // Arrange
                var collection = new List<int>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Distinct();
                    },
                    Throws.Nothing);

                // Assert
            }

            [TestFixture]
            public class WhenNegated
            {
                [Test]
                public void ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int>();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>()
                    );

                    // Assert
                }
            }
        }

        [TestFixture]
        public class OperatingOnNullCollection
        {
            [Test]
            public void ShouldThrow()
            {
                // Arrange
                List<int> collection = null;

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        // ReSharper disable once ExpressionIsAlwaysNull
                        Expect(collection).To.Be.Distinct();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>());

                // Assert
            }
        }

        [TestFixture]
        public class WhenCollectionHasRepeatedItems
        {
            [Test]
            public void ShouldThrow()
            {
                // Arrange
                var collection = new List<int> { 1, 1 };

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Distinct();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>());

                // Assert
            }

            [TestFixture]
            public class WhenNegated
            {
                [Test]
                public void ShouldNotThrow()
                {
                    // Arrange

                    var collection = new List<int> { 1, 1 };

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }
            }
        }

        [Test]
        public void OperatingOnCollectionWithUniqueItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Be.Distinct();
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void Negated_OperatingOnCollectionWithUniqueItems_ShouldThrow()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Be.Distinct();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void Negated_AltGrammar_OperatingOnCollectionWithUniqueItems_ShouldThrow()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Not.Be.Distinct();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void Negated_OperatingOnCollectionWithSameItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> { 1, 1 };

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Be.Distinct();
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void Negated_AltGrammar_OperatingOnCollectionWithSameItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> { 1, 1 };

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Not.Be.Distinct();
                },
                Throws.Nothing);

            // Assert
        }
    }
}