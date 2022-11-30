using System.Collections.Generic;
// ReSharper disable ConstantConditionalAccessQualifier
// ReSharper disable ConstantNullCoalescingCondition

namespace NExpect.Implementations;

internal class DefaultEqualityComparer<T> : IEqualityComparer<T>
{
    public bool Equals(T x, T y)
    {
        if (x == null &&
            y == null)
        {
            return true;
        }

        if (x == null ||
            y == null)
        {
            return false;
        }

        return x.Equals(y);
    }

    public int GetHashCode(T obj)
    {
        return obj?.GetHashCode() ?? 0;
    }
}