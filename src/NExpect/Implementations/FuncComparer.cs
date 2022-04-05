using System;
using System.Collections.Generic;

// ReSharper disable ConstantConditionalAccessQualifier
// ReSharper disable ConstantNullCoalescingCondition

namespace NExpect.Implementations;

internal class FuncComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T, T, bool> _comparer;

    public FuncComparer(Func<T, T, bool> comparer)
    {
        _comparer = comparer;
    }

    public bool Equals(T x, T y)
    {
        return _comparer(x, y);
    }

    public int GetHashCode(T obj)
    {
        return obj?.GetHashCode() ?? 0;
    }
}