using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
// ReSharper disable IntroduceOptionalParameters.Global

namespace NExpect.Helpers
{
    internal static class DeepTestHelpers
    {
        internal static bool AreIntersectionEqual<T>(
            T item1,
            T item2,
            params object[] customEqualityComparers)
        {
            var tester = new DeepEqualityTester(item1, item2)
            {
                OnlyTestIntersectingProperties = true,
                RecordErrors = false,
                FailOnMissingProperties = false,
                IncludeFields = true
            };
            AddCustomComparerersTo(tester, customEqualityComparers);
            return tester.AreDeepEqual();
        }

        internal static bool AreDeepEqual(
            object item1,
            object item2
        )
        {
            return AreDeepEqual(item1, item2, null);
        }

        internal static bool AreDeepEqual(
            object item1,
            object item2,
            object[] customEqualityComparers)
        {
            var tester = new DeepEqualityTester(item1, item2)
            {
                RecordErrors = false,
                FailOnMissingProperties = true,
                IncludeFields = true,
                OnlyTestIntersectingProperties = false
            };
            AddCustomComparerersTo(tester, customEqualityComparers);
            return tester.AreDeepEqual();
        }

        private static void AddCustomComparerersTo(
            DeepEqualityTester tester,
            params object[] customEqualityComparers)
        {
            ValidateAreComparers(customEqualityComparers);
            customEqualityComparers.ForEach(tester.AddCustomComparer);
        }

        private static void ValidateAreComparers(object[] customEqualityComparers)
        {
            var invalid = customEqualityComparers.Where(
                o =>
                {
                    var implemented = o.GetType().GetTypeInfo().ImplementedInterfaces;
                    var match = implemented.FirstOrDefault(i => i.IsGenericOf(typeof(IEqualityComparer<>)));
                    return match == null;
                }).ToArray();
            if (!invalid.Any())
                return;
            var names = invalid.Select(t => t.GetType().PrettyName()).JoinWith(",");
            throw new ArgumentException(
                $"Custom equality comparers must implement IEqualityComparer<T>. The following do not: {names}"
            );
        }

        internal static bool CollectionCompare<T>(
            IEnumerable<T> collection,
            IEnumerable<T> expected,
            Func<List<T>, List<T>, bool> finalComparison
        )
        {
            if (collection == null &&
                expected == null)
                return true;
            if (collection == null ||
                expected == null)
                return false;
            var master = collection.ToList();
            var compare = expected.ToList();
            if (master.Count != compare.Count)
                return false;
            return finalComparison(master, compare);
        }
    }
}