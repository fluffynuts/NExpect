using System.Collections.Generic;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Exceptions;
using NExpect.Implementations;
using static NExpect.Expectations;
// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

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
                [TestFixture]
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
                    
                    [TestFixture]
                    public class WithValue
                    {
                        [TestFixture]
                        public class OperatingOnPrimitiveTypes
                        {
                            [Test]
                            public void WhenDictionaryHasKey_AndValueMatches_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomInt(2);
                                var src = new Dictionary<string, int>()
                                {
                                    [key] = value
                                };
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(value);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryDoesNotHaveKey_ShouldThrowForThatReason()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomInt(2);
                                var src = new Dictionary<string, int>()
                                {
                                    [key] = value
                                };
                                var testingValue = GetAnother(key);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(testingValue).With.Value(value);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"to contain key \"{testingValue}\""));

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryHasKey_AndValueDoesNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomInt(2);
                                var src = new Dictionary<string, int>()
                                {
                                    [key] = value
                                };
                                var testingValue = GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected {testingValue} but got {value}"));

                                // Assert
                            }
                        }

                        [TestFixture]
                        public class OperatingOnNullable
                        {
                            [Test]
                            public void WhenDictionaryHasKey_AndValueMatches_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                int? value = GetRandomInt(2);
                                var src = new Dictionary<string, int?>()
                                {
                                    [key] = value
                                };
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(value);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryHasKey_AndValueDoesNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                int? value = GetRandomInt(2);
                                var src = new Dictionary<string, int?>()
                                {
                                    [key] = value
                                };
                                var testingValue = GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected {testingValue} but got {value}"));

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryHasKey_AndValueIsNull_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                int? value = null;
                                var src = new Dictionary<string, int?>()
                                {
                                    [key] = value
                                };
                                var testingValue = GetRandomInt();
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected {testingValue} but got (null)"));

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryHasKey_AndMatchingValueIsNull_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                int? value = GetRandomInt();
                                var src = new Dictionary<string, int?>()
                                {
                                    [key] = value
                                };
                                int? testingValue = null;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected (null) but got {value}"));

                                // Assert
                            }
                        }

                        [TestFixture]
                        public class OperatingOnEnumerable
                        {
                            [Test]
                            public void WhenDictionaryHasKey_AndValueMatches_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = new List<int> {GetRandomInt(2,9), GetRandomInt(2, 9) , GetRandomInt(2, 9) };
                                var src = new Dictionary<string, List<int>>()
                                {
                                    [key] = value
                                };
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(value);
                                },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryHasKey_AndValueDoesNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = new List<int> { GetRandomInt(2, 9), GetRandomInt(2, 9), GetRandomInt(2, 9) };
                                var src = new Dictionary<string, List<int>>()
                                {
                                    [key] = value
                                };
                                var testingValue = new List<int> { GetRandomInt(10, 19), GetRandomInt(10, 19), GetRandomInt(10, 19) };
                                // Pre-Assert
                                
                                // Act
                                Assert.That(() =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message.Contains($"Expected {testingValue} but got {value}"));

                                // Assert
                            }
                        }

                        //[TestFixture]
                        //public class OperatingOnMisMatchedTypes
                        //{
                        //    [Test]
                        //    public void WhenDictionaryValueIsByte_AndExpectedIsInt_ShouldThrow()
                        //    {
                        //        // Arrange
                        //        var key = GetRandomString(2);
                        //        var value = (sbyte)GetRandomInt(2);
                        //        var src = new Dictionary<string, sbyte>()
                        //        {
                        //            [key] = value
                        //        };
                        //        var expected = (int)value;
                        //        // Pre-Assert

                        //        // Act
                        //        Assert.That(() =>
                        //        {
                        //            //Expect(src).To.Contain.Key(key).With.Value(expected);
                        //        },
                        //            Throws.Nothing);

                        //        // Assert
                        //    }
                        //}
                    }
                }
            }
        }
    }
}