using System;
using NExpect.Implementations.Fluency;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class More<T>
        : ExpectationContextWithLazyActual<T>,
          IHasActual<T>,
          IMore<T>
    {
        public IAnd<T> And =>
            ContinuationFactory.Create<T, And<T>>(ActualFetcher, this);

        public IWith<T> With =>
            ContinuationFactory.Create<T, With<T>>(ActualFetcher, this);

        public More(Func<T> actualFetcher) : base(actualFetcher)
        {
        }
    }

    internal class TerminatedMore<T> : IMore<T>
    {
        private InvalidOperationException
            Terminated => new InvalidOperationException(
                $"IMore<{typeof(T)}> cannot be continued from incoming continuation acting on type {IncomingType}"
            );

        public TerminatedMore(Type incomingType)
        {
            IncomingType = incomingType;
        }

        public Type IncomingType { get; }

        public IAnd<T> And => throw Terminated;
        public IWith<T> With => throw Terminated;
    }
}