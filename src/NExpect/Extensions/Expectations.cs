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

        ICollectionTo<T> To { get; }
    }

    public class CollectionExpectation<T> :
        Expectation<IEnumerable<T>>,
        ICollectionExpectation<T>
    {
        public CollectionExpectation(IEnumerable<T> actual)
            : base(actual)
        {
        }

        public ICollectionTo<T> To => 
            Factory.Create<IEnumerable<T>, CollectionTo<T>>(Actual, this);
        public ICollectionNot<T> Not { get; }
    }

    public class CollectionNot<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionNot<T>
    {
        public IEnumerable<T> Actual { get; }

        public CollectionNot(IEnumerable<T> actual)
        {
            Actual = actual;
        }

        public IContain<IEnumerable<T>> Contain =>
            Factory.Create<IEnumerable<T>, CollectionContain<T>>(Actual, this);

        public ICollectionToAfterNot<T> To { get; }
    }

    public class CollectionContain<T> :
        ExpectationContext<IEnumerable<T>>,
        IContain<IEnumerable<T>>
    {
    }

    public class CollectionTo<T> :
        ExpectationContext<IEnumerable<T>>,
        ICollectionTo<T>
    {
        public IEnumerable<T> Actual { get; }

        public IContain<IEnumerable<T>> Contain 
            => Factory.Create<IEnumerable<T>, Contain<IEnumerable<T>>>(Actual, this);

        public CollectionTo(IEnumerable<T> actual)
        {
            Actual = actual;
        }
    }
}