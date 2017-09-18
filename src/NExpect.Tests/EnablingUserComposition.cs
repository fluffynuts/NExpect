using NExpect.Exceptions;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class EnablingUserComposition
    {
        [TestFixture]
        public class SimpleChain
        {
            [Test]
            public void PositiveExpectation_WhenPasses_ShouldNotThrow()
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
                    Expect(person).To.Be.A.Sally().And.A.Female();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void PositiveExpectation_WhenFirstPartFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Mary",
                    Gender = Genders.Female
                };
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(person).To.Be.A.Sally().And.A.Female();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Sally"));
                // Assert
            }

            [Test]
            public void PositiveExpectation_WhenSecondPartFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Sally",
                    Gender = Genders.Unknown
                };
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(person).To.Be.A.Sally().And.A.Female();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Female"));
                // Assert
            }

            [Test]
            public void NegativeExpectation_WhenFirstPartFails_ShouldThrow()
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
                    Expect(person).Not.To.Be.A.Sally().And.Not.To.Be.A.Female();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Sally"));
                // Assert
            }

            [Test]
            public void NegativeExpectation_WhenSecondPartFails_ShouldThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Mary",
                    Gender = Genders.Female
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person).Not.To.Be.A.Sally().And.Not.To.Be.A.Female();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Female"));
                // Assert
            }
        }

        [TestFixture]
        public class NullTestShouldAllowUserComposition
        {
            [Test]
            public void Contrived_PositiveChain_WhenAllPasses_ShouldNotThrow()
            {
                // Arrange
                var contrived = GetRandomString(2);
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(contrived).Not.To.Be.Null().And.To.Equal(contrived);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void MorePractical_PositiveChain_WhenAllPassws_ShouldNotThrow()
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
                    Expect(person)
                        .Not.To.Be.Null()
                        .And.To.Be.A.Benny();
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void MorePractical_PositiveThenNegativeChain_WhenAllPassws_ShouldNotThrow()
            {
                // Arrange
                var person = new Person()
                {
                    Name = "Bob",
                    Gender = Genders.Male
                };
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person)
                        .Not.To.Be.Null()
                        .And.Not.To.Be.A.Benny();
                }, Throws.Nothing);
                // Assert
            }            
            
            [Test]
            public void MorePractical_WhenFirstPartFails_ShouldThrowThere()
            {
                // Arrange
                Person person = null;
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expect(person)
                        .Not.To.Be.Null()
                        .And.Not.To.Be.A.Sally();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected not to get null"));
                // Assert
            }
        }
    }

    internal static class MorePersonMatchers
    {
        internal static IMore<Person> Sally(
            this IA<Person> a
        )
        {
            a.Compose(person =>
            {
                Expect(person.Name).To.Equal("Sally");
            });
            return a.More();
        }

        internal static IMore<Person> Female(
            this IA<Person> a
        )
        {
            a.Compose(person =>
            {
                Expect(person.Gender).To.Equal(Genders.Female);
            });
            return a.More();
        }
    }
}