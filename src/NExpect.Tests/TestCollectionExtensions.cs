using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
using static PeanutButter.Utils.PyLike;
using NExpect.Exceptions;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests
{
    [TestFixture]
    public class TestCollectionExtensions
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
                    [Test]
                    public void Contain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void ShortContain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Contain(search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void Contain_OperatingOnCollectionOfStrings_WhenSeeking2AndDoesContain2_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2,
                            search
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Contain.Exactly(2).Equal.To(search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void Contain_OperatingOnCollectionOfStrings_WhenSeeking1AndDoesContain2_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2,
                            search
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"to find exactly 1 occurrence of {search} but found 2"));

                        // Assert
                    }

                    [Test]
                    public void Contain_OperatingOnCollectionOfStrings_WhenDoesNoContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("find exactly 1 occurrence of"));

                        // Assert
                    }

                    [Test]
                    public void Negated_Contain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws
                                .Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"not to find exactly 1 occurrence of {search} but found 1"));

                        // Assert
                    }

                    [Test]
                    public void Negated_Contain_OperatingOnCollectionOfStrings_WhenDoesNotContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_Contain_OperatingOnCollectionOfStrings_WhenDoesContain_ShouldThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            search,
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws
                                .Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"not to find exactly 1 occurrence of {search} but found 1"));

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_Contain_OperatingOnCollectionOfStrings_WhenDoesNotContain_ShouldNotThrow()
                    {
                        // Arrange
                        var search = GetRandomString(3);
                        var other1 = GetAnother(search);
                        var other2 = GetAnother<string>(new[] {search, other1});
                        var collection = new[]
                        {
                            other1,
                            other2
                        }.Randomize();

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [TestFixture]
                    public class CustomEqualityTesting
                    {
                        // TODO: could have more tests
                        //  -> the other implementations lean on the logic to
                        //      accomplish this, so, techically, the other cases are covered...
                        [TestFixture]
                        public class WithProvidedComparer
                        {
                            [Test]
                            public void PositiveResult_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var withComparer = new FirstLetterComparer();
                                Assert.That(withComparer.Equals(left[0], right[0]), Is.True);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).To.Equal(right, withComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeNegatedResult_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};
                                var withComparer = new FirstLetterComparer();
                                Assert.That(withComparer.Equals(left[0], right[0]), Is.False);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).Not.To.Equal(right, withComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeNegatedResult_AltGrammar_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};
                                var withComparer = new FirstLetterComparer();
                                Assert.That(withComparer.Equals(left[0], right[0]), Is.False);

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).To.Not.Equal(right, withComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }
                        }

                        [TestFixture]
                        public class WithProvidedComparisonFunc
                        {
                            private static bool FirstLetterComparer(string x, string y)
                            {
                                if (x == null &&
                                    y == null)
                                    return true;
                                if (x == null ||
                                    y == null)
                                    return false;
                                if (x.Length == 0 &&
                                    y.Length == 0)
                                    return true;
                                if (x.Length == 0 ||
                                    y.Length == 0)
                                    return false;
                                return x[0] == y[0];
                            }

                            [Test]
                            public void PositiveResult_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).To.Equal(right, FirstLetterComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeNegatedResult_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).Not.To.Equal(right, FirstLetterComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }

                            [Test]
                            public void NegativeNegatedResult_AltGrammar_ShouldNotThrow()
                            {
                                // Arrange
                                var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                                var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                                // Pre-Assert

                                // Act
                                Assert.That(() =>
                                    {
                                        Expect(left).To.Not.Equal(right, FirstLetterComparer);
                                    },
                                    Throws.Nothing);

                                // Assert
                            }
                        }
                    }
                }
            }
        }

        [TestFixture]
        public class AtLeast
        {
            [TestFixture]
            public class EqualTo
            {
                [Test]
                public void Contain_GivenAtLeast1_WhenCollectionHasNone_ShouldThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.At.Least(1).Equal.To(search);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("at least 1"));
                    // Assert
                }

                [Test]
                public void Contain_Any_WhenCollectionHasNone_ShouldThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.Any().Equal.To(search);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected to find any match"));
                    // Assert
                }

                [Test]
                public void Contain_Any_WhenCollectionHas1_ShouldNotThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2, search}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.Any().Equal.To(search);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Contain_All_WhenCollectionHasMismatches_ShouldThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2, search}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.All().Equal.To(search);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected to find all matching"));
                    // Assert
                }

                [Test]
                public void Contain_All_WhenCollectionHasMismatchesWithMatcher_ShouldThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2, search, null}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.All().Matched.By(s => s == null);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected to find all matching but found 1"));
                    // Assert
                }

                [Test]
                public void Contain_Any_WhenCollectionHasMismatchesWithMatcher_ShouldThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {item1, item2, search}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.Any().Matched.By(s => s == null);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected to find any matching but found none"));
                    // Assert
                }

                [Test]
                public void Contain_Any_MatchedByWithIndex_WhenCollectionHasMismatchesWithMatcher_ShouldThrow()
                {
                    // Arrange
                    var items = Range(10).Select(i => i + 1).ToArray();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(items).To.Contain.Any().Matched.By((idx, item) => item == idx);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("Expected to find any matching but found none"));
                    // Assert
                }

                [Test]
                public void Contain_All_MatchedByWithIndex_WhenCollectionHasMismatchesWithMatcher_ShouldThrow()
                {
                    // Arrange
                    var items = Range(10).Select(i => i).ToArray();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(items).To.Contain.All().Matched.By((idx, item) => item == idx);
                        },
                        Throws.Nothing);
                    // Assert
                }


                [Test]
                public void Contain_All_WhenCollectionHasNoMismatches_ShouldNotThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var collection = Range(2, 4).Select(i => search);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.All().Equal.To(search);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Contain_GivenAtLeast1_WhenCollectionHas1_ShouldNotThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {search, item1, item2}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.At.Least(1).Equal.To(search);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Contain_GivenAtLeast1_WhenCollectionHas2_ShouldNotThrow()
                {
                    // Arrange
                    var search = GetRandomString();
                    var item1 = GetAnother(search);
                    var item2 = GetAnother<string>(new[] {item1, search});
                    var collection = new[] {search, item1, search, item2}.Randomize();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Contain.At.Least(1).Equal.To(search);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }

            [TestFixture]
            public class Matched
            {
                [TestFixture]
                public class By
                {
                    [Test]
                    public void OperatingOnCollection_WhenSeeking1Match_AndHas1Match_ShouldNotThrow()
                    {
                        // Arrange
                        var collection = GetRandomCollection<string>(3, 3).ToArray();
                        var search = collection.Randomize().First();
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection)
                                    .To.Contain
                                    .At.Least(1)
                                    .Matched.By(s => s == search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void OperatingOnCollection_WhenSeeking1Match_AndHas2Matches_ShouldNotThrow()
                    {
                        // Arrange
                        var collection = GetRandomCollection<string>(3, 3).ToArray();
                        var search = collection.Randomize().First();
                        collection = collection.And(search).Randomize().ToArray();
                        // Pre-Assert
                        Assert.That(collection.Count(s => s == search), Is.EqualTo(2));
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection)
                                    .To.Contain
                                    .At.Least(1)
                                    .Matched.By(s => s == search);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void OperatingOnCollection_WhenSeeking2Matches_AndHas1Match_ShouldThrow()
                    {
                        // Arrange
                        var collection = GetRandomCollection<string>(3, 3).ToArray();
                        var search = collection.Randomize().First();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection)
                                    .To.Contain
                                    .At.Least(2)
                                    .Matched.By(s => s == search);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());

                        // Assert
                    }

                    [Test]
                    public void Negated_OperatingOnCollection_WhenSeeking1Match_AndHas1Match_ShouldThrow()
                    {
                        // Arrange
                        var collection = GetRandomCollection<string>(3, 3).ToArray();
                        var search = collection.Randomize().First();
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection)
                                    .Not.To.Contain
                                    .At.Least(1)
                                    .Matched.By(s => s == search);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());

                        // Assert
                    }

                    [Test]
                    public void NegatedAlt_OperatingOnCollection_WhenSeeking1Match_AndHas1Match_ShouldThrow()
                    {
                        // Arrange
                        var collection = GetRandomCollection<string>(3, 3).ToArray();
                        var search = collection.Randomize().First();
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection)
                                    .To.Not.Contain
                                    .At.Least(1)
                                    .Matched.By(s => s == search);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());

                        // Assert
                    }
                }
            }
        }

        public class AtMost
        {
            [Test]
            public void Contain_GivenAtMost1_WhenCollectionHasNone_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString();
                var item1 = GetAnother(search);
                var item2 = GetAnother<string>(new[] {item1, search});
                var collection = new[] {item1, item2}.Randomize();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void Contain_GivenAtMost1_WhenCollectionHas1_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString();
                var item1 = GetAnother(search);
                var item2 = GetAnother<string>(new[] {item1, search});
                var collection = new[] {search, item1, item2}.Randomize();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void Contain_GivenAtMost1_WhenCollectionHas2_ShouldThrow()
            {
                // Arrange
                var search = GetRandomString();
                var item1 = GetAnother(search);
                var item2 = GetAnother<string>(new[] {item1, search});
                var collection = new[] {search, item1, search, item2}.Randomize();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.At.Most(1).Equal.To(search);
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("at most 1"));
                // Assert
            }
        }

        [TestFixture]
        public class CollectionAll
        {
            [Test]
            public void WhenAllMatch_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString(4);
                var collection = Range(GetRandomInt(3, 5)).Select(i => search);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.All().Equal.To(search);
                    },
                    Throws.Nothing);
                // Assert
            }

            [Test]
            public void WhenNotAllMatch_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString(4);
                var collection = Range(GetRandomInt(3, 5)).Select(i => search).Union(new[] {GetAnother(search)});

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Contain.All().Equal.To(search);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void Negated_WhenAllMatch_ShouldThrow()
            {
                // Arrange
                var search = GetRandomString(4);
                var collection = Range(GetRandomInt(3, 5)).Select(i => search);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Contain.All().Equal.To(search);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void NegatedAlt_WhenAllMatch_ShouldThrow()
            {
                // Arrange
                var search = GetRandomString(4);
                var collection = Range(GetRandomInt(3, 5)).Select(i => search);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Not.Contain.All().Equal.To(search);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }
        }

        [TestFixture]
        public class HaveAny
        {
            [Test]
            public void WhenHave1MatchInActual_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString();
                var actual = GetRandomCollection<string>(2, 4).Union(search.AsArray());

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Contain.Any().Equal.To(search);
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenHave0MatchInActual_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString();
                var actual = GetRandomCollection<string>(2, 4);

                // Pre-Assert
                Assert.That(actual, Does.Not.Contain(search), "Should not find search before test");

                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Contain.Any().Equal.To(search);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void WhenHaveActualAllMatchingSearch_ShouldNotThrow()
            {
                // Arrange
                var search = GetRandomString();
                var actual = Range(GetRandomInt(2, 4)).Select(i => search);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(actual).To.Contain.Any().Equal.To(search);
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [Test]
        public void ShouldBeAbleToOperateOnOtherCollections()
        {
            // Arrange
            var collection = new List<string>(new[] {"a", "b", "c"});
            // Pre-Assert

            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(new Queue<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(collection as IList<string>).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(collection as ICollection<string>).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(new Stack<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            Assert.That(() =>
                {
                    Expect(new HashSet<string>(collection)).To.Contain.Exactly(1).Equal.To("a");
                },
                Throws.Nothing);

            // Assert
        }

        [TestFixture]
        public class ToBeEmpty
        {
            [Test]
            public void OperatingOnEmptyCollection_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<string>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void OperatingOnEmptyCollection_WhenNegated_ShouldThrow()
            {
                // Arrange
                var collection = new List<string>();

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.EqualTo("Expected [  ] not to be an empty collection"));

                // Assert
            }

            [Test]
            public void OperatingOnNonEmptyCollection_ShouldThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Be.Empty();
                    },
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("] to be an empty collection"));

                // Assert
            }

            [Test]
            public void OperatingOnNonEmptyCollection_WhenNegated_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).Not.To.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }

            [Test]
            public void OperatingOnNonEmptyCollection_WhenNegatedAlt_ShouldNotThrow()
            {
                // Arrange
                var collection = GetRandomCollection<string>(2);

                // Pre-Assert

                // Act
                Assert.That(() =>
                    {
                        Expect(collection).To.Not.Be.Empty();
                    },
                    Throws.Nothing);

                // Assert
            }
        }

        [TestFixture]
        public class Equivalent
        {
            [TestFixture]
            public class To
            {
                [Test]
                public void OperatingOnEmptyCollection_ComparingWithEmptyCollection_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int>();
                    var compare = new int[0];

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Equivalent.To(compare);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoIdenticalCollections_ShouldNotThrow()
                {
                    // Arrange
                    var start = GetRandomCollection<int>(4, 6).ToArray();
                    var other = start.Select(i => i).ToArray();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(start).To.Be.Equivalent.To(other);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoEquivalentCollections_ShouldNotThrow()
                {
                    // Arrange
                    var start = GetRandomCollection<string>(4, 6).ToArray();
                    var other = start.Randomize();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(start).To.Be.Equivalent.To(other);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoInequivalentCollectionsOfSameSize_ShouldThrow()
                {
                    // Arrange
                    var test = GetRandomArray<decimal>(4, 6);
                    var other = GetRandomCollection<decimal>(test.Length, test.Length);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).To.Be.Equivalent.To(other);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("] to be equivalent to ["));

                    // Assert
                }

                [Test]
                public void OperatingOnTwoInequivalentCollectionsOfSameSize_Negated_ShouldNotThrow()
                {
                    // Arrange
                    var test = GetRandomArray<string>(4, 6);
                    var other = GetRandomCollection<string>(test.Length, test.Length);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).Not.To.Be.Equivalent.To(other);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoInequivalentCollectionsOfSameSize_NegatedAlt_ShouldNotThrow()
                {
                    // Arrange
                    var test = GetRandomArray<string>(4, 6);
                    var other = GetRandomCollection<string>(test.Length, test.Length);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).To.Not.Be.Equivalent.To(other);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoEquivalentCollectionsOfSameSizeWithSameRepeatedElements_ShouldNotThrow()
                {
                    // Arrange
                    var test = new[] {1, 1, 2, 3};
                    var other = new[] {1, 2, 3, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).To.Be.Equivalent.To(other);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnTwoEquivalentCollectionsOfSameSizeWithDifferentRepeatedElements_ShouldThrow()
                {
                    // Arrange
                    var test = new[] {1, 1, 2, 3};
                    var other = new[] {1, 2, 3, 2};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).To.Be.Equivalent.To(other);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("] to be equivalent to ["));

                    // Assert
                }

                [Test]
                public void
                    OperatingOnTwoEquivalentCollectionsOfSameSizeWithSameRepeatedElements_WhenNegated_ShouldThrow()
                {
                    // Arrange
                    var test = new[] {1, 1, 2, 3};
                    var other = new[] {1, 2, 3, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(test).Not.To.Be.Equivalent.To(other);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("] not to be equivalent to ["));

                    // Assert
                }

                [Test]
                public void Extending_CountMatchContinuation()
                {
                    // Arrange
                    var evens = new[] {2, 4, 6};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(evens).Not.To.Contain.Any().Odds();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Extending_CountMatchContinuationNegated()
                {
                    // Arrange
                    var evens = new[] {2, 4, 6};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(evens).To.Contain.Any().Odds();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }
            }

            [TestFixture]
            public class Null
            {
                [Test]
                public void OperatingOnNull_ShouldNotThrow()
                {
                    // Arrange
                    List<string> collection = null;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            Expect(collection).To.Be.Null();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void OperatingOnNull_Negated_ShouldThrow()
                {
                    // Arrange
                    List<string> collection = null;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            Expect(collection).Not.To.Be.Null();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("not to be null"));
                    // Assert
                }

                [Test]
                public void OperatingOnNotNull_Negated_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<string>();

                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Be.Null();
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void OperatingOnNotNull_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<string>();

                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Null();
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("] to be null"));
                    // Assert
                }

                [TestFixture]
                public class WithCustomMessage
                {
                    [Test]
                    public void OperatingOnNull_Negated_ShouldThrow()
                    {
                        // Arrange
                        var expectedMessage = "My Message";
                        List<string> collection = null;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                // ReSharper disable once ExpressionIsAlwaysNull
                                Expect(collection).Not.To.Be.Null(expectedMessage);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(expectedMessage));
                        // Assert
                    }

                    [Test]
                    public void OperatingOnNotNull_WithCustomMessage_ShouldThrow_IncludingCustomMessage()
                    {
                        // Arrange
                        var collection = new List<string>();
                        var expected = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Be.Null(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(expected));
                        // Assert
                    }

                    [Test]
                    public void OperatingOnNotNullAlt_WithCustomMessage_ShouldThrow_IncludingCustomMessage()
                    {
                        // Arrange
                        List<string> collection = null;
                        var expected = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                // ReSharper disable once ExpressionIsAlwaysNull
                                Expect(collection).To.Not.Be.Null(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains(expected));
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Distinct
            {
                [Test]
                public void OperatingOnEmptyCollection_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int>();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Distinct();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void OperatingOnNullCollection_ShouldThrow()
                {
                    // Arrange
                    List<int> collection = null;

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            Expect(collection).To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnEmptyCollection_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int>();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>()
                    );

                    // Assert
                }

                [Test]
                public void OperatingOnCollectionWithSameItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void OperatingOnCollectionWithUniqueItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Be.Distinct();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnCollectionWithUniqueItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_OperatingOnCollectionWithUniqueItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Not.Be.Distinct();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnCollectionWithSameItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Be.Distinct();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_OperatingOnCollectionWithSameItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Not.Be.Distinct();
                        },
                        Throws.Nothing);

                    // Assert
                }
            }

            [TestFixture]
            public class HaveUniqueItems
            {
                [Test]
                public void OperatingOnEmptyCollection_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int>();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Have.Unique.Items();
                        },
                        Throws.Nothing
                    );

                    // Assert
                }

                [Test]
                public void OperatingOnNullCollection_ShouldThrow()
                {
                    // Arrange
                    List<int> collection = null;

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            Expect(collection).To.Have.Unique.Items();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnEmptyCollection_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int>();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Have.Unique.Items();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>()
                    );

                    // Assert
                }

                [Test]
                public void OperatingOnCollectionWithSameItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Have.Unique.Items();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void OperatingOnCollectionWithUniqueItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Have.Unique.Items();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnCollectionWithUniqueItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Have.Unique.Items();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_OperatingOnCollectionWithUniqueItems_ShouldThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 2, 3};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Not.Have.Unique.Items();
                        },
                        Throws.Exception.TypeOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void Negated_OperatingOnCollectionWithSameItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).Not.To.Have.Unique.Items();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_OperatingOnCollectionWithSameItems_ShouldNotThrow()
                {
                    // Arrange
                    var collection = new List<int> {1, 1};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expect(collection).To.Not.Have.Unique.Items();
                        },
                        Throws.Nothing);

                    // Assert
                }

                [TestFixture]
                public class UnmetMessage
                {
                    [Test]
                    public void OperatingOnCollectionWithSameItems()
                    {
                        // Arrange
                        var collection = new List<int> {1, 1, 1};

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).To.Have.Unique.Items();
                            },
                            Throws.Exception.TypeOf<UnmetExpectationException>()
                                .With.Message.EqualTo("Expected [ 1, 1, 1 ] to only contain unique items")
                        );
                        // Assert
                    }

                    [Test]
                    public void OperatingOnCollectionWithUniqueItems()
                    {
                        // Arrange
                        var collection = new List<int> {1, 2, 3};

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).Not.To.Have.Unique.Items();
                            },
                            Throws.Exception.TypeOf<UnmetExpectationException>()
                                .With.Message.EqualTo("Expected [ 1, 2, 3 ] to contain duplicate items")
                        );
                        // Assert
                    }

                    [Test]
                    public void OperatingOnEmptyCollection()
                    {
                        // Arrange
                        var collection = new List<int>();

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expect(collection).Not.To.Have.Unique.Items();
                            },
                            Throws.Exception.TypeOf<UnmetExpectationException>()
                                .With.Message
                                .EqualTo("Expected [  ] to contain duplicate items, but found empty collection")
                        );
                        // Assert
                    }

                    [Test]
                    public void OperatingOnNullCollection()
                    {
                        // Arrange
                        List<int> collection = null;

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                // ReSharper disable once ExpressionIsAlwaysNull
                                Expect(collection).To.Have.Unique.Items();
                            },
                            Throws.Exception.TypeOf<UnmetExpectationException>()
                                .With.Message.Contains("Expected IEnumerable<Int32>, but found (null)")
                        );
                        // Assert
                    }
                }

                [TestFixture]
                public class ItemCountTesting
                {
                    [Test]
                    public void WhenCollectionHasExpectedCount_ShouldNotThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(1);
                        var input = new int[expected];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).To.Contain.Exactly(expected).Items();
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void WhenCollectionDoesNotHaveExpectedCount_ShouldThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(10);
                        var delta = GetRandomInt(1, 3);
                        var actual = GetRandomBoolean()
                            ? expected + delta
                            : expected - delta;
                        var input = new int[actual];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).To.Contain.Exactly(expected).Items();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected to find {expected}"));
                        // Assert
                    }

                    [Test]
                    public void Negated_WhenCollectionDoesNotHaveExpectedCount_ShouldNotThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(10);
                        var delta = GetRandomInt(1, 3);
                        var actual = GetRandomBoolean()
                            ? expected + delta
                            : expected - delta;
                        var input = new int[actual];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).Not.To.Contain.Exactly(expected).Items();
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void Negated_WhenCollectionDoesHaveExpectedCount_ShouldThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(10);
                        var delta = GetRandomInt(1, 3);
                        var actual = GetRandomBoolean()
                            ? expected + delta
                            : expected - delta;
                        var input = new int[actual];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).Not.To.Contain.Exactly(actual).Items();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected not to find {actual} items"));
                        // Assert
                    }

                    [Test]
                    public void Negated_AltGrammar_WhenCollectionDoesNotHaveExpectedCount_ShouldNotThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(10);
                        var delta = GetRandomInt(1, 3);
                        var actual = GetRandomBoolean()
                            ? expected + delta
                            : expected - delta;
                        var input = new int[actual];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).To.Not.Contain.Exactly(expected).Items();
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void No_WhenCollectionHasNoItems_ShouldThrow()
                    {
                        // Arrange
                        var input = new int[0];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).To.Contain.No().Items();
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void No_WhenCollectionHasItems_ShouldThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(1);
                        var input = new int[expected];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).To.Contain.No().Items();
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void No_Negated_WhenCollectionHasItems_ShouldNotThrow()
                    {
                        // Arrange
                        var expected = GetRandomInt(1);
                        var input = new int[expected];
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(input).Not.To.Contain.No().Items();
                            },
                            Throws.Nothing);
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Equal_WithNoCustomMessage
            {
                [Test]
                public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Equal(right);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).Not.To.Equal(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Not.Equal(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {2, 1};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Equal(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }
            }

            [TestFixture]
            public class Equal_NoCustomMessage_LongerSyntax
            {
                [Test]
                public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Be.Equal.To(right);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).Not.To.Be.Equal.To(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Not.Be.Equal.To(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {2, 1};
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Be.Equal.To(right);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }
            }

            [TestFixture]
            public class Equal_WithCustomMessage
            {
                [Test]
                public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Equal(right, message);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).Not.To.Equal(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Not.Equal(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }

                [Test]
                public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {2, 1};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Equal(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }
            }

            [TestFixture]
            public class Equal_WithCustomMessageLongerSyntax
            {
                [Test]
                public void GivenTwoCollectionsWithSameItemsInSameOrder_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Be.Equal.To(right, message);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void Negated_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).Not.To.Be.Equal.To(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }

                [Test]
                public void Negated_AltGrammar_GivenTwoCollectionsWithSameItemsInSameOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {1, 2};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Not.Be.Equal.To(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }

                [Test]
                public void GivenTwoCollectionsWithSameItemsOutOfOrder_ShouldThrow()
                {
                    // Arrange
                    var left = new[] {1, 2};
                    var right = new[] {2, 1};
                    var message = GetRandomString();
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expect(left).To.Be.Equal.To(right, message);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(message));
                    // Assert
                }
            }

            [TestFixture]
            public class Deep
            {
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

                private static IdentifierAndName o(int id, string name)
                {
                    return new IdentifierAndName(id, name);
                }

                [TestFixture]
                public class Equivalent
                {
                    [TestFixture]
                    public class To
                    {
                        [Test]
                        public void PositiveExpectation_WhenHaveEquivalence_ShouldNotThrow()
                        {
                            // Arrange
                            var first = new[] { o(1, "moo"), o(2, "cow") };
                            var second = new[] { o(2, "cow"), o(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first).To.Be.Deep.Equivalent.To(second);
                            }, Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void NegativeExpectation_WhenHaveEquivalence_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o(1, "moo"), o(2, "cow") };
                            var second = new[] { o(2, "cow"), o(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first).Not.To.Be.Deep.Equivalent.To(second);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void NegativeExpectation_AltGrammar_WhenHaveEquivalence_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o(1, "moo"), o(2, "cow") };
                            var second = new[] { o(2, "cow"), o(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first).To.Not.Be.Deep.Equivalent.To(second);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void PositiveExpectation_WhenCollectionsDontMatch_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o(1, "bob"), o(2, "janet") };
                            var second = new[] { o(1, "bobby"), o(2, "janet") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(first).To.Be.Deep.Equivalent.To(second);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }
                    }
                }

                [TestFixture]
                public class Equal
                {
                    [Test]
                    public void PositiveExpectation_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o(1, "bob"), o(2, "janet")};
                        var second = new[] {o(1, "bob"), o(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).To.Deep.Equal(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_WhenCollectionsMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o(1, "bob"), o(2, "janet")};
                        var second = new[] {o(1, "bob"), o(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).Not.To.Deep.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenCollectionsMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o(1, "bob"), o(2, "janet")};
                        var second = new[] {o(1, "bob"), o(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).To.Not.Deep.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_AltGrammer_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o(1, "bob"), o(2, "janet")};
                        var second = new[] {o(1, "bob"), o(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).To.Be.Deep.Equal.To(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o(1, "bob"), o(2, "janet")};
                        var second = new[] {o(1, "bob"), o(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).Not.To.Be.Deep.Equal.To(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsDontMatchInFirstRecord_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] { o(1, "bob"), o(2, "janet"), o(3, "paddy") };
                        var second = new[] { o(1, "bobby"), o(2, "janet"), o(3, "paddy") };
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).To.Deep.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsDontMatchInLastRecord_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] { o(1, "bob"), o(2, "janet"), o(3, "paddy") };
                        var second = new[] { o(1, "bob"), o(2, "janet"), o(3, "mcgee") };
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first).To.Deep.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Intersection
            {
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

                public class OtherIdentifierAndName
                {
                    public int Id { get; }
                    public string Name { get; }
                    public string Type => GetType().Name;

                    public OtherIdentifierAndName(int id, string name)
                    {
                        Id = id;
                        Name = name;
                    }
                }

                private static IdentifierAndName o1(int id, string name)
                {
                    return new IdentifierAndName(id, name);
                }

                private static OtherIdentifierAndName o2(int id, string name)
                {
                    return new OtherIdentifierAndName(id, name);
                }

                [TestFixture]
                public class Equivalent
                {
                    [TestFixture]
                    public class To
                    {
                        [Test]
                        public void PositiveExpectation_WhenHaveEquivalence_ShouldNotThrow()
                        {
                            // Arrange
                            var first = new[] { o1(1, "moo"), o1(2, "cow") };
                            var second = new[] { o2(2, "cow"), o2(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Be.Intersection.Equivalent.To(second);
                            }, Throws.Nothing);
                            // Assert
                        }

                        [Test]
                        public void NegativeExpectation_WhenHaveEquivalence_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o1(1, "moo"), o1(2, "cow") };
                            var second = new[] { o2(2, "cow"), o2(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first.AsObjects()).Not.To.Be.Intersection.Equivalent.To(second);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void NegativeExpectation_AltGrammar_WhenHaveEquivalence_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o1(1, "moo"), o1(2, "cow") };
                            var second = new[] { o2(2, "cow"), o2(1, "moo") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Not.Be.Intersection.Equivalent.To(second);
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }

                        [Test]
                        public void PositiveExpectation_WhenCollectionsDontMatch_ShouldThrow()
                        {
                            // Arrange
                            var first = new[] { o1(1, "bob"), o1(2, "janet") };
                            var second = new[] { o2(1, "bobby"), o2(2, "janet") };
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                                {
                                    Expect(first.AsObjects()).To.Be.Intersection.Equivalent.To(second);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>());
                            // Assert
                        }
                    }
                }

                [TestFixture]
                public class Equal
                {
                    [Test]
                    public void PositiveExpectation_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Intersection.Equal(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_WhenCollectionsMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).Not.To.Intersection.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenCollectionsMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Not.Intersection.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_AltGrammer_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Be.Intersection.Equal.To(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).Not.To.Be.Intersection.Equal.To(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsDontMatchInFirstRecord_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] { o1(1, "bob"), o1(2, "janet"), o1(3, "paddy") };
                        var second = new[] { o2(1, "bobby"), o2(2, "janet"), o2(3, "paddy") };
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects()).To.Intersection.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsDontMatchInLastRecord_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] { o1(1, "bob"), o1(2, "janet"), o1(3, "paddy") };
                        var second = new[] { o2(1, "bob"), o2(2, "janet"), o2(3, "mcgee") };
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expect(first.AsObjects).To.Intersection.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class WithCustomEqualityComparer
            {
                [Test]
                public void Positive_WhenMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).To.Be.Equivalent.To(right, new FirstLetterComparer());
                    }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void NegativeNegated_WhenNoMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).Not.To.Be.Equivalent.To(right, new FirstLetterComparer());
                    }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void NegativeNegated_AltGrammar_WhenNoMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).To.Not.Be.Equivalent.To(right, new FirstLetterComparer());
                    }, Throws.Nothing);

                    // Assert
                }
            }

            [TestFixture]
            public class WithCustomEqualityComparisonFunc
            {
                private static bool FirstLetterComparer(string x, string y)
                {
                    if (x == null &&
                        y == null)
                        return true;
                    if (x == null ||
                        y == null)
                        return false;
                    if (x.Length == 0 &&
                        y.Length == 0)
                        return true;
                    if (x.Length == 0 ||
                        y.Length == 0)
                        return false;
                    return x[0] == y[0];
                }

                [Test]
                public void Positive_WhenMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"B{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).To.Be.Equivalent.To(right, FirstLetterComparer);
                    }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void NegativeNegated_WhenNoMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).Not.To.Be.Equivalent.To(right, FirstLetterComparer);
                    }, Throws.Nothing);

                    // Assert
                }

                [Test]
                public void NegativeNegated_AltGrammar_WhenNoMatches_ShouldNotThrow()
                {
                    // Arrange
                    var left = new[] {$"A{GetRandomString()}", $"B{GetRandomString()}"};
                    var right = new[] {$"C{GetRandomString()}", $"A{GetRandomString()}"};

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                    {
                        Expect(left).To.Not.Be.Equivalent.To(right, FirstLetterComparer);
                    }, Throws.Nothing);

                    // Assert
                }
            }
        }
    }
}