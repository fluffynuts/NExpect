using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect.Extensions
{
    internal static class MessageHelpers
    {
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

        internal static string Quote(string str)
        {
            return str == null ? str : $"\"{str}\"";
        }

        internal static string Quote(object o)
        {
            if (o == null)
                return null;
            var asString = o as string;
            return asString == null ? o.ToString() : Quote(asString);
        }

        internal static string CollectionContainsItemMessageFor<T>(
            bool passed, T search, IEnumerable<T> collection
        )
        {
            return passed
                ? $"Expected\n{Stringify(collection)}\nnot to contain\n{search}"
                : $"Expected\n{Stringify(collection)}\nto contain\n{search}";
        }

        internal static string Stringify<T>(IEnumerable<T> collection)
        {
            return string.Join(", ", collection.Select(o => Quote(o)));
        }
    }
}