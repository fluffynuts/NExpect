using System.Reflection;
using NExpect.Implementations;
using NExpect.Interfaces;

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
            this ICountMatchContinuation<T> continuation
        )
        {
            var actual = continuation as CountMatchContinuation<T>;
            if (actual != null)
                return actual.ExpectedCount;
            return continuation.TryGetPropertyValue<int?>("ExpectedCount") ?? 0;
        }

        /// <summary>
        /// Acting on a continuation, will return the CountMatchMethod for that
        /// continuation, either directly by casting to the internal concrete
        /// implementation, or, if you have your own implementation, looking, via
        /// reflection for the CountMatchMethod property as an integer.
        /// Use like: var method = {continuation}.GetCountMatchMethod();
        /// </summary>
        /// <param name="continuation">Continuation to act on.</param>
        /// <typeparam name="T">Underlying type of the continuation.</typeparam>
        /// <returns></returns>
        public static CountMatchMethods GetCountMatchMethod<T>(
            this ICountMatchContinuation<T> continuation
        )
        {
            var actual = continuation as CountMatchContinuation<T>;
            if (actual != null)
                return actual.CountMatchMethod;
            return continuation.TryGetPropertyValue<CountMatchMethods>("CountMatchMethod");
        }

        internal static T TryGetPropertyValue<T>(this object o, string prop)
        {
            var propInfo = o.GetType().GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
                return default(T);
            try
            {
                return (T) propInfo.GetValue(o);
            }
            catch
            {
                return default(T);
            }
        }
    }
}