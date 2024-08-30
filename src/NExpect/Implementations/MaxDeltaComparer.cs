using System;
using System.Collections.Generic;

namespace NExpect.Implementations;

internal class MaxDeltaComparer(decimal within) : IEqualityComparer<decimal>
{
    private readonly decimal _within = Math.Abs(within);

    public bool Equals(decimal x, decimal y)
    {
        return Math.Abs(x - y) < _within;
    }

    public int GetHashCode(decimal obj)
    {
        throw new NotImplementedException();
    }
}
    
internal class MaxNullableDeltaComparer(decimal? within) : IEqualityComparer<decimal?>
{
    private readonly decimal? _within = within is null
        ? null
        : Math.Abs(within.Value);

    public bool Equals(decimal? x, decimal? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return Math.Abs(x.Value - y.Value) < _within;
    }

    public int GetHashCode(decimal? obj)
    {
        throw new NotImplementedException();
    }
}