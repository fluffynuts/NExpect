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
        public static IDictionaryValueContinuation<TValue> Key<TKey, TValue>(
            this IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }
        
        /// <summary>
        /// Tests if the provided collection contains the required key
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        /// <typeparam name="TValue">Type of the dictionary values</typeparam>
        public static IDictionaryValueContinuation<TValue> Key<TKey, TValue>(
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

            return CreateValueContinuation(continuation, key);
        }
        

        /// <summary>
        /// Tests if the provided key value matches the expected value
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Value to match on</param>
        /// <typeparam name="T">Type of the values</typeparam>
        public static void Value<T>(
            this IDictionaryValueWith<T> continuation,
            T expected
        )
        {
            continuation.Value(expected, null);
        }

        /// <summary>
        /// Tests if the provided key value matches the expected value
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="expected">Value to match on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="T">Type of the values</typeparam>
        public static void Value<T>(
            this IDictionaryValueWith<T> continuation,
            T expected,
            string customMessage
        )
        {
            continuation.AddMatcher(
                EqualityProviderExtensions.GenerateEqualityMatcherFor(expected, customMessage)
            );
        }

        private static IDictionaryValueContinuation<TValue> CreateValueContinuation<TKey, TValue>(IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<TValue, DictionaryValueContinuation<TValue>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, TValue>>, TValue>(
                    continuation, c => continuationValue
                )
            );
        }

        private static TValue GetValueForKey<TKey, TValue>(ICanAddMatcher<IEnumerable<KeyValuePair<TKey, TValue>>> continuation, TKey key)
        {
            return continuation
                .GetActual()
                .FirstOrDefault(kvp => kvp.Key.Equals(key))
                .Value;
        }
    }
}