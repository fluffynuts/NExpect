using System.Collections.Generic;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestBadMatcherHandling
    {
        [Test]
        public void WhenMatcherThrows_ShouldHaveReasonableMessage()
        {
            // Arrange
            // Pre-assert
            // Act
            Assert.That(() => Expect("someString").To.Be.Broken(),
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.StartsWith(
                        "Exception whilst running matcher: System.Collections.Generic.KeyNotFoundException: moo"));
            // Assert
        }
    }

    public static class BadMatchers
    {
        public static void Broken(
            this IBe<string> be
        )
        {
            be.AddMatcher(actual => throw new KeyNotFoundException("moo"));
        }
    }
}