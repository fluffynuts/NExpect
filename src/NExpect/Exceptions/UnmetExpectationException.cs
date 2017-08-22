using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NExpect.Exceptions
{
    /// <summary>
    /// Mimicks the NUnit AssertionException if it cannot be dynamically located
    /// </summary>
    public class UnmetExpectationException : Exception
    {
        /// <summary>
        /// Provides a stack trace up to the point where the expectation failed
        /// </summary>
        public override string StackTrace => GetEditedStackTrace();

        private string GetEditedStackTrace()
        {
            var stackTrace = new StackTrace(this, true);
            var reverseFrames = stackTrace.GetFrames().Reverse();
            var hitThisAssembly = false;
            var interestingFrames = reverseFrames.Aggregate(
                new List<StackFrame>(), (acc, cur) =>
                {
                    if (hitThisAssembly)
                    {
                        return acc;
                    }
                    if (cur.IsFromOrReferencesThisAssembly())
                    {
                        hitThisAssembly = true;
                    }
                    else
                        acc.Add(cur);
                    return acc;
                });
            return string.Join("",
                interestingFrames
                    .Select(f => new StackTrace(f))
                    .Select(f => f.ToString())
                    .Reverse()
            );
        }

        /// <summary>
        /// Constructs the exception
        /// </summary>
        /// <param name="message">Message to display</param>
        internal UnmetExpectationException(
            string message
        ) : base(message)
        {
        }
    }

    internal static class StackFrameExtensions
    {
        private static readonly Assembly ThisAssembly =
            typeof(UnmetExpectationException).Assembly;

        internal static bool IsFromOrReferencesThisAssembly(
            this StackFrame frame
        )
        {
            var methodInfo = frame.GetMethod();
            if (methodInfo.DeclaringType.Assembly == ThisAssembly)
                return true;
            var parameters = methodInfo.GetParameters();
            return parameters.Any(p => p.ParameterType.Assembly == ThisAssembly);
        }
    }
}