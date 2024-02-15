using System.Text.RegularExpressions;
using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class RegexMatching
    {
        [TestFixture]
        public class WithActualRegex
        {
            [TestFixture]
            public class To
            {
                [TestFixture]
                public class Be
                {
                    [TestFixture]
                    public class Matched
                    {
                        [TestFixture]
                        public class By
                        {
                            [Test]
                            public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = new Regex(input);
                                Assert.That(regex.IsMatch(input), Is.True);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Be.Matched.By(regex);
                                    },
                                    Throws.Nothing);
                                // shorter variant
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Match(regex);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_WhenShouldPass_ShouldThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = new Regex($"^{input}$");
                                Assert.That(regex.IsMatch(input), Is.True);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).Not.To.Be.Matched.By(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // shorter variant
                                Assert.That(() =>
                                    {
                                        Expect(input).Not.To.Match(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_AltSyntax_WhenShouldPass_ShouldThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = new Regex($"^{input}$");
                                Assert.That(regex.IsMatch(input), Is.True);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Not.Be.Matched.By(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                                Assert.That(() =>
                                    {
                                        Expect(input).To.Not.Match(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                                // Assert
                            }
                        }
                    }
                }
            }
        }
        [TestFixture]
        public class WithStringRegex
        {
            [TestFixture]
            public class To
            {
                [TestFixture]
                public class Be
                {
                    [TestFixture]
                    public class Matched
                    {
                        [TestFixture]
                        public class By
                        {
                            [Test]
                            public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = $"^{input}$";

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Be.Matched.By(regex);
                                    },
                                    Throws.Nothing);
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Match(regex);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_WhenShouldPass_ShouldThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = $"^{input}$";

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).Not.To.Be.Matched.By(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                Assert.That(() =>
                                    {
                                        Expect(input).Not.To.Match(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_AltSyntax_WhenShouldPass_ShouldThrow()
                            {
                                // Arrange
                                var input = GetRandomString(2);
                                var regex = $"^{input}$";

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(input).To.Not.Be.Matched.By(regex);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                                Assert.That(() =>
                                    {
                                        Expect(input).To.Not.Match(regex);
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
}