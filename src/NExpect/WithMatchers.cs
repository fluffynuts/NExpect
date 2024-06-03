using System;
using NExpect.Implementations;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect;

/// <summary>
/// Provides some default matchers to use on .With
/// </summary>
public static class WithMatchers
{
    /// <summary>
    /// Allows performing generic property tests
    /// </summary>
    /// <param name="with"></param>
    /// <param name="propertySelector"></param>
    /// <typeparam name="TActual"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    public static IBe<TProperty> Property<TActual, TProperty>(
        this IWith<TActual> with,
        Func<TActual, TProperty> propertySelector
    )
    {
        var continuation = new Be<TProperty>(default);
        with.AddMatcher(
            actual =>
            {
                var propValue = propertySelector(actual);
                continuation.SetActual(propValue);
                var newParent = new Expectation<TProperty>(propValue);
                continuation.SetParent(newParent);
                return new MatcherResult(true);
            }
        );
        return continuation;
    }
}