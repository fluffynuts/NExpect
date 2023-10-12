using System;
using NUnit.Framework;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests.Exceptions;

[TestFixture]
public class EasierUsages
{
    [TestFixture]
    public class AnyException
    {
        [Test]
        public void ShouldBeAbleToInjectExtraErrorString()
        {
            // Arrange
            var parameterName = GetRandomAlphaString(10);
            var expected = GetRandomWords();
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                                throw new ArgumentException("foo", parameterName)
                        ).To.Throw<ArgumentException>(expected)
                        .For(parameterName);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToInjectExtraErrorGenerator()
        {
            // Arrange
            var parameterName = GetRandomAlphaString(10);
            var expected = GetRandomWords();
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                                throw new ArgumentException("foo", parameterName)
                        ).To.Throw<ArgumentException>(() => expected)
                        .For(parameterName);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                                throw new ArgumentException("foo", parameterName)
                        ).To.Throw<InvalidOperationException>(() => expected);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(expected)
            );
            // Assert
        }
    }

    [TestFixture]
    public class ExpectingArgumentException
    {
        [TestFixture]
        public class EasyParameterNameValidation
        {
            [Test]
            public void ShouldBeAbleToExpectParameterNameEasily()
            {
                // Arrange
                var parameterName = GetRandomAlphaString(10);
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                    throw new ArgumentException("foo", parameterName)
                            ).To.Throw<ArgumentException>()
                            .For(parameterName);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToInjectMoreInfoToTheErrorViaString()
            {
                // Arrange
                var parameterName = GetRandomAlphaString(10);
                var other = GetAnother(parameterName, () => GetRandomAlphaString());
                var expected = GetRandomWords();
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                    throw new ArgumentException("foo", other)
                            ).To.Throw<ArgumentException>()
                            .For(
                                parameterName,
                                expected
                            );
                    },
                    Throws.Exception.With.Message.Contains(expected)
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToInjectMoreInfoToTheErrorViaFunc()
            {
                // Arrange
                var parameterName = GetRandomAlphaString(10);
                var other = GetAnother(parameterName, () => GetRandomAlphaString());
                var expected = GetRandomWords();
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                    throw new ArgumentException("foo", other)
                            ).To.Throw<ArgumentException>()
                            .For(
                                parameterName,
                                () => expected
                            );
                    },
                    Throws.Exception.With.Message.Contains(expected)
                );
                // Assert
            }
        }
    }
}