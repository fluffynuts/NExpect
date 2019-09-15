using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantArgumentDefaultValue
namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class ActingOnDecimalComparedWithNullableDecimal
    {
        [TestFixture]
        public class GreaterThan
        {
            [Test]
            public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
            {
                // Arrange
                var actual = GetRandomDecimal(5, 10);
                var expected = GetRandomDecimal(0, 4) as decimal?;

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
                var actual = GetRandomDecimal(5, 10) as decimal?;

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual.Value).To.Be.Greater.Than(actual.Value);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomDecimal(5, 10);
                var expected = GetRandomDecimal(11, 20) as decimal?;

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

            [Test]
            public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
            {
                // Arrange
                var actual = GetRandomDecimal(1, 5);
                var expected = GetRandomDecimal(-5, 0) as decimal?;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).Not.To.Be.Less.Than(expected);
                    },
                    Throws.Nothing);
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
                var actual = GetRandomDecimal(1, 5);
                var expected = GetRandomDecimal(6, 12) as decimal?;
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
                var actual = GetRandomDecimal(1, 5) as decimal?;
                var expected = actual;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual.Value).To.Be.Less.Than(expected);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
                // Assert
            }

            [Test]
            public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
            {
                // Arrange
                var actual = GetRandomDecimal(1, 5);
                var expected = GetRandomDecimal(-5, 0) as decimal?;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Less.Than(expected);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
                // Assert
            }
        }
    }
}