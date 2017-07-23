using System;
using System.Collections.Generic;
using NExpect.Implementations;
using NExpect.Interfaces;

namespace NExpect
{
    public static class Expectations
    {
        public static IExpectation<T> Expect<T>(T value)
        {
            return new Expectation<T>(value);
        }

        public static IExpectation<int> Expect(int value)
        {
            return new Expectation<int>(value);
        }

        public static IExpectation<Action> Expect(Action action)
        {
            return new Expectation<Action>(action);
        }

        public static ICollectionExpectation<T> Expect<T>(
            IEnumerable<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        // Have to provide collection-specific overloads because
        //  the Expect<T> above will be selected in preference
        //  to any non-explicitly supported collection type
        public static ICollectionExpectation<T> Expect<T>(
            T[] collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            List<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            IList<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            ICollection<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            Queue<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            Stack<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

        public static ICollectionExpectation<T> Expect<T>(
            HashSet<T> collection
        )
        {
            return new CollectionExpectation<T>(collection);
        }

    }
}