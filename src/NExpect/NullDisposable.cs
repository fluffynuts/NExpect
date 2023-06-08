using System;

namespace NExpect;

internal class NullDisposable : IDisposable
{
    public void Dispose()
    {
        // intentionally left blank
    }
}