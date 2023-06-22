// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides ".To" after a ".Not"
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IToAfterNot<T> : ICanAddMatcher<T>
{
    /// <summary>
    /// Starts a similarity expectation
    /// </summary>
    IBe<T> Be { get; }

    /// <summary>
    /// Starts a containing expectation
    /// </summary>
    IContain<T> Contain { get; }

    /// <summary>
    /// Starts a property expectation
    /// </summary>
    IHave<T> Have { get; }

    /// <summary>
    /// Starts a deep equality test expectation
    /// </summary>
    IDeep<T> Deep { get; }

    /// <summary>
    /// Starts an intersection equality test expectation
    /// -> only tests properties and fields which match by name and type
    /// </summary>
    IIntersection<T> Intersection { get; }

    /// <summary>
    /// Starts an expectation for approximate equality
    /// </summary>
    IApproximately<T> Approximately { get; }

    /// <summary>
    /// Provides a dangling grammar point for extension (.Find)
    /// </summary>
    IFind<T> Find { get; }
}