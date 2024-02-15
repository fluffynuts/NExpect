using System;
using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.Exceptions
{
    [TestFixture]
    public class EnforcingMessage
    {
        [Test]
        public void ShouldNotThrowWhenMessageContainsOneExpectedFragment()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                                throw new ArgumentException("foo")
                        ).To.Throw<ArgumentException>()
                        .With.Message.Containing("foo");
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldSupportEasyCaseInsensitiveContains_Like()
        {
            // Arrange
            var message = GetRandomWords(5, 10);
            var words = message.Split(" ");
            var skip = GetRandomInt(2, 4);
            var take = GetRandomInt(2, 4);
            var seek = words.Skip(skip).Take(take).JoinWith(" ").ToRandomCase();
            while (message.Contains(seek, StringComparison.Ordinal))
            {
                seek = seek.ToRandomCase();
            }

            // Act
            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(message))
                        .To.Throw<Exception>()
                        .With.Message.Containing(seek);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(message))
                        .To.Throw<Exception>()
                        .With.Message.Like(seek);
                },
                Throws.Nothing
            );
            var other = GetAnother(seek);
            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(message))
                        .To.Throw<Exception>()
                        .With.Message.Like(other);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [TestFixture]
        public class WhenStringComparisonSupplied
        {
            [Test]
            public void ShouldNotThrowWhenMessageContainsOneExpectedFragment()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                    throw new ArgumentException("foo")
                            ).To.Throw<ArgumentException>()
                            .With.Message.Containing("FOO", StringComparison.OrdinalIgnoreCase);
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }

        [Test]
        public void ShouldNotThrowWhenMessageContainsTwoExpectedFragments()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                                throw new ArgumentException("foo, bar")
                        ).To.Throw<ArgumentException>()
                        .With.Message.Containing("foo")
                        .And.Containing("bar");
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldNotThrowWhenTwoNegatedAdditions()
        {
            // Arrange
            // Act
            var fieldName = GetRandomString(10);
            Assert.That(
                () =>
                {
                    Expect(() => throw new ArgumentException("NO", fieldName))
                        .To.Throw<ArgumentException>()
                        .With.Message.Containing(fieldName)
                        .And.Not.Containing("{")
                        .And.Not.Containing("}");
                },
                Throws.Nothing
            );
            // Assert
        }
    }
}