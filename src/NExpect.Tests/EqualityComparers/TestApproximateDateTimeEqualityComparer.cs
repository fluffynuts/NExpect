using System;
using System.ComponentModel;
using NExpect.EqualityComparers;
using NUnit.Framework;

namespace NExpect.Tests.EqualityComparers;

[TestFixture]
public class TestApproximateDateTimeEqualityComparer
{
    [TestFixture]
    public class Equality
    {
        [TestFixture]
        public class WhenDateTimesAreWithinConfiguredDrift
        {
            [Test]
            public void ShouldReturnTrue()
            {
                // Arrange
                var d1 = GetRandomDate();
                var delta = GetRandomInt(250, 750);
                var sign = GetRandomBoolean()
                    ? -1
                    : 1;
                delta *= sign;
                var d2 = d1.AddMilliseconds(delta);
                var sut = Create(TimeSpan.FromSeconds(1));
                // Act
                var result = sut.Equals(d1, d2);
                // Assert
                Expect(result)
                    .To.Be.True();
            }
        }

        [TestFixture]
        public class WhenDateTimesAreOutsideOfConfiguredDrift
        {
            [Test]
            public void ShouldReturnFalse()
            {
                // Arrange
                var d1 = GetRandomDate();
                var delta = GetRandomInt(1250, 1750);
                var sign = GetRandomBoolean()
                    ? -1
                    : 1;
                delta *= sign;
                var d2 = d1.AddMilliseconds(delta);
                var sut = Create(TimeSpan.FromSeconds(1));
                // Act
                var result = sut.Equals(d1, d2);
                // Assert
                Expect(result)
                    .To.Be.False();
            }
        }
    }

    private static ApproximateDateTimeEqualityComparer Create(
        TimeSpan allowedDrift
    )
    {
        return new(allowedDrift);
    }
}