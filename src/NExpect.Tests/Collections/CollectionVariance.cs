using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class CollectionVariance
    {
        [TestFixture]
        public class PositiveIntent
        {
            [TestFixture]
            public class NullCollection
            {
                [Test]
                public void ShouldAlwaysFail()
                {
                    // Arrange
                    var input = null as int[];
                    // Act
                    Assert.That(() =>
                    {
                        Expect(input)
                            .To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("null"));

                    Assert.That(() =>
                    {
                        Expect(input)
                            .Not.To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("null"));

                    // Assert
                }
            }

            [TestFixture]
            public class EmptyCollection
            {
                [Test]
                public void ShouldAlwaysFail()
                {
                    // Arrange
                    var input = new int[0];
                    // Act
                    Assert.That(() =>
                    {
                        Expect(input)
                            .To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("empty"));
                    Assert.That(() =>
                    {
                        Expect(input)
                            .Not.To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("empty"));
                    // Assert
                }
            }

            [TestFixture]
            public class CollectionOf1
            {
                [Test]
                public void ShouldAlwaysFail()
                {
                    // Arrange
                    var input = new[] { GetRandomInt() };
                    // Act
                    Assert.That(() =>
                    {
                        Expect(input)
                            .To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("only one item"));
                    Assert.That(() =>
                    {
                        Expect(input)
                            .Not.To.Vary();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("only one item"));
                    // Assert
                }
            }

            [TestFixture]
            public class InvariantCollection
            {
                [TestFixture]
                public class PositiveAssertion
                {
                    [Test]
                    public void ShouldFail()
                    {
                        // Arrange
                        var input = new[] { 1, 1 };
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input)
                                    .To.Vary();
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("to vary")
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class NegativeAssertion
                {
                    [Test]
                    public void ShouldPass()
                    {
                        // Arrange
                        var input = new[] { 1, 1 };
                        // Act
                        Assert.That(() =>
                        {
                            Expect(input)
                                .Not.To.Vary();
                            Expect(input)
                                .To.Not.Vary();
                        }, Throws.Nothing);
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class VariantCollection
            {
                [TestFixture]
                public class PositiveAssertion
                {
                    [Test]
                    public void ShouldPass()
                    {
                        // Arrange
                        var input = new[] { 1, 2 };
                        // Act
                        Assert.That(() =>
                        {
                            Expect(input)
                                .To.Vary();
                        }, Throws.Nothing);
                        // Assert
                    }
                }

                [TestFixture]
                public class NegativeAssertion
                {
                    [Test]
                    public void ShouldPass()
                    {
                        // Arrange
                        var input = new[] { 1, 2 };
                        // Act
                        Assert.That(() =>
                        {
                            Expect(input)
                                .Not.To.Vary();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("not to vary"));
                        // Assert
                    }
                }
            }
        }
    }
}