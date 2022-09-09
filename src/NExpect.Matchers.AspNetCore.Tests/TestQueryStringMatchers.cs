using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.AspNetCoreExpectations;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestQueryStringMatchers
    {
        [TestFixture]
        public class CountingItems
        {
            [Test]
            public void ShouldThrowWhenTooFewParameters()
            {
                // Arrange
                var qs = new QueryString();
                qs = qs.Add("foo", "bar");
                // Act
                Assert.That(() =>
                {
                    Expect(qs)
                        .To.Contain.Only(2).Items();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }

            [Test]
            public void ShouldThrowWhenTooManyParameters()
            {
                // Arrange
                var qs = new QueryString();
                qs = qs.Add("foo", "bar");
                qs = qs.Add("quux", "wibble");
                // Act
                Assert.That(() =>
                {
                    Expect(qs)
                        .To.Contain.Only(1).Item();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [Test]
        public void ShouldBeAbleToTestEquivalence()
        {
            // Arrange
            var qs = new QueryString();
            qs = qs.Add("a", "b");
            qs = qs.Add("c", "d");
            var expected = new Dictionary<string, string>()
            {
                ["c"] = "d",
                ["a"] = "b"
            };

            // Act
            Assert.That(() =>
            {
                Expect(qs)
                    .To.Be.Equivalent.To(expected);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldBeAbleToTestByKeyAndValue()
        {
            // Arrange
            var qs = new QueryString();
            qs = qs.Add("a", "b");
            qs = qs.Add("c", "d");

            // Act
            Assert.That(() =>
            {
                Expect(qs)
                    .To.Contain.Key("a")
                    .With.Value("b");
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(qs)
                    .To.Contain.Key("a")
                    .With.Value("2");
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());

            Assert.That(() =>
            {
                Expect(qs)
                    .To.Contain.Key("1")
                    .With.Value("2");
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToCompareWithAnotherQueryString()
        {
            // Arrange
            var qs1 = new QueryString().Add("a", "b");
            var qs2 = new QueryString().Add("a", "b");
            var qs3 = new QueryString().Add("1", "2");
            // Act
            Assert.That(() =>
            {
                Expect(qs1)
                    .To.Equal(qs2);
                Expect(qs1)
                    .Not.To.Equal(qs3);
                Expect(qs1)
                    .To.Not.Equal(qs3);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(qs1)
                    .To.Equal(qs3);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(qs1)
                    .Not.To.Equal(qs2);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(qs1)
                    .To.Not.Equal(qs2);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldDealWithEmptyQueryString()
        {
            // Arrange
            var qs = new QueryString();
            // Act
            Assert.That(() =>
            {
                Expect(qs)
                    .To.Be.Empty();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(qs)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}