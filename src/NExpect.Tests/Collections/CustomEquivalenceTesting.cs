using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class CustomEquivalenceTesting
    {
        [TestFixture]
        public class WithCustomEqualityComparer
        {
            [Test]
            public void Positive_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Be.Equivalent.To(right, new FirstLetterComparer());
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegated_WhenNoMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).Not.To.Be.Equivalent.To(right, new FirstLetterComparer());
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegated_AltGrammar_WhenNoMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Not.Be.Equivalent.To(right, new FirstLetterComparer());
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class WithCustomEqualityComparisonFunc
        {
            private static bool FirstLetterComparer(string x, string y)
            {
                if (x == null &&
                    y == null)
                    return true;
                if (x == null ||
                    y == null)
                    return false;
                if (x.Length == 0 &&
                    y.Length == 0)
                    return true;
                if (x.Length == 0 ||
                    y.Length == 0)
                    return false;
                return x[0] == y[0];
            }

            [Test]
            public void Positive_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Be.Equivalent.To(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegated_WhenNoMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).Not.To.Be.Equivalent.To(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void NegativeNegated_AltGrammar_WhenNoMatches_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Not.Be.Equivalent.To(right, FirstLetterComparer);
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class WithProvidedComparer
        {
            [Test]
            public void PositiveResult_ShouldNotThrow()
            {
                // Arrange
                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var right = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                var withComparer = new FirstLetterComparer();
                Assert.That(withComparer.Equals(left[0], right[0]), Is.True);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Equal(right, withComparer);
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
                var withComparer = new FirstLetterComparer();
                Assert.That(withComparer.Equals(left[0], right[0]), Is.False);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).Not.To.Equal(right, withComparer);
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
                var withComparer = new FirstLetterComparer();
                Assert.That(withComparer.Equals(left[0], right[0]), Is.False);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(left).To.Not.Equal(right, withComparer);
                    },
                    Throws.Nothing);

                // Assert
            }
        }
    }
}