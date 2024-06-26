using System;
using NExpect.Exceptions;
using NUnit.Framework;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantArgumentDefaultValue
namespace NExpect.Tests.ObjectEquality;

[TestFixture]
public class ActingOnNullableDecimals
{
    [TestFixture]
    public class GreaterThan
    {
        [Test]
        public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
        {
            // Arrange
            var actual = GetRandomDecimal(5, 10) as decimal?;
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
                    Expect(actual).To.Be.Greater.Than(actual);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
        {
            // Arrange
            var actual = GetRandomDecimal(5, 10) as decimal?;
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
            var actual = GetRandomDecimal(1, 5) as decimal?;
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
    public class GreaterThanLong
    {
        [Test]
        public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
        {
            // Arrange
            var actual = GetRandomDecimal(5, 10) as decimal?;
            var expected = GetRandomLong(0, 4);

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
            var actual = Convert.ToDecimal(GetRandomLong(5, 10)) as decimal?;
            var expected = (long)actual;

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
        public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
        {
            // Arrange
            var actual = GetRandomDecimal(5, 10) as decimal?;
            var expected = GetRandomLong(11, 20);

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
            var actual = GetRandomDecimal(1, 5) as decimal?;
            var expected = GetRandomLong(-5, 0);
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
            var actual = GetRandomDecimal(1, 5) as decimal?;
            var expected = GetRandomLong(6, 12);
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
            var actual = Convert.ToDecimal(GetRandomLong(1, 5)) as decimal?;
            var expected = actual.Value;
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

        [Test]
        public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
        {
            // Arrange
            var actual = GetRandomDecimal(1, 5) as decimal?;
            var expected = GetRandomLong(-5, 0);
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
