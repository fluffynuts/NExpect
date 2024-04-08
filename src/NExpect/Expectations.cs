using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Implementations.Collections;
using NExpect.Implementations.Strings;
using NExpect.Interfaces;
using NExpect.Shims;

// ReSharper disable UnusedMember.Global
[assembly: InternalsVisibleTo("NExpect.Matchers.NSubstitute")]
[assembly: InternalsVisibleTo("NExpect.Matchers.Xml")]
[assembly: InternalsVisibleTo("NExpect.Matchers.AspNetCore")]

namespace NExpect;

/// <summary>
/// Provides the basic Expect() method. You should import this statically
/// into your test fixture class file.
/// </summary>
public static partial class Expectations
{
    internal const string METADATA_KEY = "__ExpectationContext__";
    internal const string KEY_COMPARER = "key-comparer";

    /// <summary>
    /// Starts an expectation with a value. Usually used to
    /// check for equality or null.
    /// </summary>
    /// <param name="value">Value to start with.</param>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <returns>IExpectation&lt;T&gt;</returns>
    public static IExpectation<T> Expect<T>(T value)
    {
        return new Expectation<T>(value);
    }

    /// <summary>
    /// Starts an expectation with sbyte value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">SByte value to start with.</param>
    /// <returns>IExpectation&lt;longT&gt;</returns>
    public static IExpectation<long> Expect(sbyte value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with short value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">Short value to start with.</param>
    /// <returns>IExpectation&lt;longT&gt;</returns>
    public static IExpectation<long> Expect(short value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with integer value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">Int value to start with.</param>
    /// <returns>IExpectation&lt;longT&gt;</returns>
    public static IExpectation<long> Expect(int value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with integer value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">Int value to start with.</param>
    /// <returns>IExpectation&lt;longT&gt;</returns>
    public static IExpectation<long> Expect(long value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with decimal value. Usually used to check for equality.
    /// </summary>
    /// <param name="value">Int value to start with.</param>
    /// <returns>IExpectation&lt;longT&gt;</returns>
    public static IExpectation<decimal> Expect(decimal value)
    {
        return new Expectation<decimal>(value);
    }

    /// <summary>
    /// Starts an expectation with byte value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">Byte value to start with.</param>
    /// <returns>IExpectation&lt;long&gt;</returns>
    public static IExpectation<long> Expect(byte value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with unsigned short value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">UShort value to start with.</param>
    /// <returns>IExpectation&lt;long&gt;</returns>
    public static IExpectation<long> Expect(ushort value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with unsigned integer value and up casts to long. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">UInt value to start with.</param>
    /// <returns>IExpectation&lt;long&gt;</returns>
    public static IExpectation<long> Expect(uint value)
    {
        return new Expectation<long>(value);
    }

    /// <summary>
    /// Starts an expectation with float value and up casts to double. Usually used to
    /// check for equality.
    /// </summary>
    /// <param name="value">Float value to start with.</param>
    /// <returns>IExpectation&lt;double&gt;</returns>
    public static IExpectation<double> Expect(float value)
    {
        return new Expectation<double>(value);
    }

    /// <summary>
    /// Starts a string-specific expectation
    /// </summary>
    /// <param name="value">Actual value to test</param>
    /// <returns></returns>
    public static IStringExpectation Expect(string value)
    {
        return new StringExpectation(value);
    }

    /// <summary>
    /// Start an expectation with an action. Usually used to check
    /// if said action throws an exception
    /// </summary>
    /// <param name="action">Action to start with</param>
    /// <returns>IExpectation&lt;Action&gt;</returns>
    public static IActionExpectation Expect(Action action)
    {
        return new ActionExpectation(action);
    }

    /// <summary>
    /// Start an expectation with a Func. Usually to check
    /// if said action throws an exception
    /// </summary>
    /// <param name="func">Func to start the expectation with</param>
    /// <typeparam name="T">Return type of the Func (discarded)</typeparam>
    /// <returns>IExpectation&lt;Action&gt;</returns>
    public static IExpectation<Action> Expect<T>(Func<T> func)
    {
        return new Expectation<Action>(
            () =>
            {
                var result = func();
                if (!(result is Task taskResult))
                {
                    return;
                }

                taskResult.ConfigureAwait(false);
                try
                {
                    var maxWait = NExpectEnvironment.TaskTimeoutMs;
                    var waitCompleted = taskResult.Wait(maxWait);
                    if (!waitCompleted)
                    {
                        throw new UnmetExpectationException(
                            new[]
                            {
                                $"Waited {maxWait}ms for task to complete.",
                                "If this is too short, consider adjusing environment ",
                                $"variable {NExpectEnvironment.Variables.TASK_TIMEOUT_MS} to suit your needs. ",
                                "If this doesn't help, please log a bug: async/await can",
                                "be fickle"
                            }.JoinWith(" ")
                        );
                    }
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerExceptions.First();
                }
            }
        );
    }

    /// <summary>
    /// Starts an expectation on an IEnumerable&lt;T&gt;
    /// </summary>
    /// <param name="collection">Collection to start with</param>
    /// <typeparam name="T">Item type of the array</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        IEnumerable<T> collection
    )
    {
        return new CollectionExpectation<T>(collection);
    }

    /// <summary>
    /// Starts an expectation on an IOrderedEnumerable&lt;T&gt;
    /// </summary>
    /// <param name="collection">Collection to start with</param>
    /// <typeparam name="T">Item type of the array</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        IOrderedEnumerable<T> collection
    )
    {
        return new CollectionExpectation<T>(collection);
    }

    // Have to provide collection-specific overloads because
    //  the Expect<T> above will be selected in preference
    //  to any non-explicitly supported collection type
    /// <summary>
    /// Start an expectation on an Array
    /// </summary>
    /// <param name="array">Array to start with</param>
    /// <typeparam name="T">Item type of the array</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        params T[] array
    )
    {
        return new CollectionExpectation<T>(array);
    }

    /// <summary>
    /// Starts an expectation on a concrete List&lt;T&gt;
    /// </summary>
    /// <param name="list">List to start with</param>
    /// <typeparam name="T">Item type of the list</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        List<T> list
    )
    {
        return new CollectionExpectation<T>(list);
    }

    /// <summary>
    /// Starts an expectation on an IList&lt;T&gt;
    /// </summary>
    /// <param name="list">List to start with</param>
    /// <typeparam name="T">Item type of the list</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        IList<T> list
    )
    {
        return new CollectionExpectation<T>(list);
    }

    /// <summary>
    /// Starts an expectation on an IList&lt;T&gt;
    /// </summary>
    /// <param name="collection">List to start with</param>
    /// <typeparam name="T">Item type of the list</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        ICollection<T> collection
    )
    {
        return new CollectionExpectation<T>(collection);
    }

