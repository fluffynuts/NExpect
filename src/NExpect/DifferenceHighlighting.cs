using System;
using System.Collections.Generic;
using System.Linq;
using Imported.PeanutButter.Utils;

namespace NExpect
{
    /// <summary>
    /// Facilitates better difference highlighting per type
    /// </summary>
    public static class DifferenceHighlighting
    {
        private static readonly Dictionary<Type, List<Func<object, object, string>>>
            DifferenceHighlighters = new();

        static DifferenceHighlighting()
        {
            AddDifferenceHighlighter<string>(
                CompareWhitespaceAndNewlines
            );
            AddDifferenceHighlighter<string>(
                CompareCaseInsensitive
            );
            AddDifferenceHighlighter<string>(
                HighlightFirstPositionOfDifference
            );
        }

        private static string HighlightFirstPositionOfDifference(
            string left,
            string right
        )
        {
            if (left is null || right is null)
            {
                return null;
            }

            var idx = FindIndexOfFirstDifference(left, right);

            var depth = 5;
            var from = idx - depth;
            if (from < 0)
            {
                from = 0;
            }

            var len = depth;
            if (from + len >= left.Length)
            {
                len = left.Length - from;
            }

            len = TruncateToNextNewLineIfTooClose(left, from, len);

            var arrowBodyLength = idx > depth
                ? depth
                : idx;
            if (arrowBodyLength > left.Length)
            {
                arrowBodyLength = left.Length;
            }

            var arrowBody = new String('-', arrowBodyLength);

            return $@"first difference found at character {
                idx
            }
{
    left.Substring(from, len)
}
{
    arrowBody
}^";
        }

        private static int TruncateToNextNewLineIfTooClose(
            string left,
            int from,
            int len
        )
        {
            var nextNewLine = left.IndexOf('\n', from);
            var nextLineFeed = left.IndexOf('\r', from);
            var closestLineEnding = Math.Min(nextNewLine, nextLineFeed);
            if (closestLineEnding < 0)
            {
                return len;
            }

            if (closestLineEnding < from + len)
            {
                len = closestLineEnding - from;
            }
            return len;
        }

        private static int FindIndexOfFirstDifference(string left, string right)
        {
            var charZip = left.ToCharArray().Zip(right.ToCharArray(), (c1, c2) => Tuple.Create(c1, c2));
            var idx = 0;
            foreach (var pair in charZip)
            {
                if (pair.Item1 != pair.Item2)
                {
                    break;
                }

                idx++;
            }

            return idx;
        }

        private static string CompareCaseInsensitive(
            string left,
            string right
        )
        {
            if (left is null || right is null)
            {
                return null;
            }

            var caseDiff = string.Equals(
                left,
                right,
                StringComparison.InvariantCultureIgnoreCase
            );
            return caseDiff
                ? "values have different casing"
                : null;
        }

        private static string CompareWhitespaceAndNewlines(
            string left,
            string right
        )
        {
            if (left is null || right is null)
            {
                return null;
            }

            return FindNewLineIssues(left, right)
                ?? FindWhitespaceIssues(left, right);
        }

        private static string FindWhitespaceIssues(string left, string right)
        {
            var whitespaceDiff = left.RegexReplace("\\s*", " ") == right.RegexReplace("\\s*", " ");
            return whitespaceDiff
                ? "(values have whitespace differences)"
                : null;
        }

        private static string FindNewLineIssues(string left, string right)
        {
            var newlineDiff = left.RegexReplace("\r\n", "\n") == right.RegexReplace("\r\n", "\n");
            return newlineDiff
                ? "(values have different line endings (CRLF vs LF))"
                : null;
        }

        /// <summary>
        /// Add a new difference highlighter, which will have output added
        /// to any existing difference highlighters
        /// </summary>
        /// <param name="generator"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddDifferenceHighlighter<T>(
            Func<T, T, string> generator
        )
        {
            lock (DifferenceHighlighters)
            {
                if (!DifferenceHighlighters.TryGetValue(typeof(T), out var highlighters))
                {
                    DifferenceHighlighters[typeof(T)] = highlighters = new List<Func<object, object, string>>();
                }

                highlighters.Add((left, right) =>
                {
                    var leftCast = Cast<T>(left);
                    var rightCast = Cast<T>(right);
                    return generator(leftCast, rightCast);
                });
            }
        }

        private static T Cast<T>(object value)
        {
            try
            {
                return (T)value;
            }
            catch
            {
                return default;
            }
        }

        internal static string ProvideMoreInfoFor<T>(
            T actual,
            T expected
        )
        {
            lock (DifferenceHighlighters)
            {
                if (!DifferenceHighlighters.TryGetValue(typeof(T), out var highlighters))
                {
                    return null;
                }

                return string.Join(Environment.NewLine,
                    highlighters
                        .Select(h => TryInvoke(h, actual, expected))
                        .Where(line => line is not null)
                );
            }
        }

        private static string TryInvoke<T>(
            Func<object, object, string> func,
            T actual,
            T expected
        )
        {
            try
            {
                return func?.Invoke(actual, expected);
            }
            catch (Exception ex)
            {
                return $"Exception encountered whilst running difference highlighter: {ex.Message}";
            }
        }
    }
}