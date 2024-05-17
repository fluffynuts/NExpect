using System;
using System.Collections.Generic;
using NUnit.Framework;
using NExpect.Exceptions;
using NExpect.Implementations;
using PeanutButter.Utils;

// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.Collections;

[TestFixture]
public class DictionaryTesting
{
    [TestFixture]
    public class Issues
    {
        [Test]
        public void NullDictionaryAssertion()
        {
            // Arrange
            var dict = null as Dictionary<string, string>;
            // Pre-assert
            // Act
            Assert.That(
                () => Expect(dict).To.Be.Null(),
                Throws.Nothing
            );
            Assert.That(
                () => Expect(dict).Not.To.Be.Null(),
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

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
            Assert.That(
                () =>
                {
                    Expect(dict).To.Contain(kvp);
                    foreach (var item in dict)
                        Expect(dict).To.Contain(item);
                },
                Throws.Nothing
            );

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
            Assert.That(
                () =>
                {
                    Expect(dict).To.Contain(missed);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Matches(
                        (string actual) => actual.Compact()
                            .Equals($"Expected {dict.Stringify()} to contain {missed.Stringify()}".Compact())
                    )
            );

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
            Assert.That(
                () =>
                {
                    Expect(dict).Not.To.Contain(have);
                },
                Throws.Exception
                    .InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected\n{dict.Stringify()}\nnot to contain\n{have.Stringify()}")
            );
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
                    Assert.That(
                        () =>
                        {
                            Expect(src).To.Contain.Key(key);
                        },
                        Throws.Nothing
                    );

                    // Assert
                }

                public static IEnumerable<StringComparer> CaseInsensitiveComparers()
                {
                    return new[]
                    {
                        StringComparer.OrdinalIgnoreCase,
                        StringComparer.CurrentCultureIgnoreCase,
                        StringComparer.InvariantCultureIgnoreCase
                    };
                }

                [TestCaseSource(nameof(CaseInsensitiveComparers))]
                public void WhenDictionaryIsCaseInsentive_ShouldNotThrowForIncorrectCasing(StringComparer comparer)
                {
                    // Arrange
                    var key = GetRandomAlphaString(2);
                    var recased = key.ToUpperInvariant();
                    if (recased == key)
                    {
                        recased = key.ToLowerInvariant();
                    }

                    var src = new Dictionary<string, int>(comparer)
                    {
                        [key] = GetRandomInt()
                    };
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(src).To.Contain.Key(recased);
                        },
                        Throws.Nothing
                    );
                    // Assert
                }

                public static IEnumerable<StringComparer> CaseSensitiveComparers()
                {
                    return new[]
                    {
                        StringComparer.CurrentCulture,
                        StringComparer.InvariantCulture,
                        StringComparer.Ordinal
                    };
                }

                [TestCaseSource(nameof(CaseSensitiveComparers))]
                public void WhenDictionaryIsCaseSentive_ShouldThrowForIncorrectCasing(StringComparer comparer)
                {
                    // Arrange
                    var key = GetRandomAlphaString(2);
                    var recased = key.ToUpperInvariant();
                    if (recased == key)
                    {
                        recased = key.ToLowerInvariant();
                    }

                    var src = new Dictionary<string, int>(comparer)
                    {
                        [key] = GetRandomInt()
                    };
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(src).To.Contain.Key(recased);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                    );
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
                    Assert.That(
                        () =>
                        {
                            Expect(src).Not.To.Contain.Key(key);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(
                                $"not to contain key\n{key.Stringify()}"
                            )
                    );

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
                    Assert.That(
                        () =>
                        {
                            Expect(src).To.Not.Contain.Key(key);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(
                                $"not to contain key\n{key.Stringify()}"
                            )
                    );

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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(value);
                                },
                                Throws.Nothing
                            );

                            // Assert
                        }

