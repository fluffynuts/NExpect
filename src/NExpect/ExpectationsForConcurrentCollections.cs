using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;

namespace NExpect
{
    public static partial class Expectations
    {
        /// <summary>
        /// Starts a collection expectation on a ConcurrentQueue
        /// </summary>
        /// <param name="queue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollectionExpectation<T> Expect<T>(
            ConcurrentQueue<T> queue
        )
        {
            return Expect(queue.AsEnumerable());
        }

        /// <summary>
        /// Starts a collection expectation on a ConcurrentBag
        /// </summary>
        /// <param name="queue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollectionExpectation<T> Expect<T>(
            ConcurrentBag<T> queue
        )
        {
            return Expect(queue.AsEnumerable());
        }

        /// <summary>
        /// Starts an expectation on a ConcurrentDictionary
        /// </summary>
        /// <param name="dictionary">Dictionary to start with</param>
        /// <typeparam name="TKey">Key type of the dictionary</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary</typeparam>
        /// <returns></returns>
        public static ICollectionExpectation<KeyValuePair<TKey, TValue>>
            Expect<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary)
        {
            return Expect(dictionary.AsEnumerable());
        }

        /// <summary>
        /// Starts an expectation on a ConcurrentStack
        /// </summary>
        /// <param name="stack"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICollectionExpectation<T> Expect<T>(
            ConcurrentStack<T> stack
        )
        {
            return Expect(stack.AsEnumerable());
        }
    }
}