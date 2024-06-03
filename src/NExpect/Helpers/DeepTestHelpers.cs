using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;

// ReSharper disable IntroduceOptionalParameters.Global

namespace NExpect.Helpers;

internal class DeepTestResult
{
    public static DeepTestResult Pass { get; } = new DeepTestResult(true);

    public static DeepTestResult Fail(params string[] errors)
    {
        return new DeepTestResult(false, errors);
    }

    public bool AreEqual { get; }
    public IEnumerable<string> Errors { get; }

    public DeepTestResult(bool areEqual) : this(areEqual, new string[0])
    {
    }

    public DeepTestResult(bool areEqual, params string[] errors)
        : this(areEqual, errors.AsEnumerable())
    {
    }

    public DeepTestResult(bool areEqual, IEnumerable<string> errors)
    {
        AreEqual = areEqual;
        Errors = errors;
    }
}

internal static class DeepTestHelpers
{
    internal static DeepTestResult AreIntersectionEqual<T>(
        T item1,
        T item2,
        object[] customEqualityComparers,
        HashSet<string> ignoreProperties
    )
    {
        var tester = new DeepEqualityTester(item1, item2, ignoreProperties.ToArray())
        {
            OnlyTestIntersectingProperties = true,
            RecordErrors = true,
            VerbosePropertyMismatchErrors = false,
            FailOnMissingProperties = false,
            IncludeFields = true
        };
        AddCustomComparerersTo(tester, customEqualityComparers);
        return new DeepTestResult(
            tester.AreDeepEqual(),
            tester.Errors
        );
    }

    internal static DeepTestResult AreDeepEqual(
        object item1,
        object item2,
        object[] customEqualityComparers,
        HashSet<string> ignoreProperties
    )
    {
        return AreDeepEqual(
            item1,
            item2,
            customEqualityComparers,
            ignoreProperties.ToArray()
        );
    }

    internal static DeepTestResult AreDeepEqual(
        object item1,
        object item2,
        object[] customEqualityComparers,
        string[] ignoreProperties
    )
    {
        var tester = new DeepEqualityTester(item1, item2, ignoreProperties)
        {
            RecordErrors = true,
            VerbosePropertyMismatchErrors = false,
            FailOnMissingProperties = true,
            IncludeFields = true,
            OnlyTestIntersectingProperties = false
        };
        AddCustomComparerersTo(tester, customEqualityComparers);
        return new DeepTestResult(
            tester.AreDeepEqual(),
            tester.Errors
        );
    }

    private static void AddCustomComparerersTo(
        DeepEqualityTester tester,
        params object[] customEqualityComparers
    )
    {
        ValidateAreComparers(customEqualityComparers);
        customEqualityComparers.ForEach(tester.AddCustomComparer);
    }

    private static void ValidateAreComparers(object[] customEqualityComparers)
    {
        var invalid = customEqualityComparers.Where(
            o =>
            {
                var implemented =
                    o.GetType().GetTypeInfo().ImplementedInterfaces;
                var match = implemented.FirstOrDefault(
                    i => i.IsGenericOf(typeof(IEqualityComparer<>))
                );
                return match == null;
            }
        ).ToArray();
        if (!invalid.Any())
        {
            return;
        }

        var names = invalid.Select(t => t.GetType().PrettyName()).JoinWith(",");
        throw new ArgumentException(
            $"Custom equality comparers must implement IEqualityComparer<T>. The following do not: {names}"
        );
    }

    internal static DeepTestResult CollectionCompare<T>(
        IEnumerable<T> collection,
        IEnumerable<T> expected,
        Func<List<T>, List<T>, DeepTestResult> finalComparison
    )
    {
        if (collection == null &&
            expected == null)
        {
            return DeepTestResult.Pass;
        }

        if (collection == null ||
            expected == null)
        {
            return DeepTestResult.Fail(
                expected == null
                    ? $"Expected collection is null but actual is not"
                    : "Actual collection is null but expected is not"
            );
        }

        var master = collection.ToList();
        var compare = expected.ToList();
        if (master.Count != compare.Count)
        {
            return DeepTestResult.Fail(
                $"Collection count mismatch: expected {expected.Count()}, but got {collection.Count()}"
            );
        }

        return finalComparison(master, compare);
    }
}