using System;
using System.Threading;
using NExpect.Exceptions;
using NUnit.Framework;
using static NExpect.Expectations;

namespace NExpect.Tests.Runtime;

public class TestRuntimeMatchers
{
    [Test]
    public void ShouldBeAbleToAssertActionRunTime()
    {
        // Arrange
        // Act
        Assert.That(() =>
        {
            Expect(() => Thread.Sleep(500))
                .RunTime
                .To.Be.Greater.Than(TimeSpan.FromMilliseconds(100))
                .And
                .To.Be.Less.Than(TimeSpan.FromMilliseconds(900));
        }, Throws.Nothing);
        Assert.That(() =>
        {
            Expect(() => Thread.Sleep(10))
                .RunTime
                .To.Be.Greater.Than(TimeSpan.FromMilliseconds(100))
                .And
                .To.Be.Less.Than(TimeSpan.FromMilliseconds(900));
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        Assert.That(() =>
        {
            Expect(() => Thread.Sleep(1000))
                .RunTime
                .To.Be.Greater.Than(TimeSpan.FromMilliseconds(100))
                .And
                .To.Be.Less.Than(TimeSpan.FromMilliseconds(900));
        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
        // Assert
    }
}