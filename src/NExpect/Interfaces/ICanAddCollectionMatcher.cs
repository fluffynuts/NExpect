using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// ICanAddMatcher, but for collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICanAddCollectionMatcher<T> 
        : ICanAddMatcher<IEnumerable<T>>
    {
    }
}