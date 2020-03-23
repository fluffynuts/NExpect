using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using PeanutButter.Utils;
using static NExpect.Expectations;

namespace NExpect.Tests.DanglingPrepositions
{
    [TestFixture]
    public class With
    {
        [Test]
        public void ShouldBeAbleToDangleWith()
        {
            // Arrange
            var dog = new Dog() { Name = "Rex" };
            // Act
            Assert.That(() =>
                {
                    Expect(dog)
                        .To.Be.An.Instance.Of<Dog>()
                        .With.Name("Spot");
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("to have name 'Spot'")
            );
            
            Assert.That(() =>
            {
                Expect(dog)
                    .To.Be.An.Instance.Of<Dog>()
                    .With.Name("Rex");
            }, Throws.Nothing);

            // Assert
        }

        [Test]
        public void ShouldHaveGenericWithPropertyAvailable()
        {
            // Arrange
            var dog = new Dog() { Name = "Fluffy" };
            // Act
            Assert.That(() =>
            {
                Expect(dog)
                    .To.Be.An.Instance.Of<Dog>()
                    .With.Property(d => d.Name)
                    .Equal.To("Fluffy");
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(dog)
                    .To.Be.An.Instance.Of<Dog>()
                    .With.Property(d => d.Name)
                    .Equal.To("Rex");
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    public static class WithExtensions
    {
        public static IMore<Dog> Name(
            this IWith<Dog> with,
            string expected)
        {
            return with.AddMatcher(actual =>
            {
                var name = actual.Name; // ?.GetOrDefault(nameof(Animal.Name), null as string);
                var passed = name == expected;
                return new MatcherResult(
                    passed,
                    () => $"Expected {actual} to have name '{expected}'"
                );
            });
        }
    }

    public abstract class Animal
    {
        public string Name { get; set; }
    }

    public class Dog : Animal
    {
    }

    public class Cat : Animal
    {
    }

    public class Frog : Animal
    {
    }
}