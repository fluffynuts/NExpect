using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingNullableTypes
    {
        [TestFixture]
        public class NullableIntComparedToInt
        {
            [Test]
            public void PositiveAssertion_WhenAreEqual_ShouldNotThrow()
            {
                // Arrange
                var expected = GetRandomInt(1) as int?;
                var actual = expected.Value;

                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(actual).To.Equal(expected);
                }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeAssertion_WhenAreEqual_ShouldThrow()
            {
                // Arrange
                var expected = GetRandomInt(1) as int?;
                var actual = expected.Value;

                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(actual).Not.To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void NegativeAssertion_AltGrammar_WhenAreEqual_ShouldThrow()
            {
                // Arrange
                var expected = GetRandomInt(1) as int?;
                var actual = expected.Value;

                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(actual).To.Not.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }
        }
    }
}
