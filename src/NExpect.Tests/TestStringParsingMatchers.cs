using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests;

[TestFixture]
public class TestUuidStringValidationMatchers
{
    [TestFixture]
    public class GuidAssertions
    {
        [Test]
        public void ShouldBeAbleToAssertAValidGuidString()
        {
            // Arrange
            var validUuid = "497173de-e495-4197-8828-5822d454f1b9";
            // Act
            Assert.That(() =>
            {
                Expect(validUuid)
                    .To.Be.A.Guid();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(validUuid)
                    .Not.To.Be.A.Guid();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAnInvalidGuidString()
        {
            // Arrange
            var inValidUuid = "497173de-e495-419n-8828-5822d454f1b9";

            // Act
            Assert.That(() =>
            {
                Expect(inValidUuid)
                    .To.Be.A.Guid();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains(inValidUuid));
            Assert.That(() =>
            {
                Expect(inValidUuid)
                    .Not.To.Be.A.Guid();
            }, Throws.Nothing);
            // Assert
        }
    }
    
    [TestFixture]
    public class UuidAssertions
    {
        [Test]
        public void ShouldBeAbleToAssertAValidUuidString()
        {
            // Arrange
            var validUuid = "497173de-e495-4197-8828-5822d454f1b9";
            // Act
            Assert.That(() =>
            {
                Expect(validUuid)
                    .To.Be.An.Uuid();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(validUuid)
                    .Not.To.Be.An.Uuid();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAnInvalidUuidString()
        {
            // Arrange
            var inValidUuid = "497173de-e495-419n-8828-5822d454f1b9";

            // Act
            Assert.That(() =>
            {
                Expect(inValidUuid)
                    .To.Be.An.Uuid();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains(inValidUuid));
            Assert.That(() =>
            {
                Expect(inValidUuid)
                    .Not.To.Be.An.Uuid();
            }, Throws.Nothing);
            // Assert
        }
    }
}