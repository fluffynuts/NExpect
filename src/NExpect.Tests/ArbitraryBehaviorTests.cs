using System;
using System.Linq;
using System.Reflection;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NExpect.Implementations;
using NExpect.Tests.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

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
                method.Invoke(null, new object[] {GetRandomString(10)});
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
            var continuation = Expect(new[] {1, 2, 3}).To.Contain.Exactly(expected);
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
            var continuation = Expect(new[] {"a", "b", "c"}).To.Contain.Exactly(123);
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
            var continuation = Expect(new[] {"a", "b", "c"}).To.Contain.At.Least(123);
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
            var continuation = Expect(new[] {"a", "b", "c"}).To.Contain.At.Most(123);
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
            var continuation = Expect(new[] {"a", "b", "c"}).To.Contain.Any();
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
            var continuation = Expect(new[] {"a", "b", "c"}).To.Contain.All();
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
            var continuation = Expect(new[] {"a"}).To.Contain.Only(1);
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
            Expect(result.Message).To.Be.Empty();
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

        [TestFixture]
        public class ShouldHaveActualProperty
        {
            [Test]
            public void CollectionEqual()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Equal;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionDeepEqual()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Deep.Equal;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionEquivalent()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Equivalent;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionDeepEquivalent()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Deep.Equivalent;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionIntersectionEquivalent()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Intersection.Equivalent;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionIntersectionEqual()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Be.Intersection.Equal;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionUnique()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Have.Unique;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void CollectionContainAt()
            {
                // Arrange
                var collection = GetRandomCollection<int>(1);
                // Pre-assert
                // Act
                var sut = Expect(collection).To.Contain.At;
                // Assert
                Expect(sut.GetActual()).To.Be(collection);
            }

            [Test]
            public void Deep()
            {
                // Arrange
                var src = new { };
                // Pre-assert
                // Act
                var sut = Expect(src).To.Deep;
                // Assert
                Expect(sut.GetActual()).To.Be(src);
            }

            [Test]
            public void DictionaryKeyWith()
            {
                // Arrange
                var dict = new Dictionary<string, string>
                {
                    ["key"] = "value"
                };
                // Pre-assert
                // Act
                var sut = Expect(dict).To.Contain.Key("key").With;
                // Assert
                Expect(sut.GetActual()).To.Equal("value");
            }

            [Test]
            public void BeEqual()
            {
                // Arrange
                var src = GetRandomString();
                // Pre-assert
                // Act
                var sut = Expect(src).To.Be.Equal;
                // Assert
                Expect(sut.GetActual()).To.Be(src);
            }

            [Test]
            public void Intersection()
            {
                // Arrange
                var expected = GetRandom<SomeNode>();
                // Pre-assert
                // Act
                var sut = Expect(expected).To.Intersection;
                // Assert
                Expect(sut.GetActual()).To.Equal(expected);
            }

            [Test]
            public void NotNullOr()
            {
                // Arrange
                var expected = GetRandomString(10);
                // Pre-assert
                // Act
                var sut = Expect(expected).Not.To.Be.Null.Or;
                // Assert
                Expect(sut.GetActual()).To.Be(expected);
            }
            
            [Test]
            public void LessContinuation()
            {
                // Arrange
                var expected = GetRandomInt();
                // Pre-assert
                // Act
                var sut = Expect(expected).To.Be.Less;
                // Assert
                Expect(sut.GetActual()).To.Equal(expected);
            }

            [Test]
            public void ThrowContinuationOfT()
            {
                // Arrange
                var expected = new InvalidOperationException(GetRandomString(10));
                // Pre-assert
                // Act
                var sut = Expect(() => throw expected).To.Throw<InvalidOperationException>();
                // Assert
                Expect(sut.GetActual()).To.Equal(expected);
            }

            [Test]
            public void ThrowContinuation()
            {
                // Arrange
                var expected = new InvalidOperationException(GetRandomString(10));
                // Pre-assert
                // Act
                var sut = Expect(() => throw expected).To.Throw();
                // Assert
                Expect(sut.GetActual()).To.Equal(expected);
            }
        }

        [Test]
        public void CountMatchDeepEqual_ShouldExposeOriginalContinuation()
        {
            // Arrange
            // Pre-assert
            // Act
            var original = Expect(new[] {1}).To.Contain;
            var sut = original.Exactly(1).Deep.Equal;
            // Assert
            Expect(sut.GetPropertyValue("Continuation")).To.Be(original);
        }

        [Test]
        public void CountMatchIntersectionEqual_ShouldExposeOriginalContinuation()
        {
            // Arrange
            // Pre-assert
            // Act
            var original = Expect(new[] {1}).To.Contain;
            var sut = original.Exactly(1).Intersection.Equal;
            // Assert
            Expect(sut.GetPropertyValue("Continuation")).To.Be(original);
        }

        [Test]
        public void DeepEquivalenceVsNull()
        {
            // Arrange
            var src = new[] {"hello"};
            var test = new[] {null as string};
            // Pre-assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(src).Not.To.Be.Equivalent.To(test);
                    Expect(test).Not.To.Be.Equivalent.To(src);
                },
                Throws.Nothing);
            // Assert
        }

        [TestFixture]
        public class DanglersForUserspaceExtension
        {
            [TestFixture]
            public class AndExtension
            {
                [Test]
                public void ShouldHave_HaveAndAn()
                {
                    // Arrange
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect("foo the ant").To.Have.A.Space()
                                .And.Have.A.Foo()
                                .And.An.Ant();
                        },
                        Throws.Nothing);
                    // Assert
                }
            }

            [Test]
            public void ExceptionPropertyCollectionEquivalenceTesting_Danglers()
            {
                // Arrange
                var expected = new SomeNode()
                {
                    Id = 1,
                    Name = "Moo"
                };
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ExceptionWithNode(expected))
                            .To.Throw<ExceptionWithNode>()
                            .With.CollectionProperty(e => e.Nodes).For.Moo();
                    },
                    Throws.Nothing);
            }

            [Test]
            public void InDangler()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var message = new[] {e1, e2}.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ArgumentNullException(message))
                            .To.Throw<ArgumentNullException>()
                            .With.Message.Containing(e1);
                    },
                    Throws.Nothing);
                // Assert
            }

            [TestCase("Negate")]
            [TestCase("ResetNegation")]
            public void NotInstanceDoesNotSupport_(string method)
            {
                // Arrange
                // Pre-assert
                // Act
                var continuation = Expect(new object()).Not.To.Be.An.Instance;
                var mi = continuation.GetType().GetMethod(method);
                Expect(mi).Not.To.Be.Null();
                Expect(() => mi.Invoke(continuation, new object[0])).To.Throw();
                // Assert
            }

            [Test]
            public void UnmetExpectationStackTraces_ShouldOmitTraversalThroughNExpect()
            {
                // Arrange
                // Pre-assert
                // Act
                UnmetExpectationException captured = null;
                try
                {
                    Expect(1).To.Be.Falsey();
                }
                catch (UnmetExpectationException ex)
                {
                    captured = ex;
                }
                
                // Assert
                Expect(captured).Not.To.Be.Null();
                var lines = captured.StackTrace.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                Expect(lines).To.Contain.Only(1).Item();
                Expect(lines[0]).To.Contain(nameof(UnmetExpectationStackTraces_ShouldOmitTraversalThroughNExpect));
            }
        }

    }

    public static class MatcherThrowingUnmentExpectationException
    {
        public static void CurrentFilePath(
            this IStringContain contain,
            [CallerFilePath] string path = null)
        {
            contain.AddMatcher(
                actual =>
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var passed = actual.Contains(path);
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual} to contain {path}");
                });
        }

        public static void Falsey(this IBe<long> be)
        {
            be.AddMatcher(
                actual =>
                {
                    var passed = actual == 0;
                    return new MatcherResult(
                        passed,
                        () => $"Expected {actual} {passed.AsNot()}to be falsey");
                });
        }

        public static void Moo(this ICollectionFor<SomeNode> continuation)
        {
            continuation.Compose(
                actual =>
                {
                    Expect(actual).To.Contain.Exactly(1).Matched.By(n => n.Name == "Moo");
                });
        }

        public static void Moo(
            this ITo<string> to,
            UnmetExpectationException ex)
        {
            to.AddMatcher(actual => throw ex);
        }

        public static IMore<string> Space(this IA<string> a)
        {
            return a.Compose(
                actual =>
                {
                    Expect(actual).To.Contain(" ");
                });
        }

        public static IMore<string> Foo(this IA<string> a)
        {
            return a.Compose(
                actual =>
                {
                    Expect(actual).To.Contain("foo");
                });
        }

        public static IMore<string> Ant(this IAn<string> an)
        {
            return an.Compose(
                actual =>
                {
                    Expect(actual).To.Contain("ant");
                });
        }
    }
}