using System;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
// ReSharper disable PossibleNullReferenceException

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable SuspiciousTypeConversion.Global

namespace NExpect.Tests
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

        [Test]
        public void MatcherWhichThrowsUnmetExpectationException_ShouldGetThatExactException()
        {
            // Arrange
            // UnmetExpectationException can only be instatiated within NExpect; however, a little
            //  reflection can get to the internal Throw method
            var method = typeof(Assertions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .FirstOrDefault(mi => mi.Name == "Throw" && mi.GetParameters().Length == 1);
            Expect(method).Not.To.Be.Null();
            UnmetExpectationException expected = null;
            try
            {
                method.Invoke(null, new object[] { GetRandomString(10) });
            }
            catch (TargetInvocationException tex)
            {
                expected = tex.InnerException as UnmetExpectationException;
            }

            // Pre-assert
            // Act
            try
            {
                Expect("moo").To.Moo(expected);
            }
            catch (Exception ex)
            {
                Expect(ex).To.Be(expected);
            }

            // Assert
        }

        [Test]
        public void CustomCodeGettingToExpectedCountOnCountMatcher()
        {
            // Arrange
            var expected = GetRandomInt(2, 7);
            // Pre-assert
            // Act
            var continuation = Expect(new[] { 1, 2, 3 }).To.Contain.Exactly(expected);
            var result = continuation.GetExpectedCount<int>();
            // Assert
            Expect(result).To.Equal(expected);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_Exactly()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a", "b", "c" }).To.Contain.Exactly(123);
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.Exactly);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_AtLeast()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a", "b", "c" }).To.Contain.At.Least(123);
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.Minimum);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_AtMost()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a", "b", "c" }).To.Contain.At.Most(123);
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.Maximum);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_Any()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a", "b", "c" }).To.Contain.Any();
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.Any);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_All()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a", "b", "c" }).To.Contain.All();
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.All);
        }

        [Test]
        public void CustomCodeGettingToCountMatchMethod_Only()
        {
            // Arrange
            // Pre-assert
            // Act
            var continuation = Expect(new[] { "a" }).To.Contain.Only(1);
            var result = continuation.GetCountMatchMethod();
            // Assert
            Expect(result).To.Equal(CountMatchMethods.Only);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MatcherResultWithNoMessage(bool expected)
        {
            // Arrange
            // Pre-assert
            // Act
            var result = new MatcherResult(expected);
            // Assert
            Expect(result.Passed).To.Equal(expected);
        }

        [Test]
        public void TryGetActual_ShouldThrowIf_ICanAddMatcher_HasNoActual()
        {
            // Arrange
            // Pre-assert
            // Act
            Expect(() => (new SomeCanAddMatcher()).GetActual())
                .To.Throw<InvalidOperationException>();
            // Assert
        }

        public class SomeCanAddMatcher : ICanAddMatcher<string>
        {
        }
    }

    public static class MatcherThrowingUnmentExpectationException
    {
        public static void Moo(
            this ITo<string> to,
            UnmetExpectationException ex)
        {
            to.AddMatcher(actual => throw ex);
        }
    }
}