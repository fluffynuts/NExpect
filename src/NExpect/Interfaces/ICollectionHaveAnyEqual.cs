using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionHaveAnyEqual<T>: ICanAddMatcher<IEnumerable<T>>
    {
    }
}