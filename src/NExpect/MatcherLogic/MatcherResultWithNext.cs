using System;
using NExpect.Helpers;

namespace NExpect.MatcherLogic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNext"></typeparam>
    public class MatcherResultWithNext<TNext>
        : MatcherResult, IMatcherResultWithNext<TNext>
    {
        /// <inheritdoc />
        public Func<TNext> NextFetcher { get; }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<TNext> nextFetcher
        )
            : base(passed)
        {
            NextFetcher = FuncFactory.Memoize(nextFetcher);
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            string message,
            Func<TNext> nextFetcher
        ) : base(passed, message)
        {
            NextFetcher = nextFetcher;
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<string> messageGenerator,
            Func<TNext> nextFetcher
        ) : base(passed, messageGenerator)
        {
            NextFetcher = nextFetcher;
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<Func<string>> messageGeneratorGenerator,
            Func<TNext> nextFetcher
        ) : base(passed, messageGeneratorGenerator)
        {
            NextFetcher = nextFetcher;
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<string> messageGenerator,
            Exception localException,
            Func<TNext> nextFetcher
        ) : base(passed, messageGenerator, localException)
        {
            NextFetcher = nextFetcher;
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<string> regularMessageGenerator,
            Func<string> customMessageGenerator,
            Func<TNext> nextFetcher
        ) : base(passed, regularMessageGenerator, customMessageGenerator)
        {
            NextFetcher = nextFetcher;
        }

        /// <inheritdoc />
        public MatcherResultWithNext(
            bool passed,
            Func<Func<string>> messageGeneratorGenerator,
            Exception localException,
            Func<TNext> nextFetcher
        ) : base(passed, messageGeneratorGenerator, localException)
        {
        }
    }
}