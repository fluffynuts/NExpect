using System;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;
using NExpect.Utilities;

// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NExpect.Tests;

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

    [TestFixture]
    public class ForgettingCustomMatchersWhichArentPure
    {
        [Test]
        public void ShouldBeAbleToForgetIt()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Assertions.EnableTrackingFor(
                        () => Expect(2)
                            .To.Be.Special()
                            .Verify()
                    );
                },
                Throws.Nothing
            );
            // Assert
        }
    }

    [TestFixture]
    public class ForgettingOnFailure
    {
        [Test]
        [Ignore("Not sure it's possible to pass this yet")]
        public void ShouldForgetOnFailure()
        {
            /*
             * Scenario: performing an assertion where the value fed to .Equals(...)
             * is never arrived at because the provider throws an exception.
             * Technically, NExpect never sees the call to .Equals(...),
             * so right now, I'm not sure how to suppress the erroneous report
             * that an assertion was left incomplete, but it would be nice
             * if we could.
             */
            // Arrange
            var person = GetRandom<Person>();

            // Act
            Assert.That(
                () =>
                    Assertions.EnableTrackingFor(
                        () =>
                        {
                            Assert.That(
                                () =>
                                {
                                    Expect("123")
                                        .To.Equal(person.Phone.AsString());
                                },
                                Throws.Exception
                            );
                        }
                    ),
                Throws.Nothing
            );
            // Assert
        }

        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public PhoneNumber Phone { get; set; }
        }

        public class PhoneNumber
        {
            public string AsString()
            {
                throw new Exception("no");
            }
        }
    }

    [TestFixture]
    public class NullValuesInCollections
    {
        [Test]
        public void ShouldBeAbleToMatchNullValueInCollection()
        {
            // Arrange
            var items = new[]
            {
                "1",
                null,
                "2"
            };

            // Act
            Assert.That(
                () =>
                {
                    Expect(items)
                        .To.Contain.Exactly(1)
                        .Equal.To(null);
                },
                Throws.Nothing
            );
            // Assert
        }
    }

    [TestFixture]
    public class OnlyActingOdd
    {
        [Test]
        public void ShouldEnforceAbsoluteSizeOfCollection()
        {
            // Arrange
            var numbers = new[] { 1, 2 };
            // Act
            Assert.That(() =>
            {
                Expect(numbers)
                    .To.Contain.Only(1)
                    .Equal.To(1);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}

public static class CustomNonNExpectMatchers
{
    public class SpecialContinuation
    {
        private readonly long _i;

        public SpecialContinuation(long i)
        {
            _i = i;
        }

        public void Verify()
        {
            if (_i % 2 == 0)
            {
                return;
            }

            throw new InvalidOperationException("moo");
        }
    }

    public static SpecialContinuation Special(
        this IBe<long> be
    )
    {
        be.Forget();
        return new SpecialContinuation(be.GetActual());
    }
}