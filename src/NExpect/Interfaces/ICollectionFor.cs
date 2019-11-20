using System.Collections.Generic;

// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces
{
    /// <summary>
    /// Interface providing the dangling .For
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionFor<T>: ICanAddMatcher<IEnumerable<T>>
    {
    }
}