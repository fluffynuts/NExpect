using System;
using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static PeanutButter.Utils.PyLike;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class MoreStringExpectations
    {
        [TestFixture]
        public class Like
        {
            [Test]
            public void ShouldBeEquivalentToContainsCaseInsensitive()
            {
                // Arrange
                var message = "The Quick Brown Fox Tripped Over The Dog's Tail And Wiped Out!";
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(message)
                            .To.Be.Like("quick brown fox");
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(message)
                            .Not.To.Be.Like("foo to the bar");
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(message)
                            .To.Be.Like("jumped over the lazy dog");
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(message)
                            .Not.To.Be.Like("jumped over the lazy dog");
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }

        [Test]
        public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.To.Contain(middle)
                        .And.To.End.With(end);
                },
                Throws.Nothing
            );
            // Assert
        }

        [TestFixture]
        public class WhenProvidedStringComparison
        {
            [Test]
            public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
            {
                // Arrange
                var start = GetRandomString(2);
                var middle = GetRandomString();
                var end = GetRandomString(2);
                var afterStart = GetRandomString();
                var actual = $"{start}{afterStart}{middle}{end}";
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(actual)
                            .To.Start.With(start.ToUpper(), StringComparison.OrdinalIgnoreCase)
                            .Then(afterStart.ToUpper(), StringComparison.OrdinalIgnoreCase)
                            .And.To.Contain(middle.ToRandomCase(), StringComparison.OrdinalIgnoreCase)
                            .And.To.End.With(end.ToRandomCase(), StringComparison.OrdinalIgnoreCase);
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }

        [Test]
        public void MultipleNegativeAssertions()
        {
            // Arrange
            var str = "hello, world!";
            // Act
            Assert.That(
                () =>
                {
                    Expect(str)
                        .To.Contain("hello")
                        .And.Not.To.Contain("submarine")
                        .And.Not.To.Contain("yellow")
                        .And.To.Contain("world");
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void PositiveAssertion_Reversed_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.End.With(end)
                        .And.To.Start.With(start);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void NegativeAssertion1_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .Not.To.Start.With(start)
                        .And.To.Contain(middle)
                        .And.To.End.With(end);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void NegativeAssertion2_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.Not.To.Contain(middle)
                        .And.To.End.With(end);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void NegativeAssertion3_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.To.Contain(middle)
                        .And.Not.To.End.With(end);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntax_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.Contain(middle)
                        .And.End.With(end);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntaxFlipped_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.End.With(end)
                        .And.Contain(middle)
                        .And.Start.With(start);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntax_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(end)
                        .And.Contain(middle)
                        .And.End.With(start);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntaxFlipped_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.End.With(start)
                        .And.Contain(middle)
                        .And.Start.With(end);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void NegativeAssertion_ShorterSyntax_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.Contain(middle)
                        .And.Not.End.With(start);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void NegativeAssertion_ShorterSyntax_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.Contain(middle)
                        .And.Not.End.With(end);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void TestingEndWith_PositiveResult()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(actual)
                        .To.Start.With(start)
                        .And.Contain(middle)
                        .And.End.With(end);
                },
                Throws.Nothing
            );
            // Assert
        }

        [TestFixture]
        public class StringLength
        {
            [Test]
            public void PositiveAssertion_WithPass()
            {
                // Arrange
                var s = GetRandomString(
                    5,
                    10
                );
                var expected = s.Length;
                // Pre-assert
                // Act
                Assert.That(
                    () => Expect(s).To.Have.Length(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void PositiveAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomString();
                var test = GetRandom<int>(i => i != s.Length);
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).To.Have.Length(test);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" to have length {test}")
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithPass()
            {
                // Arrange
                var s = GetRandomString();
                var test = GetRandom<int>(i => i != s.Length);
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Have.Length(test);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomString();
                var test = s.Length;
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Have.Length(test);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" not to have length {test}")
                );
                // Assert
            }
        }

        [TestFixture]
        public class Alphanumeric : MoreStringExpectations
        {
            [Test]
            public void PositiveAssertion_WithPass()
            {
                // Arrange
                var alphaNumeric = GetRandomAlphaNumericString();
                var alphaOnly = GetRandomAlphaString();
                var numericOnly = GetRandomInt(
                    10000,
                    200000
                ).ToString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(alphaNumeric).To.Be.Alphanumeric();
                        Expect(alphaOnly).To.Be.Alphanumeric();
                        Expect(numericOnly).To.Be.Alphanumeric();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void PositiveAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomNonAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).To.Be.Alphanumeric();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" to be alpha-numeric")
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithPass()
            {
                // Arrange
                var s = GetRandomNonAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Alphanumeric();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Alphanumeric();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" not to be alpha-numeric")
                );
                // Assert
            }
        }

        [TestFixture]
        public class Alpha : MoreStringExpectations
        {
            [Test]
            public void PositiveAssertion_WithPass()
            {
                // Arrange
                var alphaOnly = GetRandomAlphaString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(alphaOnly).To.Be.Alpha();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void PositiveAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomNonAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).To.Be.Alpha();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" to be alpha")
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithPass()
            {
                // Arrange
                var s = GetRandomNonAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Alpha();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomAlphaString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Alpha();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" not to be alpha")
                );
                // Assert
            }
        }

        [TestFixture]
        public class Numeric : MoreStringExpectations
        {
            [Test]
            public void PositiveAssertion_WithPass()
            {
                // Arrange
                var numericOnly = GetRandomInt(
                    10000,
                    200000
                ).ToString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(numericOnly).To.Be.Numeric();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void PositiveAssertion_WithFail()
            {
                // Arrange
                var alpha = GetRandomAlphaString();
                var nonAlpha = GetRandomNonAlphaNumericString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(alpha).To.Be.Numeric();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{alpha}\" to be numeric")
                );
                Assert.That(
                    () =>
                    {
                        Expect(nonAlpha).To.Be.Numeric();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{nonAlpha}\" to be numeric")
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithPass()
            {
                // Arrange
                var s = GetRandomAlphaString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Numeric();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void NegativeAssertion_WithFail()
            {
                // Arrange
                var s = GetRandomInt(
                    1,
                    10000
                ).ToString();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(s).Not.To.Be.Numeric();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo($"Expected \"{s}\" not to be numeric")
                );
                // Assert
            }
        }

        private string GetRandomNonAlphaNumericString(
            int minChars = 0,
            int maxChars = 10
        )
        {
            return Range(
                    0,
                    GetRandomInt(
                        1,
                        10
                    )
                )
                .Select(
                    i =>
                        GetRandom(
                            c => (c < 'A' || c > 'z') && (!"01234567890".Contains(c)),
                            () => (char)GetRandomInt(
                                32,
                                255
                            )
                        )
                )
                .JoinWith("");
        }
    }
}