using System;

namespace NExpect;

/// <summary>
/// Temporarily suspends any incomplete expectation tracking
/// - this should only ever be useful within NExpect tests
/// </summary>
internal class SuspendIncompleteExpectationTracking : IDisposable
{
    /// <summary>
    /// Suspends tracking until this object is disposed
    /// </summary>
    public SuspendIncompleteExpectationTracking()
    {
        Assertions.SetSuspended();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Assertions.ResumeTracking();
    }
}