using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Throw(this IContinuation<Action> src)
        {
            src.AddMatcher(fn => {
                MatcherResult result;
                try
                {
                    fn();
                    result = new MatcherResult(false, "Expected to throw an exception but none was thrown");
                }
                catch (Exception ex)
                {
                    result = new MatcherResult(true, $"Exception thrown:\n${ex.Message}\n${ex.StackTrace}");
                }
                return result;
            });
        }
    }
}
