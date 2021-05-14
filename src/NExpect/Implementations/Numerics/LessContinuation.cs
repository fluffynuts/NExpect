using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Numerics
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LessContinuation<T> :
        ExpectationContextWithLazyActual<T>,
        IHasActual<T>,
        ILessContinuation<T>
    {
        public ILessThan<T> Than => Next<LessThan<T>>();

        public LessContinuation(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}