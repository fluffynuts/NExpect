using System.Collections.ObjectModel;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Collections
{
    public class TestAgainstReadOnlyCollections
    {
        [Test]
        public void ShouldBeAbleToAssertEmpty()
        {
            // Arrange
            var collection = new ReadOnlyCollection<string>(new string[0]);
            // Act
            Assert.That(() =>
            {
                Expect(collection)
                    .To.Be.Empty();
            }, Throws.Nothing);
        
            Assert.That(() =>
            {
                Expect(collection)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}