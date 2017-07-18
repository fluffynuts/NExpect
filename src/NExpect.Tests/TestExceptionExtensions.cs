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
        public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_HappyPath()
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

        [Test]
        public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_SadPath()
        {
            // Arrange
            var expected = GetRandomString();
            var other = GetAnother(expected);
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(() =>
                {
                    throw new Exception(other);
                }).To.Throw()
                .With.Message.Containing(expected);
            }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains($"to contain \"{expected}\""));
            // Assert
        }

//        [Test]
//        public void Throw_WithGenericType_WhenThrowsThatType_ShouldContinueWithMessage_HappyPath()
//        {
//            // Arrange
//            // Pre-Assert
//            // Act
//            // Assert
//            throw new Exception("Test not yet implemented");
//        }
    }
}