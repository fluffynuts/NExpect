using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;

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
                    Expectations.Expect(new Frog() as object).To.Be.A.Frog();
                },
                Throws.Nothing);
            Assert.That(
                () =>
                {
                    Expectations.Expect(new Frog() as object).Not.To.Be.A.Frog();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected not to get a frog"));

            // Assert
        }
    }

    public class Frog
    {
    }

    public static class ExtensionsForTestingA
    {
        public static void Frog(this IA<object> continuation)
        {
            continuation.AddMatcher(
                o =>
                {
                    var passed = o is Frog;
                    return new MatcherResult(
                        passed,
                        () => passed
                            ? "Expected not to get a frog"
                            : "Expected to get a frog");
                });
        }
    }
}