using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using PeanutButter.Utils;
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
            var parent = new Container()
            {
                Subs = GetRandomArray<Sub>(2)
            };
            var search = GetRandomFrom(parent.Subs);
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                Expect(parent).Not.To.Contain.Child(search);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(parent).To.Not.Contain.Child(search);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(parent).To.Contain.Child(search);
            }, Throws.Nothing);
            // Assert
        }
    }

    public class Container
    {
        public Sub[] Subs { get; set; }
    }

    public class Sub
    {
        public string Name { get; set; }
    }

    public static class ObjectContainMatchers
    {
        public static void Child(
            this IContain<Container> contain,
            Sub sub)
        {
            contain.AddMatcher(actual =>
            {
                var passed = (actual?.Subs ?? new Sub[0])
                    .Any(c => c.Name == sub.Name);
                return new MatcherResult(
                    passed,
                    $"Expected {actual.Stringify()} {passed.AsNot()}to contain sub {sub.Name}");
            });
        }
    }    
}