using System;
using System.Collections.Generic;
using NExpect.Implementations;
using NExpect.Interfaces;

namespace NExpect.Extensions
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

    public interface ICollectionExpectation<T>
    {
        IEnumerable<T> Actual { get; }

        ICollectionTo<IEnumerable<T>> To { get; }
        ICollectionNot<IEnumerable<T>> Not { get; }
    }

    public class CollectionExpectation<T>:
        Expectation<IEnumerable<T>>, 
        ICollectionExpectation<T>
    {
        public CollectionExpectation(IEnumerable<T> actual)
            :base(actual)
        {
        }

        public ICollectionTo<IEnumerable<T>> To { get; }
        public ICollectionNot<IEnumerable<T>> Not { get; }
    }
}