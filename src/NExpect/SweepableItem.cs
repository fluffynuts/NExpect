using System;
using System.Diagnostics;
using NExpect.Implementations;

namespace NExpect;

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
        Assertions.Track(this);
        StackTrace = new StackTrace();
    }
}