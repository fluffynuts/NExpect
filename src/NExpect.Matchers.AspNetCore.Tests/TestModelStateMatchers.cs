using Microsoft.AspNetCore.Mvc.ModelBinding;
using NExpect.Exceptions;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestModelStateMatchers
{
    [TestFixture]
    public class Errors
    {
        [TestFixture]
        public class WhenModelStateHasErrors
        {
            [Test]
            public void ShouldReturnAssertErrorsExist()
            {
                // Arrange
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(GetRandomString(), GetRandomWords());
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(modelState)
                            .To.Have.Errors();
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ShouldFailIfNegatedAndErrorsExist()
            {
                // Arrange
                var modelState = new ModelStateDictionary();
                modelState.AddModelError(GetRandomString(), GetRandomWords());

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(modelState)
                            .Not.To.Have.Errors();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }

        [TestFixture]
        public class WHenModelStateHasNoErrors
        {
            [Test]
            public void ShouldAssertNoErrors()
            {
                // Arrange
                var modelState = new ModelStateDictionary();
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(modelState)
                            .Not.To.Have.Errors();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(modelState)
                            .To.Have.Errors();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }
    }
}