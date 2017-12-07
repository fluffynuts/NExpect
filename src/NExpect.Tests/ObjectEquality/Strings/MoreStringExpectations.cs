using NExpect.Exceptions;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class MoreStringExpectations
    {
        [Test]
        public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.To.Contain(middle)
                    .And.To.End.With(end);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void PositiveAssertion_Reversed_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.End.With(end)
                    .And.To.Start.With(start);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void NegativeAssertion1_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .Not.To.Start.With(start)
                    .And.To.Contain(middle)
                    .And.To.End.With(end);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void NegativeAssertion2_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.Not.To.Contain(middle)
                    .And.To.End.With(end);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void NegativeAssertion3_WhenShouldPass_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.To.Contain(middle)
                    .And.Not.To.End.With(end);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntax_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.Contain(middle)
                    .And.End.With(end);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntaxFlipped_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.End.With(end)
                    .And.Contain(middle)
                    .And.Start.With(start);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntax_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(end)
                    .And.Contain(middle)
                    .And.End.With(start);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void PositiveAssertion_ShorterSyntaxFlipped_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.End.With(start)
                    .And.Contain(middle)
                    .And.Start.With(end);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void NegativeAssertion_ShorterSyntax_WhenShouldPass_ShouldNotThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.Contain(middle)
                    .And.Not.End.With(start);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void NegativeAssertion_ShorterSyntax_WhenShouldFail_ShouldThrow()
        {
            // Arrange
            var start = GetRandomString(2);
            var middle = GetRandomString();
            var end = GetRandomString(2);
            var actual = $"{start}{middle}{end}";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual)
                    .To.Start.With(start)
                    .And.Contain(middle)
                    .And.Not.End.With(end);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}