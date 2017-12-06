using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class Containing
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
                        },
                        Throws.Nothing);

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
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected\n\"cow-moo-cow\"\nto contain\n\"foo\""));

                    // Assert
                }

                [Test]
                public void WhenActualDoesNotContainSearch_ShouldThrowWithCustomMessage()
                {
                    // Arrange
                    var actual = "cow-moo-cow";
                    var search = "foo";
                    var expected = GetRandomString(2);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(actual).To.Contain(search, expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(expected));

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
                            },
                            Throws.Nothing);
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
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("\"a-b-c\"\nto contain\n\"d\""));
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
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("\"a-b-c\"\nto contain\n\"f\""));
                        // Assert
                    }

                    [Test]
                    public void WhenActualMissingSecondBit_ShouldThrowWithCustomMessage()
                    {
                        // Arrange
                        var actual = "a-b-c";
                        var first = "b";
                        var second = "f";
                        var expected = GetRandomString(2);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(actual).To.Contain(first).And(second, expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(expected));
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
                                },
                                Throws.Nothing);
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
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("\"d\""));
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class FragmentsInOrder
                    {
                        [Test]
                        public void WhenHaveAllFragmentsInOrder_ShouldNotThrow()
                        {
                            // Arrange
                            var src = new[]
                            {
                                "Line the first",
                                "This is the second line",
                                "Another line here",
                                "And the final line"
                            }.JoinWith("\n");
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(src)
                                    .To.Contain.In.Order(
                                        "the first",
                                        "line here",
                                        "final line"
                                    );
                            }, Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void WhenHaveAllFragmentsInOrder_ShouldAllowMore()
                        {
                            // Arrange
                            var src = new[]
                            {
                                "Line the first",
                                "This is the second line",
                                "Another line here",
                                "And the final line"
                            }.JoinWith("\n");
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(src)
                                    .To.Contain.In.Order(
                                        "the first",
                                        "line here"
                                    ).Then("final line");
                            }, Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void WhenHaveAllFragmentsOutOfOrder_ShouldThrow()
                        {
                            // Arrange
                            var src = new[]
                            {
                                "Line the first",
                                "This is the second line",
                                "Another line here",
                                "And the final line"
                            }.JoinWith("\n");

                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(src)
                                    .To.Contain.In.Order(
                                        "line here",
                                        "the first",
                                        "final line"
                                    );
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void WhenHaveMissingFragment_ShouldThrow()
                        {
                            // Arrange
                            var src = new[]
                            {
                                "Line the first",
                                "This is the second line",
                                "Another line here",
                                "And the final line"
                            }.JoinWith("\n");

                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(src)
                                    .To.Contain.In.Order(
                                        "the first",
                                        "moo, said the cow",
                                        "final line"
                                    );
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void WhenHaveMissingFragment_ShouldThrow_IncludingCustomMessage()
                        {
                            // Arrange
                            var src = new[]
                            {
                                "Line the first",
                                "This is the second line",
                                "Another line here",
                                "And the final line"
                            }.JoinWith("\n");

                            var expected = GetRandomString(10);
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(src)
                                    .To.Contain.In.Order(new[]
                                    {
                                        "the first",
                                        "moo, said the cow",
                                        "final line"
                                    }, expected);
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