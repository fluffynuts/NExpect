using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class ComposingExpectations
    {
        [TestFixture]
        public class SingleItems
        {
            [Test]
            public void PositiveComposition_WhenPasses_ShouldNotThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person).To.Be.A.Benny();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void PositiveComposition_WhenFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Sally",
                    Gender = Genders.Female
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(person).To.Be.A.Benny();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {person.Stringify()} to be a Benny")
                );
                // Assert
            }

            [Test]
            public void NegativeComposition_WhenFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person).Not.To.Be.A.Benny();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        $"Expected {person.Stringify()} not to be a Benny"
                    ));
                // Assert
            }

            [Test]
            public void NegativeComposition_WhenPasseds_ShouldNotThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Andrew",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person).Not.To.Be.A.Benny();
                }, Throws.Nothing);
                // Assert
            }
        }

        [TestFixture]
        public class Collections
        {
            [Test]
            public void PositiveComposition_WhenPasses_ShouldNotThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).To.Be.Bennies();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void PositiveComposition_Multiple_WhenPasses_ShouldNotThrow()
            {
                // Arrange
                var person1 = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                var person2 = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(new[] {person1, person2}).To.Be.Bennies();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void PositiveComposition_WhenFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Sally",
                    Gender = Genders.Female
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).To.Be.Bennies();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Not.Contains("not"));
                // Assert
            }

            [Test]
            public void NegativeComposition_WhenFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).Not.To.Be.Bennies();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        "not to all be Bennies"
                    ));
                // Assert
            }

            [Test]
            public void NegativeComposition_WhenFails_WithNoCustomMessage_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Benny",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).Not.To.Be.Bennies();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void NegativeComposition_WhenPasses_ShouldNotThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Andrew",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).Not.To.Be.Bennies2();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void PositiveComposition_WhenFails_OnCompositionWithoutMessageGenerator_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Andrew",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person.AsArray()).To.Be.Bennies2();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Bennies2"));
                // Assert
            }
        }
    }

    public enum Genders
    {
        Unknown,
        Male,
        Female,
        Other
    }

    public class Person
    {
        public string Name { get; set; }
        public Genders Gender { get; set; }
    }

    internal static class PersonMatchers
    {
        internal static void Benny(this IA<Person> a)
        {
            a.Compose(person =>
            {
                Expect(person).Not.To.Be.Null("Null person");
                Expect(person.Gender).To.Equal(Genders.Male);
                Expect(person.Name).To.Equal("Benny");
            }, (person, passed) =>
                $"Expected {person.Stringify()} {passed.AsNot()}to be a Benny");
        }

        internal static void Bennies(this ICollectionBe<Person> be)
        {
            be.Compose(persons =>
            {
                persons.ForEach(p =>
                {
                    Expect(p).To.Be.A.Benny();
                });
            }, (collection, passed) =>
                $"Expected {collection.Stringify()} {passed.AsNot()}to all be Bennies");
        }

        internal static void Bennies2(this ICollectionBe<Person> be)
        {
            be.Compose(persons =>
            {
                persons.ForEach(p =>
                {
                    Expect(p).To.Be.A.Benny();
                });
            });
        }
    }
}