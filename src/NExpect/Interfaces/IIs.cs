namespace NExpect.Interfaces;

/// <summary>
/// Provides the .Is continuation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IIs<T> 
    : ICanAddMatcher<T>
{
    /// <summary>
    /// Provides the .Has.A extension point
    /// </summary>
    IA<T> A { get; }
    /// <summary>
    /// Provides the .Has.An extension point
    /// </summary>
    IAn<T> An { get; }

    /// <summary>
    /// Provies the .Is.Matched.By extension point
    /// </summary>
    /// <returns></returns>
    IMatched<T> Matched { get; }
}