using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

internal static class GreaterAndLessMatchersCommon
{
    internal static IMore<T1> AddMatcher<T1, T2>(
        ICanAddMatcher<T1> continuation,
        T2 expected,
        Func<T1, T2, bool> test,
        Func<string> customMessageGenerator,
        [CallerMemberName] string caller = null
    )
    {
        return continuation.AddMatcher(
            actual =>
            {
                var kindsMatch = HaveMatchingKindsOrCannotCompare(actual, expected);
                if (!kindsMatch)
                {
                    Assertions.Throw(
                        @$"Unable to compare dates:
{
                            actual.Stringify()
}
and
{
                            expected.Stringify()
}
Dates have different kinds, so equality based on absolute date/time could be misleading."
                    );
                }

                var passed = test(actual, expected);
                var compare = CreateCompareMessageFor(continuation, caller);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => passed
                            ? $"Expected {actual.Stringify()} not to be {compare} {expected.Stringify()}"
                            : $"Expected {actual.Stringify()} to be {compare} {expected.Stringify()}",
                        customMessageGenerator));
            });
    }

    private static bool HaveMatchingKindsOrCannotCompare(
        object left,
        object right
    )
    {
        var leftDate = TryGetDate(left);
        if (leftDate is null)
        {
            return true;
        }

        var rightDate = TryGetDate(right);
        if (rightDate is null)
        {
            return true;
        }

        // if either kind is Unspecified then allow the comparison
        if (leftDate.Value.Kind == DateTimeKind.Unspecified ||
            rightDate.Value.Kind == DateTimeKind.Unspecified)
        {
            return true;
        }

        return leftDate.Value.Kind == rightDate.Value.Kind;
    }

    private static DateTime? TryGetDate(object obj)
    {
        return obj is DateTime dt
            ? dt as DateTime?
            : null;
    }

    private static string CreateCompareMessageFor<T>(
        ICanAddMatcher<T> continuation,
        string caller
    )
    {
        switch (continuation)
        {
            case IGreaterContinuation<T> _:
                return "greater than";
            case ILessContinuation<T> _:
                return "less than";
            case IGreaterThanOrEqual<T> _:
                return "greater than or equal to";
            case ILessThanOrEqual<T> _:
                return "less than or equal to";
            case IAt<T> _:
                switch (caller)
                {
                    case nameof(AtLeastMatchers.Least):
                        return "at least";
                    case nameof(AtMostMatchers.Most):
                        return "at most";
                }

                break;
        }

        throw new Exception($"Unknown comparison type: {continuation.GetType()} ({caller})");
    }

    internal static int? TryCompare<T1, T2>(
        T1? a,
        T2? e
    ) where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        if (a == null || e == null)
        {
            return null;
        }

        return TryCompare(a.Value, e.Value);
    }

    internal static int? TryCompare<T1, T2>(
        T1? a,
        T2 e
    ) where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        if (a == null)
        {
            return null;
        }

        return TryCompare(a.Value, e);
    }

    internal static int? TryCompare<T1, T2>(
        T1 a,
        T2? e
    ) where T1 : struct, IComparable
        where T2 : struct, IComparable
    {
        if (e == null)
        {
            return null;
        }

        return TryCompare(a, e.Value);
    }

    internal static int? TryCompare<T1, T2>(
        T1 a,
        T2 e
    )
        where T1 : IComparable
        where T2 : IComparable
    {
        if (a == null || e == null)
        {
            return null;
        }

        var aType = a.GetType();
        var eType = e.GetType();
        if (aType == eType)
        {
            return a.CompareTo(e);
        }


        try
        {
            var eConverted = Convert.ChangeType(e, aType);
            return a.CompareTo(eConverted);
        }
        catch
        {
            try
            {
                var aConverted = Convert.ChangeType(a, eType) as IComparable;
                return aConverted?.CompareTo(e);
            }
            catch
            {
                var customComparer = TryFindCustomComparerFor(aType, eType);
                return customComparer?.TryCompare(a, e);
            }
        }
    }

    private static ICustomComparer TryFindCustomComparerFor(
        Type actualType,
        Type expectedType
    )
    {
        return CustomComparers.FirstOrDefault(
            o => (o.LeftType == actualType && o.RightType == expectedType) ||
                (o.LeftType == expectedType && o.RightType == actualType)
        );
    }

    private static ICustomComparer[] CustomComparers
        => _customComparers ??= FindCustomComparers();

    private static ICustomComparer[] FindCustomComparers()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Select(asm =>
            {
                try
                {
                    return asm.GetTypes();
                }
                catch
                {
                    return new Type[0];
                }
            })
            .SelectMany(o => o)
            .Where(o => o.Implements<ICustomComparer>())
            .Where(o => o.GetConstructor(new Type[0]) is not null)
            .Select(o => Activator.CreateInstance(o) as ICustomComparer)
            .ToArray();
    }

    private static ICustomComparer[] _customComparers;
}
