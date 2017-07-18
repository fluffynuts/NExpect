using NExpect.Extensions;
using NExpect.Implementations;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

namespace NExpect.Tests
{
    public class TestTruthExtensions
    {
        public class TrueEquality
        {
            [Test]
            public void ToBeTrue_WhenValueIsTrue_ShouldNotThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.True(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ToBeTrue_WhenValueIsFalse_ShouldThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.True(),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains($"Expected True but got {value}")
                );

                // Assert
            }

            [Test]
            public void ToBeTrue_WhenValueIsFalse_GivenCustomMessage_ShouldThrow()
            {
                // Arrange
                var value = false;
                var message = RandomValueGen.GetRandomString(12);

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.True(message),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains($"Expected True but got {value}")
                );

                Assert.That(
                    () => Expectations.Expect(value).To.Be.True(message),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains(message)
                );

                // Assert
            }

            [Test]
            public void NotToBeTrue_WhenValueIsFalse_ShouldNotThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).Not.To.Be.True(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ToBeNotTrue_WhenValueIsFalse_ShouldNotThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.Not.True(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void NotToBeTrue_WhenValueIsTrue_ShouldThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).Not.To.Be.True(),
                    Throws.Exception.InstanceOf<AssertionException>()
                );

                // Assert
            }

            [Test]
            public void ToBeNotTrue_WhenValueIsTrue_ShouldThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.Not.True(),
                    Throws.Exception.InstanceOf<AssertionException>()
                );

                // Assert
            }

            [Test]
            public void ShouldWorkOnNullableBooleans()
            {
                // Arrange
                var value = true as bool?;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.True(),
                    Throws.Nothing
                );

                // Assert
            }
        }

        public class FalseEquality
        {
            [Test]
            public void ToBeFalse_WhenValueIsFalse_ShouldNotThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.False(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ToBeFalse_WhenValueIsTrue_ShouldThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.False(),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains($"Expected False but got {value}")
                );

                // Assert
            }

            [Test]
            public void ToBeFalse_WhenValueIsTrue_GivenCustomMessage_ShouldThrow()
            {
                // Arrange
                var value = true;
                var message = RandomValueGen.GetRandomString(12);

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.False(message),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains($"Expected False but got {value}")
                );

                Assert.That(
                    () => Expectations.Expect(value).To.Be.False(message),
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains(message)
                );

                // Assert
            }

            [Test]
            public void NotToBeFalse_WhenValueIsTrue_ShouldNotThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).Not.To.Be.False(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ToBeNotFalse_WhenValueIsFalse_ShouldNotThrow()
            {
                // Arrange
                var value = true;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.Not.False(),
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void NotToBeFalse_WhenValueIsFalse_ShouldThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).Not.To.Be.False(),
                    Throws.Exception.InstanceOf<AssertionException>()
                );

                // Assert
            }

            [Test]
            public void ToBeNotFalse_WhenValueIsFalse_ShouldThrow()
            {
                // Arrange
                var value = false;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.Not.False(),
                    Throws.Exception.InstanceOf<AssertionException>()
                );

                // Assert
            }

            [Test]
            public void ShouldWorkOnNullableBooleans()
            {
                // Arrange
                var value = false as bool?;

                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(value).To.Be.False(),
                    Throws.Nothing
                );

                // Assert
            }
        }
    }
}