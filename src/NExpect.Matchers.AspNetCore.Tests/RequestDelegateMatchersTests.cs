using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Utils;
using static NExpect.Expectations;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class RequestDelegateMatchersTests
    {
        [Test]
        public async Task ShouldVerifyDelegateHasBeenCalled()
        {
            // Arrange
            var (ctx, next) = RequestDelegateTestArenaBuilder.BuildDefault();
            var middleware = new GoodMiddleware();
            // Act
            await middleware.InvokeAsync(ctx, next);
            // Assert
            Assert.That(() =>
            {
                Expect(next)
                    .To.Have.Been.Called();
            }, Throws.Nothing);
        }

        [Test]
        public async Task ShouldVerifyWhenDelegateNotCalled()
        {
            // Arrange
            var (ctx, next) = RequestDelegateTestArenaBuilder.BuildDefault();
            var middleware = new BadMiddleware();
            // Act
            await middleware.InvokeAsync(ctx, next);
            // Assert
            Assert.That(() =>
            {
                Expect(next)
                    .Not.To.Have.Been.Called();
            }, Throws.Nothing);
        }

        [Test]
        public async Task ShouldVerifyWhenDelegateCalledWithContext()
        {
            // Arrange
            var (ctx, next) = RequestDelegateTestArenaBuilder.BuildDefault();
            var middleware = new GoodMiddleware();
            // Act
            await middleware.InvokeAsync(ctx, next);
            // Assert
            Assert.That(() =>
            {
                Expect(next)
                    .To.Have.Been.Called(1).Time()
                    .With.Context(ctx);
            }, Throws.Nothing);
        }

        [Test]
        public async Task ShouldVerifyWhenDelegateCalledWithoutContext()
        {
            // Arrange
            var (ctx, next) = RequestDelegateTestArenaBuilder.BuildDefault();
            var middleware = new BadMiddleware();
            // Act
            await middleware.InvokeAsync(ctx, next);
            // Assert
            Assert.That(() =>
            {
                Expect(next)
                    .To.Have.Been.Called(1).Time()
                    .With.Context(ctx);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        }

        public class GoodMiddleware : IMiddleware
        {
            public Task InvokeAsync(
                HttpContext context,
                RequestDelegate next
            )
            {
                return next(context);
            }
        }

        public class BadMiddleware : IMiddleware
        {
            public Task InvokeAsync(
                HttpContext context,
                RequestDelegate next
            )
            {
                return Task.CompletedTask;
            }
        }
    }
}