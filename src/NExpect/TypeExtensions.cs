using System;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using PeanutButter.Utils;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable UnusedMethodReturnValue.Global

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

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this INotAfterTo<Type> not
        )
        {
            return not.Implement<TInterface>(null);
        }

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="not">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this INotAfterTo<Type> not,
            string customMessage
        )
        {
            return not.AddImplementsMatcher<TInterface>(customMessage);
        }

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this IToAfterNot<Type> to
        )
        {
            return to.Implement<TInterface>(null);
        }

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this IToAfterNot<Type> to,
            string customMessage
        )
        {
            return to.AddImplementsMatcher<TInterface>(customMessage);
        }

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this ITo<Type> to
        )
        {
            return to.Implement<TInterface>(null);
        }

        /// <summary>
        /// Expectes that the Actual type implements the interface provided
        /// as a generic parameter
        /// </summary>
        /// <param name="to">Continuation to operate on</param>
        /// <param name="customMessage">Custom message to add to failure messages</param>
        /// <typeparam name="TInterface">Interface type to look for</typeparam>
        /// <returns></returns>
        public static IMore<Type> Implement<TInterface>(
            this ITo<Type> to,
            string customMessage
        )
        {
            return to.AddImplementsMatcher<TInterface>(customMessage);
        }

        private static IMore<Type> AddImplementsMatcher<TInterface>(
            this ICanAddMatcher<Type> addTo,
            string customMessage
        )
        {
            addTo.AddMatcher(actual =>
            {
                var interfaces = actual?.GetAllImplementedInterfaces() ?? new Type[0];
                var expected = typeof(TInterface);
                var passed = interfaces.Contains(expected);
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        $"Expected {actual} {passed.AsNot()}to implement {expected}",
                        customMessage
                    )
                );
            });
            return addTo.More();
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