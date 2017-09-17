using System;
using System.Collections.Generic;
using System.Linq;
using PeanutButter.Utils;

namespace NExpect
{
    internal static class DeepTestHelpers
    {
        internal static bool AreIntersectionEqual<T>(T item1, T item2)
        {
            var tester = new DeepEqualityTester(item1, item2)
            {
                OnlyTestIntersectingProperties = true,
                RecordErrors = false,
                FailOnMissingProperties = false,
                IncludeFields = true
            };
            return tester.AreDeepEqual();
        }

        internal static bool AreDeepEqual(object item1, object item2)
        {
            var tester = new DeepEqualityTester(item1, item2)
            {
                RecordErrors = false,
                FailOnMissingProperties = true,
                IncludeFields = true,
                OnlyTestIntersectingProperties = false
            };
            return tester.AreDeepEqual();
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