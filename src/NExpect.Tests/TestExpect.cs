using NExpect.Extensions;
using NUnit.Framework;
using static NExpect.Implementations.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestExpect
    {
        [Test]
        public void Expect_src_ToEqual_value_WhenMatches_ShouldNotThrow()
        {
            // Arrange
            var actual = 1;
            var expected = 1;
            // Pre-Assert

            // Act
            Assert.That(
                () => Expect(actual).To.Equal(expected),
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void Expect_src_ToEqual_value_WhenDoesNotMatch_ShouldThrow()
        {
            // Arrange
            var actual = 1;
            var expected = 2;
            // Pre-Assert

            // Act
            Assert.That(
                () => Expect(actual).To.Equal(expected),
                Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains($"Expected {expected} but got {actual}")
            );
            // Assert
        }

        [Test]
        public void
            Expect_src_ToEqual_value_WhenDoesNotMatch_GivenCustomMessage_ShouldThrowWithCustomMessageAndRegularOne()
        {
            // Arrange
            var actual = 1;
            var expected = 2;
            var custom = GetRandomString(5);
            // Pre-Assert

            // Act
            Assert.That(
                () => Expect(actual).To.Equal(expected, custom),
                Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains($"Expected {expected} but got {actual}")
            );
            Assert.That(
                () => Expect(actual).To.Equal(expected, custom),
                Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains(custom)
            );
            // Assert
        }

        [Test]
        public void Negation_WhenValuesDoNotMatch_ShouldNotThrow()
        {
            // Arrange
            var actual = 1;
            var expected = 2;

            // Pre-Assert

            // Act
            Assert.That(
                () => Expect(actual).Not.To.Equal(expected),
                Throws.Nothing
            );

            // Assert
        }

        [Test]
        public void ReversedNegation_WhenValuesDoNotMatch_ShouldNotThrow()
        {
            // Arrange
            var actual = 1;
            var expected = 2;

            // Pre-Assert

            // Act
            Assert.That(
                () => Expect(actual).To.Not.Equal(expected),
                Throws.Nothing
            );

            // Assert
        }
    }
}