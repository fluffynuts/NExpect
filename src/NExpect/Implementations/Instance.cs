using System;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    // TODO: complete Instance.Of... somehow (:
    internal class Instance<T> :
        ExpectationContext<T>,
        IInstance<T>
    {
        public T Actual { get; }
        public Type Type { get; }

        public Instance(T actual)
        {
            Actual = actual;
            Type = typeof(T);
        }
    }
}