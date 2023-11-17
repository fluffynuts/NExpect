using System;
using System.IO;
using System.Text;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
using Imported.PeanutButter.Utils;

namespace NExpect
{
    /// <summary>
    /// Provides matchers for stream objects
    /// </summary>
    public static class StreamMatchers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="be"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Empty<T>(
            this IBe<T> be
        ) where T : Stream
        {
            return be.Empty(NULL_STRING);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="be"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Empty<T>(
            this IBe<T> be,
            string customMessage
        ) where T : Stream
        {
            return be.Empty(() => customMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="be"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Empty<T>(
            this IBe<T> be,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return be.AddMatcher(
                actual =>
                {
                    var passed = actual.Length == 0;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected stream {passed.AsNot()}to be empty",
                            customMessageGenerator
                        )
                    );
                }
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming that stream is UTF8 text
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek
        ) where T : Stream
        {
            return to.Contain(seek, Encoding.UTF8);
        }

        /// <summary>
        /// Looks for a string in a stream, decoding the stream with
        /// the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            Encoding encoding
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                () => NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming that
        /// stream is utf8 text
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming that
        /// stream is utf8 text
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding
        /// the stream with the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            Encoding encoding,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming the
        /// stream is utf8 text, with the provided
        /// StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                comparison,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text, with the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            StringComparison comparison,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                comparison,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding
        /// with the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            Encoding encoding,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                StringComparison.Ordinal,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding it
        /// with the provided encoding and using the
        /// provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this ITo<T> to,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return AddSeekMatcher(
                to,
                seek,
                encoding,
                comparison,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek
        ) where T : Stream
        {
            return continuation.Contain(
                seek,
                Encoding.UTF8
            );
        }

        /// <summary>
        /// Looks for a string in a stream, using
        /// the provided encoding to decode the stream
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek,
            Encoding encoding
        ) where T : Stream
        {
            return continuation.Contain(
                seek,
                encoding,
                StringComparison.Ordinal
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text, using the provided StringComparison
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek,
            StringComparison comparison
        ) where T : Stream
        {
            return continuation.Contain(
                seek,
                Encoding.UTF8,
                comparison
            );
        }

        /// <summary>
        /// Looks for a string in a stream, using the
        /// provided encoding to decode it and the
        /// provided StringComparison to seek the string
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek,
            Encoding encoding,
            StringComparison comparison
        ) where T : Stream
        {
            return continuation.Contain(
                seek,
                encoding,
                comparison,
                NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, using the
        /// provided encoding to decode it and the
        /// provided StringComparison to seek the string
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            string customMessage
        ) where T : Stream
        {
            return continuation.Contain(
                seek,
                encoding,
                comparison,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, using the
        /// provided encoding to decode it and the
        /// provided StringComparison to seek the string
        /// </summary>
        /// <param name="continuation"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this INotAfterTo<T> continuation,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return AddSeekMatcher(
                continuation,
                seek,
                encoding,
                comparison,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                StringComparison.Ordinal,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                StringComparison.Ordinal,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding
        /// the stream with the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                StringComparison.Ordinal,
                () => NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding
        /// the stream with the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                StringComparison.Ordinal,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding
        /// the stream with the provided encoding
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                StringComparison.Ordinal,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            StringComparison comparison
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                comparison,
                () => NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            StringComparison comparison,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                comparison,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, assuming it's
        /// utf8 text, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                comparison,
                customMessageGenerator
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding it with
        /// the provided encoding, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding,
            StringComparison comparison
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                comparison,
                () => NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding it with
        /// the provided encoding, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            string customMessage
        ) where T : Stream
        {
            return to.Contain(
                seek,
                encoding,
                comparison,
                () => customMessage
            );
        }

        /// <summary>
        /// Looks for a string in a stream, decoding it with
        /// the provided encoding, using the provided StringComparison
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek
        ) where T : Stream
        {
            return to.Contain(
                seek,
                Encoding.UTF8,
                StringComparison.Ordinal,
                () => NULL_STRING
            );
        }

        /// <summary>
        /// Looks for a string in a stream, using the provided
        /// encoding to decode the stream and the provided
        /// comparison to look for the string
        /// </summary>
        /// <param name="to"></param>
        /// <param name="seek"></param>
        /// <param name="encoding"></param>
        /// <param name="comparison"></param>
        /// <param name="customMessageGenerator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IMore<T> Contain<T>(
            this IToAfterNot<T> to,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return AddSeekMatcher<T>(
                to,
                seek,
                encoding,
                comparison,
                customMessageGenerator
            );
        }

        private static IMore<T> AddSeekMatcher<T>(
            ICanAddMatcher<T> continuation,
            string seek,
            Encoding encoding,
            StringComparison comparison,
            Func<string> customMessageGenerator
        ) where T : Stream
        {
            return continuation.AddMatcher(
                actual =>
                {
                    var passed = StreamContainsString(
                        actual,
                        seek,
                        encoding,
                        comparison
                    );
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected stream {passed.AsNot()}to contain string '{seek}'",
                            customMessageGenerator
                        )
                    );
                }
            );
        }

        private static bool StreamContainsString<T>(
            T actual,
            string seek,
            Encoding encoding,
            StringComparison comparison
        ) where T : Stream
        {
            // TODO: optimise by manually seeking through
            // the stream and bailing out as soon as there's
            // a hit?
            var s = actual.ReadAllText(encoding);
            return s.IndexOf(seek, comparison) > -1;
        }
    }
}