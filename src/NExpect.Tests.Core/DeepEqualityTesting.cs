using NUnit.Framework;
using static NExpect.Expectations;
using NExpect;

namespace NExpect.Tests.Core
{
    [TestFixture]
    public class DeepEqualityTesting
    {
        [Test]
        public void ShouldNotComparePrivateFieldsOrProperties()
        {
            // Arrange
            var left = new HasPrivates(1, "Bob", "red");
            var right = new HasPrivates(1, "Mary", "blue");
            // Act
            Assert.That(() => Expect(left).To.Deep.Equal(right), Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldNotStackOverflowOnEnum()
        {
            // Arrange
            var left = new
            {
                LogLevel = LogLevel.Critical
            };
            var right = new
            {
                LogLevel = LogLevel.Critical
            };
            // Act
            Assert.That(() => Expect(left)
                .To.Deep.Equal(right), Throws.Nothing);
            // Assert
        }

        public enum LogLevel
        {
            Trace,
            Debug,
            Information,
            Warning,
            Error,
            Critical,
            None,
        }

        public class HasPrivates
        {
            public int Id { get; set; }
            private string Name { get; set; }
            private string _color;

            public HasPrivates(int id, string name, string color)
            {
                Id = id;
                Name = name;
                _color = color;
            }
        }
    }
}