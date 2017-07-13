using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NExpect.Interfaces;
using NExpect.MatcherLogic;

namespace NExpect.Extensions
{
    public static class CollectionExtensions
    {
        public static void Contain(
            this IContinuation<string> continuation,
            string search
        )
        {
            continuation.AddMatcher(s =>
            {
                var passed = s?.Contains(search) ?? false;
                return new MatcherResult(
                    passed,
                    StringExpectationMethods.MessageForContainsResult(
                        passed, s, search
                    )
                );
            });
        }
    }
}
