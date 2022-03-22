namespace NExpect.Interfaces;

/// <summary>
/// Provides the extension point for Of.Type&lt;T&gt;()
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICountMatchOf<T> : ICanAddMatcher<T>
{
    /// <summary>
    /// The original continuation
    /// </summary>
    ICanAddMatcher<T> Continuation { get; }
    /// <summary>
    /// The count match method
    /// </summary>
    CountMatchMethods Method { get; }
    /// <summary>
    /// The count match expected value, if appropriate
    /// </summary>
    int Compare { get; }
}