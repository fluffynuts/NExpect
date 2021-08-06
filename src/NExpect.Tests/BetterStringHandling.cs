using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class BetterStringHandling
    {
        [Test]
        public void ShouldReportWhenStringsOnlyDifferByWhitespace()
        {
            // Arrange
            var result = "foo    bar";
            var expected = "foo\tbar";
            // Act
            Assert.That(() =>
                {
                    Expect(result)
                        .To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("values have whitespace differences")
            );
            // Assert
        }

        [Test]
        public void ShouldReportWhenStringsOnlyDifferByLineEndings()
        {
            // Arrange
            var result = "foo\r\nbar\r\nquuz";
            var expected = "foo\nbar\nquuz";
            // Act
            Assert.That(() =>
                {
                    Expect(result)
                        .To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("values have different line endings (CRLF vs LF)")
            );
            // Assert
        }

        [Test]
        public void ShouldReportWhenOnlyDifferenceIsCasing()
        {
            // Arrange
            var result = "Foo Bar";
            var expected = "foo bar";
            // Act
            Assert.That(() =>
                {
                    Expect(result)
                        .To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("values have different casing")
            );
            // Assert
        }

        [Test]
        public void ShouldGiveAClueAboutWhereDifferencesStart()
        {
            // Arrange
            var result = "foo bar";
            var expected = "foo quux";
            // Act
            Assert.That(() =>
                {
                    Expect(result)
                        .To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("first difference found at character 4")
            );
            // Assert
        }

        [Test]
        public void ShouldHandleWhenLeftIsTruncatedRight()
        {
            // Arrange
            var result = "foo";
            var expected = "foo bar";
            // Act
            Assert.That(() =>
                {
                    Expect(result)
                        .To.Equal(expected);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("character 3")
            );
            // Assert
        }
    }
}