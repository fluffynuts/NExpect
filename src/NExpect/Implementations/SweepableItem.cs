using System;
using System.Diagnostics;

namespace NExpect.Implementations;

/// <summary>
/// The base, untyped ExpectationContext, used internally
/// for tracking incomplete expectations
/// </summary>
public abstract class SweepableItem:
    CannotBeCompared
{
    internal Guid Identifier { get; } = Guid.NewGuid();
    internal StackTrace StackTrace { get; }

    /// <summary>
    /// Registers the context for testing that it was
    /// run later
    /// </summary>
    protected SweepableItem()
    {
        ExpectationTracker.Register(this);
        StackTrace = new StackTrace();
    }
}