using System.Collections.Generic;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Exceptions;
using NExpect.Implementations;
using static NExpect.Expectations;
// ReSharper disable InconsistentNaming

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class DictionaryTesting
    {
        [TestFixture]
        public class Expect_Dictionary_To_Contain
        {
            [TestFixture]
            public class Key
            {
                public class OperatingOnPlainDictionary
                {
                    [Test]
                    public void WhenDictionaryHasKey_ShouldNotThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new Dictionary<string, int>()
                        {
                            [key] = GetRandomInt()
                        };
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(src).To.Contain.Key(key);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenDictionaryHasKey_ShouldThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new Dictionary<string, int>()
                        {
                            [key] = GetRandomInt()
                        };
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(src).Not.To.Contain.Key(key);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(
                                    $"not to contain key {key.Stringify()}"
                                ));

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_WhenDictionaryHasKey_ShouldThrow()
                    {
                        // Arrange
                        var key = GetRandomString(2);
                        var src = new Dictionary<string, int>()
                        {
                            [key] = GetRandomInt()
                        };
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(src).To.Not.Contain.Key(key);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(
                                    $"not to contain key {key.Stringify()}"
                                ));

                        // Assert
                    }
                }
            }
        }
    }
}