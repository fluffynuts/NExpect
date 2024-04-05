using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestSessionItemMatching
{
    [Test]
    public void ShouldBeAbleToTreatSessionLikeADictionary()
    {
        // Arrange
        var k1 = GetRandomString();
        var k2 = GetAnother(k1);
        var v1 = GetRandomString();
        var v2 = new
        {
            id = GetRandomInt()
        };
        var ctx = HttpContextBuilder.Create()
            .WithSessionItem(k1, v1)
            .WithSessionItem(k2, v2)
            .Build();
        // Act
        Assert.That(() =>
        {
            Expect(ctx.Session)
                .To.Contain.Key(k1)
                .With.Value(v1);
        }, Throws.Nothing);
        // Assert
    }
}