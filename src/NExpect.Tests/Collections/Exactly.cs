using System;
using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Exactly
    {
        [TestFixture]
        public class Equal
        {
            [TestFixture]
            public class To
            {
                [TestFixture]
                public class OperatingOnCollectionOfStrings
                {
                    [Test]
                    public void WhenDoesContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }

                    [TestFixture]
                    public class ShortContain
                    {
                        [Test]
                        public void WhenDoesContain_ShouldNotThrow()
                        {
                            // Arrange
                            var search = GetRandomString(3);
                            var other1 = GetAnother(search);
                            var other2 = GetAnother<string>(
                                new[]
                                {
                                    search,
                                    other1
                                }
                            );
                            var collection = new[]
                            {
                                search,
                                other1,
                                other2
                            }.Randomize();

                            // Pre-Assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain(search);
                                },
                                Throws.Nothing
                            );

                            // Assert
                        }

                        [Test]
                        public void Negated_WhenDoesContain_ShouldThrow()
                        {
                            // Arrange
                            var search = GetRandomString(3);
                            var other1 = GetAnother(search);
                            var other2 = GetAnother<string>(
                                new[]
                                {
                                    search,
                                    other1
                                }
                            );
                            var collection = new[]
                            {
                                search,
                                other1,
                                other2
                            }.Randomize();

                            // Pre-Assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).Not.To.Contain(search);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );

                            // Assert
                        }

                        [Test]
                        public void
                            Negated_AltGrammar_WhenDoesContain_ShouldThrow()
                        {
                            // Arrange
                            var search = GetRandomString(3);
                            var other1 = GetAnother(search);
                            var other2 = GetAnother<string>(
                                new[]
                                {
                                    search,
                                    other1
                                }
                            );
                            var collection = new[]
                            {
                                search,
                                other1,
                                other2
                            }.Randomize();

                            // Pre-Assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Not.Contain(search);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );

                            // Assert
                        }

                        [Test]
                        public void ShouldBeAbleToChain()
                        {
                            // Arrange
                            var ints = new[]
                            {
                                1,
                                2,
                                3
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .To.Contain(3);
                                },
                                Throws.Nothing
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .Not.To.Contain(4);
                                },
                                Throws.Nothing
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .Not.To.Contain(3);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldBeAbleToChainForHashSet()
                        {
                            // Arrange
                            var ints = new HashSet<int>(
                                new[]
                                {
                                    1,
                                    2,
                                    3
                                }
                            );
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .To.Contain(3);
                                },
                                Throws.Nothing
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .Not.To.Contain(4);
                                },
                                Throws.Nothing
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(ints)
                                        .To.Contain(1)
                                        .And
                                        .Not.To.Contain(3);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class StringCollectionContainingStringContaining
                    {
                        [Test]
                        public void WhenCollectionContains1StringContainingTestValueWith1Required_ShouldNotThrow()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "moo cows",
                                "bovine",
                                "beef-witted"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1).Containing("cow");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void WhenCollectionContains2StringsContainingTestValueWith2Required_ShouldNotThrow()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "moo cows",
                                "bovine",
                                "beef-witted",
                                "cows moo"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection)
                                        .To.Contain.Exactly(2).Containing("cow");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void WhenCollectionContainsTooManyMatchingStrings_ShouldThrow()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "moo cows",
                                "bovine",
                                "beef-witted",
                                "cows moo"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1).Containing("cow");
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            // Assert
                        }

                        [Test]
                        public void WhenThrowing_ShouldIncludeCustomMessageIfSet()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "moo cows",
                                "bovine",
                                "beef-witted",
                                "cows moo"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1).Containing("cow", expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedStringComparison()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "bovine",
                                "beef-witted",
                                "cows moo"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Containing("Cow", StringComparison.OrdinalIgnoreCase);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedStringComparisonAndCustomMessage()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "bovine",
                                "beef-witted",
                                "Cows moo"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1).Containing(
                                        "cow",
                                        StringComparison.Ordinal,
                                        expected
                                    );
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class StringCollectionContainingItemEndingWith
                    {
                        [Test]
                        public void WhenShouldPassWithOneOrdinalMatch_ShouldNotThrow()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "cow says moo",
                                "foo bar"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Ending.With("moo");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedComparer()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "cow says Moo",
                                "foo bar"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Ending.With("moo", StringComparison.OrdinalIgnoreCase);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedCustomMessageForFailure()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "cow says Moo",
                                "foo bar"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Ending.With("moo", expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedStringComparerAndCustomMessageForFailure()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "cow says Moo",
                                "foo bar"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Ending.With("moo", StringComparison.InvariantCulture, expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class StringCollectionContainingItemStartingWith
                    {
                        [Test]
                        public void WhenShouldPassWithOneOrdinalMatch_ShouldNotThrow()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "moo, said the cow",
                                "foo bar"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Starting.With("moo");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedComparer()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "Moo said the cow",
                                "foo bar"
                            };
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Starting.With("moo", StringComparison.OrdinalIgnoreCase);
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedCustomMessageForFailure()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "Moo said the cow",
                                "foo bar"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Starting.With("moo", expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                                    .And.Message.Contains(collection[0])
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldUseProvidedStringComparerAndCustomMessageForFailure()
                        {
                            // Arrange
                            var collection = new[]
                            {
                                "Moo, said the cow",
                                "foo bar"
                            };
                            var expected = GetRandomString(10);
                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(collection).To.Contain.Exactly(1)
                                        .Starting.With("moo", StringComparison.InvariantCulture, expected);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(expected)
                            );
                            // Assert
                        }
                    }

                    [TestFixture]
                    public class StringCollectionContainingItemMatchedByRegex
                    {
                        [Test]
                        public void ShouldFindSingleMatch()
                        {
                            // Arrange
                            var data = new[]
                            {
                                "foo",
                                "bar",
                                "quux"
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(data)
                                        .To.Contain.Exactly(1)
                                        .Matched.By("^foo$");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldFailOnNoMatch()
                        {
                            // Arrange
                            var data = new[]
                            {
                                "bar",
                                "quux"
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(data)
                                        .To.Contain.Exactly(1)
                                        .Matched.By("^foo$");
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            // Assert
                        }

                        [Test]
                        public void ShouldFailOnCountMisMatch()
                        {
                            // Arrange
                            var data = new[]
                            {
                                "foobar",
                                "fooquux",
                                "barfoo"
                            };
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(data)
                                        .To.Contain.Exactly(1)
                                        .Matched.By("foo");
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(data)
                                        .To.Contain.Exactly(1)
                                        .Matched.By("^foo");
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                            );
                            Assert.That(
                                () =>
                                {
                                    Expect(data)
                                        .To.Contain.Exactly(1)
                                        .Matched.By("foo$");
                                },
                                Throws.Nothing
                            );
                            // Assert
                        }
                    }


                    [Test]
                    public void WhenSeeking2AndDoesContain2_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2,
                            search
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Contain.Exactly(2).Equal.To(search);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }

                    [Test]
                    public void WhenSeeking1AndDoesContain2_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var customMessage = GetRandomString(2);
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2,
                            search
                        }.Randomize();
                        var standardMessage = $"to find exactly 1 occurrence of \"{search}\" but found 2";

                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search, customMessage);
                            },
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Matches($"{customMessage}.*\\s*.*{standardMessage}")
                        );
                        // Assert
                    }

                    [Test]
                    public void WhenDoesNoContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("find exactly 1 occurrence of")
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenDoesContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws
                                .Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message
                                .Contains($"not to find exactly 1 occurrence of \"{search}\" but found 1")
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenDoesNotContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_WhenDoesContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws
                                .Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message
                                .Contains($"not to find exactly 1 occurrence of \"{search}\" but found 1")
                        );

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_WhenDoesNotContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(
                            new[]
                            {
                                search,
                                other1
                            }
                        );
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing
                        );

                        // Assert
                    }
                }
            }
        }
    }
}