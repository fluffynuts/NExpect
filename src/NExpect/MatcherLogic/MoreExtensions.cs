using System.Collections.Generic;
using NExpect.Implementations;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic;

/// <summary>
/// Allows user-specified chaining, using .And on the result, by
/// invoking this extension method on any ICanAddMatcher&lt;T&gt;
/// </summary>
public static class MoreExtensions
{
    /// <summary>
    /// Creates an object with .And on it which you can use to chain on more
    /// expectations, eg Expect(1).To.Be.SomeCustomMatcher().And.SomeOtherMatcher()
    /// </summary>
    /// <param name="continuation"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> More<T>(
        this ICanAddMatcher<T> continuation
    )
    {
        return ContinuationFactory.Create<T, More<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<T>
        );
    }

    /// <summary>
    /// Creates an object with .And on it which you can use to chain on more
    /// collection-based expectations,
    /// eg Expect(1).To.Be.SomeCustomMatcher().And.SomeOtherMatcher()
    /// </summary>
    /// <param name="continuation"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionMore<T> More<T>(
        this ICanAddCollectionMatcher<T> continuation
    )
    {
        return ContinuationFactory.Create<IEnumerable<T>, CollectionMore<T>>(
            continuation.GetActual,
            continuation as IExpectationContext<IEnumerable<T>>
        );
    }

    /// <summary>
    /// Creates an object with .And on it which you can use to chain on more
    /// expectations, eg Expect(1).To.Be.SomeCustomMatcher().And.SomeOtherMatcher()
    /// </summary>
    /// <param name="continuation"></param>
    /// <returns></returns>
    public static IStringMore More(
        this ICanAddMatcher<string> continuation
    )
    {
        var result = ContinuationFactory.Create<string, StringMore>(
            continuation.GetActual,
            continuation as IExpectationContext<string>
        );
        Assertions.Forget(result);
        return result;
    }
}