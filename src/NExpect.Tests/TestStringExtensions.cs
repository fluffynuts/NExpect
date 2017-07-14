using NExpect.Extensions;
using NUnit.Framework;
using static NExpect.Extensions.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestStringExtensions
    {
        [Test]
        public void Contain_WhenActualContainsSearch_ShouldNotThrow()
        {
            // Arrange
            var actual = "cow-moo-cow";
            var search = "moo";
            // Pre-Assert

            // Act
            Assert.That(() => { Expect(actual).To.Contain(search); }, Throws.Nothing);

            // Assert
        }

        [Test]
        public void Contain_WhenActualDoesNotContainSearch_ShouldThrow()
        {
            // Arrange
            var actual = "cow-moo-cow";
            var search = "foo";
            // Pre-Assert

            // Act
            Assert.That(() => { Expect(actual).To.Contain(search); }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Contains("Expected \"cow-moo-cow\" to contain \"foo\""));

            // Assert
        }
    }
}