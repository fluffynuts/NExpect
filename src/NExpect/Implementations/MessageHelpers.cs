using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    /// <summary>
    /// Provides some helpers for formatting and creating failure messages
    /// </summary>
    public static class MessageHelpers
    {
        /// <summary>
        /// Provides a final message, given a standard message and
        /// a custom message - if the custom message is null, the standard
        /// message alone is returned
        /// </summary>
        /// <param name="standardMessage">Message which should always be shown at the point of failure</param>
        /// <param name="customMessage">Optional custom message to show if not null</param>
        /// <returns>Final message derived from the two parameters</returns>
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

        /// <summary>
        /// Quotes a string if not null
        /// </summary>
        /// <param name="str">String to quote</param>
        /// <returns>Quoted string, if not null</returns>
        public static string Quote(string str)
        {
            return str == null ? null : $"\"{str}\"";
        }

        /// <summary>
        /// Quotes a string or object, as required. Only non-null strings
        /// get quotes.
        /// </summary>
        /// <param name="o">Object to quote</param>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>String representation of object</returns>
        public static string Quote<T>(T o)
        {
            if (o == null)
                return null;
            var asString = o as string;
            return asString == null ? o.ToString() : Quote(asString);
        }

        /// <summary>
        /// Returns a collection as a comma-separated list
        /// </summary>
        /// <param name="collection">Collection to operate on</param>
        /// <typeparam name="T">Item type of collection</typeparam>
        /// <returns>Comma-separated list representing the collection</returns>
        public static string Stringify<T>(IEnumerable<T> collection)
        {
            return collection == null 
                    ? Null : 
                    string.Join(", ", collection.Select(Quote));
        }

        private const string Null = "(null)";

        /// <summary>
        /// Returns string with up to 10 elements from a collection with ellipsis if required
        /// </summary>
        /// <param name="collection">Collection to inspect</param>
        /// <typeparam name="T">Item type of collection</typeparam>
        /// <returns>Something like `[ "a", "b", "c" ]`</returns>
        public static string CollectionPrint<T>(IEnumerable<T> collection)
        {
            if (collection == null)
                return Null;
            var asArray = collection.ToArray();
            var ellipsis = asArray.Length > 10
                ? " ..."
                : "";
            return $"[ {Stringify(asArray.Take(10))}{ellipsis} ]";
        }

    }
}