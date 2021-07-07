using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Implementations
{
    /// <summary>
    /// Provides a mechanism to switch the type of context, for when
    /// IMore&lt;T&gt; doesn't do what you want because you're interested
    /// in a sub-context. Example usage in NExpect.Matchers.AspNet where
    /// the original context is an HttpResponseMessage, but that is switched
    /// out to be a Cookie found by name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Next<T>
        : More<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public override bool? IsNegated
        {
            get => _parent.IsNegated();
            set 
            {
                var parentPropInfo = _parent?.GetType()?.GetProperty(nameof(IsNegated));
                parentPropInfo?.SetValue(_parent, value);
            }
        }
        private readonly IExpectationContext _parent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualFetcher"></param>
        /// <param name="parent"></param>
        public Next(
            Func<T> actualFetcher,
            IExpectationContext parent
        ) : base(actualFetcher)
        {
            _parent = parent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matcher"></param>
        public override IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher)
        {
            return MatcherRunner.RunMatcher(
                Actual,
                _parent.IsNegated(),
                matcher
            );
        }
    }
}