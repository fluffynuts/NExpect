using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class CustomEqualityTesting
    {
        // TODO: could have more tests
        //  -> the other implementations lean on the logic to
        //      accomplish this, so, techically, the other cases are covered...

        [TestFixture]
        public class WithProvidedComparisonFunc
        {
            private static bool FirstLetterComparer(string x, string y)
            {
                if (x == null &&
                    y == null)
                {
                    return true;
                }

                if (x == null ||
                    y == null)
                {
                    return false;
                }

                if (x.Length == 0 &&
                    y.Length == 0)
                {
                    return true;
                }

                if (x.Length == 0 ||
                    y.Length == 0)
                {
                    return false;
                }

                return x[0] == y[0];
            }

            [Test]
            public void PositiveResult_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Equal(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegatedResult_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).Not.To.Equal(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegatedResult_AltGrammar_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Not.Equal(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }
        }
    }
}