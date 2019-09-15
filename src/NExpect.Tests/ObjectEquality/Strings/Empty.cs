using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class Empty
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
            }
        }
    }
}