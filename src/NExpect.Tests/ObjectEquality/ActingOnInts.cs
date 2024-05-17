using System;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable RedundantArgumentDefaultValue
namespace NExpect.Tests.ObjectEquality;

[TestFixture]
public class ActingOnInts
{
    [TestFixture]
    public class Greater
    {
        [TestFixture]
        public class Than
        {
            [Test]
            public void WhenActualIsEqualToExpected_ShouldThrow()
            {
                // Arrange
                var actual = 8;
                var expected = 8;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Greater.Than(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("8 to be greater than 8"));
                // Assert
            }

            [Test]
            public void WhenActualIsLessThanToExpected_ShouldThrow()
            {
                // Arrange
                var actual = 7;
                var expected = 8;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Be.Greater.Than(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("7 to be greater than 8"));
                // Assert
            }

            [Test]
            public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
            {
                // Arrange
                var actual = RandomValueGen.GetRandomInt(5, 10);
                var expected = RandomValueGen.GetRandomInt(0, 4);

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
            public void GreaterThan_ShouldWorkWithDecimals()
            {
                // Arrange
                decimal start = 0.6M;
                decimal test = 0.5M;
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
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
                Assert.That(
                    () =>
                    {
                        Expect(start).To.Be.Greater.Than(test);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
            {
                // Arrange
                var actual = RandomValueGen.GetRandomInt(5, 10);

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
                var actual = RandomValueGen.GetRandomInt(5, 10);
                var expected = RandomValueGen.GetRandomInt(11, 20);

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
            public void GreaterThan_Negated_WhenActualIsGreaterThanExpected_ShouldThrow()
            {
                // Arrange
                var actual = RandomValueGen.GetRandomInt(5, 10);
                var expected = RandomValueGen.GetRandomInt(1, 4);
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
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
                var actual = RandomValueGen.GetRandomInt(5, 10);
                var expected = RandomValueGen.GetRandomInt(1, 4);
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual).To.Not.Be.Greater.Than(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class And
        {
            [TestFixture]
            public class Less
            {
                [TestFixture]
                public class Than
                {
                    [TestFixture]
                    public class HomogenousTypes
                    {
                        [TestFixture]
                        public class IntIntInt
                        {
                            private (int min, int max, int actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class LongLongLong
                        {
                            private (long min, long max, long actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DecimalDecimalDecimal
                        {
                            private (decimal min, decimal max, decimal actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class FloatFloatFloat
                        {
                            private (float min, float max, float actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DoubleDoubleDouble
                        {
                            private (double min, double max, double actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DateTimeDateTimeDateTime
                        {
                            private (DateTime min, DateTime max, DateTime actual) Source()
                            {
                                var min = RandomValueGen.GetRandomDate(new DateTime(2001, 1, 1));
                                var max = RandomValueGen.GetRandomDate(new DateTime(2030, 1, 1));
                                var actual = RandomValueGen.GetRandomDate(
                                    min.AddMilliseconds(1),
                                    max.AddMilliseconds(-1)
                                );
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void ShouldTakeDateTimeKindIntoAccount()
                            {
                                // Arrange
                                var src = RandomValueGen.GetRandomDate();
                                var local =
                                    new DateTime(
                                        src.Year,
                                        src.Month,
                                        src.Day,
                                        src.Hour,
                                        src.Minute,
                                        src.Second,
                                        DateTimeKind.Local);
                                var utc =
                                    new DateTime(
                                        src.Year,
                                        src.Month,
                                        src.Day,
                                        src.Hour,
                                        src.Minute,
                                        src.Second,
                                        DateTimeKind.Utc);
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(local).To.Equal(utc);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }
                    }

                    [TestFixture]
                    public class HeterogeneousTypes
                    {
                        [TestFixture]
                        public class DoubleDoubleDecimal
                        {
                            private (double min, double max, decimal actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class LongLongDecimal
                        {
                            private (long min, long max, decimal actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DecimalDecimalLong
                        {
                            private (decimal min, decimal max, long actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DoubleDoubleLong
                        {
                            private (double min, double max, long actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class DecimalDecimalDouble
                        {
                            private (decimal min, decimal max, double actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class LongLongDouble
                        {
                            private (long min, long max, double actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }

                        [TestFixture]
                        public class IntDoubleFloat
                        {
                            private (int min, double max, float actual) Source()
                            {
                                var min = RandomValueGen.GetRandomInt(1, 5);
                                var max = RandomValueGen.GetRandomInt(10, 15);
                                var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                return (min, max, actual);
                            }

                            [Test]
                            public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .Not.To.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                            {
                                // Arrange
                                var (min, max, actual) = Source();
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(actual)
                                            .To.Not.Be.Greater.Than(min)
                                            .And.Less.Than(max);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }
                    }
                }
            }
        }
    }

    [TestFixture]
    public class LessThan
    {
        [Test]
        public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
        {
            // Arrange
            var actual = RandomValueGen.GetRandomInt(1, 5);
            var expected = RandomValueGen.GetRandomInt(6, 12);
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
            var actual = RandomValueGen.GetRandomInt(1, 5);
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
            var actual = RandomValueGen.GetRandomInt(1, 5);
            var expected = RandomValueGen.GetRandomInt(-5, 0);
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
        public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
        {
            // Arrange
            var actual = RandomValueGen.GetRandomInt(1, 5);
            var expected = RandomValueGen.GetRandomInt(-5, 0);
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
