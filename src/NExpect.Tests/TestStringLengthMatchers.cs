using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests;

[TestFixture]
public class TestStringLengthMatchers
{
    [TestFixture]
    public class AssertingShorter
    {
        [Test]
        public void ShouldBeAbleToAssertOneStringIsShorterThanAnother()
        {
            // Arrange
            var s1 = "aaa";
            var s2 = "aaaa";
            // Act
            Assert.That(
                () =>
                {
                    Expect(s1)
                        .To.Be.Shorter.Than(s2);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(s2)
                        .Not.To.Be.Shorter.Than(s1);
                    Expect(s2)
                        .To.Not.Be.Shorter.Than(s1);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(s2)
                        .To.Be.Shorter.Than(s1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(s1)
                        .Not.To.Be.Shorter.Than(s2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(s1)
                        .To.Not.Be.Shorter.Than(s2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class AssertingLonger
    {
        [Test]
        public void ShouldBeAbleToAssertOneStringIsLongerThanAnother()
        {
            // Arrange
            var s1 = "aaaa";
            var s2 = "aaa";
            // Act
            Assert.That(
                () =>
                {
                    Expect(s1)
                        .To.Be.Longer.Than(s2);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(s2)
                        .Not.To.Be.Longer.Than(s1);
                    Expect(s2)
                        .To.Not.Be.Longer.Than(s1);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(s2)
                        .To.Be.Longer.Than(s1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(s1)
                        .Not.To.Be.Longer.Than(s2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(s1)
                        .To.Not.Be.Longer.Than(s2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }
}