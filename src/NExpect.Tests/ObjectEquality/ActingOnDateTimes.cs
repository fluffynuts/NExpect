using System;
using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class ActingOnDateTimes
    {
        [Test]
        public void ShouldThrowWhenAttemptingToCompareDateTimesWithDifferentSpecifiedKinds()
        {
            // Arrange
            var utc = new DateTime(
                2020,
                8,
                5,
                13,
                56,
                37,
                DateTimeKind.Utc);
            var local = new DateTime(
                2020,
                8,
                5,
                15,
                56,
                37,
                DateTimeKind.Local);
            // Act
            Assert.That(() =>
                    Expect(local)
                        .To.Be.Greater.Than
                        .Or.Equal.To(utc),
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("have different kinds")
            );

            // Assert
        }
        
        [Test]
        public void ShouldNotThrowIfOneDateTimeKindIsUnSpecified()
        {
            // Arrange
            var utc = new DateTime(
                2020,
                8,
                5,
                13,
                56,
                37,
                DateTimeKind.Unspecified);
            var local = new DateTime(
                2020,
                8,
                5,
                15,
                56,
                37,
                DateTimeKind.Local);
            // Act
            Assert.That(() =>
                    Expect(local)
                        .To.Be.Greater.Than
                        .Or.Equal.To(utc),
                    Throws.Nothing
            );

            // Assert
        }
    }
}