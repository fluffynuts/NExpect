using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using NExpect.Shims;
using PeanutButter.Utils;
using static NExpect.Expectations;
// ReSharper disable UnusedVariable
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests.Shims
{
    [TestFixture]
    public class TestDictionaryShim
    {
        [Test]
        public void ShouldImplement_IDictionary()
        {
            // Arrange
            var sut = typeof(DictionaryShim);
            // Pre-Assert
            // Act
            Expect(sut).To.Implement<IDictionary<string, string>>();
            // Assert
        }

        [TestFixture]
        public class KeyValueSetAndGet
        {
            [Test]
            public void ShouldBeAbleToGetExistingValue()
            {
                // Arrange
                var actual = new NameValueCollection();
                var key = GetRandomString(2);
                var value = GetRandomString(2);
                actual[key] = value;
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result = sut[key];

                // Assert
                Expect(result).To.Equal(value);
            }

            [Test]
            public void ShouldBeAbleToSetAndGetValue()
            {
                // Arrange
                var actual = new NameValueCollection();
                var sut = Create(actual);
                var key = GetRandomString(2);
                var value = GetRandomString(2);
                // Pre-Assert
                // Act
                sut[key] = value;
                var result = sut[key];
                // Assert
                Expect(result).To.Equal(value);
                Expect(actual[key]).To.Equal(value);
            }

            [Test]
            public void ShouldBeAbleWToAddKeyAndValue()
            {
                // Arrange
                var key = GetRandomString(2);
                var value = GetRandomString(2);
                var actual = new NameValueCollection();
                var sut = Create(actual);
                // Pre-Assert
                // Act
                sut.Add(key, value);
                // Assert
                Expect(sut[key]).To.Equal(value);
                Expect(actual[key]).To.Equal(value);
            }

            [Test]
            public void ShouldBeAbleWToAddKeyValuePair()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var actual = new NameValueCollection();
                var sut = Create(actual);
                // Pre-Assert
                // Act
                sut.Add(kvp.Key, kvp.Value);
                // Assert
                Expect(sut[kvp.Key]).To.Equal(kvp.Value);
                Expect(actual[kvp.Key]).To.Equal(kvp.Value);
            }

            [Test]
            public void ShouldBeAbleToClear()
            {
                // Arrange
                var actual = new NameValueCollection();
                actual[GetRandomString(2)] = GetRandomString(2);
                var sut = Create(actual);
                // Pre-Assert
                // Act
                sut.Clear();
                // Assert
                Expect(actual.Count).To.Equal(0);
                Expect(sut).To.Be.Empty();
            }

            [Test]
            public void ShouldBeAbleToRemoveByKey()
            {
                // Arrange
                var key = GetRandomString(2);
                var actual = new NameValueCollection();
                actual[key] = GetRandomString(2);
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result = sut.Remove(key);
                // Assert
                Expect(result).To.Be.True();
                Expect(actual.Count).To.Equal(0);
                Expect(sut).To.Be.Empty();
            }

            [Test]
            public void ShouldBeAbleToRemoveByKeyValuePair()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var actual = new NameValueCollection();
                actual[kvp.Key] = kvp.Value;
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result = sut.Remove(kvp);
                // Assert
                Expect(result).To.Be.True();
                Expect(sut).To.Be.Empty();
                Expect(actual.Count).To.Equal(0);
            }

            [Test]
            public void ShouldRequireFullMatchForKeyValuePairRemoval()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var actual = new NameValueCollection {[kvp.Key] = kvp.Value};
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result1 = sut.Remove(new KeyValuePair<string, string>(kvp.Key, GetAnother(kvp.Value)));
                var result2 = sut.Remove(new KeyValuePair<string, string>(GetAnother(kvp.Key), kvp.Value));
                // Assert
                Expect(result1).To.Be.False();
                Expect(result2).To.Be.False();
                Expect(sut).Not.To.Be.Empty();
                Expect(actual.Count).To.Equal(1);
            }

            [Test]
            public void TryGetValue_WhenHaveValue_ShouldSetAndReturnTrue()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var actual = new NameValueCollection {[kvp.Key] = kvp.Value};
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result = sut.TryGetValue(kvp.Key, out var value);
                // Assert
                Expect(result).To.Be.True();
                Expect(value).To.Equal(kvp.Value);
            }

            [Test]
            public void TryGetValue_WhenDoNotHaveValue_ShouldReturnFalse()
            {
                // Arrange
                var kvp = GetRandom<KeyValuePair<string, string>>();
                var actual = new NameValueCollection {[kvp.Key] = kvp.Value};
                var sut = Create(actual);
                // Pre-Assert
                // Act
                var result = sut.TryGetValue(GetAnother(kvp.Key), out var value);
                // Assert
                Expect(result).To.Be.False();
            }

            [Test]
            public void Values_ShouldReturnAllValues()
            {
                // Arrange
                var items = GetRandomCollection<KeyValuePair<string, string>>();
                var actual = new NameValueCollection();
                items.ForEach(i => actual[i.Key] = i.Value);
                var sut = Create(actual);
                var expected = items.Select(kvp => kvp.Value).ToArray();
                // Pre-Assert
                // Act
                var result = sut.Values;
                // Assert
                Expect(result).To.Be.Equivalent.To(expected);
            }

            [Test]
            public void IsReadOnly_ShouldBeFalse()
            {
                // Arrange
                var sut = Create(new NameValueCollection());
                // Pre-Assert
                // Act
                var result = sut.IsReadOnly;
                // Assert
                Expect(result).To.Be.False();
            }
        }


        [TestFixture]
        public class Enumeration
        {
            [Test]
            public void ShouldBeAbleToEnumerate()
            {
                // Arrange
                var k1 = GetRandomString(2);
                var v1 = GetRandomString(2);
                var k2 = GetAnother(k1);
                var v2 = GetAnother(v1);
                var actual = new NameValueCollection();
                actual[k1] = v1;
                actual[k2] = v2;
                var collector = new List<KeyValuePair<string, string>>();
                var sut = Create(actual);
                // Pre-Assert
                // Act
                foreach (var kvp in sut)
                    collector.Add(kvp);
                // Assert
                Expect(collector).To.Contain.Only(2).Items();
                Expect(collector).To.Contain(new KeyValuePair<string, string>(k1, v1));
                Expect(collector).To.Contain(new KeyValuePair<string, string>(k2, v2));
            }
        }


        private static IDictionary<string, string> Create(NameValueCollection actual)
        {
            return new DictionaryShim(actual);
        }
    }
}