using System;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;

namespace NExpect.Matchers.NSubstitute.Tests
{
    [TestFixture]
    public class TestReceivedExtensions
    {
        public interface IFoo
        {
            void Bar();
        }

        [TestFixture]
        public class WhenExpectingToReceive
        {
            [Test]
            public void WhenHaveReceivedAny_ShouldNotThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                // Act
                sub.Bar();
                // Assert
                sub.Received().Bar();
                Expectations.Expect(sub).To.Have
                           .Received().Bar();
            }

            [Test]
            public void WhenHaveReceivedTheCorrectAmount_ShouldNotThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                var count = RandomValueGen.GetRandomInt(1, 5);
                // Act
                PyLike.Range(0, count).ForEach(_ => sub.Bar());
                // Assert
                Expectations.Expect(sub).To.Have
                           .Received(count)
                           .Bar();
            }

            [Test]
            public void WhenHaveReceivedNoneAndExpectingAny_ShouldThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                // Act
                Assert.That(() => Expectations.Expect(sub).To.Have.Received().Bar(),
                    Throws.Exception.InstanceOf<ReceivedCallsException>());
                // Assert
            }
            
            [Test]
            public void WhenHaveReceivedTooFew_ShouldThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                var count = RandomValueGen.GetRandomInt(1, 10);
                // Act
                PyLike.Range(0, count).ForEach(_ => sub.Bar());
                Assert.That(() => Expectations.Expect(sub).To.Have.Received(count + RandomValueGen.GetRandomInt(1, 10)).Bar(),
                    Throws.Exception.InstanceOf<ReceivedCallsException>());
                // Assert
            }
            
            [Test]
            public void WhenHaveReceivedTooMany_ShouldThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                var count = RandomValueGen.GetRandomInt(1, 10);
                // Act
                PyLike.Range(0, count + RandomValueGen.GetRandomInt(1, 10)).ForEach(_ => sub.Bar());
                Assert.That(() => Expectations.Expect(sub).To.Have.Received(count).Bar(),
                    Throws.Exception.InstanceOf<ReceivedCallsException>());
                // Assert
            }
        }

        [TestFixture]
        public class WhenNotExpectingToReceive
        {
            [Test]
            public void WhenReceivedNone_ShouldNotThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                // Act
                // Assert
                Expectations.Expect(sub).Not.To.Have.Received().Bar();
            }
            
            [Test]
            public void WhenReceivedSome_ShouldThrow()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                // Act
                sub.Bar();
                // Assert
                Assert.That(() =>
                Expectations.Expect(sub).Not.To.Have.Received().Bar(),
                    Throws.Exception.InstanceOf<ReceivedCallsException>());
            }

            [Test]
            public void ShouldNotAllowCallCountOnNegation()
            {
                // Arrange
                var sub = Substitute.For<IFoo>();
                // Act
                sub.Bar();
                sub.Bar();
                // Assert
                Assert.That(() =>
                    Expectations.Expect(sub).Not.To.Have.Received(2).Bar(),
                    Throws.Exception.InstanceOf<NotSupportedException>());
            }
        }
    }
}