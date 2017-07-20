using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    public static class AddExpectationsExtensions
    {
        public static void AddMatcher<T>(
            this IContinuation<T> continuation,
            Func<T, IMatcherResult> matcher)
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher(
            this IExceptionMessageContinuation continuation,
            Func<string, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this INot<T> continuation,
            Func<T, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this IEqualityContinuation<T> continuation,
            Func<T, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this IBe<T> continuation,
            Func<T, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this IGreaterOrLessContinuation<T> continuation,
            Func<T, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this ICountMatchMatched<T> continuation,
            Func<T, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        private static void AddMatcherPrivate<T>(
            object continuation,
            Func<T, IMatcherResult> matcher)
        {
            var type = typeof(T);
            System.Diagnostics.Debug.WriteLine($"Adding matcher for type {type}");
            var asContext = continuation as IExpectationContext<T>;
            if (asContext == null)
            {
                throw new InvalidOperationException($"{continuation} does not implement IExpectationContext<T>");
            }
            asContext.RunMatcher(matcher);
        }
    }
}