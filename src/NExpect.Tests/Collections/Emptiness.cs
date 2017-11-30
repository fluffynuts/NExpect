using System.Collections.Concurrent;
using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Emptiness
    {
        [TestFixture]
        public class OperatingOnCollection
        {
            [Test]
            public void WhenIsEmpty_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<string>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenIsEmpty_WhenNegated_ShouldThrow()
            {
                // Arrange
                var collection = new List<string>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo("Expected\n[  ]\nnot to be an empty collection"));

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_ShouldThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("]\nto be an empty collection"));

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_WhenNegated_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_WhenNegatedAlt_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Not.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class OperatingOnDictionary
        {
            [Test]
            public void WhenIsEmpty_ShouldNotThrow()
            {
                // Arrange
                var collection = new Dictionary<string, object>();
                var sorted = new SortedDictionary<int, string>();
                var concurrent = new ConcurrentDictionary<string, double>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection as IDictionary<string, object>).To.Be.Empty();
                        Expect(collection).To.Be.Empty();
                        Expect(sorted).To.Be.Empty();
                        Expect(concurrent).To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenIsEmpty_WhenNegated_ShouldThrow()
            {
                // Arrange
                var collection = new List<string>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo("Expected\n[  ]\nnot to be an empty collection"));

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_ShouldThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("]\nto be an empty collection"));

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_WhenNegated_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenIsNotEmpty_WhenNegatedAlt_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Not.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }
        }
    }
}