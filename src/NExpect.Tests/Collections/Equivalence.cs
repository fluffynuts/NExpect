using System.Collections.Generic;
using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

// ReSharper disable InconsistentNaming

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Equivalence
    {
        [TestFixture]
        public class OperatingOnEmptyCollection
        {
            [Test]
            public void ComparingWithEmptyCollection_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<int>();
                var compare = new int[0];

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Equivalent.To(compare);
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class OperatingOnIdenticalCOllections
        {
            [Test]
            public void ShouldNotThrow()
            {
                // Arrange
                var start = GetRandomCollection<int>(4, 6).ToArray();
                var other = start.Select(i => i).ToArray();
                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(start).To.Be.Equivalent.To(other);
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class OperatingOnCollectionsWithSameItemsOutOfOrder
        {
            [Test]
            public void OperatingOnTwoEquivalentCollections_ShouldNotThrow()
            {
                // Arrange
                var start = GetRandomCollection<string>(4, 6).ToArray();
                var other = start.Randomize();
                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(start).To.Be.Equivalent.To(other);
                    },
                    Throws.Nothing);

                // Assert
            }
        }


        [Test]
        public void OperatingOnTwoInequivalentCollectionsOfSameSize_ShouldThrow()
        {
            // Arrange
            var test = GetRandomArray<decimal>(4, 6);
            var other = GetRandomCollection<decimal>(test.Length, test.Length);
            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).To.Be.Equivalent.To(other);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("]\nto be equivalent to\n["));

            // Assert
        }

        [Test]
        public void OperatingOnTwoInequivalentCollectionsOfSameSize_Negated_ShouldNotThrow()
        {
            // Arrange
            var test = GetRandomArray<string>(4, 6);
            var other = GetRandomCollection<string>(test.Length, test.Length);
            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).Not.To.Be.Equivalent.To(other);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void OperatingOnTwoInequivalentCollectionsOfSameSize_NegatedAlt_ShouldNotThrow()
        {
            // Arrange
            var test = GetRandomArray<string>(4, 6);
            var other = GetRandomCollection<string>(test.Length, test.Length);
            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).To.Not.Be.Equivalent.To(other);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void
            OperatingOnTwoEquivalentCollectionsOfSameSizeWithSameRepeatedElements_ShouldNotThrow()
        {
            // Arrange
            var test = new[] {1, 1, 2, 3};
            var other = new[] {1, 2, 3, 1};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).To.Be.Equivalent.To(other);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void
            OperatingOnTwoEquivalentCollectionsOfSameSizeWithDifferentRepeatedElements_ShouldThrow()
        {
            // Arrange
            var test = new[] {1, 1, 2, 3};
            var other = new[] {1, 2, 3, 2};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).To.Be.Equivalent.To(other);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("]\nto be equivalent to\n["));

            // Assert
        }

        [Test]
        public void
            OperatingOnTwoEquivalentCollectionsOfSameSizeWithSameRepeatedElements_WhenNegated_ShouldThrow()
        {
            // Arrange
            var test = new[] {1, 1, 2, 3};
            var other = new[] {1, 2, 3, 1};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(test).Not.To.Be.Equivalent.To(other);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("]\nnot to be equivalent to\n["));

            // Assert
        }

        [Test]
        public void Extending_CountMatchContinuation()
        {
            // Arrange
            var evens = new[] {2, 4, 6};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(evens)
                        .Not.To.Contain.Any.Odds()
                        .And.To.Contain.All.Evens();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Extending_CountMatchContinuationNegated()
        {
            // Arrange
            var evens = new[] {2, 4, 6};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(evens).To.Contain.Any.Odds();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    [TestFixture]
    public class Null
    {
        [Test]
        public void OperatingOnNull_ShouldNotThrow()
        {
            // Arrange
            List<string> collection = null;
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    // ReSharper disable once ExpressionIsAlwaysNull
                    Expect(collection).To.Be.Null();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void OperatingOnNull_Negated_ShouldThrow()
        {
            // Arrange
            List<string> collection = null;
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    // ReSharper disable once ExpressionIsAlwaysNull
                    Expect(collection).Not.To.Be.Null();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("not to be null"));
            // Assert
        }

        [Test]
        public void OperatingOnNotNull_Negated_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<string>();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Be.Null();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void OperatingOnNotNull_ShouldThrow()
        {
            // Arrange
            var collection = new List<string>();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Be.Null();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("]\nto be null"));
            // Assert
        }

        [TestFixture]
        public class WithCustomMessage
        {
            [Test]
            public void OperatingOnNull_Negated_ShouldThrow()
            {
                // Arrange
                var expectedMessage = "My Message";
                List<string> collection = null;
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        // ReSharper disable once ExpressionIsAlwaysNull
                        Expect(collection).Not.To.Be.Null(expectedMessage);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(expectedMessage));
                // Assert
            }

            [Test]
            public void OperatingOnNotNull_WithCustomMessage_ShouldThrow_IncludingCustomMessage()
            {
                // Arrange
                var collection = new List<string>();
                var expected = GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Null(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(expected));
                // Assert
            }

            [Test]
            public void OperatingOnNotNullAlt_WithCustomMessage_ShouldThrow_IncludingCustomMessage()
            {
                // Arrange
                List<string> collection = null;
                var expected = GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        // ReSharper disable once ExpressionIsAlwaysNull
                        Expect(collection).To.Not.Be.Null(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(expected));
                // Assert
            }
        }
    }

    [TestFixture]
    public class HaveUniqueItems
    {
        [Test]
        public void OperatingOnEmptyCollection_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int>();

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Have.Unique.Items();
                },
                Throws.Nothing
            );

            // Assert
        }

        [Test]
        public void OperatingOnNullCollection_ShouldThrow()
        {
            // Arrange
            List<int> collection = null;

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    // ReSharper disable once ExpressionIsAlwaysNull
                    Expect(collection).To.Have.Unique.Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void Negated_OperatingOnEmptyCollection_ShouldThrow()
        {
            // Arrange
            var collection = new List<int>();

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Have.Unique.Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
            );

            // Assert
        }

        [Test]
        public void OperatingOnCollectionWithSameItems_ShouldThrow()
        {
            // Arrange
            var collection = new List<int> {1, 1};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Have.Unique.Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void OperatingOnCollectionWithUniqueItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> {1, 2, 3};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Have.Unique.Items();
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void Negated_OperatingOnCollectionWithUniqueItems_ShouldThrow()
        {
            // Arrange
            var collection = new List<int> {1, 2, 3};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Have.Unique.Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void Negated_AltGrammar_OperatingOnCollectionWithUniqueItems_ShouldThrow()
        {
            // Arrange
            var collection = new List<int> {1, 2, 3};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Not.Have.Unique.Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void Negated_OperatingOnCollectionWithSameItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> {1, 1};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).Not.To.Have.Unique.Items();
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void Negated_AltGrammar_OperatingOnCollectionWithSameItems_ShouldNotThrow()
        {
            // Arrange
            var collection = new List<int> {1, 1};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Not.Have.Unique.Items();
                },
                Throws.Nothing);

            // Assert
        }

        [TestFixture]
        public class UnmetMessage
        {
            [Test]
            public void OperatingOnCollectionWithSameItems()
            {
                // Arrange
                var collection = new List<int> {1, 1, 1};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Have.Unique.Items();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>()
                        .With.Message.EqualTo("Expected [ 1, 1, 1 ] to only contain unique items")
                );
                // Assert
            }

            [Test]
            public void OperatingOnCollectionWithUniqueItems()
            {
                // Arrange
                var collection = new List<int> {1, 2, 3};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Have.Unique.Items();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>()
                        .With.Message.EqualTo("Expected [ 1, 2, 3 ] to contain duplicate items")
                );
                // Assert
            }

            [Test]
            public void OperatingOnEmptyCollection()
            {
                // Arrange
                var collection = new List<int>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Have.Unique.Items();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>()
                        .With.Message
                        .EqualTo("Expected [  ] to contain duplicate items, but found empty collection")
                );
                // Assert
            }

            [Test]
            public void OperatingOnNullCollection()
            {
                // Arrange
                List<int> collection = null;

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        // ReSharper disable once ExpressionIsAlwaysNull
                        Expect(collection).To.Have.Unique.Items();
                    },
                    Throws.Exception.TypeOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected IEnumerable<Int32>, but found (null)")
                );
                // Assert
            }
        }
    }

    [TestFixture]
    public class ItemCountTesting
    {
        [Test]
        public void WhenCollectionHasExpectedCount_ShouldNotThrow()
        {
            // Arrange
            var expected = GetRandomInt(1);
            var input = new int[expected];
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(input).To.Contain.Exactly(expected).Items();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void WhenCollectionDoesNotHaveExpectedCount_ShouldThrow()
        {
            // Arrange
            var expected = GetRandomInt(10);
            var delta = GetRandomInt(1, 3);
            var actual = GetRandomBoolean()
                ? expected + delta
                : expected - delta;
            var input = new int[actual];
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(input).To.Contain.Exactly(expected).Items();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected to find exactly {expected} occurrences of any int but found {actual}"));
            // Assert
        }

        [Test]
        public void Negated_WhenCollectionDoesNotHaveExpectedCount_ShouldNotThrow()
        {
            // Arrange
            var expected = GetRandomInt(10);
            var delta = GetRandomInt(1, 3);
            var actual = GetRandomBoolean()
                ? expected + delta
                : expected - delta;
            var input = new int[actual];
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(input).Not.To.Contain.Exactly(expected).Items();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Negated_WhenCollectionDoesHaveExpectedCount_ShouldThrow()
        {
            // Arrange
            var expected = GetRandomInt(10);
            var delta = GetRandomInt(1, 3);
            var actual = GetRandomBoolean()
                ? expected + delta
                : expected - delta;
            var input = new int[actual];
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(input).Not.To.Contain.Exactly(actual).Items();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected not to find exactly {actual} occurrences of any int but found {actual}"));
            // Assert
        }

        [Test]
        public void Negated_AltGrammar_WhenCollectionDoesNotHaveExpectedCount_ShouldNotThrow()
        {
            // Arrange
            var expected = GetRandomInt(10);
            var delta = GetRandomInt(1, 3);
            var actual = GetRandomBoolean()
                ? expected + delta
                : expected - delta;
            var input = new int[actual];
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(input).To.Not.Contain.Exactly(expected).Items();
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Item_Alias()
        {
            // Arrange
            var collection = new[] {GetRandomInt()};

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Exactly(1).Item();
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(collection).Not.To.Contain.Exactly(1).Item();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            Assert.That(() =>
                {
                    Expect(collection).To.Not.Contain.Exactly(1).Item();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [TestFixture]
        public class No
        {
            [Test]
            public void WhenCollectionHasNoItems_ShouldThrow()
            {
                // Arrange
                var input = new int[0];
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(input).To.Contain.No.Items();
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenCollectionHasItems_ShouldThrow()
            {
                // Arrange
                var expected = GetRandomInt(1);
                var input = new int[expected];
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(input).To.Contain.No.Items();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void Negated_WhenCollectionHasItems_ShouldNotThrow()
            {
                // Arrange
                var expected = GetRandomInt(1);
                var input = new int[expected];
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(input).Not.To.Contain.No.Items();
                    },
                    Throws.Nothing);
                // Assert
            }
        }

        [TestFixture]
        public class None
        {
            [Test]
            public void ShouldNotThrowWhenNoMatches()
            {
                // Arrange
                // Act
                Assert.That(() =>
                {
                    Expect(new[] { 1, 2, 3 })
                        .To.Contain.None
                        .Matched.By(i => i > 3);
                }, Throws.Nothing);
                
                Assert.That(() =>
                {
                    Expect(new[] { 1, 2, 3 })
                        .Not.To.Contain.None
                        .Matched.By(i => i > 3);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void ShouldThrowWhenHaveMatches()
            {
                // Arrange
                // Act
                Assert.That(() =>
                {
                    Expect(new[] { 1, 2, 3 })
                        .To.Contain.None
                        .Equal.To(3);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                
                Assert.That(() =>
                {
                    Expect(new[] { 1, 2, 3 })
                        .To.Contain.None
                        .Equal.To(4);
                }, Throws.Nothing);
                // Assert
            }
        }
    }

    [TestFixture]
    public class Equal_WithNoCustomMessage
    {
        [Test]
        public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Equal(right);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).Not.To.Equal(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Not.Equal(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {2, 1};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Equal(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    [TestFixture]
    public class Equal_NoCustomMessage_LongerSyntax
    {
        [Test]
        public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Be.Equal.To(right);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).Not.To.Be.Equal.To(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Not.Be.Equal.To(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {2, 1};
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Be.Equal.To(right);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    [TestFixture]
    public class Equal_WithCustomMessage
    {
        [Test]
        public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Equal(right, message);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).Not.To.Equal(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }

        [Test]
        public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Not.Equal(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }

        [Test]
        public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {2, 1};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Equal(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }
    }

    [TestFixture]
    public class Equal_WithCustomMessageLongerSyntax
    {
        [Test]
        public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Be.Equal.To(right, message);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).Not.To.Be.Equal.To(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }

        [Test]
        public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {1, 2};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Not.Be.Equal.To(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }

        [Test]
        public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
        {
            // Arrange
            var left = new[] {1, 2};
            var right = new[] {2, 1};
            var message = GetRandomString();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(left).To.Be.Equal.To(right, message);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));
            // Assert
        }
    }
}