using NUnit.Framework;
using NExpect;
using static NExpect.Expectations;

namespace CoreConsumer
{
    [TestFixture]
    public class TestConsumption
    {
        [Test]
        public void ShouldBeAbleToExpect()
        {
            // Arrange
            // Pre-Assert
            // Act
            Expect(1).To.Equal(1);
            // Assert
        }
    }
}
