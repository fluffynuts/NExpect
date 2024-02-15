using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests
{
    [TestFixture]
    public class BetterStringErrors
    {
        [Test]
        public void ShouldReportNewlineDifferences()
        {
            // Arrange
            var a = "foo\nbar";
            var b = "foo\r\nbar";
            // Act
            Assert.That(() =>
                {
                    Expect(a)
                        .To.Equal(b);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("values have different line endings")
            );
            // Assert
        }

        [Test]
        public void ShouldReportPositionOfFirstDifference()
        {
            // Arrange
            var a = "foobar";
            var b = "fooquux";
            // Act
            Assert.That(() =>
                {
                    Expect(a)
                        .To.Equal(b);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("first difference found at character 3")
            );
            // Assert
        }
    }
}