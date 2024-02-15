using System;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;

namespace NExpect.Tests
{
    [TestFixture]
    public class UsingCustomAssertionsGenerator
    {
        [TearDown]
        public void TearDown()
        {
            Assertions.UseDefaultAssertionsFactory();
        }

        [Test]
        public void RegisteringAssertionFactoryForMessageOnly()
        {
            // Arrange
            Assertions.RegisterAssertionsFactory(
                message => new AssertionException(message));
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                Expect(5).To.Equal(4);
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Matches("Expected\\s4\\sbut\\sgot\\s5"));
            // Assert
        }

        [Test]
        public void RegisteringAssertionFactoryForMessageAndInnerException()
        {
            // Arrange
            Assertions.RegisterAssertionsFactory(
                (message, inner) => new AssertionException(message, inner));
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                Expect(1).To.Fail();
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.InnerException.Message.EqualTo("my bad"));
            // Assert
        }

        [Test]
        public void ResettingAssertionsFactory()
        {
            // Arrange
            Assertions.RegisterAssertionsFactory(message => new AssertionException(message));
            // Pre-assert
            // Act
            Assertions.UseDefaultAssertionsFactory();
            Assert.That(() =>
            {
                Expect(true).To.Be.False();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    public static class ThrowingMatchers
    {
        public static void Fail(this ITo<long> to)
        {
            to.AddMatcher(actual => throw new Exception("my bad"));
        }
    }
}