namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation of Not after To, ie ...Not.To...
    /// </summary>
    /// <typeparam name="T">Underlying type of the continuation</typeparam>
    public interface INotAfterTo<T>: IContinuation<T>
    {
        /// <summary>
        /// Continuation of Be, carrying type
        /// </summary>
        IBe<T> Be { get; }
    }
}