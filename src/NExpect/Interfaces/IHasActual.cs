namespace NExpect.Interfaces
{
    /// <summary>
    /// Interface to implement to surface the Actual value
    /// </summary>
    /// <typeparam name="T">Type of Actual</typeparam>
    public interface IHasActual<out T>
    {
        /// <summary>
        /// Actual value, volleyed down the chain, in case any element needs it.
        /// </summary>
        T Actual { get; }
    }
}