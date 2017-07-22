using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestA
    {
        [Test]
        public void A_ShouldProvideExtensionPoint()
        {
            // Arrange

            // Pre-Assert

            // Act
            Assert.That(() =>
            {
                Expect(new Frog() as object).To.Be.A.Frog();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(new Frog() as object).Not.To.Be.A.Frog();
            }, Throws.Exception.InstanceOf<AssertionException>()
                .With.Message.Contains("Expected not to get a frog"));

            // Assert
        }
    }

    public class Frog
    {
    }

    public static class TestAExtensions
    {
        public static void Frog(this IA<object> continuation)
        {
            continuation.AddMatcher(o =>
            {
                var passed = o is Frog;
                var message = passed ? "Expected not to get a frog": "Expected to get a frog";
                return new MatcherResult(passed, message);
            });
        }
    }
}