                        [Test]
                        public void WhenCaseInsensitiveDictionaryHasKey_AndValueMatches_ShouldNotThrow()
                        {
                            // Arrange
                            var key = GetRandomString(2);
                            var value = GetRandomInt(2);
                            var src = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
                            {
                                [key] = value
                            };
                            var recased = key.ToUpperInvariant();
                            if (recased == key)
                            {
                                recased = key.ToLowerInvariant();
                            }
                            // Pre-Assert

                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(recased).With.Value(value);
                                },
                                Throws.Nothing
                            );

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
                            TestUtils.WithNoLineBreaks(
                                () =>
                                {
                                    Assert.That(
                                        () =>
                                        {
                                            Expect(src).To.Contain.Key(testingValue).With.Value(value);
                                        },
                                        Throws.Exception.TypeOf<UnmetExpectationException>()
                                            .With.Message.Contains($"to contain key\n\"{testingValue}\"")
                                    );
                                }
                            );

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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                },
                                Throws.Exception.TypeOf<UnmetExpectationException>()
                                    .With.Message.Contains($"Expected\n{testingValue}\nbut got\n{value}")
                            );

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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(value);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                },
                                Throws.Exception.TypeOf<UnmetExpectationException>()
                                    .With.Message.Contains($"Expected\n{testingValue}\nbut got\n{value}")
                            );

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
                            TestUtils.WithNoLineBreaks(
                                () =>
                                {
                                    Assert.That(
                                        () =>
                                        {
                                            Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                        },
                                        Throws.Exception.TypeOf<UnmetExpectationException>()
                                            .With.Message.Contains($"Expected\n{testingValue}\nbut got\n(null)")
                                    );
                                }
                            );
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                },
                                Throws.Exception.TypeOf<UnmetExpectationException>()
                                    .With.Message.Contains($"Expected\n(null)\nbut got\n{value}")
                            );

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
                            var value = new List<int>
                            {
                                GetRandomInt(2, 9),
                                GetRandomInt(2, 9),
                                GetRandomInt(2, 9)
                            };
                            var test = new List<int>(value);
                            var src = new Dictionary<string, List<int>>()
                            {
                                [key] = value
                            };
                            // Pre-Assert

                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(src)
                                        .To.Contain.Key(key)
                                        .With.Value(test);
                                },
                                Throws.Nothing
                            );

                            // Assert
                        }

                        [Test]
                        public void WhenTestAndMatchAreNull_ShouldNotThrow()
                        {
                            // Arrange
                            var key = GetRandomString(2);
                            var src = new Dictionary<string, List<int>>()
                            {
                                [key] = null
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(src)
                                        .To.Contain.Key(key)
                                        .With.Value(null);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void TestingDeepEqualityOnDateTimeValues()
                        {
                            // Arrange
                            var date1 = DateTime.Now;
                            var date2 = new DateTime(date1.Ticks, date1.Kind);
                            var tester = new DeepEqualityTester(
                                date1,
                                date2
                            );
                            Expect(date1.Equals(date2))
                                .To.Be.True("test values don't actually match...");
                            Expect(date1.Kind).To.Equal(
                                date2.Kind,
                                () => "Not really equal if their kinds are different, eh"
                            );
                            // Act
                            var result = tester.AreDeepEqual();
                            // Assert
                            Expect(result).To.Be.True();
                        }

                        [Test]
                        public void TestingDeepEqualityOnNulls()
                        {
                            // Arrange
                            var left = null as string;
                            var right = null as string;
                            var tester = new DeepEqualityTester(left, right);
                            // Act
                            var result = tester.AreDeepEqual();
                            // Assert
                            Expect(result).To.Be.True();
                        }

                        [Test]
                        public void WhenDictionaryHasKey_AndValueDoesNotMatch_ShouldThrow()
                        {
                            // Arrange
                            var key = GetRandomString(2);
                            var value = new List<int>
                            {
                                GetRandomInt(2, 9),
                                GetRandomInt(2, 9),
                                GetRandomInt(2, 9)
                            };
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(testingValue);
                                },
                                Throws.Exception.TypeOf<UnmetExpectationException>()
                                    .With.Message
                                    .Contains(
                                        $"Expected\n{testingValue.Stringify()}\nbut got\n{value.Stringify()}"
                                    )
                            );

                            // Assert
                        }
                    }

                    [TestFixture]
                    public class DeepEquality
                    {
                        [Test]
                        public void ShouldPassWhenMatched()
                        {
                            // Arrange
                            var data = new { foo = "bar" };
                            var copy = new { foo = "bar" };
                            var key = GetRandomString(1);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(copy);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldFailWhenUnMatched()
                        {
                            // Arrange
                            var data = new { foo = "bar" };
                            var other = new { foo1 = "bar" };
                            var another = new { foo = "qux" };
                            var key = GetRandomString(1);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(other);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(other.Stringify())
                            );

                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(another);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(another.Stringify())
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldIncludeCustomMessagesWhenFailing()
                        {
                            // Arrange
                            var data = new { foo = "bar" };
                            var other = new { foo1 = "bar" };
                            var another = new { foo = "qux" };
                            var key = GetRandomString(1);
                            var message1 = GetRandomString(10);
                            var message2 = GetRandomString(10);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(other, message1);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(message1)
                                    .And.Message.Contains(data.Stringify())
                                    .And.Message.Contains(other.Stringify())
                            );

                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(another, () => message2);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(another.Stringify())
                                    .And.Message.Contains(message2)
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class IntersectionEquality
                    {
                        [Test]
                        public void ShouldPassWhenMatched()
                        {
                            // Arrange
                            var data = new { foo = "bar", other = "moo" };
                            var copy = new { foo = "bar", quuz = "wibble" };
                            var key = GetRandomString(1);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Intersection.Equal.To(copy);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldFailWhenUnMatched()
                        {
                            // Arrange
                            var data = new { foo = "bar" };
                            var other = new { foo1 = "bar" };
                            var another = new { foo = "qux" };
                            var key = GetRandomString(1);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Intersection.Equal.To(other);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(other.Stringify())
                            );

                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(another);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(another.Stringify())
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldIncludeCustomMessagesWhenFailing()
                        {
                            // Arrange
                            var data = new { foo = "bar" };
                            var other = new { foo1 = "bar" };
                            var another = new { foo = "qux" };
                            var key = GetRandomString(1);
                            var message1 = GetRandomString(10);
                            var message2 = GetRandomString(10);
                            var dictionary = new Dictionary<string, object>()
                            {
                                [key] = data
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Intersection.Equal.To(other, message1);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(message1)
                                    .And.Message.Contains(data.Stringify())
                                    .And.Message.Contains(other.Stringify())
                            );

                            Assert.That(
                                () =>
                                {
                                    Expect(dictionary)
                                        .To.Contain.Key(key)
                                        .With.Value.Deep.Equal.To(another, () => message2);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(data.Stringify())
                                    .And.Message.Contains(another.Stringify())
                                    .And.Message.Contains(message2)
                            );
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Nothing
                            );

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
                            Assert.That(
                                () =>
                                {
                                    Expect(src).To.Contain.Key(key).With.Value(expected);
                                },
                                Throws.Exception.TypeOf<UnmetExpectationException>()
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class MatchingValueWithLambda
                    {
                        [Test]
                        public void ShouldBeAbleToMatchValueWithLambda()
                        {
                            // Arrange
                            var dict = new Dictionary<string, IdentifierAndName>();
                            var key = GetRandomString(1);
                            var value = GetRandom<IdentifierAndName>();
                            dict[key] = value;
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dict)
                                        .To.Contain.Key(key)
                                        .With.Value.Matched.By(
                                            o => o.Name == value.Name &&
                                                o.Id == value.Id
                                        );
                                },
                                Throws.Nothing
                            );
                            var customMessage = GetRandomWords();
                            Assert.That(
                                () =>
                                {
                                    Expect(dict)
                                        .To.Contain.Key(key)
                                        .With.Value.Matched.By(
                                            o => o.Name != value.Name,
                                            () => customMessage
                                        );
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(customMessage)
                            );
                            // Assert
                        }

                        [Test]
                        public void ValueMismatchOnFoundKeyShouldOutputValueOfValue()
                        {
                            // Arrange
                            var dict = new Dictionary<string, IdentifierAndName>();
                            var key = GetRandomString(1);
                            var value = GetRandom<IdentifierAndName>();
                            dict[key] = value;
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(dict)
                                        .To.Contain.Key(key)
                                        .With.Value.Matched.By(
                                            o => o.Name != value.Name
                                        );
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(value.Name)
                                    .And.Message.Contains(value.Id.ToString())
                            );
                            // Assert
                        }

                        public class IdentifierAndName
                        {
                            public int Id { get; }
                            public string Name { get; }

                            public IdentifierAndName(int id, string name)
                            {
                                Id = id;
                                Name = name;
                            }
                        }
                    }
                }
            }
        }
    }
}

public static class TestingStringExtensions
{
    public static string Compact(this string str)
    {
        return str.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
    }
}
