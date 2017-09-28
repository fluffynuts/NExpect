using System;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class OrderedPartialContaining
    {
        [Test]
        public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var next = GetAnother(start);
            var input = $"{start}{next}";

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(input)
                        .To.Contain(start)
                        .Then(next);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void PositiveAssertion_WhenNextPartIsOutOfOrder_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var next = GetAnother(start);
            var input = $"{start}{next}";
            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(input)
                        .To.Contain(next)
                        .Then(start);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void PositiveAssertion_MultipleChains()
        {
            // Arrange
            var strings = GetRandomArray<string>(6, 8);
            var input = strings.JoinWith("");

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(input)
                        .To
                        .Contain(strings[0])
                        .Then(strings[1])
                        .Then(strings[3])
                        .Then(strings[5]);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void ThenShouldNotMatchPartialOfAlreadyMatchedPart()
        {
            // Arrange
            var input = "moo said the cat";

            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(input)
                    .To.Contain("moo")
                    .Then("o");
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void PositiveAssertion_OnExceptionMessage_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var first = GetRandomString(2);
            var second = GetAnother(first);

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(() => throw new Exception($"{first}{GetRandomString(15)}{second}"))
                        .To.Throw<Exception>()
                        .With.Message.Containing(first)
                        .Then(second);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void PositiveAssertion_OnExceptionMessage_WhenShouldNotPass_ShouldThrow()
        {
            // Arrange
            var first = GetRandomString(2);
            var second = GetAnother(first);
            var another = GetAnother<string>(new[] { first, second });

            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(() => throw new Exception($"{first}{GetRandomString(15)}{second}"))
                        .To.Throw<Exception>()
                        .With.Message.Containing(first)
                        .Then(another);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }
    }
}