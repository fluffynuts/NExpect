using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace NExpect;

internal static class CustomComparerExtensions
{
    internal static int? TryCompare(
        this ICustomComparer comparer,
        object left,
        object right
    )
    {
        try
        {
            return comparer.Compare(left, right);
        }
        catch
        {
            // can't compare;
            return null;
        }
    }

    internal static int Compare(
        this ICustomComparer comparer,
        object left,
        object right
    )
    {
        var method = comparer.GetType().GetMethod(nameof(ICustomComparer<int, int>.Compare));
        return comparer.LeftType == left.GetType()
            ? (int)method.Invoke(comparer, new[] { left, right })
            : Invert((int)method.Invoke(comparer, new[] { right, left }));
    }

    private static int Invert(int comparisonResult)
    {
        if (comparisonResult == 0)
        {
            return 0;
        }
        return comparisonResult < 0
            ? 1
            : -1;
    }

    private static MethodInfo FindCompareMethodOn(ICustomComparer comparer)
    {
        var type = comparer.GetType();
        if (CompareMethods.TryGetValue(type, out var result))
        {
            return result;
        }
        
        result = type.GetMethod(nameof(ICustomComparer<int, int>.Compare));
        CompareMethods.TryAdd(type, result);
        return result;
    }
    
    private static readonly ConcurrentDictionary<Type, MethodInfo> CompareMethods
        = new();
}