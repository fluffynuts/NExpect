using System;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
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
        public IOf<T> Of => throw Terminated;
        public IBy<T> By => throw Terminated;
    }
}