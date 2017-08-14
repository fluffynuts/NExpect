using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

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

        [Test]
        public void Contain_And_WhenActualContainsBothBits_ShouldNotThrow()
        {
            // Arrange
            var actual = "a-b-c";
            var first = "a";
            var second = "b";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual).To.Contain(first).And(second);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void Contain_And_WhenActualMissingFirstBit_ShouldThrow()
        {
            // Arrange
            var actual = "a-b-c";
            var first = "d";
            var second = "b";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual).To.Contain(first).And(second);
            }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains("\"a-b-c\" to contain \"d\""));
            // Assert
        }
        [Test]
        public void Contain_And_WhenActualMissingSecondBit_ShouldThrow()
        {
            // Arrange
            var actual = "a-b-c";
            var first = "b";
            var second = "f";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual).To.Contain(first).And(second);
            }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains("\"a-b-c\" to contain \"f\""));
            // Assert
        }

        [Test]
        public void Contain_And_And_ShouldKeepOnChecking_HappyPath()
        {
            // Arrange
            var actual = "a-b-c";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual).To.Contain("a").And("b").And("c");
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void Contain_And_And_ShouldKeepOnChecking_SadPath()
        {
            // Arrange
            var actual = "a-b-c";
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(actual).To.Contain("a").And("b").And("d");
            }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.Contains("\"d\""));
            // Assert
        }

        [Test]
        public void To_Be_Equal_To_WithCustomMessage()
        {
            // Arrange
            var expected = GetRandomString();
            var test = GetRandomString();
            var nonMatch = GetAnother(test);
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(test).To.Be.Equal.To(nonMatch, expected);
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Contains(expected));
            // Assert
        }
    }
}