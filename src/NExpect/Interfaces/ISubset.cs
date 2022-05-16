using System.Collections.Generic;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Starts testing if the actual is a subset of the expected value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubset<T>
        : IHasActual<IEnumerable<T>>,
          ICanAddMatcher<IEnumerable<T>>
    {
    }
}