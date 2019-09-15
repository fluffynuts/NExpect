using System;
using System.Collections.Generic;
using System.Globalization;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Tests.Collections;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingObjectEquality
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
                            .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                            .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                    Assert.That(
                        () =>
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
                    Assert.That(
                        () =>
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
                    Assert.That(
                        () =>
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
                    Assert.That(
                        () =>
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
                    Assert.That(
                        () =>
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
                    Assert.That(
                        () =>
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
                                .With.Message.Contains($"Expected\n\"{expected}\"\nbut got\n{actual}")
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
                                .With.Message.Contains($"Expected\n(null)\nbut got\n\"{actual}\"")
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

                [TestFixture]
                public class LongStuff
                {
                    [Test]
                    public void ShouldSplitLongStringsForEasierComparison()
                    {
                        // Arrange
                        var left = GetRandomString(72, 128);
                        var right = GetRandomString(72, 128);
                        // Pre-Assert
                        // Act
                        var ex = Assert.Throws<UnmetExpectationException>(
                            () => Expect(left).To.Equal(right)
                        );
                        // Assert
                        Expect(ex.Message)
                            .To.Start.With("Expected")
                            .And.Contain("\n")
                            .Then(right)
                            .Then("\n")
                            .Then("but got")
                            .Then("\n")
                            .Then(left);
                    }

                    [Test]
                    public void ShouldSplitLongObjectsToo()
                    {
                        // Arrange
                        var left = new
                        {
                            SomeLongProperty = GetRandomString(64)
                        };
                        var right = new
                        {
                            SomeLongProperty = GetAnother(left.SomeLongProperty)
                        };
                        // Pre-Assert
                        // Act
                        var ex = Assert.Throws<UnmetExpectationException>(
                            () => Expect(left).To.Equal(right)
                        );
                        // Assert
                        var parts = ex.Message.Split('\n');
                        Expect(parts).To.Contain.Exactly(8).Items();
                    }

                    [Test]
                    public void ShouldSplitLongObjectsForDeepEqualityToo()
                    {
                        // Arrange
                        var left = new
                        {
                            SomeLongProperty = GetRandomString(64)
                        };
                        var right = new
                        {
                            SomeLongProperty = GetAnother(left.SomeLongProperty)
                        };
                        // Pre-Assert
                        // Act
                        var ex = Assert.Throws<UnmetExpectationException>(
                            () => Expect(left).To.Deep.Equal(right)
                        );
                        // Assert
                        var beforeDiagnostics = ex.Message.IndexOf(
                            "Property value mismatch", 
                            StringComparison.InvariantCulture
                        );
                        if (beforeDiagnostics == -1)
                        {
                            // just in case diagnostics disappear -- shouldn't break this test
                            beforeDiagnostics = ex.Message.Length;
                        }

                        var testMessage = ex.Message.Substring(0, beforeDiagnostics).Trim();
                        
                        var parts = testMessage.Split('\n');
                        Expect(parts).To.Contain.Exactly(8).Items();
                    }
                }
            }

            [TestFixture]
            public class Intersection
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

                    public class OtherNamedIdentifier
                    {
                        public int Id { get; }
                        public string Name { get; }
                        public string Type => GetType().Name;

                        public OtherNamedIdentifier(int id, string name)
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
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Intersection.Equal(right);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    // TODO: it would be nice if the message could clarify that there
                    //  are no intersecting properties
                    [Test]
                    public void WhenNoPropertiesInCommon_ShouldThrow()
                    {
                        // Arrange
                        var left = new {moo = "cow"};
                        var right = new {cow = "cake"};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Intersection.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void NegativeResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(2, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Intersection.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).Not.To.Intersection.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_AltGrammar_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Not.Intersection.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void WithCustomEqualityComparer()
                    {
                        // Arrange
                        var left = new {Date = DateTime.Now};
                        var right = new {Date = DateTime.Now.AddSeconds(-1)};
                        // Pre-assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Intersection.Equal(
                                    right,
                                    new DriftingDateTimeEqualityComparer()
                                );
                            },
                            Throws.Nothing);
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
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Deep.Equal(right);
                            },
                            Throws.Nothing);
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
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Deep.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
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
                        Assert.That(
                            () =>
                            {
                                Expect(left).Not.To.Deep.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
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
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Not.Deep.Equal(right);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void WithCustomEqualityComparer()
                    {
                        // Arrange
                        var left = new {Date = DateTime.Now};
                        var right = new {Date = DateTime.Now.AddSeconds(-1)};
                        // Pre-assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Deep.Equal(
                                    right,
                                    new DriftingDateTimeEqualityComparer());
                            },
                            Throws.Nothing);
                        // Assert
                    }
                }

                [TestFixture]
                public class Issues
                {
                    [Test]
                    public void DeepEqualityTestingBetweenShorts_ShouldNotFailWhenTheyAreEqual()
                    {
                        // Arrange
                        var left = new {x = (short) 1};
                        var right = new {x = (short) 1};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Deep.Equal(right);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void IntersectionEqualityTestingBetweenShortAndIntProperty_ShouldNotFailWhenTheyAreEqual()
                    {
                        // Arrange
                        var left = new {x = short.MaxValue};
                        var right = new {x = (int) short.MaxValue};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(left).To.Intersection.Equal(right);
                            },
                            Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(str).To.Match(s => s == str);
                    },
                    Throws.Nothing);
                Assert.That(
                    () =>
                    {
                        Expect(str).To.Match(s => s == str, "looking for: " + str);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_WithSimpleBooleanReturn_ShouldThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(str).To.Match(s => s != str, "looking for: !" + str);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_Negated_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(str).Not.To.Match(s => s != str);
                    },
                    Throws.Nothing);
                Assert.That(
                    () =>
                    {
                        Expect(str).Not.To.Match(s => s != str, "looking for: !" + str);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_NegatedAlt_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(str).To.Not.Match(s => s != str);
                    },
                    Throws.Nothing);
                Assert.That(
                    () =>
                    {
                        Expect(str).To.Not.Match(s => s != str, "looking for: !" + str);
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class ActingOnTimespans
        {
            [TestFixture]
            public class Greater
            {
                [TestFixture]
                public class Than
                {
                }
            }
        }

        [TestFixture]
        public class ActingOnDateTimes
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(null, actual.AddTicks(-1));

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
                    var actual = GetRandomDate();

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
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(actual.AddTicks(1), null);

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
                public void GreaterThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(actual.AddTicks(1), null);
                    // Pre-Assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actual).Not.To.Be.Greater.Than(expected);
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
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(actual.AddTicks(1), null);
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
                    var actual = GetRandomDate();
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
                            .With.Message.Contains($"{actual.Stringify()} to be less than {expected.Stringify()}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(null, actual.AddTicks(-1));
                    // Pre-Assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual.Stringify()} to be less than {expected.Stringify()}"));
                    // Assert
                }

                [Test]
                public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomDate();
                    var expected = GetRandomDate(null, actual.AddTicks(-1));
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
                    var actual = GetRandomLong(5, 10);

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
                    var actual = GetRandomLong(5, 10);
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

                [TestFixture]
                public class OrEqualTo
                {
                    [TestFixture]
                    public class Longs
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class NullableLongs
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }
                    [TestFixture]
                    public class LongsToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                   
                    
                    [TestFixture]
                    public class NullableLongsToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                   
                    
                    [TestFixture]
                    public class LongsToDoubles
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                   
                    
                    [TestFixture]
                    public class NullableLongsToDoubles
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 5D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as long?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                   
                    
                    [TestFixture]
                    public class DecimalsToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomDecimal(5, 10);
                            var expected = 5M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                    
                                        
                    [TestFixture]
                    public class NullableDecimalsToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomDecimal(5, 10);
                            var expected = 5M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as decimal?).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual as decimal?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }                    
                    
                    [TestFixture]
                    public class DoublesToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomFloat(5, 10);
                            var expected = 5M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class NullableDoublesToDecimals
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = (double)(GetRandomFloat(5, 10));
                            var expected = 5;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as double?).To.Be.Greater.Than.Or.Equal.To(actual);
                                    Expect(actual as double?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = (double)GetRandomLong(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual as double?).To.Be.Greater.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class DateTimes
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.To).To.Be.Greater.Than.Or.Equal.To(range.To);
                                    Expect(range.To).To.Be.Greater.Than.Or.Equal.To(range.From);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.From).To.Be.Greater.Than.Or.Equal.To(range.To);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }

                        [Test]
                        public void ContinuingWithMore()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.From).To.Be.Greater.Than.Or.Equal.To(range.From)
                                        .And.To.Be.Less.Than.Or.Equal.To(range.To);
                                }, Throws.Nothing);
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class NullableDateTimes
                    {
                        [Test]
                        public void GreaterThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.To as DateTime?).To.Be.Greater.Than.Or.Equal.To(range.To);
                                    Expect(range.To as DateTime?).To.Be.Greater.Than.Or.Equal.To(range.From);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void GreaterThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.From as DateTime?).To.Be.Greater.Than.Or.Equal.To(range.To);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            Assert.That(
                                () => 
                                {
                                    Expect(null as DateTime?).To.Be.Greater.Than.Or.Equal.To(range.To);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("greater than or equal to"));
                            // Assert
                        }
                    }
                }
            }

            [TestFixture]
            public class LessThan
            {
                [TestFixture]
                public class OrEqualTo
                {
                    [TestFixture]
                    public class Longs
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 10;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 4;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class LongsToDecimals
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 4M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class DecimalsToDecimals
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomDecimal(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 4M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class DoublesToDecimals
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomFloat(5, 10);
                            var expected = 11M;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 4;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }
                    
                    [TestFixture]
                    public class LongsToDoubles
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 11D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(actual);
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var actual = GetRandomLong(5, 10);
                            var expected = 4D;
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(actual).To.Be.Less.Than.Or.Equal.To(expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class DateTimes
                    {
                        [Test]
                        public void LessThanOrEqualTo_PositiveResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.From).To.Be.Less.Than.Or.Equal.To(range.From);
                                    Expect(range.From).To.Be.Less.Than.Or.Equal.To(range.To);
                                },
                                Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void LessThanOrEqualTo_NegativeResult()
                        {
                            // Arrange
                            var range = GetRandomDateRange();
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(range.To).To.Be.Less.Than.Or.Equal.To(range.From);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("less than or equal to"));
                            // Assert
                        }
                    }
                }

                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(1, 5);
                    var expected = GetRandomInt(6, 12);
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
                    var actual = GetRandomInt(1, 5);
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
                        .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                        .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                        .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                        .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                Assert.That(
                    () =>
                    {
                        Expect(instance).To.Be(instance);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(instance).To.Be(other);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                Assert.That(
                    () =>
                    {
                        Expect(instance).Not.To.Be(instance);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                Assert.That(
                    () =>
                    {
                        Expect(instance).Not.To.Be(other);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_ActingOnCollection_WhenRefEqual_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<int>();
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(collection).To.Be(collection);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(collection).To.Be(other);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());
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
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected\n\"{expected}\"\nbut got\n\"{actual}\"")
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
                Assert.That(
                    () =>
                    {
                        Expect(value).To.Not.Equal(value);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Did not expect\n\"{value}\"\nbut got exactly that")
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
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected\n\"{expected}\"\nbut got\n(null)")
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
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected\n{}\nbut got\n(null)")
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
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected\n{expected}\nbut got\n{actual}")
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
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected\n{}\nbut got\n{}")
                );
                // Assert
            }
        }
    }
}