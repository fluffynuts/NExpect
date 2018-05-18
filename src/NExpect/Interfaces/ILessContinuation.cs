// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Less grammar continuation
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface ILessContinuation<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Starts the .Less.Than.Or.Equal.To
        /// </summary>
        ILessThan<T> Than { get; }
    }
    
    /// <summary>
    /// Continues with the .Than part of .Less.Than.Or.Equal.To
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILessThan<T>: IHasActual<T>
    {
        /// <summary>
        /// Continues with the .Or of .Less.Than.Or.Equal.To
        /// </summary>
        ILessThanOr<T> Or { get; }
    }

    /// <summary>
    /// Continues with the .Or part of .Less.Than.Or.Equal.To
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILessThanOr<T>: IHasActual<T>
    {
        /// <summary>
        /// Continues with the .Equal of .Less.Than.Or.Equal.To
        /// </summary>
        ILessThanOrEqual<T> Equal { get; }
    }

    /// <summary>
    /// Penultimate part of .Less.Than.Or.Equal.To
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILessThanOrEqual<T>: ICanAddMatcher<T>, IHasActual<T>
    {
    }
}