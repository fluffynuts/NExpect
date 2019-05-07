using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class EndingWith
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
                            Expect($"{start}{end}").To.End.With(end);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void PositiveAssertion_WhenStringDoesNotStartsWithExpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetAnother<string>(start, () => GetRandomString(2));
                        var actual = $"{start}{end}";
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(actual).To.End.With(start);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message
                                .Contains($"Expected\n\"{actual}\"\nto end with\n\"{start}\"")
                        );
                        // Assert
                    }

                    [Test]
                    public void PositiveAssertion_WithCustomMessage_WhenStringDoesNotStartsWithExpected_ShouldThrow()
                    {
                        // Arrange
                        var start = GetRandomString(2);
                        var end = GetRandomString(2);
                        var actual = $"{start}{end}";
                        var customMessage = GetRandomString(2);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(actual).To.End.With(start, customMessage);
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
                            Expect(actual).Not.To.End.With(end);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"\"{actual}\"\nnot to end with\n\"{end}\""));
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
                            Expect(actual).To.Not.End.With(end);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"\"{actual}\"\nnot to end with\n\"{end}\""));
                        // Assert
                    }
                }
            }
        }
    }
}