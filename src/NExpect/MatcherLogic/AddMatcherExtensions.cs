using System;
using System.Collections.Generic;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Provides extension methods to add matchers to continuations which support it
    /// </summary>
    public static class AddMatcherExtensions
    {
        /// <summary>
        /// Most general matcher add - onto ICanAddMatcher&lt;T&gt;
        /// </summary>
        /// <param name="continuation">Continuation to add matcher to</param>
        /// <param name="matcher">Matcher to run</param>
        /// <typeparam name="T">Type of the object under test</typeparam>
        public static void AddMatcher<T>(
            this ICanAddMatcher<T> continuation,
            Func<T, IMatcherResult> matcher)
        {
            AddMatcherPrivate(continuation, matcher);
        }

        /// <summary>
        /// Add a matcher onto an Exception property continuation
        /// </summary>
        /// <param name="continuation">Continuation to add matcher to</param>
        /// <param name="matcher">Matcher to run</param>
        /// <typeparam name="T">Type of the object under test</typeparam>
        public static void AddMatcher<T>(
            this IExceptionPropertyContinuation<T> continuation,
            Func<string, IMatcherResult> matcher
        )
        {
            AddMatcherPrivate(continuation, matcher);
        }

        /// <summary>
        /// Add a matcher onto a Collection continuation
        /// </summary>
        /// <param name="continuation">Continuation to add matcher to</param>
        /// <param name="matcher">Matcher to run</param>
        /// <typeparam name="T">Type of the object under test</typeparam>
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