    /// <summary>
    /// Start an expectation on an IReadOnlyCollection
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<T> Expect<T>(
        IReadOnlyCollection<T> collection
    )
    {
        return new CollectionExpectation<T>(collection);
    }

    /// <summary>
    /// Start an expectation on a concrete ReadOnlyCollection
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<T> Expect<T>(
        ReadOnlyCollection<T> collection
    )
    {
        return Expect(collection as IReadOnlyCollection<T>);
    }

    /// <summary>
    /// Starts an expectation on a concrete Queue&lt;T&gt;
    /// </summary>
    /// <param name="collection">Queue to start with</param>
    /// <typeparam name="T">Item type of the queue</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        Queue<T> collection
    )
    {
        return new CollectionExpectation<T>(collection);
    }

    /// <summary>
    /// Starts an expectation on a concrete Stack&lt;T&gt;
    /// </summary>
    /// <param name="stack">Stack to start with</param>
    /// <typeparam name="T">Item type of the stack</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        Stack<T> stack
    )
    {
        return new CollectionExpectation<T>(stack);
    }

    /// <summary>
    /// Starts an expectation on a concrete HashSet&lt;T&gt;
    /// </summary>
    /// <param name="hashSet">HashSet to start with</param>
    /// <typeparam name="T">Item type of the HashSet</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<T> Expect<T>(
        HashSet<T> hashSet
    )
    {
        return new CollectionExpectation<T>(hashSet);
    }

    /// <summary>
    /// Starts an expectation on any ISet&lt;T&gt;
    /// </summary>
    /// <param name="set"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<T> Expect<T>(
        ISet<T> set
    )
    {
        return new CollectionExpectation<T>(set);
    }

    /// <summary>
    /// Starts an expectation on a concrete KeyCollection from a Dictionary
    /// </summary>
    /// <param name="keys">KeyCollection to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<TKey> Expect<TKey, TValue>(
        Dictionary<TKey, TValue>.KeyCollection keys
    )
    {
        return new CollectionExpectation<TKey>(keys.ToArray());
    }

    /// <summary>
    /// Starts an expectation on a concrete KeyCollection from a Dictionary
    /// </summary>
    /// <param name="values">KeyCollection to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns>ICollectionExpectation&lt;T&gt;</returns>
    public static ICollectionExpectation<TValue> Expect<TKey, TValue>(
        Dictionary<TKey, TValue>.ValueCollection values
    )
    {
        return new CollectionExpectation<TValue>(values.ToArray());
    }

    /// <summary>
    /// Starts an expectation on a concrete Dictionary
    /// </summary>
    /// <param name="dictionary">Dictionary to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
        Expect<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        dictionary?.SetMetadata(KEY_COMPARER, dictionary.Comparer);
        return new CollectionExpectation<KeyValuePair<TKey, TValue>>(dictionary);
    }


    /// <summary>
    /// Starts an expectation on a concrete Dictionary
    /// </summary>
    /// <param name="dictionary">Dictionary to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
        Expect<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary)
    {
        return new CollectionExpectation<KeyValuePair<TKey, TValue>>(dictionary);
    }

    /// <summary>
    /// Starts an expectation on something implementing IDictionary&lt;TKey,TValue&gt;
    /// </summary>
    /// <param name="dictionary"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
        Expect<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
        return new CollectionExpectation<KeyValuePair<TKey, TValue>>(
            dictionary
        );
    }

    /// <summary>
    /// Starts an expectation on a concrete ReadOnlyDictionary
    /// </summary>
    /// <param name="dictionary">Dictionary to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
        Expect<TKey, TValue>(ReadOnlyDictionary<TKey, TValue> dictionary)
    {
        return Expect(dictionary as IReadOnlyDictionary<TKey, TValue>);
    }

    /// <summary>
    /// Starts an expectation on an IReadOnlyDictionary
    /// </summary>
    /// <param name="dictionary">Dictionary to start with</param>
    /// <typeparam name="TKey">Key type of the dictionary</typeparam>
    /// <typeparam name="TValue">Value type of the dictionary</typeparam>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
        Expect<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary)
    {
        return new CollectionExpectation<KeyValuePair<TKey, TValue>>(
            dictionary
        );
    }

    /// <summary>
    /// Starts an expectation on a NameValueCollection
    /// <param name="collection">NameValueCollection to start with</param>
    /// </summary>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, string>>
        Expect(NameValueCollection collection)
    {
        return new CollectionExpectation<KeyValuePair<string, string>>(
            new DictionaryShim(collection)
        );
    }

    /// <summary>
    /// Starts an expectation on a KeysCollection
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static ICollectionExpectation<string>
        Expect(NameObjectCollectionBase.KeysCollection keys)
    {
        return new CollectionExpectation<string>(keys.Cast<string>());
    }
}