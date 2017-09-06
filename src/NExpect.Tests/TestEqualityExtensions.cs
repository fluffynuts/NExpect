using System;
using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests
{
    [TestFixture]
    public class TestEqualityExtensions
    {
        [TestFixture]
        public class To
        {
            public class Equal
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
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"Expected {expected} but got {actual}")
                    );
                    Assert.That(
                        () => Expect(actual).To.Equal(expected, custom),
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
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


                [Test]
                public void AlternativeEqualGrammar_HappyPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).To.Be.Equal.To(value);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_SadPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    var expected = GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).To.Be.Equal.To(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_Negated_HappyPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    var unexpected = GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).Not.To.Be.Equal.To(unexpected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_Negated_SadPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).Not.To.Be.Equal.To(value);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_AltNegated_HappyPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    var unexpected = GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).To.Not.Be.Equal.To(unexpected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_AltNegated_SadPath()
                {
                    // Arrange
                    var value = GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(value).To.Not.Be.Equal.To(value);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [TestFixture]
                public class ActingOnNulls
                {
                    [Test]
                    public void GivenActualIsNull_WhenDoesNotMatch_ShouldThrow_WithValidMessage()
                    {
                        // Arrange
                        string actual = null;
                        string expected = "1";
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expect(actual).To.Equal(expected),
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected \"{expected}\" but got {actual}")
                        );
                        // Assert
                    }

                    [Test]
                    public void GivenExpectationIsNull_WhenDoesNotMatch_ShouldThrow_WithValidMessage()
                    {
                        // Arrange
                        string actual = "1";
                        string expected = null;
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expect(actual).To.Equal(expected),
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected (null) but got \"{actual}\"")
                        );
                        // Assert
                    }

                    [Test]
                    public void GivenActualAndExpectationAreNull_WhenMatches_ShouldNotThrow()
                    {
                        // Arrange
                        string actual = null;
                        string expected = null;
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expect(actual).To.Equal(expected),
                            Throws.Nothing
                        );
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Deep
            {
                [TestFixture]
                public class Equal
                {
                    public class NamedIdentifier
                    {
                        public int Id { get; }
                        public string Name { get; }
                        public NamedIdentifier(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    [Test]
                    public void PositiveResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expect(left).To.Deep.Equal(right);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(2, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expect(left).To.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expect(left).Not.To.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_AltGrammar_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expect(left).To.Not.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }
            }
        }

        public class Match
        {
            [Test]
            public void WhenMatches_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(str).To.Match(s => s == str, "looking for: " + str);
                }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_WithSimpleBooleanReturn_ShouldThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(str).To.Match(s => s != str, "looking for: !" + str);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_Negated_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(str).Not.To.Match(s => s != str, "looking for: !" + str);
                }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_NegatedAlt_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(str).To.Not.Match(s => s != str, "looking for: !" + str);
                }, Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class Be
        {
            [TestFixture]
            public class Null
            {
                [Test]
                public void OperatingOnString_WhenIsNull_ShouldNotThrow()
                {
                    // Arrange
                    var input = null as string;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void OperatingOnStringNegated_WhenIsNull_ShouldThrow()
                {
                    // Arrange
                    var input = null as string;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).Not.To.Be.Null();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected not to get null"));
                    // Assert
                }

                [Test]
                public void OperatingOnStringNegated_GivenCustomMessage_WhenIsNull_ShouldThrowIncludingCustomMessage()
                {
                    // Arrange
                    var input = null as string;
                    var expected = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).Not.To.Be.Null(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.EqualTo($"{expected}\n\nExpected not to get null"));
                    // Assert
                }

                [Test]
                public void ExpectOnPureNull()
                {
                    // Arrange
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(null).To.Be.Null();
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void OperatingOnObjectAltNegated_WhenIsNull_ShouldThrow()
                {
                    // Arrange
                    var input = null as object;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected not to get null"));
                    // Assert
                }

                [Test]
                public void OperatingOnString_WhenIsNotNull_ShouldThrow()
                {
                    // Arrange
                    var input = GetRandomString();
                    Assert.That(input, Is.Not.Null);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Be.Null();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"Expected null but got \"{input}\""));
                    // Assert
                }

                [Test]
                public void OperatingOnString_Negated_WhenIsNotNull_ShouldNotThrow()
                {
                    // Arrange
                    var input = GetRandomString();
                    Assert.That(input, Is.Not.Null);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).Not.To.Be.Null();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void OperatingOnString_AltNegated_WhenIsNotNull_ShouldNotThrow()
                {
                    // Arrange
                    var input = GetRandomString();
                    Assert.That(input, Is.Not.Null);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(input).To.Not.Be.Null();
                        },
                        Throws.Nothing);
                    // Assert
                }
            }
        }

        [TestFixture]
        public class Be_ActingOnStrings
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
                            Expect(input).To.Be.Empty();
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
                            Expect(input).To.Not.Be.Empty();
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
                            Expect(input).To.Be.Empty();
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
                            Expect(input).To.Not.Be.Empty();
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
                            Expect(input).To.Be.Null.Or.Empty();
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
                            Expect(input).To.Be.Null.Or.Empty();
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
                            Expect(input).To.Not.Be.Null.Or.Empty();
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
                            Expect(input).Not.To.Be.Null.Or.Empty();
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
                            Expect(input).To.Be.Null.Or.Empty();
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
                            Expect(input).To.Not.Be.Null.Or.Empty();
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
                            Expect(input).To.Not.Be.Null.Or.Empty();
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
                            Expect(input).To.Be.Null.Or.Whitespace();
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

        [TestFixture]
        public class ActingOnInts
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(5, 10);
                    var expected = GetRandomInt(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDecimals()
                {
                    // Arrange
                    decimal start = 0.6M;
                    decimal test = 0.5M;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDecimalsAndDouble()
                {
                    // Arrange
                    decimal start = 0.6M;
                    double test = 0.5;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDoublesAndDecimal()
                {
                    // Arrange
                    double start = 0.6;
                    decimal test = 0.5M;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDoublesAndFloat()
                {
                    // Arrange
                    double start = 0.6;
                    float test = 0.5F;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDoublesAndLong()
                {
                    // Arrange
                    double start = 0.6;
                    long test = -1;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithFloatAndDecimal()
                {
                    // Arrange
                    float start = 0.6F;
                    decimal test = 0.5M;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithFloatAndLong()
                {
                    // Arrange
                    double start = 0.6;
                    long test = 0;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithLongAndDecimal()
                {
                    // Arrange
                    long start = 1;
                    decimal test = 0;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithLongAndDouble()
                {
                    // Arrange
                    long start = 1;
                    double test = 0;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDecimalsAndFloat()
                {
                    // Arrange
                    decimal start = 0.6M;
                    float test = 0.5F;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDecimalsAndInt()
                {
                    // Arrange
                    decimal start = 0.6M;
                    int test = 0;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_ShouldWorkWithDecimalsAndLong()
                {
                    // Arrange
                    decimal start = 0.6M;
                    long test = 0;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    }, Throws.Nothing);
                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(5, 10);
                    var expected = GetRandomInt(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_Negated_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(5, 10);
                    var expected = GetRandomInt(1, 4);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).Not.To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void GreaterThan_AltNegated_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(5, 10);
                    var expected = GetRandomInt(1, 4);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).To.Not.Be.Greater.Than(expected);
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
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).Not.To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnDecimals
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomDecimal(5, 10);
                    var expected = GetRandomDecimal(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDecimal(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDecimal(5, 10);
                    var expected = GetRandomDecimal(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var expected = GetRandomDecimal(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var expected = GetRandomDecimal(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDecimal(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDecimal(1, 5);
                    var expected = GetRandomDecimal(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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

        [TestFixture]
        public class ActingOnDoubles
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomDouble(5, 10);
                    var expected = GetRandomDouble(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDouble(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDouble(5, 10);
                    var expected = GetRandomDouble(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDouble(1, 5);
                    var expected = GetRandomDouble(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDouble(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomDouble(1, 5);
                    var expected = GetRandomDouble(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    Assert.That(() =>
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
                    Assert.That(() =>
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
                    Assert.That(() =>
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
                    Assert.That(() =>
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
                    Assert.That(() =>
                        {
                            Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{(double) actual} to be less than {(double) expected}"));
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
                    Assert.That(() =>
                        {
                            Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{(double) actual} to be less than {(double) expected}"));
                    // Assert
                }
            }
        }

        private static float GetRandomFloat(float min, float max)
        {
            return (float) GetRandomDouble(min, max);
        }

        private static long GetRandomLong(long min, long max)
        {
            return GetRandomInt((int) min, (int) max);
        }

        [TestFixture]
        public class ActingOnLongs
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomLong(5, 10);
                    var expected = GetRandomLong(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomLong(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomLong(5, 10);
                    var expected = GetRandomLong(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
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

        [TestFixture]
        public class ActingOnMismatchedTypes
        {
            [Test]
            public void Expect_Byte_ToEqual_Int_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                byte actual = 1;
                int expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Byte_ToEqual_Int_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                byte actual = 1;
                int expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Short_ToEqual_Long_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                short actual = 1;
                long expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Short_ToEqual_Long_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                short actual = 1;
                long expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Int_ToEqual_Long_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                int actual = 1;
                long expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Int_ToEqual_Long_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                int actual = 1;
                long expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Float_ToEqual_Double_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                float actual = 1.1f;
                double expected = 1.1f;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Float_ToEqual_Double_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                float actual = 1.1f;
                double expected = 1.2f;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }
        }

        [TestFixture]
        public class ReferenceEqualityTesting
        {
            public class Coordinate
            {
                int X { get; }
                int Y { get; }

                public Coordinate(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public override int GetHashCode()
                {
                    return $"{X}-{Y}".GetHashCode();
                }

                public override bool Equals(object obj)
                {
                    var other = obj as Coordinate;
                    if (other == null)
                        return false;
                    return other.X == X && other.Y == Y;
                }
            }

            [Test]
            public void Be_WhenHaveSameRef_ShouldNotThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(instance).To.Be(instance);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_WhenHaveDifferentRefButAreEqual_ShouldThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                var other = new Coordinate(2, 3);
                // Pre-Assert
                Assert.That(instance, Is.EqualTo(other));
                // Act
                Assert.That(() =>
                {
                    Expect(instance).To.Be(other);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.EqualTo($"Expected {instance} to be the same reference as {other}"));
                // Assert
            }

            [Test]
            public void Be_Negated_WhenHaveSameRef_ShouldThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(instance).Not.To.Be(instance);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.EqualTo($"Expected {instance} not to be the same reference as {instance}"));
                // Assert
            }

            [Test]
            public void Be_Negated_WhenHaveDifferentRefButAreEqual_ShouldNotThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                var other = new Coordinate(2, 3);
                // Pre-Assert
                Assert.That(instance, Is.EqualTo(other));
                // Act
                Assert.That(() =>
                {
                    Expect(instance).Not.To.Be(other);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_ActingOnCollection_WhenRefEqual_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<int>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(collection).To.Be(collection);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_ActingOnCollection_WhenNotRefEqual_ShouldThrow()
            {
                // Arrange
                var collection = new List<int>();
                var other = new List<int>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(collection).To.Be(other);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class UnmetExpectationMessageTesting
        {
            [Test]
            public void GivenStrings_WhenNotMatched_ShouldThrow_WithQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = GetRandomString();
                var expected = GetAnother(actual);
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected \"{expected}\" but got \"{actual}\"")
                );
                // Assert
            }

            [Test]
            public void GivenStrings_WhenMatchedAndNegated_ShouldThrow_WithQuotesAroundValuesInMessage()
            {
                // Arrange
                var value = GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(value).To.Not.Equal(value);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Did not expect \"{value}\", but got exactly that")
                );
                // Assert
            }

            [Test]
            public void GivenStringAndNullString_WhenNotMatched_ShouldThrow_WithNullIdentifierInMessage()
            {
                // Arrange
                string actual = null;
                var expected = GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected \"{expected}\" but got (null)")
                );
                // Assert
            }

            [Test]
            public void GivenObjectAndNullObject_WhenNotMatched_ShouldThrow_WithNullIdentifierInMessage()
            {
                // Arrange
                object actual = null;
                var expected = new object();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected {} but got (null)")
                );
                // Assert
            }

            [Test]
            public void GivenInts_WhenNotMatched_ShouldThrow_WithoutQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = GetRandomInt();
                var expected = GetAnother(actual);
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void GivenObjects_WhenNotMatched_ShouldThrow_WithoutQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = new object();
                var expected = new object();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected {} but got {}")
                );
                // Assert
            }
        }
    }
}