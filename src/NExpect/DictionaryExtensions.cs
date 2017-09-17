using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    /// <summary>
    /// Provides matchers for dictionaries
    /// </summary>
    public static class DictionaryExtensions 
    {
        /// <summary>
        /// Tests if the provided collection contains the required key
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        /// <typeparam name="TValue">Type of the dictionary values</typeparam>
        public static void Key<TKey, TValue>(
            this IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
            TKey key
        )
        {
            continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        /// <typeparam name="TValue">Type of the dictionary values</typeparam>
        public static void Key<TKey, TValue>(
            this IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
            TKey key,
            string customMessage
        )
        {
            continuation.AddMatcher(collection =>
            {
                var passed = collection?.Any(kvp => kvp.Key.Equals(key)) ?? false;
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(
                        $"Expected {collection.PrettyPrint()} {passed.AsNot()}to contain key {key?.Stringify()}",
                        customMessage
                    )
                );
            });
        }
    }
}