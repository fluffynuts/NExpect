using System;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
using NExpect;
using NExpect.Exceptions;
using PeanutButter.Utils;

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class ApproximateEquality
    {
        [TestFixture]
        public class DateTimeValues
        {
            [Test]
            public void WhenWithinASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(-1000, 1000));
                // Pre-assert
                // Act
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2);
                    Expect(d2).To.Approximately.Equal(d1);
                }, Throws.Nothing);
                // Assert
            }            
                        
            [Test]
            public void WhenAtASecond_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(1000);
                // Pre-assert
                // Act
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2);
                    Expect(d2).To.Approximately.Equal(d1);
                }, Throws.Nothing);
                // Assert
            }            
            
            [Test]
            public void WhenProvidedDrift_ShouldNotThrow()
            {
                // Arrange
                var d1 = GetRandomDate();
                var allowedDrift = TimeSpan.FromSeconds(2);
                var d2 = d1.AddMilliseconds(GetRandomInt(-2000, 2000));
                // Pre-assert
                // Act
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, allowedDrift);
                    Expect(d2).To.Approximately.Equal(d1, allowedDrift);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenOutsideOneSecond_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(1001, 2000));
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, message);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));                
                                
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, () => message);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));                
                
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected {d1.Stringify()} to approximately equal {d2.Stringify()}"));
                // Assert
            }
            
            [Test]
            public void WhenOutsideAllowedDrift_ShouldThrow_IncludingCustomMessageIfPresent()
            {
                // Arrange
                var d1 = GetRandomDate();
                var d2 = d1.AddMilliseconds(GetRandomInt(501, 1000));
                var allowedDrift = TimeSpan.FromMilliseconds(500);
                var message = GetRandomString(1);
                // Pre-assert
                // Act
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, allowedDrift, message);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));                
                                
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, allowedDrift, () => message);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(message));                
                
                Assert.That(() =>
                {
                    Expect(d1).To.Approximately.Equal(d2, allowedDrift);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected {d1.Stringify()} to approximately equal {d2.Stringify()}"));
                // Assert
            }
        }
    }
}