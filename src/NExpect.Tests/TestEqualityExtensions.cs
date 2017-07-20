using NExpect.Extensions;
using NUnit.Framework;
using static NExpect.Implementations.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestEqualityExtensions
    {
        public class Equality
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
                    Throws.Exception.InstanceOf<AssertionException>()
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
                    Throws.Exception.InstanceOf<AssertionException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                Assert.That(
                    () => Expect(actual).To.Equal(expected, custom),
                    Throws.Exception.InstanceOf<AssertionException>()
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
                Assert.That(() => { Expect(value).To.Be.Equal.To(value); }, Throws.Nothing);

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
                Assert.That(() => { Expect(value).To.Be.Equal.To(expected); },
                    Throws.Exception.InstanceOf<AssertionException>());

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
                Assert.That(() => { Expect(value).Not.To.Be.Equal.To(unexpected); }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void AlternativeEqualGrammar_Negated_SadPath()
            {
                // Arrange
                var value = GetRandomInt();
                // Pre-Assert

                // Act
                Assert.That(() => { Expect(value).Not.To.Be.Equal.To(value); },
                    Throws.Exception.InstanceOf<AssertionException>());

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
                Assert.That(() => { Expect(value).To.Not.Be.Equal.To(unexpected); }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void AlternativeEqualGrammar_AltNegated_SadPath()
            {
                // Arrange
                var value = GetRandomInt();
                // Pre-Assert

                // Act
                Assert.That(() => { Expect(value).To.Not.Be.Equal.To(value); },
                    Throws.Exception.InstanceOf<AssertionException>());

                // Assert
            }
        }

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
                }, Throws.Nothing);
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
                }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains("Expected not to get null"));
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
                }, Throws.Exception.InstanceOf<AssertionException>()
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
                }, Throws.Exception.InstanceOf<AssertionException>()
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
                }, Throws.Nothing);
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
                }, Throws.Nothing);
                // Assert
            }
        }

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomInt(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(actual); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).Not.To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());
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
                    Assert.That(() => { Expect(actual).To.Not.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
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
                    Assert.That(() => { Expect(actual).Not.To.Be.Less.Than(expected); }, Throws.Nothing);
                    // Assert
                }
            }
        }

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomDecimal(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(actual); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).Not.To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomDouble(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(actual); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(actual); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomLong(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(actual); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Greater.Than(expected); },
                        Throws.Exception.InstanceOf<AssertionException>());

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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
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
                    Assert.That(() => { Expect(actual).To.Be.Less.Than(expected); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }
    }
}