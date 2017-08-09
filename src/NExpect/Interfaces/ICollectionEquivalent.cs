using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionEquivalent<T>: ICanAddMatcher<IEnumerable<T>>
    {
    }
}