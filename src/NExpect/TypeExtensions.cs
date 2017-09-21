using System;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using PeanutButter.Utils;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect
{
    /// <summary>
    /// Provides matchers for testing if object are instances of specific types
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Tests if actual is an instance of TExpected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <typeparam name="TExpected">Expected Type of the Instance</typeparam>
        public static void Of<TExpected>(this IInstanceContinuation instance)
        {
            Of<TExpected>(instance, null);
        }

        /// <summary>
        /// Tests if actual is an instance of TExpected
        /// </summary>
        /// <param name="instance">Instance to operate on</param>
        /// <param name="customMessage">Custom error message</param>
        /// <typeparam name="TExpected">Expected Type of the Instance</typeparam>
        public static void Of<TExpected>(this IInstanceContinuation instance, string customMessage)
        {
            instance.AddMatcher(expected =>
            {
                var theExpectedType = typeof(TExpected);
                var passed = theExpectedType.IsAssignableFrom(instance.Actual);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected <{instance.Actual.PrettyName()}> to {passed.AsNot()}be an instance of <{theExpectedType.PrettyName()}>",
                        customMessage
                    ));
            });
        }

        //TODO: find a better home for this method
        private static string PrettyName(this Type type)
        {
            if (type == null)
                return "(null Type)";
            if (type.IsGenericType())
            {
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var underlyingType = type.GetGenericArguments()[0];
                    return $"{PrettyName(underlyingType)}?";
                }
                var typeFyllName = type.FullName ?? string.Empty;
                var baseName = typeFyllName.Substring(0, typeFyllName.IndexOf("`", StringComparison.Ordinal));
                var parts = baseName.Split('.');
                return parts.Last() + "<" + string.Join(", ", type.GetGenericArguments().Select(PrettyName)) + ">";
            }
            else
                return type.FullName;
        }
    }
}