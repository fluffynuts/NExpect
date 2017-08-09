using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;
using static PeanutButter.Utils.PyLike;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestCollectionExtensions
    {
        [TestFixture]
        public class Exactly
        {
            [TestFixture]
            public class EqualTo
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
                    Assert.That(() => { Expect(collection).To.Contain(search); },
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
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
                            .InstanceOf<AssertionException>()
                            .With.Message.Contains("Expected to find all matching"));
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
            public class ThatMatches
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
                        Throws.Exception.InstanceOf<AssertionException>());

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
                        Throws.Exception.InstanceOf<AssertionException>());

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
                        Throws.Exception.InstanceOf<AssertionException>());

                    // Assert
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
                        .InstanceOf<AssertionException>()
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
                    Throws.Exception.InstanceOf<AssertionException>());
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
                    Throws.Exception.InstanceOf<AssertionException>());

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
                    Throws.Exception.InstanceOf<AssertionException>());

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
                    Throws.Exception.InstanceOf<AssertionException>());

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
                }, Throws.Nothing);

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
                }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.EqualTo($"Expected {collection} not to be an empty collection"));

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
                }, Throws.Exception.InstanceOf<AssertionException>()
                    .With.Message.EqualTo($"Expected {collection} to be an empty collection"));

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
                }, Throws.Nothing);

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
                }, Throws.Nothing);

                // Assert
            }
        }
    }
}