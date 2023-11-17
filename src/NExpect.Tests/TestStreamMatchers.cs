using System.IO;
using System.Text;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;
using static NExpect.Expectations;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Tests;

[TestFixture]
public class TestStreamMatchers
{
    [Test]
    public void ShouldBeAbleToTestForEmpty()
    {
        // Arrange
        var empty = new MemoryStream();
        var notEmpty = new MemoryStream(GetRandomBytes(100));
        // Act
        Assert.That(
            () =>
            {
                Expect(empty)
                    .To.Be.Empty();
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(empty)
                    .Not.To.Be.Empty();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(notEmpty)
                    .To.Be.Empty();
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(notEmpty)
                    .Not.To.Be.Empty();
            },
            Throws.Nothing
        );
        // Assert
    }

    [Test]
    public void ShouldBeAbleToTestIfContainsString()
    {
        // Arrange
        var strings = GetRandomArray<string>(5, 8);
        var seek = GetRandomFrom(strings);
        var text = strings.JoinWith(" ");
        var miss = GetRandom(
            s => !text.Contains(s),
            () => GetRandomString(5, 10)
        );

        var stream = new MemoryStream(
            Encoding.UTF8.GetBytes(
                text
            )
        );

        // Act
        Assert.That(
            () =>
            {
                Expect(stream)
                    .To.Contain(seek);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(stream)
                    .Not.To.Contain(miss);
            },
            Throws.Nothing
        );
        Assert.That(
            () =>
            {
                Expect(stream)
                    .Not.To.Contain(seek);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
        Assert.That(
            () =>
            {
                Expect(stream)
                    .To.Contain(miss);
            },
            Throws.Exception.InstanceOf<UnmetExpectationException>()
        );
    }
}