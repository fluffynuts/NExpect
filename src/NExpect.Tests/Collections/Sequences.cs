using System.Linq;
using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests.Collections;

[TestFixture]
public class Sequences
{
    [TestFixture]
    public class AscendingOrder
    {
        [Test]
        public void ShouldThrowForEmptyCollection()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(new int[0])
                        .To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            Assert.That(
                () =>
                {
                    Expect(new int[0])
                        .Not.To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            // Assert
        }

        [Test]
        public void ShouldAlwaysThrowForSingleItem()
        {
            // Arrange
            var collection = new int[]
            {
                GetRandomInt()
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            // Assert
        }

        [Test]
        public void ShouldHandleOrderedCollection()
        {
            // Arrange
            var collection = new[]
            {
                1,
                2
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );

            var customMessage = GetRandomWords(2);
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending(() => customMessage);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage)
            );
            // Assert
        }

        [Test]
        public void ShouldHandleHomogenousCollection()
        {
            // Arrange
            var collection = new[]
            {
                1,
                1,
                1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ParamsOverload()
        {
            // Arrange

            // Act
            Assert.That(
                () =>
                {
                    Expect(1, 2, 3, 4)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHaveAscendingMessage()
        {
            // Arrange

            // Act
            Expect(
                    () => Expect(3, 2, 1)
                        .To.Be.Ordered.Ascending()
                ).To.Throw<UnmetExpectationException>()
                .With.Message.Containing("ascending");
            // Assert
        }

        [Test]
        public void ShouldHaveDescendingMessage()
        {
            // Arrange

            // Act
            Expect(
                    () => Expect(1, 2, 3)
                        .To.Be.Ordered.Descending()
                ).To.Throw<UnmetExpectationException>()
                .With.Message.Containing("descending");
            // Assert
        }

        [Test]
        public void ShouldOrderCollectionOfStrings()
        {
            // Arrange
            var items = new[]
            {
                "'Murican",
                "Queen's English",
                "Boerie",
                "English"
            };

            // Act
            Assert.That(
                () =>
                {
                    Expect(items)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldHandleIOrderedEnumerableToo()
        {
            // Arrange
            var numbers = new[]
            {
                4,
                7,
                3,
                9,
                2
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(numbers.OrderBy(x => x))
                        .To.Be.Ordered.Ascending();
                    Expect(numbers.OrderByDescending(x => x))
                        .To.Be.Ordered.Descending();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHandleOrderedCollectionOfStrings()
        {
            // Arrange
            var collection = new[]
            {
                "aardvark",
                "dingo",
                "hippo",
                "zebra"
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );

            var customMessage = GetRandomWords(2);
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending(() => customMessage);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage)
            );
            // Assert
        }


        [Test]
        public void ShouldHandleAllEqualCollection()
        {
            // Arrange
            var collection = new[]
            {
                0,
                0
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHandleDescendingCollection()
        {
            // Arrange
            var collection = new[]
            {
                3,
                2,
                1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending();
                },
                Throws.Nothing
            );
            // Assert
        }
    }

    [TestFixture]
    public class DescendingOrder
    {
        [Test]
        public void ShouldThrowForEmptyCollection()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(new int[0])
                        .To.Be.Ordered.Descending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            Assert.That(
                () =>
                {
                    Expect(new int[0])
                        .Not.To.Be.Ordered.Descending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            // Assert
        }

        [Test]
        public void ShouldAlwaysThrowForSingleItem()
        {
            // Arrange
            var collection = new int[]
            {
                GetRandomInt()
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items")
            );
            // Assert
        }

        [Test]
        public void ShouldHandleOrderedCollection()
        {
            // Arrange
            var collection = new[]
            {
                2,
                1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                },
                Throws.Nothing
            );

            var customMessage = GetRandomWords(2);
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending(() => customMessage);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage)
            );
            // Assert
        }

        [Test]
        public void ShouldHandleAllEqualCollection()
        {
            // Arrange
            var collection = new[]
            {
                0,
                0
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHandleDescendingCollection()
        {
            // Arrange
            var collection = new[]
            {
                1,
                2,
                3
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending();
                },
                Throws.Nothing
            );
            // Assert
        }
    }

    [TestFixture]
    public class GivenOrderingLambda
    {
        public class HasId
        {
            public int Id { get; set; }
        }

        [Test]
        public void ShouldEnforceOrderingOnCollectionOfTwo()
        {
            // Arrange
            var collection = new[]
            {
                new
                {
                    Id = 1,
                    Name = "Zebra"
                },
                new
                {
                    Id = 2,
                    Name = "Aardvark"
                }
            };

            // Act
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.By(
                            o => o.Id
                        );
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.By(
                            o => o.Name,
                            Direction.Descending
                        );
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Be.Ordered.By(
                            o => o.Name
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
        }

        [Test]
        public void ShouldEnforceOrderingOnCollectionOfThree()
        {
            // Arrange
            var ordered = new[]
            {
                1,
                2,
                3
            };
            var unordered = new[]
            {
                3,
                1,
                2
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(ordered)
                        .To.Be.Ordered.By(i => i);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(unordered)
                        .To.Be.Ordered.By(i => i);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldNegateCorrectly()
        {
            // Arrange

            var ordered = new[]
            {
                1,
                2,
                3
            };
            var unordered = new[]
            {
                3,
                1,
                2
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(unordered)
                        .Not.To.Be.Ordered.By(i => i);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(ordered)
                        .Not.To.Be.Ordered.By(i => i);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }
}