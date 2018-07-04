using System;
using NUnit.Framework;
using NExpect.Exceptions;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class DeepEqualityTesting
    {
        [TestFixture]
        public class Expect_Collection_To
        {
            [TestFixture]
            public class Deep
            {
                [TestFixture]
                public class Equal
                {
                    public class IdentifierAndName1
                    {
                        public int Id { get; }
                        public string Name { get; }

                        public IdentifierAndName1(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    public class IdentifierAndName2
                    {
                        public int Id { get; }
                        public string Name { get; }

                        public IdentifierAndName2(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    private static IdentifierAndName1 o1(int id, string name)
                    {
                        return new IdentifierAndName1(id, name);
                    }

                    private static IdentifierAndName2 o2(int id, string name)
                    {
                        return new IdentifierAndName2(id, name);
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bob"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).As.Objects.To.Deep.Equal(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_WhenCollectionsMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o1(1, "bob"), o1(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
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
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o1(1, "bob"), o1(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
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
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o1(1, "bob"), o1(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).To.Be.Deep.Equal.To(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void WithCustomComparer()
                    {
                        // Arrange
                        var first = new[] {new {Date = DateTime.Now}};
                        var second = new[] {new {Date = DateTime.Now.AddSeconds(-1)}};
                        // Pre-assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).To.Be.Deep.Equal.To(
                                    second,
                                    new DriftingDateTimeEqualityComparer());
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenCollectionsMatch_ShouldNotThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o1(1, "bob"), o1(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
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
                        var first = new[] {o1(1, "bob"), o1(2, "janet"), o1(3, "paddy")};
                        var second = new[] {o1(1, "bobby"), o1(2, "janet"), o1(3, "paddy")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
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
                        var first = new[] {o1(1, "bob"), o1(2, "janet"), o1(3, "paddy")};
                        var second = new[] {o1(1, "bob"), o1(2, "janet"), o1(3, "mcgee")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).To.Deep.Equal(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void ShouldNotTreatDateTimesWithDifferentKindsAsEqual()
                    {
                        // Arrange
                        var src = GetRandomDate();
                        var local = new
                        {
                            Date = new DateTime(
                                src.Year,
                                src.Month,
                                src.Day,
                                src.Hour,
                                src.Minute,
                                src.Second,
                                src.Millisecond,
                                DateTimeKind.Local)
                        };
                        var utc = new
                        {
                            Date = new DateTime(
                                src.Year,
                                src.Month,
                                src.Day,
                                src.Hour,
                                src.Minute,
                                src.Second,
                                src.Millisecond,
                                DateTimeKind.Utc)
                        };
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(local).To.Deep.Equal(utc);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [TestFixture]
                    public class UsingCustomEqualityComparers
                    {
                        [Test]
                        public void AllowingDateTimeDrift()
                        {
                            // Arrange
                            var left = new {Date = DateTime.Now};
                            var right = new {Date = left.Date.AddSeconds(1)};

                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(left).To.Deep.Equal(
                                        right,
                                        new DriftingDateTimeEqualityComparer());
                                },
                                Throws.Nothing);
                            // Assert
                        }                        
                        
                        [Test]
                        public void MessageWhenCustomEqualityComparerSaysNotEqual()
                        {
                            // Arrange
                            var left = new {Date = DateTime.Now};
                            var right = new {Date = left.Date.AddSeconds(1)};

                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(left).To.Deep.Equal(
                                        right,
                                        new NeverEqualEqualityComparer());
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contain(nameof(NeverEqualEqualityComparer)));
                            // Assert
                        }
                    }
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
                        var first = new[] {o1(1, "moo"), o1(2, "cow")};
                        var second = new[] {o2(2, "cow"), o2(1, "moo")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).As.Objects.To.Be.Intersection.Equivalent.To(second);
                            },
                            Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_WhenHaveEquivalence_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "moo"), o1(2, "cow")};
                        var second = new[] {o2(2, "cow"), o2(1, "moo")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).As.Objects.Not.To.Be.Intersection.Equivalent.To(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void NegativeExpectation_AltGrammar_WhenHaveEquivalence_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "moo"), o1(2, "cow")};
                        var second = new[] {o2(2, "cow"), o2(1, "moo")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).As.Objects.To.Not.Be.Intersection.Equivalent.To(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveExpectation_WhenCollectionsDontMatch_ShouldThrow()
                    {
                        // Arrange
                        var first = new[] {o1(1, "bob"), o1(2, "janet")};
                        var second = new[] {o2(1, "bobby"), o2(2, "janet")};
                        // Pre-Assert
                        // Act
                        Assert.That(
                            () =>
                            {
                                Expect(first).As.Objects.To.Be.Intersection.Equivalent.To(second);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [TestFixture]
                    public class UsingCustomEqualityComparers
                    {
                        [Test]
                        public void AllowingDateTimeDrift()
                        {
                            // Arrange
                            var left = new {Date = DateTime.Now}.AsArray();
                            var right = new {Date = left[0].Date.AddSeconds(1)}.AsArray();

                            // Pre-assert
                            // Act
                            Assert.That(
                                () =>
                                {
                                    Expect(left).As.Objects.To.Be.Intersection.Equivalent.To(
                                        right,
                                        new DriftingDateTimeEqualityComparer());
                                },
                                Throws.Nothing);
                            // Assert
                        }
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
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.To.Intersection.Equal(second);
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
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.Not.To.Intersection.Equal(second);
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
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.To.Not.Intersection.Equal(second);
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
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.To.Be.Intersection.Equal.To(second);
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
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.Not.To.Be.Intersection.Equal.To(second);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void PositiveExpectation_WhenCollectionsDontMatchInFirstRecord_ShouldThrow()
                {
                    // Arrange
                    var first = new[] {o1(1, "bob"), o1(2, "janet"), o1(3, "paddy")};
                    var second = new[] {o2(1, "bobby"), o2(2, "janet"), o2(3, "paddy")};
                    // Pre-Assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.To.Intersection.Equal(second);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void PositiveExpectation_WhenCollectionsDontMatchInLastRecord_ShouldThrow()
                {
                    // Arrange
                    var first = new[] {o1(1, "bob"), o1(2, "janet"), o1(3, "paddy")};
                    var second = new[] {o2(1, "bob"), o2(2, "janet"), o2(3, "mcgee")};
                    // Pre-Assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(first).As.Objects.To.Intersection.Equal(second);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());
                    // Assert
                }

                [Test]
                public void AllowingDateTimeDrift()
                {
                    // Arrange
                    var left = new {Date = DateTime.Now}.AsArray();
                    var right = new {Date = left[0].Date.AddSeconds(1)}.AsArray();

                    // Pre-assert
                    // Act
                    Assert.That(
                        () =>
                        {
                            Expect(left).As.Objects.To.Intersection.Equal(
                                right,
                                new DriftingDateTimeEqualityComparer());
                            Expect(left).As.Objects.To.Be.Intersection.Equal.To(
                                right,
                                new DriftingDateTimeEqualityComparer());
                        },
                        Throws.Nothing);
                    // Assert
                }
            }
        }

        [TestFixture]
        public class Expect_Collection_To_Contain
        {
            [TestFixture]
            public class Exactly_N
            {
                [TestFixture]
                public class Intersection
                {
                    [TestFixture]
                    public class Equal
                    {
                        [TestFixture]
                        public class To
                        {
                            public class Item1
                            {
                                public int Id { get; set; }
                                public string Name { get; set; }
                                public DateTime DateTime => DateTime.Now;
                            }

                            public class Item2
                            {
                                public int Id { get; set; }
                                public string Name { get; set; }
                            }

                            [Test]
                            public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new Item1
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new Item1
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                // FIXME: this is lazy -- should have a separate test fixture
                                //  for At.Least and At.Most; I'm just in a bit of a hurry and
                                //  need to prove the syntax
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Nothing);
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src.And(new Item1
                                            {
                                                Id = 1,
                                                Name = "moo"
                                            }))
                                            .To.Contain.At.Least(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Nothing);
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.At.Most(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 2,
                                                    Name = "Cake"
                                                }
                                            );
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_WhenShouldFail_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new Item1
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new Item1
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .Not.To.Contain.Exactly(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void CountMatchWithCustomComparer()
                            {
                                // Arrange
                                var left = new[] {new {Date = DateTime.Now}};
                                var search = new {Date = DateTime.Now.AddSeconds(-1)};
                                // Pre-assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(left).To.Contain.Only(1)
                                            .Intersection.Equal.To(
                                                search,
                                                new DriftingDateTimeEqualityComparer()
                                            );
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void GivenInvalidComparers_ShouldThrowArgumentException()
                            {
                                // Arrange
                                var left = new[] {new {Date = DateTime.Now}};
                                var search = new {Date = DateTime.Now.AddSeconds(-1)};
                                // Pre-assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(left).To.Contain.Only(1)
                                            .Intersection.Equal.To(
                                                search,
                                                new NotAnEqualityComparer()
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.InnerException.InstanceOf<ArgumentException>()
                                        .With.Message.Contains("must implement IEqualityComparer"));
                                // Assert
                            }

                            public class NotAnEqualityComparer : IDisposable
                            {
                                public void Dispose()
                                {
                                }
                            }

                            [Test]
                            public void NegativeAssertion_AltGrammer_WhenShouldFail_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new Item1
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new Item1
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Not.Contain.Exactly(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void PositiveAssertion_WhenShouldFailForObjectMisMatch_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new Item1
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new Item1
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "bar"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void PositiveAssertion_WhenShouldFailForCountMismatch_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new Item1
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new Item1
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.At.Least(2)
                                            .Intersection.Equal.To(
                                                new Item2
                                                {
                                                    Id = 1,
                                                    Name = "Cake"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }
                        }
                    }
                }

                [TestFixture]
                public class Deep
                {
                    [TestFixture]
                    public class Equal
                    {
                        [TestFixture]
                        public class To
                        {
                            [Test]
                            public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Deep.Equal.To(
                                                new
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_WhenShouldFail_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .Not.To.Contain.Exactly(1)
                                            .Deep.Equal.To(
                                                new
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeAssertion_AltGrammer_WhenShouldFail_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Not.Contain.Exactly(1)
                                            .Deep.Equal.To(
                                                new
                                                {
                                                    Id = 1,
                                                    Name = "moo"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void PositiveAssertion_WhenShouldFailForObjectMisMatch_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Deep.Equal.To(
                                                new
                                                {
                                                    Id = 1,
                                                    Name = "bar"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void PositiveAssertion_WhenShouldFailForCountMismatch_ShouldThrow()
                            {
                                // Arrange
                                var src = new[]
                                {
                                    new
                                    {
                                        Id = 1,
                                        Name = "moo"
                                    },
                                    new
                                    {
                                        Id = 2,
                                        Name = "Cake"
                                    }
                                };
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(2)
                                            .Deep.Equal.To(
                                                new
                                                {
                                                    Id = 1,
                                                    Name = "Cake"
                                                }
                                            );
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void UsingCustomEqualityComparer_ForDateTime()
                            {
                                // Arrange
                                var src = new[] {new {Date = DateTime.Now}};
                                var search = new {Date = DateTime.Now.AddSeconds(-1)};
                                // Pre-assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Deep.Equal.To(search, new DriftingDateTimeEqualityComparer());
                                    },
                                    Throws.Nothing);                                
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Intersection.Equal.To(search, new DriftingDateTimeEqualityComparer());
                                    },
                                    Throws.Nothing);
                                // Assert
                            }
                            
                            [Test]
                            public void UsingCustomEqualityComparer_ForDouble()
                            {
                                // Arrange
                                var src = new[] {new {Date = DateTime.Now}};
                                var search = new {Date = DateTime.Now.AddSeconds(-1)};
                                // Pre-assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Deep.Equal.To(search, new DriftingDateTimeEqualityComparer());
                                    },
                                    Throws.Nothing);                                
                                Assert.That(
                                    () =>
                                    {
                                        Expect(src)
                                            .To.Contain.Exactly(1)
                                            .Intersection.Equal.To(search, new DriftingDateTimeEqualityComparer());
                                    },
                                    Throws.Nothing);
                                // Assert
                            }
                            
                        }
                    }

                    [TestFixture]
                    public class Equivalent
                    {
                        [TestFixture]
                        public class To
                        {
                            public class IdentifierAndName1
                            {
                                public int Id { get; }
                                public string Name { get; }

                                public IdentifierAndName1(int id, string name)
                                {
                                    Id = id;
                                    Name = name;
                                }
                            }

                            public class IdentifierAndName2
                            {
                                public int Id { get; }
                                public string Name { get; }

                                public IdentifierAndName2(int id, string name)
                                {
                                    Id = id;
                                    Name = name;
                                }
                            }

                            private static IdentifierAndName1 o1(int id, string name)
                            {
                                return new IdentifierAndName1(id, name);
                            }

                            [Test]
                            public void PositiveExpectation_WhenHaveEquivalence_ShouldNotThrow()
                            {
                                // Arrange
                                var first = new[] {o1(1, "moo"), o1(2, "cow")};
                                var second = new[] {o1(2, "cow"), o1(1, "moo")};
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(first).To.Be.Deep.Equivalent.To(second);
                                    },
                                    Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_WhenHaveEquivalence_ShouldThrow()
                            {
                                // Arrange
                                var first = new[] {o1(1, "moo"), o1(2, "cow")};
                                var second = new[] {o1(2, "cow"), o1(1, "moo")};
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(first).Not.To.Be.Deep.Equivalent.To(second);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void NegativeExpectation_AltGrammar_WhenHaveEquivalence_ShouldThrow()
                            {
                                // Arrange
                                var first = new[] {o1(1, "moo"), o1(2, "cow")};
                                var second = new[] {o1(2, "cow"), o1(1, "moo")};
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(first).To.Not.Be.Deep.Equivalent.To(second);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void PositiveExpectation_WhenCollectionsDontMatch_ShouldThrow()
                            {
                                // Arrange
                                var first = new[] {o1(1, "bob"), o1(2, "janet")};
                                var second = new[] {o1(1, "bobby"), o1(2, "janet")};
                                // Pre-Assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(first).To.Be.Deep.Equivalent.To(second);
                                    },
                                    Throws.Exception.InstanceOf<UnmetExpectationException>());
                                // Assert
                            }

                            [Test]
                            public void WithCustomEqualityComparer()
                            {
                                // Arrange
                                var first = new[] {new {Date = DateTime.Now}};
                                var second = new[] {new {Date = DateTime.Now.AddSeconds(-1)}};
                                // Pre-assert
                                // Act
                                Assert.That(
                                    () =>
                                    {
                                        Expect(first).To.Be.Deep.Equivalent.To(
                                            second,
                                            new DriftingDateTimeEqualityComparer());
                                    },
                                    Throws.Nothing);
                                // Assert
                            }
                        }
                    }
                }
            }
        }
    }
}