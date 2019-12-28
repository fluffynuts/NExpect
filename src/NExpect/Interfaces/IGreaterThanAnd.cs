namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .And on .Greater.Than
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGreaterThanAnd<T>
    {
        /// <summary>
        /// Prepares to test the value for being less than another
        /// </summary>
        ILessContinuation<T> Less { get; }

        /// <summary>
        /// Prepares for a fluency continuation
        /// </summary>
        ITo<T> To { get; }
    }
}