using System;
using NExpect.Exceptions;
using NExpect.Tests.Exceptions.B;
using NUnit.Framework;

namespace NExpect.Tests.Exceptions
{
    namespace A
    {
        public class SomeException : Exception
        {
            public SomeException()
                : base("Some Exception!")
            {
            }
        }
    }

    namespace B
    {
        public class SomeException : Exception
        {
            public SomeException(): base("Some Exception!")
            {
            }
        }

        public class AnotherException : Exception
        {
            public AnotherException()
                : base("Another exception!")
            {
            }
        }
    }

    [TestFixture]
    public class Disambiguation
    {
        [Test]
        public void ShouldDisambiguateWhenExceptionHasSameClassName()
        {
            // Arrange
            // Act
            Assert.That(() =>
                {
                    Expect(() =>
                        {
                            throw new B.SomeException();
                        })
                        .To.Throw<A.SomeException>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("A.SomeException")
                    .And.Message.Contains("B.SomeException")
            );
            // Assert
        }

        [Test]
        public void ShouldNotDisambiguateWhenNoNeed()
        {
            // Arrange
            // Act
            Assert.That(() =>
                {
                    Expect(() => throw new B.SomeException())
                        .To.Throw<AnotherException>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.InnerException.InstanceOf<ArgumentException>()
            );
            Assert.That(() =>
                {
                    Expect(() => throw new A.SomeException())
                        .To.Throw<AnotherException>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.InnerException.InstanceOf<ArgumentException>()
            );
            // Assert
        }
    }
}