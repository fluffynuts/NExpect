using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Mostly grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionMostly<T> : ICanAddMatcher<IEnumerable<T>>
    {
    }
}