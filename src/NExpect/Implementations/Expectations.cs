using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    public static class Expectations
    {
        public static IExpectation<T> Expect<T>(T value)
        {
            return new Expectation<T>(value);
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
    }
}