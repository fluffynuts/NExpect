using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Helpers;
using NExpect.Implementations;
using NExpect.Implementations.Dictionaries;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.EqualityProviderMatchers;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration

namespace NExpect;

/// <summary>
/// Provides matchers for dictionaries
/// </summary>
public static class DictionaryMatchers
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    /// <typeparam name="TValue">Type of the dictionary values</typeparam>
    public static IDictionaryValueContinuation<TValue> Key<TKey, TValue>(
        this IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, TValue, TValue>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from sbyte to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, sbyte>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, sbyte, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from short to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, short>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, short, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from int to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, int>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);
        return CreateValueContinuationFor<TKey, int, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from byte to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, byte>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, byte, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from ushort to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, ushort>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, ushort, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from uint to long for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<long> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, uint>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);

        return CreateValueContinuationFor<TKey, uint, long>(
            continuation,
            key
        );
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
        return continuation.Key(key, NULL_STRING);
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
        return continuation.Key(key, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided collection contains the required key.
    /// Upcast from float to double for convenience
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="key">Key to look for</param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <typeparam name="TKey">Type of the dictionary keys</typeparam>
    public static IDictionaryValueContinuation<double> Key<TKey>(
        this IContain<IEnumerable<KeyValuePair<TKey, float>>> continuation,
        TKey key,
        Func<string> customMessageGenerator
    )
    {
        AddKeyMatcher(continuation, key, customMessageGenerator);
        return CreateValueContinuationFor<TKey, float, double>(
            continuation,
            key
        );
    }

    /// <summary>
    /// Tests if the provided key value matches the expected value
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Value to match on</param>
    /// <typeparam name="T">Type of the values</typeparam>
    public static IMore<T> Value<T>(
        this IDictionaryValueWith<T> continuation,
        T expected
    )
    {
        return continuation.Value(expected, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided key value matches the expected value
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Value to match on</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the values</typeparam>
    public static IMore<T> Value<T>(
        this IDictionaryValueWith<T> continuation,
        T expected,
        string customMessage
    )
    {
        return continuation.Value(expected, () => customMessage);
    }

    /// <summary>
    /// Tests if the provided key value matches the expected value
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="expected">Value to match on</param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <typeparam name="T">Type of the values</typeparam>
    public static IMore<T> Value<T>(
        this IDictionaryValueWith<T> continuation,
        T expected,
        Func<string> customMessage
    )
    {
        return continuation.AddMatcher(
            GenerateEqualityMatcherFor(expected, customMessage)
        );
    }

    /// <summary>
    /// Test the value matched by a prior key match for deep equality
    /// with another object
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="otherValue">Value to test deep equality against</param>
    /// <typeparam name="T">Type of the dictionary-sourced value</typeparam>
    /// <returns></returns>
    public static IMore<T> To<T>(
        this IDictionaryValueEqual<T> continuation,
        object otherValue)
    {
        return continuation.To(
            otherValue,
            NULL_STRING);
    }

    /// <summary>
    /// Test the value matched by a prior key match for deep equality
    /// with another object
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="otherValue">Value to test deep equality against</param>
    /// <param name="customMessage">Custom failure message</param>
    /// <typeparam name="T">Type of the dictionary-sourced value</typeparam>
    /// <returns></returns>
    public static IMore<T> To<T>(
        this IDictionaryValueEqual<T> continuation,
        object otherValue,
        string customMessage)
    {
        return continuation.To(
            otherValue,
            () => customMessage);
    }

    /// <summary>
    /// Test the value matched by a prior key match for deep equality
    /// with another object
    /// </summary>
    /// <param name="continuation">Continuation to operate on</param>
    /// <param name="otherValue">Value to test deep equality against</param>
    /// <param name="customMessageGenerator">Custom failure message generator</param>
    /// <typeparam name="T">Type of the dictionary-sourced value</typeparam>
    /// <returns></returns>
    public static IMore<T> To<T>(
        this IDictionaryValueEqual<T> continuation,
        object otherValue,
        Func<string> customMessageGenerator)
    {
        continuation.AddMatcher(actual =>
        {
            var isDeep = continuation.GetMetadata(
                DictionaryValueEqual<T>.DICTIONARY_VALUE_DEEP_EQUALITY_TESTING,
                true
            );
            var tester = new DeepEqualityTester(
                actual,
                otherValue);

            if (!isDeep)
            {
                tester.OnlyTestIntersectingProperties = true;
            }

            var passed = tester.AreDeepEqual();
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () =>
                        $@"Expected
{actual.Stringify()}
{passed.AsNot()}to deep equal
{otherValue.Stringify()}",
                    customMessageGenerator
                )
            );
        });
        return continuation.More();
    }

    private static IMore<IEnumerable<KeyValuePair<TKey, TValue>>> AddKeyMatcher<TKey, TValue>(
        IContain<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
        TKey key,
        Func<string> customMessage)
    {
        continuation.SetMetadata(CONTINUATION_KEY, key);
        return continuation.AddMatcher(collection =>
        {
            var passed = collection != null &&
                TryFindValueForKey(collection, key, out var _);
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => new[]
                    {
                        "Expected",
                        collection.LimitedPrint(),
                        $"{passed.AsNot()}to contain key",
                        key?.Stringify()
                    },
                    customMessage
                )
            );
        });
    }
        
    private const string CONTINUATION_KEY = "__NEXPECT_CONTINUATION_KEY__";

    private static bool TryFindValueForKey<TKey, TValue>(
        IEnumerable<KeyValuePair<TKey, TValue>> collection,
        TKey key,
        out TValue value
    )
    {
        if (key is string stringKey &&
            typeof(TKey) == typeof(string))
        {
            var comparer = collection.HasMetadata<StringComparer>(Expectations.KEY_COMPARER)
                ? collection.GetMetadata<StringComparer>(Expectations.KEY_COMPARER)
                : StringComparer.Ordinal;
            var keyMatches = collection.Select(kvp => kvp.Key as string)
                .Where(k => comparer.Compare(k, stringKey) == 0);
            var hasMatch = keyMatches.Any();
            value = hasMatch
                ? collection.First(kvp => comparer.Compare(kvp.Key as string, stringKey) == 0)
                    .Value
                : default;

            return hasMatch;
        }
        else
        {
            var matches = collection.Where(kvp => kvp.Key.Equals(key));
            var hasMatch = matches.Any();
            value = hasMatch
                ? matches.Single().Value
                : default;
            return hasMatch;
        }

    }

    private static IDictionaryValueContinuation<TTo> CreateValueContinuationFor<TKey, TFrom, TTo>(
        IContain<IEnumerable<KeyValuePair<TKey, TFrom>>> continuation,
        TKey key
    )
    {
        try
        {
            var fetcher = FuncFactory.Memoize<TTo>(() =>
            {
                var specificMethod = GenericUpcast.MakeGenericMethod(typeof(TTo));
                var continuationValue =
                    (TTo) (specificMethod.Invoke(null, new object[] { GetValueForKey(continuation, key) }));
                return continuationValue;
            });

            var result = ContinuationFactory.Create<TTo, DictionaryValueContinuation<TTo>>(
                fetcher,
                new WrappingContinuation<IEnumerable<KeyValuePair<TKey, TFrom>>, TTo>(
                    continuation as IHasActual<IEnumerable<KeyValuePair<TKey, TFrom>>>,
                    c => fetcher()
                )
            );
            continuation.CopyAllMetadataTo(result);
            return result;
        }
        catch (KeyNotFoundException)
        {
            return new KeyNotFoundContinuation<TKey, TTo>(key);
        }
    }

    private static readonly MethodInfo GenericUpcast =
        typeof(DictionaryMatchers).GetMethod(
            nameof(Upcast),
            BindingFlags.Static | BindingFlags.NonPublic
        );

    private static T2 Upcast<T2>(
        T2 value)
    {
        // takes advantage of automatic casting of method parameters
        //  - by expecting a 'T2', any value which can be auto-cast to 'T2'
        //    is just spat out again as a value of type 'T2' (eg, feed in an int
        //    with T2 being 'long', and you'll get a long.
        return value;
    }

    private static TValue GetValueForKey<TKey, TValue>(
        ICanAddMatcher<IEnumerable<KeyValuePair<TKey, TValue>>> continuation,
        TKey key)
    {
        var collection = continuation.GetActual();
        return TryFindValueForKey(collection, key, out var result)
            ? result
            : throw new KeyNotFoundException($"{key}");
    }

    /// <summary>
    /// Facilitates matching a value within a dictionary by arbitrary logic
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="matcher">Func containing custom logic: return true if the value meets expectations</param>
    /// <typeparam name="T">Type of the dictionary value</typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IDictionaryValueMatched<T> matched,
        Func<T, bool> matcher
    )
    {
        return matched.By(matcher, NULL_STRING);
    }

    /// <summary>
    /// Facilitates matching a value within a dictionary by arbitrary logic
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="matcher">Func containing custom logic: return true if the value meets expectations</param>
    /// <param name="customMessage">Custom message to add on failure</param>
    /// <typeparam name="T">Type of the dictionary value</typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IDictionaryValueMatched<T> matched,
        Func<T, bool> matcher,
        string customMessage
    )
    {
        return matched.By(matcher, () => customMessage);
    }

    /// <summary>
    /// Facilitates matching a value within a dictionary by arbitrary logic
    /// </summary>
    /// <param name="matched">Continuation to operate on</param>
    /// <param name="matcher">Func containing custom logic: return true if the value meets expectations</param>
    /// <param name="customMessageGenerator">Provides a custom message to add on failure</param>
    /// <typeparam name="T">Type of the dictionary value</typeparam>
    /// <returns></returns>
    public static IMore<T> By<T>(
        this IDictionaryValueMatched<T> matched,
        Func<T, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return matched.AddMatcher(actual =>
        {
            var passed = matcher(actual);
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () =>
                    {
                        if (matched.TryGetMetadata<string>(CONTINUATION_KEY, out var key))
                        {
                            // TODO: 
                            return $"Mismatch for value keyed by: {key.Stringify()}\nreceived value was:\n{actual.Stringify()}";
                        }
                        else
                        {
                            return $"Expected {passed.AsNot()}to find match for value, but none was found";
                        }
                    },
                    customMessageGenerator
                )
            );
        });
    }
}