// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Provides the extension point for testing count-matches within collections.
/// Essentially, the original continuation is frankensquished into this.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICountMatchEqual<T>: ICountMatch, IHasActual<T>
{
    /// <summary>
    /// Original continuation
    /// </summary>
    ICanAddMatcher<T> Continuation { get; }
}