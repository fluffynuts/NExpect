﻿using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Fakes;
using static NExpect.AspNetCoreExpectations;
using FakeQueryCollection = NExpect.Matchers.AspNet.Tests.Implementations.FakeQueryCollection;

// leave this in: without it, the need to cast as IQueryCollection is dropped
// ReSharper disable once RedundantUsingDirective

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestQueryCollectionMatchers
{
    [Test]
    public void ShouldBeAbleToAssertAgainstQueryLikeADictionary()
    {
        // Arrange
        var query = new FakeQueryCollection
        {
            ["foo"] = "bar"
        } as IQueryCollection;
        // Act
        Assert.That(
            () =>
            {
                Expect(query)
                    .To.Contain.Key("foo")
                    .With.Value("bar");
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(query)
                    .To.Contain.Key("foo1")
                    .With.Value("bar");
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(query)
                    .To.Contain.Key("foo")
                    .With.Value("bar1");
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        // Assert
    }
}