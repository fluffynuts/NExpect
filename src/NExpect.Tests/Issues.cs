using System;
using NExpect.Implementations;
using NUnit.Framework;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests
{
    [TestFixture]
    public class Issues
    {
        [Test]
        public void ComparingTypes_ShouldNotStall()
        {
            // Arrange

            // Pre-Assert

            // Act
            Assert.That(
                () =>
                {
                    Expect(new Object().GetType()).To.Equal(typeof(Object));
                },
                Throws.Nothing
            );

            // Assert
        }

        [TestFixture]
        public class MultipleChainedStringContains
        {
            [Test]
            public void ShouldNotCarryStartMarkerThroughContains()
            {
                // Arrange
                var data =
                    "SomeEntity does not have a parameterless constructor or is not a class Type. You must override SomeBuilder.CreateInstance for this type to provide an instance to work with";
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(data)
                            .To.Contain("SomeBuilder")
                            .And.To.Contain("SomeEntity")
                            .And.To.Contain("parameterless constructor")
                            .And.To.Contain("override SomeBuilder.CreateInstance");
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }

        [TestFixture]
        public class Issue30
        {
            [TestFixture]
            public class WhenHaveCustomToString
            {
                [Test]
                public void ShouldReturnToString()
                {
                    // Arrange
                    var data = new MyStruct(GetRandomString());
                    var expected = $"<< {data.ToString()} >>";
                    // Act
                    var result = data.Stringify();
                    // Assert
                    Expect(result)
                        .To.Equal(expected);
                }
            }

            [TestFixture]
            public class WhenNoCustomToString
            {
                [TestFixture]
                public class OnValueType
                {
                    [Test]
                    public void ShouldNotToString()
                    {
                        // Arrange
                        var data = new MyStruct2(GetRandomString());
                        var expected = "{}";
                        // Act
                        var result = data.Stringify();
                        // Assert
                        Expect(result)
                            .To.Equal(expected);
                    }
                }

                [TestFixture]
                public class OnRefType
                {
                    [Test]
                    public void ShouldNotToString()
                    {
                        // Arrange
                        var data = new object();
                        var expected = "{}";
                        // Act
                        var result = data.Stringify();
                        // Assert
                        Expect(result)
                            .To.Equal(expected);
                    }
                }
            }

            public struct MyStruct
            {
                private readonly string _text;

                public MyStruct(string text)
                {
                    _text = text;
                }

                public override string ToString()
                {
                    return $"{nameof(_text)}: {_text}";
                }
            }

            public struct MyStruct2
            {
                private readonly string _text;

                public MyStruct2(string text)
                {
                    _text = text;
                }
            }
        }
    }
}