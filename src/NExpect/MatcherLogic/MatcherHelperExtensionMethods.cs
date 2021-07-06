using System;
using System.Linq;
using NExpect.Implementations;
using NExpect.Implementations.Collections;
using NExpect.Interfaces;
// ReSharper disable once RedundantUsingDirective
using Imported.PeanutButter.Utils;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Extension methods to help with creating matchers,
    /// primarily for getting to properties which would muddle intellisense
    /// during assertion use, but which are interesting when writing a matcher
    /// </summary>
    public static class MatcherHelperExtensionMethods
    {
        /// <summary>
        /// Acting on a continuation, will return the ExpectedCount for that
        /// continuation, either directly by casting to the internal concrete
        /// implementation, or, if you have your own implementation, looking, via
        /// reflection for the ExpectedCount property as an integer.
        /// Use like: var expected = {continuation}.GetExpectedCount();
        /// </summary>
        /// <param name="continuation">Continuation to act on.</param>
        /// <typeparam name="T">Underlying type of the continuation.</typeparam>
        /// <returns></returns>
        public static int GetExpectedCount<T>(
            this ICountMatch continuation
        )
        {
            switch (continuation)
            {
                case CountMatchContinuation<T> countMatch:
                    return countMatch.ExpectedCount;
                default:
                    return continuation.TryGetPropertyValue<int?>(
                        nameof(CountMatchContinuation<T>.ExpectedCount)
                    ) ?? 0;
            }
        }

        /// <summary>
        /// Acting on a continuation, will return the Method for that
        /// continuation, either directly by casting to the internal concrete
        /// implementation, or, if you have your own implementation, looking, via
        /// reflection for the Method property as an integer.
        /// Use like: var method = {continuation}.GetCountMatchMethod();
        /// </summary>
        /// <param name="continuation">Continuation to act on.</param>
        /// <typeparam name="T">Underlying type of the continuation.</typeparam>
        /// <returns></returns>
        public static CountMatchMethods GetCountMatchMethod<T>(
            this ICountMatchContinuation<T> continuation
        )
        {
            switch (continuation)
            {
                case CountMatchContinuation<T> countMatch:
                    return countMatch.Method;
                default:
                    return continuation.TryGetPropertyValue<CountMatchMethods?>(
                            nameof(CountMatchContinuation<T>.Method)) ??
                        CountMatchMethods.Exactly;
            }
        }

        /// <summary>
        /// Attempts to sniff the actual value off of a continuation
        /// </summary>
        /// <param name="matcher">Continuation to operate on</param>
        /// <typeparam name="T">Expected type of the Actual value</typeparam>
        /// <returns>Actual value, if available</returns>
        public static T GetActual<T>(this ICanAddMatcher<T> matcher)
        {
            // ReSharper disable once UsePatternMatching
            var explicitImpl = matcher as IHasActual<T>;
            return explicitImpl == null
                ? TryGetActual(matcher)
                : explicitImpl.Actual;
        }

        private static T TryGetActual<T>(ICanAddMatcher<T> matcher)
        {
            if (matcher == null)
            {
                throw new InvalidOperationException(
                    $"Can't get an Actual value from a NULL matcher"
                );
            }

            var prop = matcher?.GetType()
                .GetProperties()
                .FirstOrDefault(pi => pi.Name.ToLower() == "actual");
            if (prop == null)
            {
                throw new InvalidOperationException(
                    $"Failed to GetActual on type {typeof(T)}. GetActual only works on IHasActual<T> or objects with an 'Actual' property");
            }

            try
            {
                return (T) prop.GetValue(matcher);
            }
            catch
            {
                throw new InvalidOperationException(
                    @$"Unable to get Actual value matching type {
                            typeof(T)
                        } from {
                            matcher.Stringify()
                        }"
                );
            }
        }
    }
}