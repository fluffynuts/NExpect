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

                    [TestFixture]
                    public class ShortContain
                    {
                        [Test]
                        public void WhenDoesContain_ShouldNotThrow()
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
                        public void Negated_WhenDoesContain_ShouldThrow()
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
                                    Expect(collection).Not.To.Contain(search);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>());

                            // Assert
                        }

                        [Test]
                        public void
                            Negated_AltGrammar_WhenDoesContain_ShouldThrow()
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
                                    Expect(collection).To.Not.Contain(search);
                                },
                                Throws.Exception.InstanceOf<UnmetExpectationException>());

                            // Assert
                        }
                    }

                    [Test]
                    public void WhenSeeking2AndDoesContain2_ShouldNotThrow()
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
                    public void WhenSeeking1AndDoesContain2_ShouldThrow()
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
                                .With.Message
                                .Contains($"to find exactly 1 occurrence of \"{search}\" but found 2"));

                        // Assert
                    }

                    [Test]
                    public void WhenDoesNoContain_ShouldThrow()
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
                    public void Negated_WhenDoesContain_ShouldThrow()
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
                                .With.Message
                                .Contains($"not to find exactly 1 occurrence of \"{search}\" but found 1"));

                        // Assert
                    }

                    [Test]
                    public void Negated_WhenDoesNotContain_ShouldNotThrow()
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
                    public void Negated_Alt_WhenDoesContain_ShouldThrow()
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
                                .With.Message
                                .Contains($"not to find exactly 1 occurrence of \"{search}\" but found 1"));

                        // Assert
                    }

                    [Test]
                    public void Negated_Alt_WhenDoesNotContain_ShouldNotThrow()
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
        }
    }
}