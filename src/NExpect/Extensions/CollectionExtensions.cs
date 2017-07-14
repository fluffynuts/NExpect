using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class CollectionExtensions
    {
        public static void EqualTo<T>(
            this IContinuation<IEnumerable<T>> continuation,
            T search
        )
        {
            continuation.AddMatcher(collection =>
            {
                var passed = collection.Contains(search);
                return new MatcherResult(
                    passed,
                    MessageHelpers.CollectionContainsItemMessageFor(passed, search, collection)
                );
            });
        }

        public static IContinuation<IEnumerable<T>> Exactly<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            return new EnumerableContinuation<T>(contain, howMany, null, null);
        }
    }

    internal class EnumerableContinuation<T> :
        ExpectationContext<T>,
        IContinuation<IEnumerable<T>>
    {
        public int? Exactly { get; }
        public int? AtLeast { get; }
        public int? AtMost { get; }

        public EnumerableContinuation(
            IContain<IEnumerable<T>> parent,
            int? exactly,
            int? atLeast,
            int? atMost
        )
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            SetParent(parent as IExpectationContext<T>);
            Exactly = exactly;
            AtLeast = atLeast;
            AtMost = atMost;
        }
    }
}