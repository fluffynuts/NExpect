using System.Collections.Generic;
using System.Linq;
using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using PeanutButter.Utils;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NExpect.Tests.DanglingPrepositions
{
    [TestFixture]
    public class For
    {
        [TestFixture]
        public class ForObjects
        {
            [Test]
            public void ShouldProvideExtensionPoint()
            {
                // Arrange
                var pet = GetRandom<Pet>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(pet).To.Be.For.Owner(pet.Owner);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldProvideExtensionPoint_Negative()
            {
                // Arrange
                var pet = GetRandom<Pet>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(pet).Not.To.Be.For.Owner(pet.Owner);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class ForCollections
        {
            [Test]
            public void ShouldProvideExtensionPoint()
            {
                // Arrange
                var pet1 = GetRandom<Pet>();
                var pet2 = GetRandom<Pet>();
                pet2.Owner = pet1.Owner;
                var pets = new[] { pet1, pet2 };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(pets).To.Be.For.Owner(pet1.Owner);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldProvideExtensionPoint_Negative()
            {
                // Arrange
                var pet1 = GetRandom<Pet>();
                var pet2 = GetRandom<Pet>();
                pet2.Owner = pet1.Owner;
                var pets = new[] { pet1, pet2 };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(pets).Not.To.Be.For.Owner(pet1.Owner);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class ForMore
        {
            [Test]
            public void ShouldProvideExtensionPointOnMore()
            {
                // Arrange
                var pet = GetRandom<Pet>();
                
                // Act
                Assert.That(() =>
                {
                    Expect(pet)
                        .To.Be.For.Owner(pet.Owner)
                        // yes, the syntax here doesn't make sense - I just
                        // need a quick 'n dirty test to enforce that .For is in
                        .For.Owner(pet.Owner);
                }, Throws.Nothing);
                // Assert
            }
        }
    }

    public class Pet
    {
        public Person Owner { get; set; }
    }

    internal static class ExtendingFor
    {
        internal static IMore<Pet> Owner(this IFor<Pet> petFor, Person expectedOwner)
        {
            return petFor.Compose(pet => Expect(pet.Owner).To.Deep.Equal(expectedOwner));
        }

        internal static IMore<IEnumerable<Pet>> Owner(this ICollectionFor<Pet> petFor, Person expectedOwner)
        {
            return petFor.Compose(pets =>
            {
                Expect(pets.All(p => p.Owner.DeepEquals(expectedOwner))).To.Be.True();
            });
        }
    }
}