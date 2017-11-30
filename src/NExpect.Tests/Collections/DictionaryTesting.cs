using System;
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
        public class ShortContain
        {
            [Test]
            public void WhenDoesContainSoughtValue_ShouldNotThrow()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var src = new Dictionary<string, string>()
                {
                    [kvp.Key] = kvp.Value
                };
                var dict = src as IDictionary<string, string>;

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(dict).To.Contain(kvp);
                        foreach (var item in dict)
                            Expect(dict).To.Contain(item);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void PositiveAssertion_WhenDoesNotContainSoughtValue_ShouldThrow()
            {
                // Arrange
                var missed = GetRandom<KeyValuePair<string, string>>();
                var have = GetAnother(missed);
                var dict = new Dictionary<string, string>()
                {
                    [have.Key] = have.Value
                };

                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(dict).To.Contain(missed);
                }, Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected {dict.Stringify()} to contain {missed.Stringify()}"));

                // Assert
            }

            [Test]
            public void NegativeAssertion_WhenDoesContainSoughtValue_ShouldThrow()
            {
                // Arrange
                var have = GetRandom<KeyValuePair<string, string>>();
                var dict = new Dictionary<string, string>()
                {
                    [have.Key] = have.Value
                };

                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expect(dict).Not.To.Contain(have);
                }, Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected\n{dict.Stringify()}\nnot to contain\n{have.Stringify()}"));
                // Assert
            }
        }

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
                                    $"not to contain key\n{key.Stringify()}"
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
                                    $"not to contain key\n{key.Stringify()}"
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
                                TestUtils.WithNoLineBreaks(() =>
                                {
                                    Assert.That(() =>
                                        {
                                            Expect(src).To.Contain.Key(testingValue).With.Value(value);
                                        },
                                        Throws.Exception.TypeOf<UnmetExpectationException>()
                                            .With.Message.Contains($"to contain key\n\"{testingValue}\""));
                                });

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
                                        .With.Message.Contains($"Expected\n{testingValue}\nbut got\n{value}"));

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
                                        .With.Message.Contains($"Expected\n{testingValue}\nbut got\n{value}"));

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
                                TestUtils.WithNoLineBreaks(() =>
                                {
                                    Assert.That(() =>
                                        {
                                            Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                        },
                                        Throws.Exception.TypeOf<UnmetExpectationException>()
                                            .With.Message.Contains($"Expected\n{testingValue}\nbut got\n(null)"));
                                });
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
                                        .With.Message.Contains($"Expected\n(null)\nbut got\n{value}"));

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
                                var value = new List<int> {GetRandomInt(2, 9), GetRandomInt(2, 9), GetRandomInt(2, 9)};
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
                                var value = new List<int> {GetRandomInt(2, 9), GetRandomInt(2, 9), GetRandomInt(2, 9)};
                                var src = new Dictionary<string, List<int>>()
                                {
                                    [key] = value
                                };
                                var testingValue = new List<int>
                                {
                                    GetRandomInt(10, 19),
                                    GetRandomInt(10, 19),
                                    GetRandomInt(10, 19)
                                };
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                        .With.Message
                                        .Contains($"Expected\n{testingValue.Stringify()}\nbut got\n{value.Stringify()}"));

                                // Assert
                            }
                        }

                        [TestFixture]
                        public class OperatingOnMisMatchedTypes
                        {
                            [Test]
                            public void WhenDictionaryValueIsSByte_AndExpectedIsInt_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (sbyte) GetRandomInt(2);
                                var src = new Dictionary<string, sbyte>()
                                {
                                    [key] = value
                                };
                                var expected = (int) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsSByte_AndExpectedIsInt_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (sbyte) GetRandomInt(2);
                                var src = new Dictionary<string, sbyte>()
                                {
                                    [key] = value
                                };
                                var expected = (int) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsShort_AndExpectedIsInt_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (short) GetRandomInt(2);
                                var src = new Dictionary<string, short>()
                                {
                                    [key] = value
                                };
                                var expected = (int) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsShort_AndExpectedIsInt_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (short) GetRandomInt(2);
                                var src = new Dictionary<string, short>()
                                {
                                    [key] = value
                                };
                                var expected = (int) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsInt_AndExpectedIsLong_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomInt(2);
                                var src = new Dictionary<string, int>()
                                {
                                    [key] = value
                                };
                                var expected = (long) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsInt_AndExpectedIsLong_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = GetRandomInt(2);
                                var src = new Dictionary<string, int>()
                                {
                                    [key] = value
                                };
                                var expected = (long) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsByte_AndExpectedIsInt_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (byte) GetRandomInt(2);
                                var src = new Dictionary<string, byte>()
                                {
                                    [key] = value
                                };
                                var expected = (int) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsByte_AndExpectedIsInt_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (byte) GetRandomInt(2);
                                var src = new Dictionary<string, byte>()
                                {
                                    [key] = value
                                };
                                var expected = (int) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsUShort_AndExpectedIsInt_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (ushort) GetRandomInt(2);
                                var src = new Dictionary<string, ushort>()
                                {
                                    [key] = value
                                };
                                var expected = (int) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsUShort_AndExpectedIsInt_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (ushort) GetRandomInt(2);
                                var src = new Dictionary<string, ushort>()
                                {
                                    [key] = value
                                };
                                var expected = (int) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsUInt_AndExpectedIsLong_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (uint) GetRandomInt(2);
                                var src = new Dictionary<string, uint>()
                                {
                                    [key] = value
                                };
                                var expected = (long) value;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsUInt_AndExpectedIsLong_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = (uint) GetRandomInt(2);
                                var src = new Dictionary<string, uint>()
                                {
                                    [key] = value
                                };
                                var expected = (long) GetAnother(value);
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsFloat_AndExpectedIsDouble_ValuesMatch_ShouldNotThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = 123f;
                                var src = new Dictionary<string, float>()
                                {
                                    [key] = value
                                };
                                var expected = 123d;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void WhenDictionaryValueIsFloat_AndExpectedIsDouble_ValuesDoNotMatch_ShouldThrow()
                            {
                                // Arrange
                                var key = GetRandomString(2);
                                var value = 123.1f;
                                var src = new Dictionary<string, float>()
                                {
                                    [key] = value
                                };
                                var expected = 54d;
                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(src).To.Contain.Key(key).With.Value(expected);
                                    },
                                    Throws.Exception.TypeOf<UnmetExpectationException>()
                                );
                                // Assert
                            }
                        }
                    }
                }
            }
        }
    }
}