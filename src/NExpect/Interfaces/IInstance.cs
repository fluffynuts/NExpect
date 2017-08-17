using System;

namespace NExpect.Interfaces
{
    public interface IInstance
    {
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