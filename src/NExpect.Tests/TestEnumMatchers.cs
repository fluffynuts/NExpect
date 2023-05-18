using System;
using NUnit.Framework;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestEnumMatchers
    {
        [Test]
        public void ShouldBeAbleToAssertEnumValueHasFlag()
        {
            // Arrange
            var value = Numbers.One | Numbers.Three;
            // Act
            Assert.That(
                () =>
                {
                    Expect(value)
                        .To.Have.Flag(Numbers.One)
                        .And
                        .To.Have.Flag(Numbers.Three);
                    Expect(value)
                        .Not.To.Have.Flag(Numbers.Two)
                        .And
                        .Not.To.Have.Flag(Numbers.Four);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(value)
                        .To.Have.Flag(Numbers.Two);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        $"Expected ({value}) to have flag ({Numbers.Two})"
                    )
            );

            Assert.That(
                () =>
                {
                    Expect(value)
                        .Not.To.Have.Flag(Numbers.One);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        $"Expected ({value}) not to have flag ({Numbers.One})"
                    )
            );

            // Assert
        }

        [Test]
        public void ShouldAlwaysFailForNonFlagsEnums()
        {
            // Arrange
            var value = NotFlags.Strange;
            // Act
            Assert.That(
                () =>
                {
                    Expect(value)
                        .To.Have.Flag(NotFlags.Strange);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("not decorated with [Flags]")
            );
            Assert.That(
                () =>
                {
                    Expect(value)
                        .Not.To.Have.Flag(NotFlags.Strange);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("not decorated with [Flags]")
            );
            // Assert
        }

        public enum NotFlags
        {
            Unknown,
            Up,
            Down,
            Strange,
            Charm,
            Bottom,
            Top
        }

        [Flags]
        public enum Numbers
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 4,
            Four = 8
        }
    }
}