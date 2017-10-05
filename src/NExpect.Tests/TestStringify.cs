using NUnit.Framework;
using NExpect.Implementations;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestStringify
    {
        [TestFixture]
        public class Enums
        {
            public enum Numbers
            {
                Zero,
                One,
                Two,
                Three
            }

            [Test]
            public void EnumOnly_ShouldBeReportedAsString()
            {
                // Arrange
                var value = Numbers.Zero;
                var expected = "Zero";
                // Pre-Assert
                // Act
                var result = value.Stringify();
                // Assert
                Expect(result).To.Equal(expected);
            }

            [Test]
            public void NullableEnumOnly_WhenNotNull_ShouldBeReportedAsString()
            {
                // Arrange
                Numbers? value = Numbers.Three;
                var expected = "Three";
                // Pre-Assert
                // Act
                var result = value.Stringify();
                // Assert
                Expect(result).To.Equal(expected);
            }

            public class HasEnum
            {
                public Numbers Number { get; set; }
            }

            [Test]
            public void AsProperty_ShouldBeReportedAsString()
            {
                // Arrange
                var data = GetRandom<HasEnum>();
                var expected = new[]
                {
                    "{",
                    $"  Number: {data.Number.ToString()}",
                    "}"
                }.JoinWith("\n");
                // Pre-Assert
                // Act
                var result = data.Stringify();
                // Assert
                Expect(result).To.Equal(expected);
            }
        }

        [TestFixture]
        public class Arrays
        {
            [Test]
            public void AsPrimarySubject()
            {
                // Arrange
                var subject = new[]
                {
                    new { id = 1, name = "moo" }
                };
                var expected = new[]
                {
                    "[ {",
                    "  id: 1",
                    "  name: \"moo\"",
                    "} ]"
                }.JoinWith("\n");
                // Pre-Assert
                // Act
                var result = subject.Stringify();
                // Assert
                Expect(result).To.Equal(expected);
            }

            [Test]
            public void AsProperty()
            {
                // Arrange
                var subject = new
                {
                    collection = new[] {
                        new { id = 1, name = "cow" }
                    }
                };
                var expected = new[]
                {
                    "{",
                    "  collection: [ {",
                    "    id: 1",
                    "    name: \"cow\"",
                    "  } ]",
                    "}"
                }.JoinWith("\n");
                // Pre-Assert
                // Act
                var result = subject.Stringify();
                // Assert
                Expect(result).To.Equal(expected);
            }
        }

    }
}