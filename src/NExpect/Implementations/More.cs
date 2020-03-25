using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;
// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class More<T>
        : ExpectationContext<T>,
          IHasActual<T>,
          IMore<T>
    {
        public T Actual => _haveFetchedActual ? _actual : _actual = _actualGenerator();
        
        private T _actual;
        private readonly Func<T> _actualGenerator;
        private bool _haveFetchedActual = false;

        public IAnd<T> And =>
            ContinuationFactory.Create<T, And<T>>(Actual, this);

        public IWith<T> With =>
            ContinuationFactory.Create<T, With<T>>(Actual, this);

        public More(Func<T> actualGenerator)
        {
            _actualGenerator = actualGenerator;
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