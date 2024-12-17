using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using NExpect.Exceptions;
using PeanutButter.Utils;

namespace NExpect.Matchers.AspNet.Tests;

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
            Assert.That(
                () =>
                {
                    Expect(qs)
                        .To.Contain.Only(2).Items();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
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
            Assert.That(
                () =>
                {
                    Expect(qs)
                        .To.Contain.Only(1).Item();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
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
        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Be.Equivalent.To(expected);
            },
            Throws.Nothing
        );
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
        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Contain.Key("a")
                    .With.Value("b");
            },
            Throws.Nothing
        );

        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Contain.Key("a")
                    .With.Value("2");
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );

        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Contain.Key("1")
                    .With.Value("2");
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
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
        Assert.That(
            () =>
            {
                Expect(qs1)
                    .To.Equal(qs2);
                Expect(qs1)
                    .Not.To.Equal(qs3);
                Expect(qs1)
                    .To.Not.Equal(qs3);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(qs1)
                    .To.Equal(qs3);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(qs1)
                    .Not.To.Equal(qs2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(qs1)
                    .To.Not.Equal(qs2);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }

    [Test]
    public void ShouldDealWithEmptyQueryString()
    {
        // Arrange
        var qs = new QueryString();
        // Act
        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Be.Empty();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(qs)
                    .Not.To.Be.Empty();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(qs)
                    .To.Equal(
                        new Dictionary<string, string>()
                        {
                            ["a"] = "b"
                        }
                    );
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }

    [Test]
    public void ShouldMatchTheSameParameterRepeated()
    {
        // Arrange
        var query = new QueryString("?a=1&a=2");
        var expected = new Dictionary<string, string>()
        {
            ["a"] = "1,2"
        };
        // Act
        Assert.That(() =>
        {
            Expect(query)
                .To.Be.Equivalent.To(expected);
        }, Throws.Nothing);
        // Assert
    }
}
internal static class QueryStringExtensions
{
    /// <summary>
    /// Provides a dictionary snapshot of a QueryString
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static IDictionary<string, string> AsDictionary(
        this QueryString queryString
    )
    {
        if (queryString.Value is null)
        {
            return new Dictionary<string, string>();
        }

        var str = queryString.Value;
        var start = str.StartsWith("?")
            ? 1
            : 0;
        var result = new Dictionary<string, string>();
        var pairs = str.Substring(start)
            .Split('&')
            .Select(
                p =>
                {
                    var subs = p.Split('=');
                    return new KeyValuePair<string, string>(
                        WebUtility.UrlDecode(subs.First()),
                        WebUtility.UrlDecode(subs.Skip(1).JoinWith("="))
                    );
                }
            );
        foreach (var item in pairs)
        {
            if (result.TryGetValue(item.Key, out var existing))
            {
                result[item.Key] = $"{existing},{item.Value}";
                continue;
            }
            result[item.Key] = item.Value;
        }
        return result;
    }
}