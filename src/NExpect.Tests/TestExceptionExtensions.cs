using System;
using NUnit.Framework;
using NExpect.Extensions;
using static NExpect.Extensions.Expectations;
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
    }
}