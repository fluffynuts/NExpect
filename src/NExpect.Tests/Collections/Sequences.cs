using System.Linq;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
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
                Assert.That(() =>
                {
                    Expect(new int[0])
                        .To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                Assert.That(() =>
                {
                    Expect(new int[0])
                        .Not.To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                // Assert
            }

            [Test]
            public void ShouldAlwaysThrowForSingleItem()
            {
                // Arrange
                var collection = new int[] { GetRandomInt() };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                // Assert
            }

            [Test]
            public void ShouldHandleOrderedCollection()
            {
                // Arrange
                var collection = new[] { 1, 2 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                }, Throws.Nothing);

                var customMessage = GetRandomWords(2);
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending(() => customMessage);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage));
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
                Assert.That(() =>
                {
                    Expect(items)
                        .To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void ShouldHandleIOrderedEnumerableToo()
            {
                // Arrange
                var numbers = new[] { 4, 7, 3, 9, 2 };
                // Act
                Assert.That(() =>
                {
                    Expect(numbers.OrderBy(x => x))
                        .To.Be.Ordered.Ascending();
                    Expect(numbers.OrderByDescending(x => x))
                        .To.Be.Ordered.Descending();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldHandleOrderedCollectionOfStrings()
            {
                // Arrange
                var collection = new[] { "aardvark", "dingo", "hippo", "zebra" };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                }, Throws.Nothing);

                var customMessage = GetRandomWords(2);
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending(() => customMessage);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage));
                // Assert
            }


            [Test]
            public void ShouldHandleAllEqualCollection()
            {
                // Arrange
                var collection = new[] { 0, 0 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldHandleDescendingCollection()
            {
                // Arrange
                var collection = new[] { 3, 2, 1 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Ascending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Ascending();
                }, Throws.Nothing);
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
                Assert.That(() =>
                {
                    Expect(new int[0])
                        .To.Be.Ordered.Descending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                Assert.That(() =>
                {
                    Expect(new int[0])
                        .Not.To.Be.Ordered.Descending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                // Assert
            }

            [Test]
            public void ShouldAlwaysThrowForSingleItem()
            {
                // Arrange
                var collection = new int[] { GetRandomInt() };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at least two items"));
                // Assert
            }

            [Test]
            public void ShouldHandleOrderedCollection()
            {
                // Arrange
                var collection = new[] { 2, 1 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                }, Throws.Nothing);

                var customMessage = GetRandomWords(2);
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending(() => customMessage);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(customMessage));
                // Assert
            }

            [Test]
            public void ShouldHandleAllEqualCollection()
            {
                // Arrange
                var collection = new[] { 0, 0 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldHandleDescendingCollection()
            {
                // Arrange
                var collection = new[] { 1, 2, 3 };
                // Act
                Assert.That(() =>
                {
                    Expect(collection)
                        .To.Be.Ordered.Descending();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                {
                    Expect(collection)
                        .Not.To.Be.Ordered.Descending();
                }, Throws.Nothing);
                // Assert
            }
        }
    }
}