using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class StringMatchingNonStringEntities
    {
        [Test]
        public void ShouldComparePathsAsStrings()
        {
            // Arrange
            var expected = "foo/bar"; // this is wrong: path should start with a /
            var req = HttpRequestBuilder.Create()
                .WithPath($"/{expected}") // latest PB updates will add the leading / if required
                .Build();
            // Act
            Assert.That(() =>
            {
                Expect(req.Path)
                    .To.Equal(expected);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}