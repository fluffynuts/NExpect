using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .More continuation for collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionMore<T> 
        : ICanAddCollectionMatcher<T>
    {
        /// <summary>
        /// Provides the .And grammar extension
        /// </summary>
        ICollectionAnd<T> And { get; }

        /// <summary>
        /// Provides the .Having grammar extension
        /// </summary>
        ICollectionHaving<T> Having { get; }
    }
}