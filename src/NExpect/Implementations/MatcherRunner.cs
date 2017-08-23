using System;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    internal static class MatcherRunner
    {
        public static void RunMatcher<T>(
            T actual,
            bool negated,
            Func<T, IMatcherResult> matcher
        )
        {
            IMatcherResult result;
            try
            {
                result = matcher(actual);
                var isPass = negated ? !result.Passed : result.Passed;
                if (isPass)
                    return;
            }
            catch (Exception ex)
            {
                // TODO: make this better, ie, include the exception as an inner
                Assertions.Throw(ex.Message);
                return;
            }
            Assertions.Throw(result.Message);
        }
    }
}