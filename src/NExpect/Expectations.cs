using System;
using System.Collections.Generic;
using NExpect.Exceptions;
using NExpect.Implementations;
using NExpect.Interfaces;

namespace NExpect
{
    /// <summary>
    /// Provides the basic Expect() method. You should import this statically
    /// into your test fixture class file.
    /// </summary>
    public static class Expectations
    {
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
        /// Start an expectation with an action. Usually used to check
        /// if said action throws an exception
        /// </summary>
        /// <param name="action">Action to start with</param>
        /// <returns>IExpectation&lt;Action&gt;</returns>
        public static IExpectation<Action> Expect(Action action)
        {
            return new Expectation<Action>(action);
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
            return new Expectation<Action>(() => { 
                func();
            });
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
            T[] array
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
        /// Starts an expectation on a concrete Stack&lt;T&gt;
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
    }
}