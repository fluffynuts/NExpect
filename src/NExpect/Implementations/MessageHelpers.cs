using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    public static class MessageHelpers
    {
        public static string FinalMessageFor(
            string standardMessage,
            string customMessage
        )
        {
            return string.IsNullOrWhiteSpace(customMessage) 
                ? standardMessage 
                : $"{customMessage}\n\n{standardMessage}";
        }

        internal static string MessageForContainsResult(
            bool passed,
            string src,
            string search
        )
        {
            return passed
                ? $"Expected {Quote(src)} not to contain {Quote(search)}"
                : $"Expected {Quote(src)} to contain {Quote(search)}";
        }

        internal static string MessageForMatchResult(
            bool passed,
            string src
        )
        {
            return passed
                ? $"Expected {Quote(src)} not to be matched"
                : $"Expected {Quote(src)} to be matched";
        }

        internal static string MessageForNotMatchResult(
            bool passed,
            string src
        )
        {
            return passed
                ? $"Expected {Quote(src)} to be matched"
                : $"Expected {Quote(src)} not to be matched";
        }

        internal static string MessageForNotContainsResult(
            bool passed,
            string src,
            string search
        )
        {
            return passed
                ? $"Expected {Quote(src)} to contain {Quote(search)}"
                : $"Expected {Quote(src)} not to contain {Quote(search)}";
        }

        public static string Quote(string str)
        {
            return str == null ? str : $"\"{str}\"";
        }

        public static string Quote<T>(T o)
        {
            if (o == null)
                return null;
            var asString = o as string;
            return asString == null ? o.ToString() : Quote(asString);
        }

        public static string Stringify<T>(IEnumerable<T> collection)
        {
            return collection == null ? NULL : string.Join(", ", collection.Select(o => Quote(o)));
        }

        private const string NULL = "(null)";

        public static string CollectionPrint<T>(IEnumerable<T> collection)
        {
            if (collection == null)
                return NULL;
            var asArray = collection.ToArray();
            var ellipsis = asArray.Length > 10
                ? " ..."
                : "";
            return $"[ {Stringify(asArray.Take(10))}{ellipsis} ]";
        }

    }
}