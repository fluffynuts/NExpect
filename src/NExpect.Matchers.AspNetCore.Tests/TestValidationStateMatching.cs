using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestValidationStateMatching
    {
        [Test]
        public void ShouldBeAbleToAssertEmpty()
        {
            // Arrange
            var empty = new ValidationStateDictionary();
            var notEmpty = new ValidationStateDictionary();
            notEmpty.Add("foo", new ValidationStateEntry());
            notEmpty["foo"].SuppressValidation = true;
            // Act
            Assert.That(() =>
            {
                AspNetCoreExpectations.Expect(empty)
                    .To.Be.Empty();
                AspNetCoreExpectations.Expect(notEmpty)
                    .Not.To.Be.Empty();
            }, Throws.Nothing);
        
            Assert.That(() =>
            {
                AspNetCoreExpectations.Expect(notEmpty)
                    .To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        
            Assert.That(() =>
            {
                AspNetCoreExpectations.Expect(empty)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}