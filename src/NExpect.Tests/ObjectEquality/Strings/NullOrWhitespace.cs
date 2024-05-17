using NExpect.Exceptions;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.ObjectEquality.Strings;

[TestFixture]
public class NullOrWhitespace
{
    [TestFixture]
    public class To
    {
        [TestFixture]
        public class Be
        {
            [TestFixture]
            public class NullOrWhitespace
            {
                [Test]
                public void WhenIsEmpty_ShouldNotThrow()
                {
                    // Arrange
                    var input = "";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null.Or.Whitespace();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void WhenIsWhitespace_ShouldNotThrow()
                {
                    // Arrange
                    var input = "  ";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null.Or.Whitespace();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void WhenIsNull_ShouldNotThrow()
                {
                    // Arrange
                    var input = null as string;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null.Or.Whitespace();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Negated_WhenIsNotEmpty_ShouldNotThrow()
                {
                    // Arrange
                    var input = "123";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null.Or.Whitespace();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void WhenIsNotEmpty_ShouldThrow()
                {
                    // Arrange
                    var input = "123";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null.Or.Whitespace("Le Error");
                        },
                        Throws.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Le Error"));
                    // Assert
                }

                [Test]
                public void Negated_WhenIsEmpty_ShouldThrow()
                {
                    // Arrange
                    var input = "";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null.Or.Whitespace();
                        },
                        Throws.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void Negated_WhenIsWhitespace_ShouldThrow()
                {
                    // Arrange
                    var input = "  ";
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null.Or.Whitespace();
                        },
                        Throws.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void Negated_WhenIsNull_ShouldThrow()
                {
                    // Arrange
                    var input = null as string;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null.Or.Whitespace();
                        },
                        Throws.InstanceOf<UnmetExpectationException>());
                    // Assert
                }
            }
        }
    }
}
