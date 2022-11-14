using Microsoft.AspNetCore.Http;
using NExpect.Exceptions;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.TestUtils.AspNetCore.Builders;
using static NExpect.Expectations;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestComparingAspNetCoreTypes
    {
        [Test]
        public void ShouldBeAbleToPerformDeepEqualityTesting()
        {
            // Arrange
            var item1 = new { Path = new PathString("/moo") };
            var item2 = new { Path = new PathString("/moo") };
            var item3 = new { Path = new PathString("/cow") };
            // Act
            Assert.That(() =>
            {
                Expect(item1)
                    .Not.To.Deep.Equal(item3);
            }, Throws.Nothing);
        
            Assert.That(() =>
            {
                Expect(item1)
                    .Not.To.Deep.Equal(item3);
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(item1)
                    .To.Deep.Equal(item3);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        
            Assert.That(() =>
            {
                Expect(item1)
                    .Not.To.Deep.Equal(item2);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldNotStackOverflowWhenAttemptingToStringifyHttpContext()
        {
            // Arrange
            // duplicates conditions in the wild
            var accessor = Substitute.For<IHttpContextAccessor>();
            var ctx = HttpContextBuilder.BuildDefault();
            accessor.HttpContext.Returns(ctx);
            // Act
            Assert.That(() =>
            {
                Expect(accessor.HttpContext)
                    .To.Be.Null();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}