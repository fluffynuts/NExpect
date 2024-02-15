using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests.Collections
{
    public static class CountMatchContinuationExtensionsForTesting
    {
        public static IMore<IEnumerable<int>> Odds(this ICountMatchContinuation<IEnumerable<int>> continuation)
        {
            return continuation.AddMatcher(collection =>
            {
                return TestCollection(
                    collection,
                    continuation.ExpectedCount,
                    continuation.GetCountMatchMethod(),
                    i => i % 2 == 1,
                    passed => $"Expected {collection.LimitedPrint()} {passed.AsNot()}to be only odd numbers"
                );
            });
        }

        public static IMore<IEnumerable<int>> Evens(
            this ICountMatchContinuation<IEnumerable<int>> continuation)
        {
            return continuation.AddMatcher(collection =>
            {
                return TestCollection(
                    collection,
                    continuation.ExpectedCount,
                    continuation.GetCountMatchMethod(),
                    i => i % 2 == 0,
                    passed => $"Expected {collection.LimitedPrint()} {passed.AsNot()}to be only even numbers"
                );
            });
        }

        private static MatcherResult TestCollection(
            IEnumerable<int> collection,
            int expectedCount,
            CountMatchMethods method,
            Func<int, bool> itemTester,
            Func<bool, string> messageGenerator)
        {
            var count = collection.Count(itemTester);
            var total = collection.Count();
            var passed = _strategies[method](total, count, expectedCount);
            return new MatcherResult(
                passed,
                () => messageGenerator(passed)
            );
        }

        private static Dictionary<CountMatchMethods, Func<int, int, int, bool>> _strategies =
            new Dictionary<CountMatchMethods, Func<int, int, int, bool>>()
            {
                [CountMatchMethods.All] = (total, matched, expected) => total == matched,
                [CountMatchMethods.Any] = (total, matched, expected) => matched > 0,
                [CountMatchMethods.Exactly] = (total, matched, expected) => matched == expected,
                [CountMatchMethods.Maximum] = (total, matched, expected) => matched <= expected,
                [CountMatchMethods.Minimum] = (total, matched, expected) => matched >= expected
            };
    }
}