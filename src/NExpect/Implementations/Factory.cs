using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal static class Factory
    {
        internal static T2 Create<T1, T2>(
            T1 actual,
            IExpectationContext<T1> parent,
            Action<T2> afterConstruction = null
        ) where T2 : IExpectationContext<T1>
        {
            var result = (T2) Activator.CreateInstance(typeof(T2), actual);
            result.Parent = parent;
            afterConstruction?.Invoke(result);
            return result;
        }
    }
}