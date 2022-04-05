namespace NExpect.Interfaces;

/// <summary>
/// Continuation to test for null or something else (typically empty or whitespace)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INull<T>
{
    /// <summary>
    /// Continuation used to test that a value is either null or
    /// some other value. Implemented for strings (null or empty / whitespace)
    /// but you could also extend this.
    /// </summary>
    INullOr<T> Or { get; }
}