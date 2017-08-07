using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect
{
    /// <summary>
    /// Provides extensions for collection expectations
    /// </summary>
    public static class CollectionExtensions
    {

        /// <summary>
        /// Short contain, equivalent to .Contain.At.Least.One.Equal.To(x)
        /// -> less expressive, but shorter to type (:
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="search"></param>
        /// <typeparam name="T"></typeparam>
        public static void Contain<T>(
            this ICollectionTo<T> continuation,
            T search)
        {
            continuation.AddMatcher(collection => 
            {
                var passed = collection.Contains(search);
                var notPart = passed ? "" : "not ";
                return new MatcherResult(
                    passed,
                    $"Expected {collection} {notPart}to contain {search}"
                );
            });
        }
        /// <summary>
        /// Match exactly N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Exactly<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Exactly,
                howMany
            );
        }

        /// <summary>
        /// Match at least N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match at least</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Least<T>(
            this IContainAt<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Minimum,
                howMany
            );
        }

        /// <summary>
        /// Starts continuation to match any items in the source collection
        /// </summary>
        /// <param name="contain">Continuation to continue from</param>
        /// <typeparam name="T">Type of items in collection</typeparam>
        /// <returns></returns>
        public static ICountMatchContinuation<IEnumerable<T>> Any<T>(
            this IContain<IEnumerable<T>> contain
        )
        {
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Any, 
                0
            );
        }

        /// <summary>
        /// Starts continuation to match all items in the source collection
        /// </summary>
        /// <param name="contain">Continuation to continue from</param>
        /// <typeparam name="T">Type of items in collection</typeparam>
        /// <returns></returns>
        public static ICountMatchContinuation<IEnumerable<T>> All<T>(
            this IContain<IEnumerable<T>> contain
        )
        {
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.All, 
                0
            );
        }


        /// <summary>
        /// Match at most N elements with following matchers
        /// </summary>
        /// <param name="contain">contain continuation</param>
        /// <param name="howMany">how many items to match at most</param>
        /// <typeparam name="T">Type of item to match</typeparam>
        /// <returns>continuation be used: .Equal.To()</returns>
        public static ICountMatchContinuation<IEnumerable<T>> Most<T>(
            this IContainAt<IEnumerable<T>> contain,
            int howMany
        )
        {
            CheckContain(contain);
            return new CountMatchContinuation<IEnumerable<T>>(
                contain,
                CountMatchMethods.Maximum,
                howMany
            );
        }

        /// <summary>
        /// Performs the equality match with the provided limit (exactly, min, max)
        /// on the collection, using {T}.Equals()
        /// </summary>
        /// <param name="countMatch">Count matcher continuation</param>
        /// <param name="search">Thing to search for</param>
        /// <typeparam name="T">Type of underlying continuation</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the countMatch is null -- mainly to keep the library sane and testable</exception>
        public static void To<T>(
            this ICountMatchEqual<IEnumerable<T>> countMatch,
            T search)
        {
            if (countMatch == null)
                throw new ArgumentNullException(nameof(countMatch),
                    $"EqualTo<T> cannot extend null ICanAddMatcher<IEnumerable<{typeof(T)}>>");
            countMatch.Continuation.AddMatcher(collection =>
            {
                var asArray = collection.ToArray();
                var have = countMatch.Method == CountMatchMethods.Any 
                            ? (asArray.Any(o => o.Equals(search)) ? 1 : 0 )
                            :  asArray.Count(o => o.Equals(search));
                var passed = _collectionCountMatchStrategies[countMatch.Method](have, 
                    countMatch.Method == CountMatchMethods.All ? asArray.Length :  countMatch.Compare
                );
                var message =
                    _collectionCountMessageStrategies[countMatch.Method](passed, search, have, countMatch.Compare);

                return new MatcherResult(
                    passed,
                    message
                );
            });
        }

        /// <summary>
        /// Continuation for Matched, allowing testing the artifact with a simple func
        /// </summary>
        /// <param name="countMatch">Matched continuation</param>
        /// <param name="test">Func to test Actual value with; return true for match, false for non-match</param>
        /// <typeparam name="T">Type of artifact being tested</typeparam>
        public static void By<T>(
            this ICountMatchMatched<IEnumerable<T>> countMatch,
            Func<T, bool> test
        )
        {
            countMatch.Continuation.AddMatcher(collection =>
            {
                var have = collection.Where(test).Count();
                var passed = _collectionCountMatchStrategies[countMatch.Method](have, countMatch.Compare);
                var message =
                    _collectionCountMatchMessageStrategies[countMatch.Method](passed, have, countMatch.Compare);
                return new MatcherResult(passed, message);
            });
        }

        private static void CheckContain<T>(ICanAddMatcher<IEnumerable<T>> contain)
        {
            if (contain == null)
                throw new ArgumentNullException(nameof(contain),
                    $"Exactly<T>() cannot extend null IContain<IEnumerable<{typeof(T).Name}>>");
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, object, int, int, string>> _collectionCountMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, object, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMessageFor("exactly"),
                [CountMatchMethods.Minimum] = CreateMessageFor("at least"),
                [CountMatchMethods.Maximum] = CreateMessageFor("at most"),
                [CountMatchMethods.Any] = CreateAnyMessage,
                [CountMatchMethods.All] = CreateAllMessage
            };

        private static string CreateAllMessage(bool passed, object search, int have, int want)
        {
            return passed 
                ? $"Expected not to find all matching {search}" 
                : $"Expected to find all matching {search}";
        }

        private static string CreateAnyMessage(bool passed, object search, int have, int want)
        {
            return passed 
                ? $"Expected not to find any matches for {search}" 
                : $"Expected to find any match for {search}";
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, int, int, string>> _collectionCountMatchMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMatchMessageFor("exactly"),
                [CountMatchMethods.Minimum] = CreateMatchMessageFor("at least"),
                [CountMatchMethods.Maximum] = CreateMatchMessageFor("at most")
            };

        private static readonly Dictionary<CountMatchMethods,
            Func<int, int, bool>> _collectionCountMatchStrategies =
            new Dictionary<CountMatchMethods, Func<int, int, bool>>()
            {
                [CountMatchMethods.Exactly] = (have, want) => have == want,
                [CountMatchMethods.Minimum] = (have, want) => have >= want,
                [CountMatchMethods.Maximum] = (have, want) => have <= want,
                [CountMatchMethods.Any] = (have, want) => have > 0,
                [CountMatchMethods.All] = (have, collectionTotal) => have == collectionTotal
            };

        private static Func<bool, object, int, int, string> CreateMessageFor(
            string context
        )
        {
            return (passed, search, have, want) => passed
                ? CreatePassMessageFor(context, search, have, want)
                : CreateFailedMessageFor(context, search, have, want);
        }

        private static Func<bool, int, int, string> CreateMatchMessageFor(
            string context
        )
        {
            return (passed, have, want) => passed
                ? CreatePassMatchMessageFor(context, have, want)
                : CreateFailedMatchMessageFor(context, have, want);
        }

        private static string CreateFailedMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "s";
            return $"Expected to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

        private static string CreatePassMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "s";
            return $"Expected not to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

        private static string CreateFailedMatchMessageFor(
            string comparison,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "es";
            return $"Expected to find {comparison} {want} match{s} but found {have}";
        }

        private static string CreatePassMatchMessageFor(
            string comparison,
            int have,
            int want
        )
        {
            var s = want == 1
                ? ""
                : "es";
            return $"Expected not to find {comparison} {want} match{s} but found {have}";
        }
    }
}