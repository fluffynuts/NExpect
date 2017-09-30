using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
    public class AtMost
    {
        [Test]
        public void Contain_GivenAtMost1_WhenCollectionHasNone_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString();
            var item1 = GetAnother(search);
            var item2 = GetAnother<string>(new[] {item1, search});
            var collection = new[] {item1, item2}.Randomize();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Contain_GivenAtMost1_WhenCollectionHas1_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString();
            var item1 = GetAnother(search);
            var item2 = GetAnother<string>(new[] {item1, search});
            var collection = new[] {search, item1, item2}.Randomize();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void Contain_GivenAtMost1_WhenCollectionHas2_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString();
            var item1 = GetAnother(search);
            var item2 = GetAnother<string>(new[] {item1, search});
            var collection = new[] {search, item1, search, item2}.Randomize();
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("at most 1"));
            // Assert
        }
    }
}