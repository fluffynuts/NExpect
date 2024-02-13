using System.Net;
using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class StatusCodeTests
    {
        [Test]
        public void LiveIssue_ComparingResponseStatusCodeToEnum()
        {
            // Arrange
            var statusCode = GetRandom<HttpStatusCode>();
            var res = HttpResponseBuilder.Create()
                .WithStatusCode(statusCode)
                .Build();
            // Act
            Assert.That(
                () =>
                {
                    Expect(res.StatusCode)
                        .To.Equal(statusCode);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void LiveIssue_NullableLongVsEnum()
        {
            // Arrange
            var statusCode = GetRandom<HttpStatusCode>();
            var res = HttpResponseBuilder.Create()
                .WithStatusCode(statusCode)
                .Build();
            long? testMe = res.StatusCode;
            // Act
            Assert.That(() =>
            {
                Expect(testMe)
                    .To.Equal(statusCode);
            }, Throws.Nothing);
            // Assert
        }
    }
}