using NExpect.Exceptions;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;
// ReSharper disable InconsistentNaming

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Only_N
    {
        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding1_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                search
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting2Items_AndFinding2_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                search,
                search
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(2).Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenContainsCorrectNumber_AndNotEqual_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                GetAnother(search)
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected to find only 1 occurrence of \"{search}\" but found 0"));

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding0_ShouldThrow()
        {
            // Arrange
            var collection = new string[] { };
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected to find only 1 item in collection, but found 0")
            );

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding3_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var other1 = GetAnother(search);
            var other2 = GetAnother<string>(new[] {search, other1});
            var other3 = GetAnother<string>(new[] {search, other1, other2});
            var collection = new[]
            {
                other1,
                other2,
                other3
            }.Randomize();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected to find only 1 item in collection, but found 3")
            );

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting2Items_AndFinding3_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var other1 = GetAnother(search);
            var other2 = GetAnother<string>(new[] {search, other1});
            var other3 = GetAnother<string>(new[] {search, other1, other2});
            var collection = new[]
            {
                other1,
                other2,
                other3
            }.Randomize();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(2).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected to find only 2 items in collection, but found 3")
            );

            // Assert
        }
    }
}