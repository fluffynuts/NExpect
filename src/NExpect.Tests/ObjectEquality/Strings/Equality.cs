using System;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class Equality
    {
        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Equal
            {
                [Test]
                public void WithCustomMessage()
                {
                    // Arrange
                    var expected = RandomValueGen.GetRandomString();
                    var test = RandomValueGen.GetRandomString();
                    var nonMatch = RandomValueGen.GetAnother(test);
                    // Pre-Assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(test).To.Equal(nonMatch, expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(expected)
                    );
                    Assert.That(
                        () =>
                        {
                            Expect(test).To.Equal(nonMatch, () => expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(expected)
                    );
                    // Assert
                }

                [TestFixture]
                public class DifferenceOutput
                {
                    [Test]
                    public void ShouldBeHelpful()
                    {
                        // Arrange
                        var input = @"
{
    ""Foo_Bar"": ""value_bar"",
    ""Quux_Bar"": ""value_var""
}
";
                        var expected = @"
{
    ""Foo_Bar"": ""value_bar"",
    ""Quux_Bar"": ""1value_var""
}
";

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(input)
                                    .To.Equal(expected);
                            },
                            Throws.Exception.With.Message.Contains(
                                "ux_Bar\": \"value_var\""
                            )
                        );
                        // Assert
                    }

                    [Test]
                    public void ShouldDealWithShortMisses()
                    {
                        // Arrange
                        var input = @"
line1
line2
";
                        var expected = @"
line1
line3
";

                        // Act
                        var result = DifferenceHighlighting.ProvideMoreInfoFor(
                            input,
                            expected
                        );
                        Console.WriteLine(result);
                        // Assert
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
                            var expected = RandomValueGen.GetRandomString();
                            var test = RandomValueGen.GetRandomString();
                            var nonMatch = RandomValueGen.GetAnother(test);
                            // Pre-Assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(test).To.Be.Equal.To(nonMatch, expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }
                }
            }
        }
    }
}