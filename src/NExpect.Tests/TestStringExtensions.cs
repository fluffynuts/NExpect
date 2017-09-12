using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
// ReSharper disable MemberHidesStaticFromOuterClass

namespace NExpect.Tests
{
    [TestFixture]
    public class TestStringExtensions
    {
        [TestFixture]
        public class To
        {
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
                        Expect(actual).To.Contain(search);
                    }, Throws.Nothing);

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
                        Expect(actual).To.Contain(search);
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Expect(actual).To.Contain(first).And(second);
                        }, Throws.Nothing);
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
                            Expect(actual).To.Contain(first).And(second);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                            Expect(actual).To.Contain(first).And(second);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
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
                                Expect(actual).To.Contain("a").And("b").And("c");
                            }, Throws.Nothing);
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
                                Expect(actual).To.Contain("a").And("b").And("d");
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("\"d\""));
                            // Assert
                        }
                    }
                }
            }

            [TestFixture]
            public class Be
            {
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
                            var expected = GetRandomString();
                            var test = GetRandomString();
                            var nonMatch = GetAnother(test);
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(test).To.Be.Equal.To(nonMatch, expected);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(expected));
                            // Assert
                        }
                    }
                }
            }

        }
    }
}