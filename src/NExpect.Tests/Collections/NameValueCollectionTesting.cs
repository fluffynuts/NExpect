using System.Collections.Specialized;
using NUnit.Framework;
using NExpect.Exceptions;
using NExpect.Implementations;

// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class NameValueCollectionTesting
    {
        [Test]
        public void ShouldBeAbleToAssertEmpty()
        {
            // Arrange
            var collection = new NameValueCollection();
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );
            collection["a"] = "b";
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Empty();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertEmptyKeys()
        {
            // Arrange
            var collection = new NameValueCollection();
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection.Keys)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );
            collection["a"] = "b";
            Assert.That(
                () =>
                {
                    Expect(collection.Keys)
                        .Not.To.Be.Empty();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(collection.Keys)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [TestFixture]
        public class ContainingAssertions
        {
            [TestFixture]
            public class Key
            {
                [TestFixture]
                public class OperatingOnPlainNameValueCollection
                {
                    [Test]
                    public void WhenHasKey_ShouldNotThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new NameValueCollection();
                        src[key] = GetRandomString();
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(src).To.Contain.Key(key);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenHasKey_ShouldThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new NameValueCollection()
                        {
                            [key] = GetRandomString()
                        };
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(src).Not.To.Contain.Key(key);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(
                                    $"not to contain key\n{key.Stringify()}"
                                )
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenDoesNotHaveKey_ShouldNotThrow()
                    {
                        // Arrange
                        var src = new NameValueCollection();
                        var key = GetRandomString();
                        // Pre-assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(src).Not.To.Contain.Key(key);
                            },
                            Throws.Nothing
                        );

                        Assert.That(
                            () =>
                            {
                                Expect(src).To.Not.Contain.Key(key);
                            },
                            Throws.Nothing
                        );
                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_WhenHasKey_ShouldThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new NameValueCollection()
                        {
                            [key] = GetRandomString()
                        };
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(src).To.Not.Contain.Key(key);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(
                                    $"not to contain key\n{key.Stringify()}"
                                )
                        );

                        // Assert
                    }

                    [TestFixture]
                    public class WithValue
                    {
                        [TestFixture]
                        public class OperatingOnPrimitiveTypes
                        {
                            [Test]
                            public void WhenHasKey_AndValueMatches_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomString(2);
                                var src = new NameValueCollection()
                                {
                                    [key] = value
                                };
                                // Pre-Assert

                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(value);
                                    },
                                    Throws.Nothing
                                );

                                // Assert
                            }

                            [Test]
                            public void WhenDoesNotHaveKey_ShouldThrowForThatReason()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomString(2);
                                var src = new NameValueCollection()
                                {
                                    [key] = value
                                };
                                var testingValue = GetAnother(key);
                                // Pre-Assert

                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src).To.Contain.Key(testingValue).With.Value(value);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"to contain key\n\"{testingValue}\"")
                                );

                                // Assert
                            }

                            [Test]
                            public void WhenHasKey_AndValueDoesNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomString(2);
                                var src = new NameValueCollection()
                                {
                                    [key] = value
                                };
                                var testingValue = GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Key(key)
                                            .With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected\n\"{testingValue}\"\nbut got\n\"{value}\"")
                                );

                                // Assert
                            }
                        }
                    }
                }
            }
        }

        [TestFixture]
        public class Emptiness
        {
            [Test]
            public void ShouldBeAbleToAssertEmptiness()
            {
                // Arrange
                var empty = new NameValueCollection();
                var notEmpty = new NameValueCollection
                {
                    [GetRandomString(1)] = GetRandomString()
                };
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(empty)
                            .To.Be.Empty();
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }
    }
}