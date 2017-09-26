using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingForNull
    {
        [TestFixture]
        public class Expect_Value_To
        {
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
                    public void
                        OperatingOnStringNegated_GivenCustomMessage_WhenIsNull_ShouldThrowIncludingCustomMessage()
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
        }
    }
}