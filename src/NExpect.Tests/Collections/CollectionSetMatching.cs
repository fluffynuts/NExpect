using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class CollectionSetMatching
    {
        [Test]
        public void PositiveCase()
        {
            // Arrange
            var sub = new[] { 1, 2, 3 };
            var collection = new[] { 10, 1, 3, 7, 2 };
            // Act
            Assert.That(() =>
            {
                Expect(collection)
                    .To.Be.A.Superset.Of(sub);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(sub)
                    .To.Be.A.Subset.Of(collection);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void NegatedPositiveCase()
        {
            // Arrange
            var sub = new[] { 1, 2, 3 };
            var collection = new[] { 10, 1, 3, 7, 2 };
            // Act
            Assert.That(() =>
            {
                Expect(sub)
                    .Not.To.Be.A.Superset.Of(collection);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(collection)
                    .Not.To.Be.A.Subset.Of(sub);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void FailingCases()
        {
            // Arrange
            var sub = new[] { 1, 2, 3 };
            var collection = new[] { 10, 1, 3, 7, 2 };
            // Act
            Assert.That(() =>
            {
                Expect(sub)
                    .To.Be.A.Superset.Of(collection);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(collection)
                    .To.Be.A.Subset.Of(sub);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}