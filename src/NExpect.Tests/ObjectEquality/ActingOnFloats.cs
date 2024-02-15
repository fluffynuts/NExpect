using NExpect.Exceptions;
using NUnit.Framework;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantArgumentDefaultValue
namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class ActingOnFloats
    {
        [TestFixture]
        public class GreaterThan
        {
            [Test]
            public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
            {
                // Arrange
                var actual = GetRandomFloat(5, 10);
                var expected = GetRandomFloat(0, 4);

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Greater.Than(expected);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomFloat(5, 10);

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Greater.Than(actual);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomFloat(5, 10);
                var expected = GetRandomFloat(11, 20);

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Greater.Than(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }
        }

        [TestFixture]
        public class LessThan
        {
            [Test]
            public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
            {
                // Arrange
                var actual = GetRandomFloat(1, 5);
                var expected = GetRandomFloat(6, 12);
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Less.Than(expected);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomFloat(1, 5);
                var expected = actual;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Less.Than(expected);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"{(double) actual} to be less than {expected}"));
                // Assert
            }

            [Test]
            public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomFloat(1, 5);
                var expected = GetRandomFloat(-5, 0);
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Less.Than(expected);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"{(double) actual} to be less than {expected}"));
                // Assert
            }
        }
    }
}