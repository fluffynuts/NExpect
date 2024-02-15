using System.Drawing;
using NExpect.Exceptions;
using NUnit.Framework;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NExpect.Implementations;

namespace NExpect.Tests.DanglingPrepositions
{
    [TestFixture]
    public class Contain
    {
        [Test]
        public void DanglingContain()
        {
            // Arrange
            var outer = new Rectangle(0, 0, 100, 100);
            var inner = new Rectangle(10, 10, 10, 10);
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                Expect(outer).Not.To.Contain.Shape(inner);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(outer).To.Not.Contain.Shape(inner);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(outer).To.Contain.Shape(inner);
            }, Throws.Nothing);
            // Assert
        }
    }

    public static class ShapeMatchers
    {
        public static void Shape(
            this IContain<Rectangle> contain,
            Rectangle other
        )
        {
            contain.AddMatcher(actual =>
            {
                var passed = actual.Left <= other.Left &&
                             actual.Right >= other.Right &&
                             actual.Top <= other.Top &&
                             actual.Bottom >= other.Bottom;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual.Stringify()} {passed.AsNot()}to contain {other.Stringify()}");
            });
        }
    }
}