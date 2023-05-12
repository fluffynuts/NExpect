using System;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

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
            Assert.That(() =>
            {
                Expect(() =>
                        throw new ArgumentException("foo")
                    ).To.Throw<ArgumentException>()
                    .With.Message.Containing("foo");
            }, Throws.Nothing);
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
                Assert.That(() =>
                {
                    Expect(() =>
                            throw new ArgumentException("foo")
                        ).To.Throw<ArgumentException>()
                        .With.Message.Containing("FOO", StringComparison.OrdinalIgnoreCase);
                }, Throws.Nothing);
                // Assert
            }
        }

        [Test]
        public void ShouldNotThrowWhenMessageContainsTwoExpectedFragments()
        {
            // Arrange
            // Act
            Assert.That(() =>
            {
                Expect(() =>
                        throw new ArgumentException("foo, bar")
                    ).To.Throw<ArgumentException>()
                    .With.Message.Containing("foo")
                    .And.Containing("bar");
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldNotThrowWhenTwoNegatedAdditions()
        {
            // Arrange
            // Act
            var fieldName = GetRandomString(10);
            Assert.That(() =>
            {
                Expect(() => throw new ArgumentException("NO", fieldName))
                    .To.Throw<ArgumentException>()
                    .With.Message.Containing(fieldName)
                    .And.Not.Containing("{")
                    .And.Not.Containing("}");
            }, Throws.Nothing);
            // Assert
        }
    }
}