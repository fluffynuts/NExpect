using System.Linq;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;

namespace NExpect.Tests.DanglingPrepositions
{
    [TestFixture]
    public class Have
    {
        [Test]
        public void DanglingHave()
        {
            // Arrange
            var parent = new Container()
            {
                Subs = RandomValueGen.GetRandomArray<Sub>(2)
            };
            var search = RandomValueGen.GetRandomFrom(parent.Subs);
            // Pre-assert
            // Act
            Assert.That(() =>
            {
                Expect(parent).Not.To.Have.Child(search);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(parent).To.Not.Have.Child(search);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            
            Assert.That(() =>
            {
                Expect(parent).To.Have.Child(search);
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
    public static class ObjectHaveMatchers
    {
        public static void Child(
            this IHave<Container> contain,
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