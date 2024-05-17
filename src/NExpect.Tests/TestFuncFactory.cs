using System;
using NUnit.Framework;
using NExpect.Helpers;

namespace NExpect.Tests;

[TestFixture]
public class TestFuncFactory
{
    [Test]
    public void ShouldMemoize()
    {
        // Arrange
        var calls = 0;
        Func<string> generator = () =>
        {
            calls++;
            return GetRandomString();
        };
        var sut = FuncFactory.Memoize(generator);
        // Act
        var result1 = sut();
        var result2 = sut();
        // Assert
        Expect(calls)
            .To.Equal(1);
        Expect(result1)
            .To.Equal(result2);
    }
}
