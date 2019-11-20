using NUnit.Framework;
using static NExpect.Expectations;
using NExpect;

namespace NExpect.Tests.Core
{
    [TestFixture]
    public class DeepEqualityTesting
    {
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
    }
}