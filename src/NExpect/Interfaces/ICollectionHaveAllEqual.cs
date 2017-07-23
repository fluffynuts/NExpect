using System.Collections.Generic;

namespace NExpect.Interfaces
{
    public interface ICollectionHaveAllEqual<T>: ICanAddMatcher<IEnumerable<T>>
    {
    }
}