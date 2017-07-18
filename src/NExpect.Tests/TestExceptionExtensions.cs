using System;
using NUnit.Framework;
using NExpect.Extensions;
using static NExpect.Implementations.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestExceptionExtensions
    {
        [Test]
        public void Throw_WithNoGenericType_WhenSUTThrows_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(
                () => 
                {
                    Expect(() => 
                    {
                        throw new Exception(GetRandomString());
                    }).To.Throw();
                }
            );
            // Assert
        }

        [Test]
        public void Throw_WithNoGenericType_WhenSUTDoesNotThrow_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.Throws<AssertionException>(() =>
            {
                Expect(() => { }).To.Throw();
            });
            // Assert
        }

        [Test]
        public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage()
        {
            // Arrange
            var expected = GetRandomString();
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(() =>
            {
                Expect(() =>
                {
                    throw new Exception(expected);
                }).To.Throw()
                .With.Message.Containing(expected);
            });
            // Assert
        }
    }
}