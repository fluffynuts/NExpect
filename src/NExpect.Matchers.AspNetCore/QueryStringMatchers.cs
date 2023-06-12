using System;
using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Http;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace NExpect
{
    /// <summary>
    /// Provides matchers for QueryStrings
    /// </summary>
    public static class QueryStringMatchers
    {
        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionTo<KeyValuePair<string, string>> to,
            QueryString queryString
        )
        {
            return to.Equal(queryString, NULL_STRING);
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionTo<KeyValuePair<string, string>> to,
            QueryString queryString,
            string customMessage
        )
        {
            return to.Equal(queryString, () => customMessage);
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionTo<KeyValuePair<string, string>> to,
            QueryString queryString,
            Func<string> customMessageGenerator
        )
        {
            return to.AddMatcher(
                actual =>
                {
                    var dict = queryString.AsDictionary();
                    var passed = dict.IsEquivalentTo(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected\n{actual.Stringify()}\nto be equivalent to\n{dict.Stringify()}",
                            customMessageGenerator
                        )
                    );
                }
            );
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionToAfterNot<KeyValuePair<string, string>> to,
            QueryString queryString
        )
        {
            return to.Equal(queryString, NULL_STRING);
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionToAfterNot<KeyValuePair<string, string>> to,
            QueryString queryString,
            string customMessage
        )
        {
            return to.Equal(queryString, () => customMessage);
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionToAfterNot<KeyValuePair<string, string>> to,
            QueryString queryString,
            Func<string> customMessageGenerator
        )
        {
            return to.AddMatcher(
                actual =>
                {
                    var dict = queryString.AsDictionary();
                    var passed = dict.IsEquivalentTo(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected\n{actual.Stringify()}\nnot to be equivalent to\n{dict.Stringify()}",
                            customMessageGenerator
                        )
                    );
                }
            );
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionNotAfterTo<KeyValuePair<string, string>> to,
            QueryString queryString
        )
        {
            return to.Equal(
                queryString,
                NULL_STRING
            );
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionNotAfterTo<KeyValuePair<string, string>> to,
            QueryString queryString,
            string customMessage
        )
        {
            return to.Equal(
                queryString,
                () => customMessage
            );
        }

        /// <summary>
        /// Tests equivalence of a KeyValuePair collection
        /// to a QueryString
        /// </summary>
        /// <param name="to"></param>
        /// <param name="queryString"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ICollectionMore<KeyValuePair<string, string>> Equal(
            this ICollectionNotAfterTo<KeyValuePair<string, string>> to,
            QueryString queryString,
            Func<string> customMessageGenerator
        )
        {
            return to.AddMatcher(
                actual =>
                {
                    var dict = queryString.AsDictionary();
                    var passed = dict.IsEquivalentTo(actual);
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected\n{actual.Stringify()}\nnot to be equivalent to\n{dict.Stringify()}",
                            customMessageGenerator
                        )
                    );
                }
            );
        }
    }
}