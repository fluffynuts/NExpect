using System;
using System.Linq;
using NExpect.Exceptions;
using NExpect.MatcherLogic;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable SuspiciousTypeConversion.Global

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class ArbitraryBehaviorTests
    {
        [Test]
        public void CountMatchContinuation_ShouldHaveActualPropertyExposingOriginalCollection()
        {
            // Arrange
            var collection = GetRandomCollection<int>(2).ToArray();
            // Pre-assert
            // Act
            var result = Expect(collection).To.Contain.Exactly(1);
            // Assert
            Expect(result.GetActual()).To.Be(collection);
        }

        [Test]
        public void WhenCustomMessageGeneratorThrows()
        {
            // Arrange
            var collection = GetRandomCollection<int>(2).ToArray();
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(collection).To.Be.Empty(() => throw new Exception("moo"));
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contain("Unable to evaluate custom message expression"));
            // Assert
        }

        [Test]
        public void MessageForNotContains()
        {
            // Arrange
            var str = "moo, cow";
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(str))
                        .To.Throw<Exception>()
                        .With.Message.Not.Containing("beef");
                },
                Throws.Nothing);

            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(str))
                        .To.Throw<Exception>()
                        .With.Message.Not.Containing("cow");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Match("not\\sto\\scontain\\s\"cow\""));

            Assert.That(
                () =>
                {
                    Expect(() => throw new Exception(str))
                        .To.Throw<Exception>()
                        .With.Message.Containing("beef");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Match("to\\scontain\\s\"beef\""));
            // Assert
        }

        [Test]
        public void Inadvertant_Equals_InsteadOfEqual()
        {
            // Arrange
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(1).To.Equals(1);
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably intend to use .Equal(), not .Equals()"));

            Assert.That(
                () =>
                {
                    Expect(1).Equals(1);
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably intend to use .Equal(), not .Equals()"));

            Assert.That(
                () =>
                {
                    Expect(1).Not.To.Equals(1);
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably intend to use .Equal(), not .Equals()"));
            // Assert
        }

        [Test]
        public void ExpectationContextHashingShouldThrow()
        {
            // Arrange
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(1).GetHashCode();
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably shouldn't be hashing this"));
            Assert.That(
                () =>
                {
                    Expect(1).To.GetHashCode();
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably shouldn't be hashing this"));
            Assert.That(
                () =>
                {
                    Expect(1).To.Be.GetHashCode();
                },
                Throws.Exception.InstanceOf<InvalidOperationException>()
                    .With.Message.Contain("You probably shouldn't be hashing this"));
            // Assert
        }

        [Test]
        public void CollectionsOfNulls()
        {
            // Arrange
            var left = new string[] {null};
            var right = new string[] {null};
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(left).To.Be.Equivalent.To(right);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void CollectionsOfNullsDeep()
        {
            // Arrange
            var left = new string[] {null};
            var right = new string[] {null};
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(left).To.Be.Deep.Equivalent.To(right);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void CollectionsOfNullsDeepWhereOneHasNoNull()
        {
            // Arrange
            var left = new string[] {null};
            var right = new[] {"cow"};
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(left).Not.To.Be.Deep.Equivalent.To(right);
                },
                Throws.Nothing);
            // Assert
        }

        [Test]
        public void ContainInOrderContinuationAfterAnd()
        {
            // Arrange
            var left = GetRandomString(72, 128);
            var right = GetRandomString(72, 128);
            // Pre-Assert
            // Act
            var ex = Assert.Throws<UnmetExpectationException>(
                () => Expect(left).To.Equal(right)
            );
            // Assert
            Expect(ex.Message)
                .To.Start.With("Expected")
                .And.To.Contain.In.Order(
                    "\n",
                    right,
                    "\n",
                    "but got",
                    "\n",
                    left);
        }
    }
}