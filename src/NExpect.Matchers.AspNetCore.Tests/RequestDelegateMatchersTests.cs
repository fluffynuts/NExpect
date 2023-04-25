using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using NExpect;
using PeanutButter.TestUtils.AspNetCore.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class RequestDelegateMatchersTests
    {
        [Test]
        public void NAME()
        {
            // Arrange
            var (_, _) = RequestDelegateTestArenaBuilder.BuildDefault();
            var asm = RequestDelegateMatchers.FindLoadedPeanutButterAssembly();
            var foo = 1;
            // Act
            // Assert
        }

        [Test]
        public async Task ShouldVerifyDelegateHasBeenCalled()
        {
            // Arrange
            var (ctx, next) = RequestDelegateTestArenaBuilder.BuildDefault();
            var good = new GoodMiddleware();
            // Act
            await good.InvokeAsync(ctx, next);
            // Assert
            Expect(next)
                .To.Have.Been.Called();
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