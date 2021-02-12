using System;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;

namespace NExpect.Implementations.Fluency
{
    internal class Been<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IBeen<T>
    {
        public IInstanceContinuation Instance => new InstanceContinuation(
            () => Actual?.GetType(),
            this);

        public Been(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }
}