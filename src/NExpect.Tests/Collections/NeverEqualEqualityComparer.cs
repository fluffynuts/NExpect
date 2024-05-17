using System;
using System.Collections.Generic;

namespace NExpect.Tests.Collections;

public class NeverEqualEqualityComparer : IEqualityComparer<DateTime>
{
    public bool Equals(DateTime x, DateTime y)
    {
        return false;
    }

    public int GetHashCode(DateTime obj)
    {
        return 0;
    }
}
