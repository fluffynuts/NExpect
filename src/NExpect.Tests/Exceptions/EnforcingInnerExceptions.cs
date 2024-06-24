using System;
using NUnit.Framework;

namespace NExpect.Tests.Exceptions
{
    [TestFixture]
    public class EnforcingInnerExceptions
    {
        [Test]
        public void ShouldBeAbleToWorkWithInnerExceptionLikeOuterOne()
        {
            // Arrange
            // Act
            Expect(
                    () =>
                    {
                        throw new InvalidOperationException(
                            "nope",
                            new TimeoutException("down low - too slow!")
                        );
                    }
                ).To.Throw<Exception>()
                .With.Inner.Exception<TimeoutException>()
                .With.Message.Containing("too slow");
            // Assert
        }

        [Test]
        public void ShouldBeAbleToWorkWithInnerExceptionLikeOuterOneTurtleEdition()
        {
            // Arrange
            // Act
            Expect(
                    () =>
                    {
                        throw new InvalidOperationException(
                            "nope",
                            new TimeoutException(
                                "down low - too slow!",
                                new FooException("bingbong")
                            )
                        );
                    }
                ).To.Throw<Exception>()
                .With.Inner.Exception<TimeoutException>()
                .With.Inner.Exception<FooException>()
                .With.Message.Containing("too slow");
            // Assert
        }

        public class FooException(string message) : Exception(message);
    }
}