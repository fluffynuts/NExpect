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
        ICollectionTo<T> To { get; }
        ICollectionNot<T> Not { get; }
    }

    internal class CollectionExpectation<T> :
        Expectation<IEnumerable<T>>,
        ICollectionExpectation<T>
    {
        public CollectionExpectation(IEnumerable<T> actual)
            : base(actual)
        {
        }

        public ICollectionTo<T> To =>
            Factory.Create<IEnumerable<T>, CollectionTo<T>>(Actual, this);

        public ICollectionNot<T> Not =>
            Factory.Create<IEnumerable<T>, CollectionNot<T>>(Actual, this);
    }

    internal class CollectionNot<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionNot<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionNot(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }

        public ICollectionToAfterNot<T> To =>
            Factory.Create<IEnumerable<T>, CollectionToAfterNot<T>>(Actual, this);
    }

    internal class CollectionToAfterNot<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionToAfterNot<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain =>
            Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public CollectionToAfterNot(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }

    internal class CollectionTo<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionTo<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain
            => Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public ICollectionNotAfterTo<T> Not =>
            Factory.Create<IEnumerable<T>, CollectionNotAfterTo<T>>(Actual, this);

        public CollectionTo(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }

    internal class CollectionNotAfterTo<T>
        : ExpectationContext<IEnumerable<T>>,
            ICollectionNotAfterTo<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain =>
            Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public CollectionNotAfterTo(IEnumerable<T> actual)
        {
            Actual = actual;
            Negate();
        }
    }
}