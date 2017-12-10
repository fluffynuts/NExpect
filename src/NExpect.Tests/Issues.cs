using System;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class Issues
    {
        [Test]
        public void ComparingTypes_ShouldNotStall()
        {
            // Arrange

            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(new Object().GetType()).To.Equal(typeof(Object));
            }, Throws.Nothing);

            // Assert
        }
    }
}
