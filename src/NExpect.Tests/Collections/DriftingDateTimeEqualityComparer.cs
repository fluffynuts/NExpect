using System;
using System.Collections.Generic;

namespace NExpect.Tests.Collections;

public class DriftingDateTimeEqualityComparer : IEqualityComparer<DateTime>
{
    public bool Equals(DateTime x, DateTime y)
    {
        var delta = x - y;
        return Math.Abs(delta.TotalSeconds) < 2;
    }

    public int GetHashCode(DateTime obj)
    {
        return 0;
    }
}
