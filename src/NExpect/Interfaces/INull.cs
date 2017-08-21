namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation to test for null or something else (typically empty or whitespace)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INull<T>
    {
        INullOr<T> Or { get; }
    }
}