using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Implementations
{
    /// <summary>
    /// Provides some helpers for formatting and creating failure messages
    /// </summary>
    public static class MessageHelpers
    {
        /// <summary>
        /// Provides an easy access to a null string value
        /// - use to disambiguate calls between the version which
        /// takes a static string and the version which takes a Func&lt;string&gt;
        /// </summary>
        internal const string NULL = null;
        
        /// <summary>
        /// Not to be confused with NULL, this is the string
        /// put in place of nulls within stringified messages
        /// </summary>
        internal const string NULL_REPLACER = "(null)";

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

        /// <summary>
        /// Creates a final message, given standard message parts and
        /// a custom message. When the parts, concatenated with a space,
        /// are longer than the expected max line length, they are split across lines.
        /// </summary>
        /// <param name="standardMessageParts"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static string FinalMessageFor(
            string[] standardMessageParts,
            string customMessage
        )
        {
            return FinalMessageFor(MakeMessage(standardMessageParts), customMessage);
        }

        private static string MakeMessage(params string[] templateParts)
        {
            var firstPass = templateParts.JoinWith(" ");
            return firstPass.Length > DetermineMaxLineLength()
                ? templateParts.JoinWith("\n")
                : firstPass;
        }


        /// <summary>
        /// Default max-width for an equality failure message line.
        /// When the message would run over this length, it will be split
        /// onto multiple lines for easier reading
        /// </summary>
        public const int DEFAULT_MAX_LINE_LENGTH = 72;

        private static int DetermineMaxLineLength()
        {
            var value = Environment.GetEnvironmentVariable("COLS") ??
                        Environment.GetEnvironmentVariable("MAX_LINE_LENGTH") ??
                        DEFAULT_MAX_LINE_LENGTH.ToString();
            return int.TryParse(value, out var result)
                ? result
                : DEFAULT_MAX_LINE_LENGTH;
        }

        /// <summary>
        /// Provides the "not" for a matcher message, based on the passed flag.
        /// When your matcher passes, you get "not ", so you can do, eg $"Expected foo {not}to be bar"
        /// When your matcher fails, you get an empty string, which also works for the example given.
        /// </summary>
        /// <param name="passed"></param>
        /// <returns></returns>
        public static string AsNot(this bool passed)
        {
            return passed ? "not " : "";
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
        /// Quotes a string or object, as required. Only non-null strings
        /// get quotes.
        /// </summary>
        /// <param name="o">Object to quote</param>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>String representation of object</returns>
        public static string Quote<T>(T o)
        {
            return Stringifier.Stringify(o, NULL_REPLACER);
        }

        /// <summary>
        /// Returns a collection as a comma-separated list
        /// </summary>
        /// <param name="collection">Collection to operate on</param>
        /// <typeparam name="T">Item type of collection</typeparam>
        /// <returns>Comma-separated list representing the collection</returns>
        public static string Stringify<T>(this IEnumerable<T> collection)
        {
            return collection == null
                ? NULL_REPLACER
                : $"[ {string.Join(", ", collection.Select(Quote))} ]";
        }

        /// <summary>
        /// Returns string with up to 10 elements from a collection with ellipsis if required
        /// </summary>
        /// <param name="collection">Collection to inspect</param>
        /// <typeparam name="T">Item type of collection</typeparam>
        /// <returns>Something like `[ "a", "b", "c" ]`</returns>
        public static string LimitedPrint<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return NULL_REPLACER;
            var asArray = collection.ToArray();
            var ellipsis = asArray.Length > 10
                ? " ..."
                : "";
            return $"[ {asArray.Take(10).Select(o => o.Stringify()).JoinWith(", ")}{ellipsis} ]";
        }

        /// <summary>
        /// Performs default stringification of an object
        /// </summary>
        /// <param name="item">Object to stringify</param>
        /// <typeparam name="T">Type of object to stringify</typeparam>
        /// <returns></returns>
        public static string Stringify<T>(this T item)
        {
            return Stringifier.Stringify(item, NULL);
        }
    }
}