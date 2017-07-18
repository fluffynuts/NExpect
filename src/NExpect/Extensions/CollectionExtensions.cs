using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class CollectionExtensions
    {
        public static ICountMatchContinuation<IEnumerable<T>> Exactly<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain, CountMatchMethods.Exactly, howMany
            );
        }

        public static ICountMatchContinuation<IEnumerable<T>> Least<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain, CountMatchMethods.Minimum, howMany
            );
        }

        public static ICountMatchContinuation<IEnumerable<T>> Most<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain, CountMatchMethods.Maximum, howMany
            );
        }

        public static void To<T>(
            this ICountMatchEquals<IEnumerable<T>> countMatch,
            T search)
        {
            if (countMatch == null)
                throw new ArgumentNullException(nameof(countMatch),
                    $"EqualTo<T> cannot extend null IContinuation<IEnumerable<{typeof(T)}>>");
            countMatch.Continuation.AddMatcher(collection =>
            {
                var have = collection.Count(o => o.Equals(search));
                var passed = CollectionCountMatchStrategies[countMatch.Method](have, countMatch.Compare);
                var message =
                    CollectionCountMessageStrategies[countMatch.Method](passed, search, have, countMatch.Compare);

                return new MatcherResult(
                    passed,
                    message
                );
            });
        }

        private static void CheckContain<T>(IContain<IEnumerable<T>> contain)
        {
            if (contain == null)
                throw new ArgumentNullException(nameof(contain),
                    $"Exactly<T>() cannot extend null IContain<IEnumerable<{typeof(T).Name}>>");
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, object, int, int, string>> CollectionCountMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, object, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMessageFor("exactly"),
                [CountMatchMethods.Minimum] = CreateMessageFor("at least"),
                [CountMatchMethods.Maximum] = CreateMessageFor("at most")
            };

        private static readonly Dictionary<CountMatchMethods,
            Func<int, int, bool>> CollectionCountMatchStrategies =
            new Dictionary<CountMatchMethods, Func<int, int, bool>>()
            {
                [CountMatchMethods.Exactly] = (have, want) => have == want,
                [CountMatchMethods.Minimum] = (have, want) => have >= want,
                [CountMatchMethods.Maximum] = (have, want) => have <= want
            };

        private static Func<bool, object, int, int, string> CreateMessageFor(
            string context
        )
        {
            Func<bool, object, int, int, string> result = 
                (passed, search, have, want) =>
                    passed
                    ? CreatePassMessageFor(context, search, have, want)
                    : CreateFailedMessageFor(context, search, have, want);
            return result;
        }

        private static string CreateFailedMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1 ? "" : "s";
            return $"Expected to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

        private static string CreatePassMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1 ? "" : "s";
            return $"Expected not to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

    }
}