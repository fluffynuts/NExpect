using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;

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
                            Expect(collection)
                                .Not.To.Be.Distinct();
                        },
                        Throws.Nothing
                    );

                    // Assert
                }

                [Test]
                public void AltGrammar_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> { 1, 1 };

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection)
                                .To.Not.Be.Distinct();
                        },
                        Throws.Nothing
                    );

                    // Assert
                }
            }
        }

        [TestFixture]
        public class WhenCollectionHasUniqueItems
        {
            [Test]
            public void ShouldNotThrow()
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

            [TestFixture]
            public class AndIsNegated
            {
                [Test]
                public void ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> { 1, 2, 3 };

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection)
                                .Not.To.Be.Distinct();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );

                    // Assert
                }

                [Test]
                public void AltGrammar_ShouldThrow()
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
            }
        }
    }
}