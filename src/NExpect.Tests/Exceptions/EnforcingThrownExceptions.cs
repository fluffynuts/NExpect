using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable NotResolvedInText
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedParameter.Local
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable ConvertToLambdaExpression

namespace NExpect.Tests.Exceptions
{
    [TestFixture]
    public class EnforcingThrownExceptions
    {
        [Test]
        public void Throw_ForAction_WithNoGenericType_WhenSUTThrows_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(
                () =>
                {
                    Expect(
                            () =>
                            {
                                throw new Exception(GetRandomString());
                            }
                        )
                        .To.Throw();
                }
            );
            // Assert
        }

        [Test]
        public void Throw_WhenShouldNotThrowButDoes_ShouldIncludeOriginalExceptionIn_UnmetExpectation_InnerException()
        {
            // Arrange
            var expected = new InvalidOperationException(GetRandomString(20));
            // Pre-assert
            // Act
            try
            {
                Expect(() => throw expected).Not.To.Throw();
            }
            catch (UnmetExpectationException ex)
            {
                Expect(ex.InnerException).To.Be(expected);
            }

            // Assert
        }

        [Test]
        public void Throw_ForAction_WithNoGenericType_WhenSUTDoesNotThrow_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                            {
                            }
                        )
                        .To.Throw();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void Throw_ForFunc_WithNoGenericType_WhenSUTThrows_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.DoesNotThrow(
                () =>
                {
                    Expect(
                            () =>
                            {
                                if (MakeFalse())
                                {
                                    return 1;
                                }

                                throw new Exception(GetRandomString());
                            }
                        )
                        .To.Throw();
                }
            );
            // Assert
        }

        private bool MakeFalse()
        {
            return false;
        }

        [Test]
        public void Throw_ForFunc_ForAction_WithNoGenericType_WhenSUTDoesNotThrow_ShouldThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(
                            () =>
                            {
                                return "moo";
                            }
                        )
                        .To.Throw();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        public class MessageContaining
        {
            [Test]
            public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_HappyPath()
            {
                // Arrange
                var expected = GetRandomString();
                // Pre-Assert
                // Act
                Assert.DoesNotThrow(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(expected);
                                }
                            )
                            .To.Throw()
                            .With.Message.Containing(expected);
                    }
                );
                // Assert
            }

            [Test]
            public void Throw_WhenGettingMessageViaProperty_ShouldNotFailWhenHaveSoughtStringAtStartOfMessage()
            {
                // Arrange
                var message = "LOOKFORME could not moo";
                var seek = "LOOKFORME";
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new Exception(message))
                            .To.Throw<Exception>()
                            .With.Property(e => e.Message)
                            .Containing(seek);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new Exception(message))
                            .To.Throw().With.Type(typeof(Exception))
                            .And.Property(e => e.Message)
                            .Containing(seek);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldIncludeStacktraceWhenFailing1()
            {
                // Arrange
                var message = "LOOKFORME could not moo";

                // Act
                Expect(
                        () =>
                            Expect(() => throw new Exception(message))
                                .Not.To.Throw()
                    ).To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(message)
                    .Then("Stacktrace")
                    .Then(" at ");

                // Assert
            }

            [Test]
            public void ShouldIncludeStacktraceWhenFailing2()
            {
                // Arrange
                var message = "LOOKFORME could not moo";

                // Act
                Expect(
                        () =>
                            Expect(() => throw new Exception(message))
                                .To.Throw<InvalidOperationException>()
                    ).To.Throw<UnmetExpectationException>()
                    .With.Message.Containing(message)
                    .Then("Stacktrace")
                    .Then(" at ");

                // Assert
            }

            [Test]
            public void Throw_WithNoGenericType_AllowsMultipleSubStringContainingOnMessage_HappyPath()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(message);
                                }
                            )
                            .To.Throw()
                            .With.Message.Containing(e1)
                            .And(e2);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Throw_WithNoGenericType_WhenThrows_ShouldBeAbleToContinueWith_WithMessage_SadPath()
            {
                // Arrange
                var expected = GetRandomString();
                var other = GetAnother(expected);
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(other);
                                }
                            )
                            .To.Throw()
                            .With.Message.Containing(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"to contain \"{expected}\"")
                );
                // Assert
            }

            [Test]
            public void Throw_WithNoGenericType_AllowsMultipleSubStringContainingOnMessage_SadPath()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var e3 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(message);
                                }
                            )
                            .To.Throw()
                            .With.Message.Containing(e1)
                            .And(e3);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(e3)
                );
                // Assert
            }

            [Test]
            public void Throw_WithNoGenericType_AllowsMultipleSubStringContainingOnMessage_SadPathNegated()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var e3 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(message);
                                }
                            )
                            .To.Throw()
                            .With.Message.Containing(e1)
                            .And.Not.Containing(e3);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Throw_WithNoGenericType_AllowsMultipleSubStringContainingOnMessage_HappyPathNegated()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(message);
                                }
                            )
                            .To.Throw()
                            .With.Message.Not.Containing(e1)
                            .And(e2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }

        public class WithMessageMatching
        {
            [Test]
            public void WhenMessageMatches_ShouldNotThrow()
            {
                // Arrange
                var msg = GetRandomString();
                var re = new Regex(msg);
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(msg);
                                }
                            )
                            .To.Throw()
                            .With.Message.Matching(s => s == msg);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(msg);
                                }
                            )
                            .To.Throw()
                            .With.Message.Matching(re);
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void WhenMessageDoesNotMatch_ShouldThrow()
            {
                // Arrange
                var msg = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(GetAnother(msg));
                                }
                            )
                            .To.Throw()
                            .With.Message.Matching(s => s == msg);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }

            [Test]
            public void Negated_WhenMessageDoesNotMatch_ShouldThrow()
            {
                // Arrange
                var msg = GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new Exception(GetAnother(msg));
                                }
                            )
                            .To.Throw()
                            .With.Message.Not.Matching(s => s == msg);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );

                // Assert
            }
        }

        [Test]
        public void Throw_WithGenericType_WhenThrowsThatType_ShouldNotThrow()
        {
            // Arrange
            // Pre-Assert
            // Act
            Assert.That(
                () =>
                {
                    Expect(() => throw new InvalidOperationException("moo"))
                        .To.Throw<InvalidOperationException>();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(() => throw new InvalidOperationException("moo"))
                        .To.Throw<InvalidOperationException>("Custom message");
                },
                Throws.Nothing
            );
            // Assert
        }

        public class GenericMessageContaining
        {
            [Test]
            public void Throw_WithGenericType_AllowsMultipleSubStringContainingOnMessage_SadPath()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var e3 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new InvalidOperationException(message);
                                }
                            )
                            .To.Throw<InvalidOperationException>()
                            .With.Message.Containing(e1)
                            .And(e3);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(e3)
                );

                // Assert
            }

            [Test]
            public void ShouldIncludeCustomMessage()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                var customMessage = $"{GetRandomString()} (custom message)";
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new InvalidOperationException(message);
                                }
                            )
                            .To.Throw<ArgumentException>(customMessage);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(customMessage)
                );
                // Assert
            }

            [Test]
            public void Throw_WithGenericType_AllowsMultipleSubStringContainingOnMessage_SadPathNegated()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var e3 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new ArgumentNullException(message);
                                }
                            )
                            .To.Throw<ArgumentNullException>()
                            .With.Message.Containing(e1)
                            .And.Not.Containing(e3);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Throw_WithGenericType_AllowsMultipleSubStringContainingOnMessage_HappyPathNegated()
            {
                // Arrange
                var e1 = GetRandomString();
                var e2 = GetRandomString();
                var message = new[] { e1, e2 }.Randomize().JoinWith(" ");
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new ArgumentOutOfRangeException(message);
                                }
                            )
                            .To.Throw<ArgumentOutOfRangeException>()
                            .With.Message.Not.Containing(e1)
                            .And(e2);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void Throw_Negated_WithGenericType_WhenThrowsThatType_ShouldThrow()
            {
                // Arrange
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new InvalidOperationException("moo"))
                            .Not.To.Throw<InvalidOperationException>();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Matches(
                            $"Expected not to throw an exception of type (System.|){typeof(InvalidOperationException).Name}"
                        )
                );
                // Assert
            }

            [Test]
            public void Throw_WithGenericType_WhenThrowsAnotherTypeType_ShouldThrow()
            {
                // Arrange
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new InvalidOperationException("moo"))
                            .To.Throw<NotImplementedException>();
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Matches(
                            $"Expected to throw an exception of type (System.|){typeof(NotImplementedException).Name} but {typeof(InvalidOperationException).Name} was thrown instead"
                        )
                );
                // Assert
            }

            [Test]
            public void Throw_WithGenericType_ShouldContinueOnToMessageTest_HappyPath()
            {
                // Arrange
                var expected = GetRandomString();

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new AccessViolationException(expected))
                            .To.Throw<AccessViolationException>()
                            .With.Message.Containing(expected);
                    },
                    Throws.Nothing
                );

                // Assert
            }
        }

        [TestFixture]
        public class ShouldBeAbleToTestAgainstExceptionTypeArgument
        {
            [Test]
            public void ShouldNotFailWhenDoesThrow()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        var message = GetRandomString();
                        Expect(() => throw new LocalException(message))
                            .To.Throw()
                            .With.Type(typeof(LocalException))
                            .And.Message.Equal.To(message);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        var message = GetRandomString();
                        Expect(() => throw new LocalException(message))
                            .To.Throw()
                            .With.Type(typeof(LocalException))
                            .And.Property(e => (e as LocalException).MyMessage == message);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldFailWhenDoesNotThrowCorrectType()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        var message = GetRandomString();
                        Expect(() => throw new Exception(message))
                            .To.Throw()
                            .With.Type(typeof(LocalException))
                            .And.Message.Equal.To(message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldFailWhenDoesNotThrow()
            {
                // Arrange
                // Act
                Assert.That(
                    () =>
                    {
                        var message = GetRandomString();
                        Expect(
                                () =>
                                {
                                }
                            )
                            .To.Throw()
                            .With.Type(typeof(LocalException))
                            .And.Message.Equal.To(message);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            public class LocalException : InvalidOperationException
            {
                public string MyMessage { get; }

                public LocalException(
                    string message
                ) : base(message)
                {
                    MyMessage = message;
                }
            }
        }

        [TestFixture]
        public class ExceptionMessages
        {
            [Test]
            public void ShouldNotThrowWhenMessageIsExactMatch()
            {
                // Arrange
                var expected = GetRandomString();

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new AccessViolationException(expected))
                            .To.Throw<AccessViolationException>()
                            .With.Message.Equal.To(expected);
                    },
                    Throws.Nothing
                );
                // alt. syntax - for the lazy! (like me)
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new AccessViolationException(expected))
                            .To.Throw<AccessViolationException>()
                            .With.Message(expected);
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ShouldThrowWhenMessageIsNotExactMatch()
            {
                // Arrange
                var expected = GetRandomString();
                var unexpected = GetAnother(expected);
                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new AccessViolationException(unexpected))
                            .To.Throw<AccessViolationException>()
                            .With.Message.Equal.To(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected\n\"{unexpected}\"\nto equal\n\"{expected}\"")
                );

                // Assert
            }
        }


        [TestFixture]
        public class GenericProperty
        {
            [Test]
            public void ShouldContinueOnToEqualTo()
            {
                // Arrange
                var expected = GetRandomString();

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(expected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Equal.To(expected);
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ShouldContinueToNegatedEqualTo()
            {
                // Arrange
                var expected = GetRandomString();

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(expected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Not.Equal.To(GetAnother(expected));
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ShouldThrowWhenExtractedPropertyDoesNotMatch()
            {
                // Arrange
                var expected = GetRandomString();
                var unexpected = GetAnother(expected);

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(unexpected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Equal.To(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected\n\"{unexpected}\"\nto equal\n\"{expected}\"")
                );

                // Assert
            }

            [Test]
            public void ShouldNotThrowWhenExtractedPropertyContainsExpectedSubstring()
            {
                // Arrange
                var expected = GetRandomString(
                    8,
                    8
                );
                var expectedSubstring = expected.Substring(
                    2,
                    4
                );

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(expected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Containing(expectedSubstring);
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [Test]
            public void ShouldNotThrowWhenExtractedPropertyContainsExpectedSubstrings()
            {
                // Arrange
                var expected = GetRandomString(
                    10,
                    20
                );
                var expectedSubstring = expected.Substring(
                    2,
                    4
                );
                var next = expected.Substring(
                    6,
                    4
                );

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(expected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Containing(expectedSubstring)
                            .Then(next);
                    },
                    Throws.Nothing
                );

                // Assert
            }

            [TestFixture]
            public class EndingWith
            {
                [Test]
                public void HappyPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = message.Substring(10);
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new InvalidOperationException(message))
                                .To.Throw<InvalidOperationException>()
                                .With.Message.Ending.With(search);
                        },
                        Throws.Nothing
                    );
                    // Assert
                }

                [Test]
                public void SadPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = GetAnother(message.Substring(10));
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new InvalidOperationException(message))
                                .To.Throw<InvalidOperationException>()
                                .With.Message.Ending.With(search);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(
                                $"\"{message}\" to end with \"{search}\""
                            )
                    );
                    // Assert
                }

                [Test]
                public void Negated_HappyPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = GetAnother(message.Substring(10));
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new InvalidOperationException(message))
                                .To.Throw<InvalidOperationException>()
                                .With.Message.Not.Ending.With(search);
                        },
                        Throws.Nothing
                    );
                    // Assert
                }
            }

            [TestFixture]
            public class StartingWith
            {
                [Test]
                public void HappyPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = message.Substring(
                        0,
                        5
                    );
                    var next = message.Substring(
                        5,
                        5
                    );
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new OverflowException(message))
                                .To.Throw<OverflowException>()
                                .With.Message.Starting.With(search)
                                .Then(next);
                        },
                        Throws.Nothing
                    );
                    // Assert
                }

                [Test]
                public void HappyPathWithComparer()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = message.Substring(
                        0,
                        5
                    );
                    var next = message.Substring(
                        5,
                        5
                    );
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new OverflowException(message))
                                .To.Throw<OverflowException>()
                                .With.Message.Starting.With(
                                    search.ToUpper(),
                                    StringComparison.OrdinalIgnoreCase
                                ).Then(
                                    next.ToUpper(),
                                    StringComparison.OrdinalIgnoreCase
                                );
                        },
                        Throws.Nothing
                    );
                    // Assert
                }

                [Test]
                public void SadPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = GetAnother(
                        message.Substring(
                            0,
                            10
                        )
                    );
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new InvalidOperationException(message))
                                .To.Throw<InvalidOperationException>()
                                .With.Message.Starting.With(search);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(
                                $"\"{message}\" to start with \"{search}\""
                            )
                    );
                    // Assert
                }

                [Test]
                public void Negated_HappyPath()
                {
                    // Arrange
                    var message = GetRandomString(
                        10,
                        20
                    );
                    var search = GetAnother(
                        message.Substring(
                            0,
                            10
                        )
                    );
                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(() => throw new InvalidOperationException(message))
                                .To.Throw<InvalidOperationException>()
                                .With.Message.Not.Starting.With(search);
                        },
                        Throws.Nothing
                    );
                    // Assert
                }
            }

            [Test]
            public void
                Throw_WithArgumentNullType_GivenParamNameProperty_ShouldContinueOnToPropertyTest_SadPath_TestingContains()
            {
                // Arrange
                var expected = GetRandomString();
                var unexpected = GetAnother(expected);
                var unexpectedSubstring = GetRandomString(
                    4,
                    4
                );

                // Pre-Assert

                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ArgumentNullException(unexpected))
                            .To.Throw<ArgumentNullException>()
                            .With.Property(ex => ex.ParamName)
                            .Containing(unexpectedSubstring);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected \"{unexpected}\" to contain \"{unexpectedSubstring}\"")
                );

                // Assert
            }

            public class ExceptionWithInts : Exception
            {
                public IEnumerable<int> Ints { get; }

                public ExceptionWithInts(
                    IEnumerable<int> ints
                )
                {
                    Ints = ints;
                }
            }

            [Test]
            public void Throw_UsingProperty_ShouldDoCollectionComparisonOnCollections()
            {
                // Arrange
                var expected = new[] { 1, 2 };
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () =>
                                {
                                    throw new ExceptionWithInts(new[] { 1, 2 });
                                }
                            )
                            .To.Throw<ExceptionWithInts>()
                            .With.CollectionProperty(e => e.Ints)
                            .Equal.To(expected);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            public class SomeExtendedNode : SomeNode
            {
                public DateTime Created { get; set; }
            }

            [Test]
            public void ExceptionPropertyCollectionDeepEqualityTesting()
            {
                // Arrange
                var expected = new SomeNode()
                {
                    Id = 1,
                    Name = "Moo"
                };
                var test = new[]
                {
                    new SomeNode()
                    {
                        Id = 1,
                        Name = "Moo"
                    }
                };
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ExceptionWithNode(expected))
                            .To.Throw<ExceptionWithNode>()
                            .With.CollectionProperty(e => e.Nodes)
                            .Deep.Equal.To(test);
                    },
                    Throws.Nothing
                );
            }

            [Test]
            public void ExceptionPropertyCollectionEquivalenceTesting()
            {
                // Arrange
                var expected = new SomeNode()
                {
                    Id = 1,
                    Name = "Moo"
                };
                var test = new[]
                {
                    new SomeNode()
                    {
                        Id = 1,
                        Name = "Moo"
                    }
                };
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ExceptionWithNode(expected))
                            .To.Throw<ExceptionWithNode>()
                            .With.CollectionProperty(e => e.Nodes)
                            .Equivalent.To(test);
                    },
                    Throws.Nothing
                );
            }

            [Test]
            public void ExceptionPropertyIntersectionEqualityTesting()
            {
                // Arrange
                var expected = new SomeExtendedNode()
                {
                    Id = 1,
                    Name = "Moo",
                    Created = DateTime.Now
                };
                var test = new[]
                {
                    new SomeNode()
                    {
                        Id = 1,
                        Name = "Moo"
                    }
                };
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(() => throw new ExceptionWithNode(expected))
                            .To.Throw<ExceptionWithNode>()
                            .With.CollectionProperty(e => e.Nodes)
                            .Intersection.Equal.To(test);
                    },
                    Throws.Nothing
                );
            }
        }

        [TestFixture]
        public class WithGenericPropertyFetcher
        {
            [TestFixture]
            public class WithArgumentNullType
            {
                [TestFixture]
                public class GivenParamNameProperty
                {
                    [Test]
                    public void Throw_ShouldContinueOnToPropertyTest_HappyPath_TestingEqualTo()
                    {
                        // Arrange
                        var expected = GetRandomString();

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(() => throw new ArgumentNullException(expected))
                                    .To.Throw<ArgumentNullException>()
                                    .With(ex => ex.ParamName)
                                    .Equal.To(expected);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }

                    [Test]
                    public void ContinuingOnToPropertyTestWithNullPropertyValue_ShouldThrowForCorrectReason()
                    {
                        // Arrange
                        // Pre-assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(() => throw new ArgumentException("message, forgetting paramName"))
                                    .To.Throw<ArgumentException>()
                                    .With.Property(e => e.ParamName)
                                    .Equal.To("expectedParameter");
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Not.Contains("Object reference not set to an instance of an object")
                        );
                        // Assert
                    }

                    [Test]
                    public void Throw_ShouldContinueOnToPropertyTest_SadPath_TestingEqualTo()
                    {
                        // Arrange
                        var expected = GetRandomString();
                        var unexpected = GetAnother(expected);

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(() => throw new ArgumentNullException(unexpected))
                                    .To.Throw<ArgumentNullException>()
                                    .With(ex => ex.ParamName)
                                    .Equal.To(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected\n\"{unexpected}\"\nto equal\n\"{expected}\"")
                        );

                        // Assert
                    }
                }
            }
        }

        [TestFixture]
        public class AsyncFunctions
        {
            private async Task ThrowStuffAction()
            {
                await Task.Run(
                    () =>
                    {
                    }
                );
                throw new InvalidOperationException("moo cows");
            }

            private async Task<int> ThrowStuffFunc()
            {
                await Task.Run(
                    () =>
                    {
                    }
                );
                throw new InvalidOperationException("moo cows");
            }

            [Test]
            public void ShouldHandleAsyncActions()
            {
                // Arrange
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(ThrowStuffAction)
                            .To.Throw<InvalidOperationException>()
                            .With.Message.Containing("moo cows");
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(ThrowStuffAction)
                            .To.Throw();
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldHandleAsyncFuncs()
            {
                // Arrange
                // Pre-Assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(ThrowStuffFunc)
                            .To.Throw<InvalidOperationException>();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(ThrowStuffFunc)
                            .To.Throw();
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }

        [TestFixture]
        public class RippingOffComplexPropertiesOnExceptions
        {
            [Test]
            public void ShouldBeAbleToDoAIntersectionEqual()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Intersection.Equal.To(expected);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToDoAIntersectionEqual_Fail()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                var unexpected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Intersection.Equal.To(unexpected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToDoAIntersectionEqual_Negated()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                var unexpected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Not.Intersection.Equal.To(unexpected);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToDoADeepEqual()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Deep.Equal.To(expected);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToDoADeepEqual_Fail()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                var unexpected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Deep.Equal.To(unexpected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToDoADeepEqual_Negated()
            {
                // Arrange
                var expected = GetRandom<Complex>();
                var unexpected = GetRandom<Complex>();
                // Pre-assert
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ExceptionWithComplex(
                                    GetRandomString(1),
                                    expected
                                )
                            )
                            .To.Throw<ExceptionWithComplex>()
                            .With.Property(e => e.Complex)
                            .Not.Deep.Equal.To(unexpected);
                    },
                    Throws.Nothing
                );
                // Assert
            }

            public class Complex
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            public class ExceptionWithComplex : Exception
            {
                public Complex Complex { get; set; }

                public ExceptionWithComplex(
                    string message,
                    Complex complex
                ) : base(message)
                {
                    Complex = complex;
                }
            }
        }

        [TestFixture]
        public class ConvenienceSyntaxForArgumentException
        {
            [Test]
            public void ShouldHaveShorthandToVerifyParameterName()
            {
                // Arrange
                var expected = GetRandomString(4);
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ArgumentException(
                                    "message here",
                                    expected
                                )
                            )
                            .To.Throw<ArgumentException>()
                            .For(expected)
                            .With.Message.Containing("message here");
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(
                                () => throw new ArgumentException(
                                    "message",
                                    expected
                                )
                            )
                            .Not.To.Throw<ArgumentException>()
                            .For(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }
        }
    }

    public class ExceptionWithNode : Exception
    {
        public SomeNode[] Nodes { get; set; }

        public ExceptionWithNode(
            params SomeNode[] nodes
        )
            : base($"{nodes[0].Id} / {nodes[0].Name}")
        {
            Nodes = nodes;
        }
    }

    public class SomeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(
            object obj
        )
        {
            var other = obj as SomeNode;
            if (other == null)
            {
                return false;
            }

            return Id == other.Id &&
                Name == other.Name;
        }

        protected bool Equals(
            SomeNode other
        )
        {
            return Id == other.Id &&
                String.Equals(
                    Name,
                    other.Name
                );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null
                    ? Name.GetHashCode()
                    : 0);
            }
        }
    }
}