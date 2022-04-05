namespace NExpect.Interfaces;

/// <summary>
/// Provides the properties required to work with a count-match
/// </summary>
public interface ICountMatch
{
    /// <summary>
    /// Method to use when comparing the count, eg:
    /// - at least
    /// - at most
    /// - exactly
    /// - any
    /// - all
    /// </summary>
    CountMatchMethods Method { get; }
    /// <summary>
    /// Comparison count, where appropriate (at least, at most, exactly)
    /// </summary>
    int ExpectedCount { get; }
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICountMatch<T> : ICountMatch, ICanAddMatcher<T>
{
}