using System;

namespace NExpect.Helpers;

/// <summary>
/// Helper class to memoize functions
/// </summary>
public static class FuncFactory
{
    /// <summary>
    /// Memoizes a func so that it's only called once
    /// </summary>
    /// <param name="generator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Func<T> Memoize<T>(
        Func<T> generator)
    {
        T fetchedValue = default;
        var haveFetched = false;

        return () =>
        {
            return haveFetched
                ? fetchedValue
                : Fetch();

            T Fetch()
            {
                var result = fetchedValue = generator();
                haveFetched = true;
                return result;
            }
        };
    }
}