using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class StringExtensions
    {
        public static IStringContainContinuation Contain(
            this IContinuation<string> continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }

        private static void AddContainsMatcherTo(
            IContinuation<string> continuation,
            string search
        )
        {
            continuation.AddMatcher(s =>
            {
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    MessageHelpers.MessageForContainsResult(
                        passed, s, search
                    )
                );
            });
        }

        public static IStringContainContinuation And(
            this IStringContainContinuation continuation,
            string search
        )
        {
            AddContainsMatcherTo(continuation, search);
            return new StringContainContinuation(continuation);
        }
    }

    public class StringContainContinuation : IStringContainContinuation,
        IExpectationContext<string>
    {
        private IContinuation<string> _continuation;
        private IExpectationContext<string> _expectationContext;

        public StringContainContinuation(IContinuation<string> continuation)
        {
            _continuation = continuation;
            _expectationContext = continuation as IExpectationContext<string>;
        }

        public void Negate()
        {
            _expectationContext.Negate();
        }

        public void RunMatcher(Func<string, IMatcherResult> matcher)
        {
            _expectationContext.RunMatcher(matcher);
        }

        IExpectationContext<string> IExpectationContext<string>.Parent
        {
            get => _expectationContext.Parent;
            set => _expectationContext.Parent = value;
        }
    }

    public interface IStringContainContinuation : IContinuation<string>
    {
    }
}