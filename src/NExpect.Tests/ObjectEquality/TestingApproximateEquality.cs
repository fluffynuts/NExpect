using System;
using System.Collections.Generic;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
using NExpect;
using NExpect.EqualityComparers;
using NExpect.Exceptions;
using PeanutButter.Utils;

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingApproximateEquality
    {
        [TestFixture]
        public class DateTimeValues
        {
            [Test]
            public void WhenWithinASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(-1000, 1000));
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d2)
                            .To.Approximately.Equal(d1);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenAtASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(1000);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d2)
                            .To.Approximately.Equal(d1);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenProvidedDrift_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var allowedDrift = TimeSpan.FromSeconds(2);
                var d2 = d1.AddMilliseconds(GetRandomInt(-2000, 2000));
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift);
                        Expect(d2)
                            .To.Approximately.Equal(d1, allowedDrift);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void
                WhenOutsideOneSecond_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(1001, 2000));
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, () => message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message
                        .Contains(
                            $"Expected {d1.Stringify()} to approximately equal {d2.Stringify()}"));
                // Assert
            }

            [Test]
            public void
                WhenOutsideAllowedDrift_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(501, 1000));
                var allowedDrift = TimeSpan.FromMilliseconds(500);
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift, message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately
                            .Equal(d2, allowedDrift, () => message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));
                
                var d1String = d1.Stringify();
                var d2String = d2.Stringify();
                
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message
                        .Contains(
                            $"Expected {d1String} to approximately equal {d2String}"));
                // Assert
            }
        }

        [TestFixture]
        public class NullableDateTimeValues
        {
            [Test]
            public void WhenWithinASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate() as DateTime?;
                var d2 = d1.Value.AddMilliseconds(GetRandomInt(-1000, 1000));
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d2)
                            .To.Approximately.Equal(d1.Value);
                    },
                    Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(d2)
                        .To.Approximately.Equal(d1);
                    Expect(d2)
                        .To.Approximately.Equal(d1.Value);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenAtASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate() as DateTime?;
                var d2 = d1.Value.AddMilliseconds(1000);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d2)
                            .To.Approximately.Equal(d1.Value);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenProvidedDrift_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate() as DateTime?;
                var allowedDrift = TimeSpan.FromSeconds(2);
                var d2 = d1.Value.AddMilliseconds(GetRandomInt(-2000, 2000));
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift);
                        Expect(d2)
                            .To.Approximately.Equal(d1.Value, allowedDrift);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void
                WhenOutsideOneSecond_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate() as DateTime?;
                var d2 = d1.Value.AddMilliseconds(GetRandomInt(1001, 2000));
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, () => message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message
                        .Contains(
                            $"Expected {d1.Stringify()} to approximately equal {d2.Stringify()}"));
                // Assert
            }

            [Test]
            public void
                WhenOutsideAllowedDrift_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate() as DateTime?;
                var d2 = d1.Value.AddMilliseconds(GetRandomInt(501, 1000));
                var allowedDrift = TimeSpan.FromMilliseconds(500);
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift, message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately
                            .Equal(d2, allowedDrift, () => message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(message));

                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, allowedDrift);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message
                        .Contains(
                            $"Expected {d1.Stringify()} to approximately equal {d2.Stringify()}"));
                // Assert
            }
        }

        [TestFixture]
        public class DecimalValues
        {
            [Test]
            public void ShouldDefaultToEqualityWithinTwoDecimalPlacesRounded()
            {
                // Arrange
                var d1 = 0.1234M;
                var d2 = 0.12456M;
                var d3 = 0.119M;
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d1)
                            .To.Approximately.Equal(d3);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldThrowWhenNotApproximatelyEqual()
            {
                // Arrange
                var d1 = 1.1M;
                var d2 = 1.2M;
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("approximately equal"));
                // Assert
            }

            [Test]
            public void ShouldAllowCustomEqualityTesting()
            {
                // Arrange
                var d1 = 1.5M;
                var d2 = 2.1M;
                var comparer = new DecimalsEqualToDecimalPlacesRounded(0);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, comparer);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldFacilitateSpecifyingTheAllowableDrift()
            {
                // Arrange
                var d1 = 1.10M;
                var d2 = 1.15M;
                
                // Act
                Assert.That(() =>
                {
                    Expect(d1)
                        .To.Approximately.Equal(d2, within: 0.1);
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(d1)
                        .To.Approximately.Equal(d2, within: 0.1M);
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(d1)
                        .To.Approximately.Equal(d2, within: 0.1f);
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(d1)
                        .To.Approximately.Equal(d2, within: 1);
                }, Throws.Nothing);
                
                Assert.That(() =>
                {
                    Expect(d1)
                        .To.Approximately.Equal(d2, within: 0.01);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                {
                    Expect(d1)
                        .Not.To.Approximately.Equal(d2, within: 0.01);
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(d1)
                        .Not.To.Approximately.Equal(d2, within: 0.1);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class DoubleValues
        {
            [Test]
            public void ShouldDefaultToEqualityWithinTwoDecimalPlacesRounded()
            {
                // Arrange
                var d1 = 0.1234D;
                var d2 = 0.12456D;
                var d3 = 0.119D;
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                        Expect(d1)
                            .To.Approximately.Equal(d3);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldThrowWhenNotApproximatelyEqual()
            {
                // Arrange
                var d1 = 1.1D;
                var d2 = 1.2D;
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("approxmiately equal"));
                // Assert
            }

            [Test]
            public void ShouldAllowCustomEqualityTesting()
            {
                // Arrange
                var d1 = 1.5D;
                var d2 = 2.1D;
                var comparer = new DoublesEqualToDecimalPlacesRounded(0);
                // Pre-assert
                // Act
                Assert.That(() =>
                    {
                        Expect(d1)
                            .To.Approximately.Equal(d2, comparer);
                    },
                    Throws.Nothing);
                // Assert
            }
        }
    }

    [TestFixture]
    public class TestDecimalsEqualToDecimalPlacesTruncated
    {
        public static
            IEnumerable<(decimal x, decimal y, int places, bool expected)>
            Generator()
        {
            yield return (1.123M, 1.129M, 2, true);
            yield return (1.111M, 1.199M, 1, true);
            yield return (1.1234M, 1.1243M, 3, false);
        }

        [TestCaseSource(nameof(Generator))]
        public void ShouldReturnFor_(
            (decimal x,
                decimal y,
                int places,
                bool expected) testCase)
        {
            // Arrange
            var sut = Create(testCase.places);
            // Pre-assert
            // Act
            var result = sut.Equals(testCase.x, testCase.y);
            // Assert
            Expect(result).To.Equal(testCase.expected);
        }

        private DecimalsEqualToDecimalPlacesTruncated Create(
            int places)
        {
            return new DecimalsEqualToDecimalPlacesTruncated(places);
        }
    }

    [TestFixture]
    public class TestDoublesEqualToDecimalPlacesTruncated
    {
        public static
            IEnumerable<(double x, double y, int places, bool expected)>
            Generator()
        {
            yield return (1.123D, 1.129D, 2, true);
            yield return (1.111D, 1.199D, 1, true);
            yield return (1.1234D, 1.1243D, 3, false);
        }

        [TestCaseSource(nameof(Generator))]
        public void ShouldReturnFor_(
            (double x,
                double y,
                int places,
                bool expected) testCase)
        {
            // Arrange
            var sut = Create(testCase.places);
            // Pre-assert
            // Act
            var result = sut.Equals(testCase.x, testCase.y);
            // Assert
            Expect(result).To.Equal(testCase.expected);
        }

        private DoublesEqualToDecimalPlacesTruncated Create(
            int places)
        {
            return new DoublesEqualToDecimalPlacesTruncated(places);
        }
    }
}