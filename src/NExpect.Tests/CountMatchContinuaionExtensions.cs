using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect.Tests
{
    public static class CountMatchContinuaionExtensions
    {
        public static void Odds(this ICountMatchContinuation<IEnumerable<int>> continuation)
        {
            continuation.AddMatcher(collection =>
            {
                var expectedCount = continuation.ExpectedCount;
                var method = continuation.GetCountMatchMethod();
                // TODO: use count and method
                var count = collection.Count(i => i % 2 == 1);
                var total = collection.Count();
                var passed = _strategies[method](total, count, expectedCount);
                var not = passed ? "" : "not ";
                return new MatcherResult(
                    passed,
                    $"Expected {MessageHelpers.PrettyPrint(collection)} {not}to be only odd numbers"
                );
            });
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