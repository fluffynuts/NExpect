using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NUnit.Framework;

namespace NExpect.Tests.DanglingPrepositions;

[TestFixture]
public class OnAndIn
{
    [Test]
    public void ShouldBeAbleToDangle()
    {
        // Arrange
        // Act
        Expect(new object())
            .To.Be.On.Top()
            .On.A.Shelf()
            .In.A.Box()
            .And
            .In.An.Elephant()
            .And
            .On.A.Shelf();
        // Assert
    }
}

public static class WoodExtensions
{
    public static IMore<T> Box<T>(this IA<T> a)
    {
        return a.AlwaysPass();
    }

    public static IMore<T> Top<T>(this IOn<T> on)
    {
        return on.AlwaysPass();
    }

    public static IMore<T> Shelf<T>(this IA<T> a)
    {
        return a.AlwaysPass();
    }

    public static IMore<T> Elephant<T>(this IAn<T> an)
    {
        return an.AlwaysPass();
    }


    private static IMore<T> AlwaysPass<T>(this ICanAddMatcher<T> canAddMatcher)
    {
        return canAddMatcher.AddMatcher(_ => new EnforcedMatcherResult(true, "yay"));
    }
}