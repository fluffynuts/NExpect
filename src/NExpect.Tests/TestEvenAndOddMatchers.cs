using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestEvenAndOddMatchers
    {
        [TestFixture]
        public class Even
        {
            [Test]
            public void ShouldBeAbleToAssertEvenness()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(2)
                            .To.Be.Even();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(GetRandomInt() * 2)
                            .To.Be.Even();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(1)
                            .Not.To.Be.Even();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(1)
                            .To.Not.Be.Even();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(1)
                            .To.Be.Even();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(2)
                            .Not.To.Be.Even();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(GetRandomInt() * 2)
                            .To.Not.Be.Even();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }
        [TestFixture]
        public class Odd
        {
            [Test]
            public void ShouldBeAbleToAssertOddness()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(3)
                            .To.Be.Odd();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(GetRandomInt() * 2 + 1)
                            .To.Be.Odd();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(4)
                            .Not.To.Be.Odd();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(2)
                            .To.Not.Be.Odd();
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(2)
                            .To.Be.Odd();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(3)
                            .Not.To.Be.Odd();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(GetRandomInt() * 2 + 1)
                            .To.Not.Be.Odd();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }
    }
}