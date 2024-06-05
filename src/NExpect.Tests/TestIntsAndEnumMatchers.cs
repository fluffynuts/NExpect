using NExpect.Exceptions;
using NUnit.Framework;

// ReSharper disable RedundantCast

namespace NExpect.Tests;

[TestFixture]
public class TestIntsAndEnumMatchers
{
    [TestFixture]
    public class ComparingEnumsToInts
    {
        [Test]
        public void ShouldBeAbleToAssertEqualityEasily()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .To.Equal(1);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .Not.To.Equal(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .To.Not.Equal(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.Two)
                        .To.Equal(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldStillBeAbleToEasilyCompareLongs()
        {
            // Arrange
            var value = 1L;

            // Act
            Assert.That(
                () =>
                {
                    Expect(value)
                        .To.Equal(1M);
                    Expect(value)
                        .To.Equal(1f);
                    Expect(value)
                        .To.Equal((double)1);
                    Expect(value)
                        .To.Equal((short)1);

                    Expect(value)
                        .Not.To.Equal(2M);
                    Expect(value)
                        .Not.To.Equal(2f);
                    Expect(value)
                        .Not.To.Equal((double)2);
                    Expect(value)
                        .Not.To.Equal((short)2);

                    Expect(value)
                        .To.Not.Equal(2M);
                    Expect(value)
                        .To.Not.Equal(2f);
                    Expect(value)
                        .To.Not.Equal((double)2);
                    Expect(value)
                        .To.Not.Equal((short)2);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldStillBeAbleToEasilyCompareInts()
        {
            // Arrange
            var value = (int)1;
            // Act
            Assert.That(
                () =>
                {
                    Expect(value)
                        .To.Equal(1M);
                    Expect(value)
                        .To.Equal(1f);
                    Expect(value)
                        .To.Equal((double)1);
                    Expect(value)
                        .To.Equal((short)1);

                    Expect(value)
                        .Not.To.Equal(2M);
                    Expect(value)
                        .Not.To.Equal(2f);
                    Expect(value)
                        .Not.To.Equal((double)2);
                    Expect(value)
                        .Not.To.Equal((short)2);

                    Expect(value)
                        .To.Not.Equal(2M);
                    Expect(value)
                        .To.Not.Equal(2f);
                    Expect(value)
                        .To.Not.Equal((double)2);
                    Expect(value)
                        .To.Not.Equal((short)2);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldAlwaysFailIfExpectedValueIsOutOfRange()
        {
            // Arrange
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .To.Equal(-1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .Not.To.Equal(-1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .To.Equal(20);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(Numbers.One)
                        .Not.To.Equal(20);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            // Act
            // Assert
        }
    }

    [TestFixture]
    public class ComparingIntsToEnums
    {
        [Test]
        public void ShouldBeAbleToAssertEqualityEasily()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(1)
                        .To.Equal(Numbers.One);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(1)
                        .Not.To.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(1)
                        .To.Not.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(1)
                        .To.Equal(Numbers.Two);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldAlwaysFailIfExpectedValueIsOutOfRange()
        {
            // Arrange
            Assert.That(
                () =>
                {
                    Expect(-1)
                        .To.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(-1)
                        .Not.To.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(20)
                        .To.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            Assert.That(
                () =>
                {
                    Expect(20)
                        .Not.To.Equal(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("cannot be mapped onto a pure value")
            );
            // Act
            // Assert
        }
    }

    public enum Numbers
    {
        One = 1,
        Two = 2,
        Three = 3
    }
}