using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestHttpRequestMatching
    {
        [Test]
        public void HeaderAccessShouldBeCaseInsensitive()
        {
            // Arrange
            var res = HttpRequestBuilder.BuildDefault();
            res.Headers["Moo-Cow"] = "beef";
            // Act
            Assert.That(
                () =>
                {
                    Expect(res.Headers)
                        .To.Contain.Key("moo-cow");
                },
                Throws.Nothing
            );
            // Assert
        }
    }
}