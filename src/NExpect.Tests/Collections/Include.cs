using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Include
    {
        [Test]
        public void ShouldBeAbleToQuicklyTestIfCollectionIncludesValue()
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
                        .To.Include(1);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(collection)
                        .Not.To.Include(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Not.Include(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(collection)
                        .To.Include(-1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            // Assert
        }

        [Test]
        public void ShouldEmitUsefulAndGrammaticallyCorrectErrors()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(3, 4, 5)
                        .To.Include(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        "Expected to find value 1 in collection"
                    )
            );

            Assert.That(
                () =>
                {
                    Expect(1, 2, 3)
                        .Not.To.Include(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        "Expected not to find value 1 in collection"
                    )
            );
            // Assert
        }
    }
}