using System;
using NExpect.Interfaces;

namespace NExpect.Implementations;

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
    public IMax<T> Max => throw Terminated;
    public ITo<T> To => throw Terminated;
    public IWhich<T> Which => throw Terminated;
    public IWithout<T> Without => throw Terminated;
    public IFor<T> For => throw Terminated;
    public IHaving<T> Having => throw Terminated;
    public IOn<T> On => throw Terminated;
    public IIn<T> In => throw Terminated;
    public IThen<T> Then => throw Terminated;
    public IFind<T> Find => throw Terminated;
}