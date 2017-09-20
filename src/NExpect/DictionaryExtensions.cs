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
        /// Tests if the provided collection contains the required key.
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
        /// Tests if the provided collection contains the required key.
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
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }
        
        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from sbyte to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, sbyte>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from sbyte to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, sbyte>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from short to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, short>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from short to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, short>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from int to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, int>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from int to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, int>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from byte to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, byte>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from byte to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, byte>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from ushort to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, ushort>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from ushort to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, ushort>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from uint to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, uint>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from uint to long for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<long> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, uint>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

            return CreateValueContinuation(continuation, key);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from float to double for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<double> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, float>>> continuation,
            TKey key
        )
        {
            return continuation.Key(key, null);
        }

        /// <summary>
        /// Tests if the provided collection contains the required key.
        /// Upcast from float to double for convenience
        /// </summary>
        /// <param name="continuation">Continuation to operate on</param>
        /// <param name="key">Key to look for</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
        public static IDictionaryValueContinuation<double> Key<TKey>(
            this IContain<IEnumerable<KeyValuePair<TKey, float>>> continuation,
            TKey key,
            string customMessage
        )
        {
            AddKeyMatcher(continuation, key, customMessage);

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

        private static void AddKeyMatcher<TKey, TValue>(IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation, TKey key, string customMessage)
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

        private static IDictionaryValueContinuation<TValue> CreateValueContinuation<TKey, TValue>(
            IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<TValue, DictionaryValueContinuation<TValue>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, TValue>>, TValue>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, sbyte>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, sbyte>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, short>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, short>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, int>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, int>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, byte>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, byte>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, ushort>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, ushort>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<long> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, uint>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<long, DictionaryValueContinuation<long>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, uint>>, long>(
                    continuation, c => continuationValue
                )
            );
        }

        private static IDictionaryValueContinuation<double> CreateValueContinuation<TKey>(
            IContain<IEnumerable<KeyValuePair<TKey, float>>> continuation, TKey key)
        {
            var continuationValue = GetValueForKey(continuation, key);
            return Factory.Create<double, DictionaryValueContinuation<double>>(
                continuationValue,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, float>>, double>(
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