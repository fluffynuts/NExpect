using System.Reflection;
using NExpect.Implementations;
using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    public static class CountMatchExtensionMethods
    {
        public static int GetExpectedCount<T>(
            this ICountMatchContinuation<T> continuation
        )
        {
            var actual = continuation as CountMatchContinuation<T>;
            if (actual != null)
                return actual.ExpectedCount;
            return continuation.TryGetPropertyValue<int?>("ExpectedCount") ?? 0;
        }

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