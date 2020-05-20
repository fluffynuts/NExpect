using System;
using NExpect.Interfaces;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Tests.Exceptions
{
    [TestFixture]
    public class UserSpaceImplementations
    {
        [Test]
        public void ShouldPullExceptionPropertyWhenExists()
        {
            // Arrange
            var ex = new ArgumentNullException("moo");
            var subject = new MyContinuationWithExceptionProperty<ArgumentNullException>(ex);
            // Pre-assert
            // Act
            var result = subject.With(e => e.ParamName);
            // Assert
            var actual = result.GetPropertyValue("Actual");
            Expect(actual).To.Equal("moo");
        }

        [Test]
        public void WhenNoExceptionProperty()
        {
            // Arrange
            var ex = new ArgumentNullException("moo");
            var subject = new MyContinuationWithoutExceptionProperty<ArgumentNullException>(ex);
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                subject.With(e => e.ParamName);
            }, Throws.Exception.InstanceOf<ArgumentException>()
                .With.Message.Contain("something with an Exception property"));
            // Assert
        }

        [Test]
        public void WhenExceptionPropertyThrows()
        {
            // Arrange
            var ex = new ArgumentNullException("moo");
            var subject = new MyContinuationWithBrokenExceptionProperty<ArgumentNullException>(ex);
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                subject.With(e => e.ParamName);
            }, Throws.Exception.InstanceOf<ArgumentException>()
                .With.Message.Contain("something with an Exception property"));
            // Assert
        }

        public class MyContinuationWithExceptionProperty<T> : IThrowContinuation<T> where T : Exception
        {
            public MyContinuationWithExceptionProperty(T exception)
            {
                Exception = exception;
                Id = Guid.NewGuid();
            }

            public IWithAfterThrowContinuation<T> With { get; }
            public T Exception { get; }
            public Guid Id { get; }
        }

        public class MyContinuationWithBrokenExceptionProperty<T> : IThrowContinuation<T> where T : Exception
        {
            public MyContinuationWithBrokenExceptionProperty(T exception)
            {
                Id = Guid.NewGuid();
            }

            public IWithAfterThrowContinuation<T> With { get; }
            public T Exception => throw new NotImplementedException();
            public Guid Id { get; }
        }

        public class MyContinuationWithoutExceptionProperty<T> : IThrowContinuation<T> where T : Exception
        {
            public MyContinuationWithoutExceptionProperty(T ex)
            {
            }

            public IWithAfterThrowContinuation<T> With { get; }
        }
    }
}