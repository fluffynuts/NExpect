using System;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Base interface for instance testing (TODO)
    /// </summary>
    public interface IInstance
    {
        /// <summary>
        /// Type of the object being tested
        /// </summary>
        Type Type { get; }
    }

    /// <summary>
    /// Continuation for .Instance.Of&lt;T&gt;()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInstance<T> :
        IInstance,
        ICanAddMatcher<T>,
        IHasActual<T>
    {
    }
}