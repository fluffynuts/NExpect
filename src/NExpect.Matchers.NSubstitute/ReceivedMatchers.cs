using System;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using NSubstitute;
// ReSharper disable InvokeAsExtensionMethod

namespace NExpect
{
    /// <summary>
    /// Provides NSubstitute extensions for NExpect
    /// </summary>
    public static class ReceivedMatchers
    {
        /// <summary>
        /// Returns NSubstitute Received()
        /// </summary>
        /// <param name="have"></param>
        /// <typeparam name="T"></typeparam>
        public static T Received<T>(this IHave<T> have) where T: class
        {
            var actual = have.GetActual();
            var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
            return context.IsNegated() 
                ? SubstituteExtensions.DidNotReceive(actual) 
                : SubstituteExtensions.Received(actual);
        }

        /// <summary>
        /// Returns NSubstitute Received(count)
        /// </summary>
        /// <param name="have"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        public static T Received<T>(this IHave<T> have, int count) where T : class
        {
            var actual = have.GetActual();
            var context = actual.GetMetadata<IExpectationContext>(Expectations.METADATA_KEY);
            if (context.IsNegated())
            {
                throw new NotSupportedException($"Negation of numbered Receive(N) expectations is not supported! (What would it mean, anyway?)");
            }

            return SubstituteExtensions.Received(have.GetActual(), count);
        }
    }
}