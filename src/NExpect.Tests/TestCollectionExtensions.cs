using NExpect.Extensions;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Extensions.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestCollectionExtensions
    {
        [Test]
        [Ignore("WIP")]
        public void Contain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var other1 = GetAnother(search);
            var other2 = GetAnother<string>(new[] { search, other1 });
            var collection = new[]
            {
                search, other1, other2
            }.Randomize();

            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(collection).To.Contain.Exactly(1).EqualTo(search);
            }, Throws.Nothing);

            // Assert
        }

//        [Test]
//        public void Negated_Contain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldThrow()
//        {
//            // Arrange
//            var search = GetRandomString(3);
//            var other1 = GetAnother(search);
//            var other2 = GetAnother<string>(new[] { search, other1 });
//            var collection = new[]
//            {
//                search, other1, other2
//            }.Randomize();
//
//            // Pre-Assert
//
//            // Act
//            Assert.That(() =>
//            {
//                Expect(collection).Not.To.EqualTo(search);
//            }, Throws.Exception.InstanceOf<AssertionException>()
//                .With.Message.Contains("\nnot to contain\n"));
//
//            // Assert
//        }
//
//        [Test]
//        public void Contain_OperatingOnCollectionOfStrings_WhenDoesNotContain_ShouldThrow()
//        {
//            // Arrange
//            var search = GetRandomString(3);
//            var other1 = GetAnother(search);
//            var other2 = GetAnother<string>(new[] { search, other1 });
//            var collection = new[]
//            {
//                other1, other2
//            }.Randomize();
//
//            // Pre-Assert
//
//            // Act
//            Assert.That(() =>
//            {
//                Expect(collection).To.EqualTo(search);
//            }, Throws.Exception
//                .InstanceOf<AssertionException>()
//                .With.Message.Contains("\nto contain\n"));
//
//            // Assert
//        }
    }
}