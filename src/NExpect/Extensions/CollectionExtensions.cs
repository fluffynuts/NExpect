using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class CollectionExtensions
    {
        public static ICountMatchContinuation<IEnumerable<T>> Exactly<T>(
            this IContain<IEnumerable<T>> contain,
            int howMany
        )
        {
            if (contain == null)
                throw new ArgumentNullException(nameof(contain),
                    $"Exactly<T>() cannot extend null IContain<IEnumerable<{typeof(T).Name}>>");
            return new CountMatchContinuation<IEnumerable<T>>(
                contain, CountMatchMethods.Exactly, howMany
            );
        }

        public static void To<T>(
            this ICountMatchEquals<IEnumerable<T>> countMatch,
            T search)
        {
            if (countMatch == null)
                throw new ArgumentNullException(nameof(countMatch),
                    $"EqualTo<T> cannot extend null IContinuation<IEnumerable<{typeof(T)}>>");
            countMatch.Continuation.AddMatcher(collection =>
            {
                var have = collection.Count(o => o.Equals(search));
                var passed = CollectionCountMatchStrategies[countMatch.Method](have, countMatch.Compare);
                var message =
                    CollectionCountMessageStrategies[countMatch.Method](passed, search, have, countMatch.Compare);

                return new MatcherResult(
                    passed,
                    message
                );
            });
        }

        private static readonly Dictionary<CountMatchMethods,
            Func<bool, object, int, int, string>> CollectionCountMessageStrategies =
            new Dictionary<CountMatchMethods, Func<bool, object, int, int, string>>()
            {
                [CountMatchMethods.Exactly] = CreateMessageFor("exactly"),
                [CountMatchMethods.AtLeast] = CreateMessageFor("at least"),
                [CountMatchMethods.AtMost] = CreateMessageFor("at most")
            };

        private static readonly Dictionary<CountMatchMethods,
            Func<int, int, bool>> CollectionCountMatchStrategies =
            new Dictionary<CountMatchMethods, Func<int, int, bool>>()
            {
                [CountMatchMethods.Exactly] = (have, want) => have == want,
                [CountMatchMethods.AtLeast] = (have, want) => have >= want,
                [CountMatchMethods.AtMost] = (have, want) => have <= want
            };

        private static Func<bool, object, int, int, string> CreateMessageFor(
            string context
        )
        {
            Func<bool, object, int, int, string> result = 
                (passed, search, have, want) =>
                    passed
                    ? CreatePassMessageFor(context, search, have, want)
                    : CreateFailedMessageFor(context, search, have, want);
            return result;
        }

        private static string CreateFailedMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1 ? "" : "s";
            return $"Expected to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

        private static string CreatePassMessageFor(
            string comparison,
            object search,
            int have,
            int want
        )
        {
            var s = want == 1 ? "" : "s";
            return $"Expected not to find {comparison} {want} occurrence{s} of {search} but found {have}";
        }

    }

    public interface ICountMatchContinuation<T>
    {
        ICountMatchEquals<T> Equal { get; }
    }

    public interface ICountMatchEquals<T>
    {
        IContinuation<T> Continuation { get; }
        CountMatchMethods Method { get; }
        int Compare { get; }
    }

    public enum CountMatchMethods
    {
        Exactly,
        AtLeast,
        AtMost
    }

    internal class CountMatchContinuation<T>
        : ExpectationContext<IEnumerable<T>>, ICountMatchContinuation<T>
    {
        private int _compare;
        private CountMatchMethods _method;
        private IContinuation<T> _wrapped;

        public ICountMatchEquals<T> Equal =>
            new CountMatchEquals<T>(
                _wrapped,
                _method,
                _compare
            );


        public CountMatchContinuation(
            IContinuation<T> wrapped,
            CountMatchMethods method,
            int compare
        )
        {
            _wrapped = wrapped;
            _method = method;
            _compare = compare;
        }
    }

    internal class CountMatchEquals<T>
        : ICountMatchEquals<T>
    {
        public IContinuation<T> Continuation { get; }
        public CountMatchMethods Method { get; }
        public int Compare { get; }

        public CountMatchEquals(
            IContinuation<T> continuation,
            CountMatchMethods method,
            int compare)
        {
            Continuation = continuation;
            Method = method;
            Compare = compare;
        }
    }


    internal class EnumerableContinuation<T> :
        ExpectationContext<IEnumerable<T>>,
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
            SetParent(parent as IExpectationContext<IEnumerable<T>>);
            Exactly = exactly;
            AtLeast = atLeast;
            AtMost = atMost;
        }
    }
}