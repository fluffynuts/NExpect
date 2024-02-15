using NExpect.Exceptions;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable MemberHidesStaticFromOuterClass

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class StartingWith
    {
        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Start
            {
                [TestFixture]
                public class With
                {
                    [Test]
                    public void PositiveAssertion_WhenStringStartsWithExpected_ShouldNotThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetRandomString(2);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect($"{start}{end}").To.Start.With(start);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void PositiveAssertion_WhenStringDoesNotStartsWithExpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetRandomString(2);
                        var actual = $"{start}{end}";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(actual).To.Start.With(end);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message
                                .Contains($"Expected\n\"{actual}\"\nto start with\n\"{end}\"")
                        );
                        // Assert
                    }

                    [Test]
                    public void PositiveAssertion_WithCustomMessage_WhenStringDoesNotStartsWithExpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetAnother<string>(start, () => GetRandomString(2));
                        var actual = $"{start}{end}";
                        var customMessage = GetRandomString(2);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(actual).To.Start.With(end, customMessage);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message
                                .Contains(customMessage)
                        );
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_WhenStringStartsWithUnexpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetRandomString(2);
                        var actual = $"{start}{end}";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(actual).Not.To.Start.With(start);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"\"{actual}\"\nnot to start with\n\"{start}\""));
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_AltGrammar_WhenStringStartsWithUnexpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetRandomString(2);
                        var actual = $"{start}{end}";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(actual).To.Not.Start.With(start);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"\"{actual}\"\nnot to start with\n\"{start}\""));
                        // Assert
                    }
                }
            }
        }
    }
}