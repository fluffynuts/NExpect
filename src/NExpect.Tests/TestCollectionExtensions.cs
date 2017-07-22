using System.Linq;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;
using static NExpect.Expectations;

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
                        search, other1, other2
                    }.Randomize();

                    // Pre-Assert
                    // Act
                    Assert.That(() => { Expect(collection).To.Contain.Exactly(1).Equal.To(search); }, Throws.Nothing);

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
                        search, other1, other2, search
                    }.Randomize();

                    // Pre-Assert
                    // Act
                    Assert.That(() => { Expect(collection).To.Contain.Exactly(2).Equal.To(search); }, Throws.Nothing);

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
                        search, other1, other2, search
                    }.Randomize();

                    // Pre-Assert
                    // Act
                    Assert.That(() => { Expect(collection).To.Contain.Exactly(1).Equal.To(search); }, Throws.Exception
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
                        other1, other2
                    }.Randomize();

                    // Pre-Assert
                    // Act
                    Assert.That(() => { Expect(collection).To.Contain.Exactly(1).Equal.To(search); }, Throws.Exception
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
                        search, other1, other2
                    }.Randomize();

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search); }, Throws
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
                        other1, other2
                    }.Randomize();

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(collection).Not.To.Contain.Exactly(1).Equal.To(search); },
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
                        search, other1, other2
                    }.Randomize();

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search); }, Throws
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
                        other1, other2
                    }.Randomize();

                    // Pre-Assert

                    // Act
                    Assert.That(() => { Expect(collection).To.Not.Contain.Exactly(1).Equal.To(search); },
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
                    Assert.That(() => { Expect(collection).To.Contain.At.Least(1).Equal.To(search); }, Throws.Exception
                        .InstanceOf<AssertionException>()
                        .With.Message.Contains("at least 1"));
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
                    Assert.That(() => { Expect(collection).To.Contain.At.Least(1).Equal.To(search); }, Throws.Nothing);
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
                    Assert.That(() => { Expect(collection).To.Contain.At.Least(1).Equal.To(search); }, Throws.Nothing);
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
                        Expect(collection).To.Contain
                            .At.Least(1)
                            .Matched.By(s => s == search); 
                     }, Throws.Nothing);

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
                        Expect(collection).To.Contain
                            .At.Least(1)
                            .Matched.By(s => s == search); 
                     }, Throws.Nothing);

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
                        Expect(collection).To.Contain
                            .At.Least(2)
                            .Matched.By(s => s == search); 
                     }, Throws.Exception.InstanceOf<AssertionException>());

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
                        Expect(collection).Not.To.Contain
                            .At.Least(1)
                            .Matched.By(s => s == search); 
                    }, Throws.Exception.InstanceOf<AssertionException>());

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
                        Expect(collection).To.Not.Contain
                            .At.Least(1)
                            .Matched.By(s => s == search); 
                    }, Throws.Exception.InstanceOf<AssertionException>());

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
                Assert.That(() => { Expect(collection).To.Contain.At.Most(1).Equal.To(search); }, Throws.Nothing);
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
                Assert.That(() => { Expect(collection).To.Contain.At.Most(1).Equal.To(search); }, Throws.Nothing);
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
                Assert.That(() => { Expect(collection).To.Contain.At.Most(1).Equal.To(search); }, Throws.Exception
                    .InstanceOf<AssertionException>()
                    .With.Message.Contains("at most 1"));
                // Assert
            }
        }
    }
}