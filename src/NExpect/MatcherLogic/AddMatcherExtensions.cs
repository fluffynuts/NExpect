using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    public static class AddMatcherExtensions
    {
        public static void AddMatcher<T>(
            this ICanAddMatcher<T> continuation,
            Func<T, IMatcherResult> matcher)
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this IExceptionPropertyContinuation<T> continuation,
            Func<string, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        public static void AddMatcher<T>(
            this ICanAddMatcher<IEnumerable<T>> continuation,
            Func<IEnumerable<T>, IMatcherResult> matcher
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