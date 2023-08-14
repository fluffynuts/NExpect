using System;
using NUnit.Framework;
using static NExpect.Expectations;
// ReSharper disable SuspiciousTypeConversion.Global
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class Safety
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Assertions.DisableTracking();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            Assertions.EnableTracking();
        }

        [Test]
        public void To_Equals_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(1).To.Equals(1);
            }, Throws.Exception.InstanceOf<InvalidOperationException>()
                .With.Message.Contains(
                    "You probably intend to use .Equal(), not .Equals()"
                ));
            // Assert
        }

        [Test]
        public void ToAfterNot_Equals_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(1).Not.To.Equals(1);
            }, Throws.Exception.InstanceOf<InvalidOperationException>()
                .With.Message.Contains(
                    "You probably intend to use .Equal(), not .Equals()"
                ));
            // Assert
        }

        [Test]
        public void NotAfterTo_Equals_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(() =>
            {
                Expect(1).To.Not.Equals(1);
            }, Throws.Exception.InstanceOf<InvalidOperationException>()
                .With.Message.Contains(
                    "You probably intend to use .Equal(), not .Equals()"
                ));
            // Assert
        }
    }
}