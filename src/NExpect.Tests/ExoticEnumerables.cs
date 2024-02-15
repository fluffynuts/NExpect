using System.Collections.Concurrent;
using NUnit.Framework;
using NExpect.Exceptions;

namespace NExpect.Tests;

[TestFixture]
public class ExoticEnumerables
{
    [TestFixture]
    public class ConcurrentQueues
    {
        [Test]
        public void ShouldBeAbleToAssertEmptiness()
        {
            // Arrange
            var q = new ConcurrentQueue<string>();
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );

            q.Enqueue(GetRandomString());
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertWithMatchers()
        {
            // Arrange
            var q = new ConcurrentQueue<string>();
            q.Enqueue("foo");
            q.Enqueue("bar");
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Equal.To("foo");

                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Matched.By(s => s == "foo");
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class ConcurrentBags
    {
        [Test]
        public void ShouldBeAbleToAssertEmptiness()
        {
            // Arrange
            var q = new ConcurrentBag<string>();
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );

            q.Add(GetRandomString());
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertWithMatchers()
        {
            // Arrange
            var q = new ConcurrentBag<string>();
            q.Add("foo");
            q.Add("bar");
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Equal.To("foo");

                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Matched.By(s => s == "foo");
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class ConcurrentStacks
    {
        [Test]
        public void ShouldBeAbleToAssertEmptiness()
        {
            // Arrange
            var q = new ConcurrentStack<string>();
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );

            q.Push(GetRandomString());
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertWithMatchers()
        {
            // Arrange
            var q = new ConcurrentStack<string>();
            q.Push("foo");
            q.Push("bar");
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Equal.To("foo");

                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Matched.By(s => s == "foo");
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Matched.By(s => s == "foo");
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Equal.To("Foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class ConcurrentDictionaries
    {
        [Test]
        public void ShouldBeAbleToAssertEmptiness()
        {
            // Arrange
            var q = new ConcurrentDictionary<string, string>();
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Nothing
            );

            var added = q.TryAdd(GetRandomString(), GetRandomString());
            Expect(added)
                .To.Be.True();
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Be.Empty();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertWithMatchers()
        {
            // Arrange
            var q = new ConcurrentDictionary<string, string>();
            q.TryAdd("foo", "foo");
            q.TryAdd("bar", "bar");
            // Act
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain
                        .Key("foo")
                        .With.Value("foo");
                    Expect(q)
                        .To.Contain
                        .Key("foo")
                        .With.Value("foo");
                    Expect(q)
                        .To.Contain
                        .Key("foo")
                        .With.Value("foo");

                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Matched.By(s => s is { Key: "foo", Value: "foo" });
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Matched.By(s => s is { Key: "foo", Value: "foo" });
                    Expect(q)
                        .To.Contain.At.Most(1)
                        .Matched.By(s => s is { Key: "foo", Value: "bar" });
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.Exactly(1)
                        .Matched.By(o => o is { Key: "Foo" });
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(q)
                        .To.Contain.At.Least(1)
                        .Matched.By(o => o is { Key: "Foo" });
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }
}