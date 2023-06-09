using NExpect.Interfaces;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// Provides a mechanism to terminate NExpect syntax
    /// without triggering the incomplete assertions.
    /// This is an escape hatch and you should only use it
    /// if absolutely necessary - there's probably a better
    /// way to write your custom assertions.
    /// </summary>
    public static class TerminationExtensions
    {
        /// <summary>
        /// Terminates the continuation so that it won't trigger
        /// incomplete-assertion errors. However, this should be
        /// used as a last resort - chances are good you can rewrite
        /// your custom assertion differently so as not to use this.
        /// </summary>
        /// <param name="continuation"></param>
        /// <typeparam name="T"></typeparam>
        public static void Terminate<T>(this ICanAddMatcher<T> continuation)
        {
            Assertions.Forget(continuation);
        }
    }
}