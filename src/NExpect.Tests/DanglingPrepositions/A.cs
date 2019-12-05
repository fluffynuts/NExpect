using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.MatcherLogic;
using NUnit.Framework;
using NExpect.Interfaces;
using static NExpect.Expectations;

namespace NExpect.Tests.DanglingPrepositions
{
    [TestFixture]
    public class A
    {
        [Test]
        public void ShouldProvideExtensionPoint()
        {
            // Arrange

            // Pre-Assert

            // Act
            Assert.That(
                () =>
                {
                    Expect(new Frog() as object).To.Be.A.Frog();
                },
                Throws.Nothing);
            Assert.That(
                () =>
                {
                    Expect(new Frog() as object).Not.To.Be.A.Frog();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected not to get a frog"));

            // Assert
        }
    }

    [TestFixture]
    public class MoreFromAddMatcher
    {
        [Test]
        public void ShouldBeAbleToContinueWithMore()
        {
            // Arrange
            // Act
            Assert.DoesNotThrow(() =>
            {
                Expect(new Frog() as object)
                    .To.Be.A.Frog()
                    .And.Not.To.Be.A.Dog();
            });
            // Assert
        }
    }

    public class Frog
    {
    }

    public class Dog
    {
    }

    public static class ExtensionsForTestingA
    {
        public static IMore<object> Frog(this IA<object> continuation)
        {
            return continuation.AddMatcher(
                o =>
                {
                    var passed = o is Frog;
                    return new MatcherResult(
                        passed,
                        () => $"Expected {passed.AsNot()}to get a dog"
                    );
                });
        }

        public static IMore<object> Dog(
            this IA<object> continuation)
        {
            return continuation.AddMatcher(o =>
            {
                var passed = o is Dog;
                return new MatcherResult(
                    passed,
                    () => $"Expected {passed.AsNot()}to get a dog"
                );
            });
        }
    }
}