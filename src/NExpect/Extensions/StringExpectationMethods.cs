namespace NExpect.Extensions
{
    internal static class StringExpectationMethods
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

        private static string Quote(string str)
        {
            return str == null ? str : $"\"{str}\"";
        }
    }
}