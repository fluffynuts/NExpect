using NExpect.Exceptions;
using NUnit.Framework;

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
