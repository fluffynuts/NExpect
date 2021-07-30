using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class SetTesting
    {
        [Test]
        public void ShouldBeAbleToAssertHashSetIsEmpty()
        {
            // Arrange
            var empty = new HashSet<int>() as ISet<int>;
            var notEmpty = new HashSet<int>(new[] { 1 }) as ISet<int>;
            // Act
            Assert.That(() =>
            {
                Expect(empty)
                    .To.Be.Empty();
            }, Throws.Nothing);
            
            Assert.That(() =>
            {
                Expect(empty)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(notEmpty)
                    .To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(notEmpty)
                    .Not.To.Be.Empty();
            }, Throws.Nothing);
            // Assert
        }
    }
}