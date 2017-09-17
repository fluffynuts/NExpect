using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

// ReSharper disable ExpressionIsAlwaysNull

// ReSharper disable MemberHidesStaticFromOuterClass

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingStringValues
    {
        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Be
            {
                [TestFixture]
                public class Empty
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
                                Expectations.Expect(input).To.Be.Empty();
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
                                Expectations.Expect(input).To.Not.Be.Empty();
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
                                Expectations.Expect(input).To.Be.Empty();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
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
                                Expectations.Expect(input).To.Not.Be.Empty();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }

                [TestFixture]
                public class NullOrEmpty
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
                                Expectations.Expect(input).To.Be.Null.Or.Empty();
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
                                Expectations.Expect(input).To.Be.Null.Or.Empty();
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Empty();
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegatedAlt_WhenIsNotEmpty_ShouldNotThrow()
                    {
                        // Arrange
                        var input = "123";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(input).Not.To.Be.Null.Or.Empty();
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
                                Expectations.Expect(input).To.Be.Null.Or.Empty();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Empty();
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Empty();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }

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
                                Expectations.Expect(input).To.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Be.Null.Or.Whitespace();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Whitespace();
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
                                Expectations.Expect(input).To.Not.Be.Null.Or.Whitespace();
                            },
                            Throws.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }

                [TestFixture]
                public class Equal
                {
                    [TestFixture]
                    public class To
                    {
                        [Test]
                        public void WithCustomMessage()
                        {
                            // Arrange
                            var expected = RandomValueGen.GetRandomString();
                            var test = RandomValueGen.GetRandomString();
                            var nonMatch = RandomValueGen.GetAnother(test);
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expectations.Expect(test).To.Be.Equal.To(nonMatch, expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected));
                            // Assert
                        }
                    }
                }
            }

            [TestFixture]
            public class Contain
            {
                [Test]
                public void WhenActualContainsSearch_ShouldNotThrow()
                {
                    // Arrange
                    var actual = "cow-moo-cow";
                    var search = "moo";
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Contain(search);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void WhenActualDoesNotContainSearch_ShouldThrow()
                {
                    // Arrange
                    var actual = "cow-moo-cow";
                    var search = "foo";
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Contain(search);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected \"cow-moo-cow\" to contain \"foo\""));

                    // Assert
                }

                [TestFixture]
                public class And
                {
                    [Test]
                    public void WhenActualContainsBothBits_ShouldNotThrow()
                    {
                        // Arrange
                        var actual = "a-b-c";
                        var first = "a";
                        var second = "b";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Contain(first).And(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void WhenActualMissingFirstBit_ShouldThrow()
                    {
                        // Arrange
                        var actual = "a-b-c";
                        var first = "d";
                        var second = "b";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Contain(first).And(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("\"a-b-c\" to contain \"d\""));
                        // Assert
                    }

                    [Test]
                    public void WhenActualMissingSecondBit_ShouldThrow()
                    {
                        // Arrange
                        var actual = "a-b-c";
                        var first = "b";
                        var second = "f";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Contain(first).And(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("\"a-b-c\" to contain \"f\""));
                        // Assert
                    }

                    [TestFixture]
                    public class AndAgain
                    {
                        [Test]
                        public void ShouldKeepOnChecking_HappyPath()
                        {
                            // Arrange
                            var actual = "a-b-c";
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expectations.Expect(actual).To.Contain("a").And("b").And("c");
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void ShouldKeepOnChecking_SadPath()
                        {
                            // Arrange
                            var actual = "a-b-c";
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expectations.Expect(actual).To.Contain("a").And("b").And("d");
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("\"d\""));
                            // Assert
                        }
                    }
                }
            }
        }
    }
}