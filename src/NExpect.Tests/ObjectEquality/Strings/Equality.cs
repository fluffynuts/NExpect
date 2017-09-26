using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;
// ReSharper disable MemberHidesStaticFromOuterClass

namespace NExpect.Tests.ObjectEquality.Strings
{
    [TestFixture]
    public class Equality
    {
        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Equal
            {
                [Test]
                public void WithCustomMessage()
                {
                    // Arrange
                    var expected = RandomValueGen.GetRandomString();
                    var test = RandomValueGen.GetRandomString();
                    var nonMatch = RandomValueGen.GetAnother(test);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).To.Equal(nonMatch, expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(expected));
                    // Assert
                }
            }

            [TestFixture]
            public class Be
            {
                [TestFixture]
                public class Equal
                {
                    [TestFixture]
                    public class To
                    {
                        [Test]
                        public void WithCustomMessage()
                        {
                            // Arrange
                            var expected = RandomValueGen.GetRandomString();
                            var test = RandomValueGen.GetRandomString();
                            var nonMatch = RandomValueGen.GetAnother(test);
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(test).To.Be.Equal.To(nonMatch, expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected));
                            // Assert
                        }
                    }
                }
            }
        }
    }
}