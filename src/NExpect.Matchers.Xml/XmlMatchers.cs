using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using Imported.PeanutButter.Utils;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Provides Xml matchers via XDocument and xpath expressions
    /// </summary>
    public static class XmlMatchers
    {
        /// <summary>
        /// Asserts that the provided document has a single element matched by the
        /// provided xpath
        /// </summary>
        /// <param name="have"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static IMore<XDocument> Element(
            this IHave<XDocument> have,
            string xpath
        )
        {
            return have.Element(xpath, NULL_STRING);
        }

        /// <summary>
        /// Asserts that the provided document has a single element matched by the
        /// provided xpath
        /// </summary>
        /// <param name="have"></param>
        /// <param name="xpath"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Element(
            this IHave<XDocument> have,
            string xpath,
            string customMessage
        )
        {
            return have.Element(xpath, () => customMessage);
        }

        /// <summary>
        /// Asserts that the provided document has a single element matched by the
        /// provided xpath
        /// </summary>
        /// <param name="have"></param>
        /// <param name="xpath"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Element(
            this IHave<XDocument> have,
            string xpath,
            Func<string> customMessageGenerator
        )
        {
            return have.AddMatcher(actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(false, "document is null");
                }

                var node = actual.XPathSelectElement(xpath);
                actual.SetMetadata(SELECTED_NODE, node);
                actual.SetMetadata(XPATH_CONTEXT, xpath);
                var passed = node is not null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected document {
                            passed.AsNot()
                        }to contain node matched by '{
                            xpath
                        }':\nfull document follows:\n{
                            actual
                        }{Dump(actual)}",
                        customMessageGenerator
                    )
                );
            });
        }

        private const string SELECTED_NODE = "__xml_matchers::selected_node__";
        private const string XPATH_CONTEXT = "__xml_matchers::xpath_context__";
        private const string SELECTED_ATTRIBUTE = "__xml_matchers::selected_attribute__";
        private const string EXPECTED_ELEMENT_TEXT = "__xml_matchers::expected_element_text__";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static IMore<XDocument> Attribute(
            this IWith<XDocument> with,
            string attribute
        )
        {
            return with.Attribute(attribute, NULL_STRING);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="attribute"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Attribute(
            this IWith<XDocument> with,
            string attribute,
            string customMessage
        )
        {
            return with.Attribute(attribute, () => customMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="attribute"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Attribute(
            this IWith<XDocument> with,
            string attribute,
            Func<string> customMessageGenerator
        )
        {
            return with.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<XElement>(SELECTED_NODE, out var el) ||
                    !actual.TryGetMetadata<string>(XPATH_CONTEXT, out var xpath))
                {
                    return new EnforcedMatcherResult(false,
                        "no current element context; start with .To.Have.Element()");
                }

                var attrib = el.Attribute(attribute);
                actual.SetMetadata(SELECTED_ATTRIBUTE, attrib);
                var passed = attrib != null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected node selected by '{
                            xpath
                        }' {
                            passed.AsNot()
                        }to have attribute '{
                            attribute
                        }'{Dump(actual)}",
                        customMessageGenerator
                    )
                );
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            string value
        )
        {
            return having.Value(value, NULL_STRING);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="value"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            string value,
            string customMessage
        )
        {
            return having.Value(value, () => customMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="value"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            string value,
            Func<string> customMessageGenerator
        )
        {
            return having.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<XAttribute>(SELECTED_ATTRIBUTE, out var attrib) ||
                    !actual.TryGetMetadata<string>(XPATH_CONTEXT, out var xpath))
                {
                    return new EnforcedMatcherResult(false,
                        "no current attribute context; start with .To.Have.Element().With.Attribute()"
                    );
                }

                var passed = value == attrib?.Value;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected node selected by '{
                            xpath
                        }' {passed.AsNot()}to have attribute '{
                            attrib?.Name
                        }' with value '{
                            value
                        }'{(passed ? "" : $" but found value '{attrib?.Value}' instead")}{
                            Dump(actual)
                        }",
                        customMessageGenerator
                    )
                );
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="matching"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            Regex matching
        )
        {
            return having.Value(matching, NULL_STRING);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="matching"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            Regex matching,
            string customMessage
        )
        {
            return having.Value(matching, () => customMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="having"></param>
        /// <param name="matching"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Value(
            this IHaving<XDocument> having,
            Regex matching,
            Func<string> customMessageGenerator
        )
        {
            return having.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<XAttribute>(SELECTED_ATTRIBUTE, out var attrib) ||
                    !actual.TryGetMetadata<string>(XPATH_CONTEXT, out var xpath))
                {
                    return new EnforcedMatcherResult(false,
                        "no current attribute context; start with .To.Have.Element().With.Attribute()"
                    );
                }

                var passed = matching.IsMatch(attrib?.Value ?? "");
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected node selected by '{
                            xpath
                        }' {passed.AsNot()}to have attribute '{
                            attrib?.Name
                        }' with value matching '{
                            matching
                        }'{(passed ? "" : $" but found value '{attrib?.Value}' instead")}{
                            Dump(actual)
                        }",
                        customMessageGenerator
                    )
                );
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expectedText"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            string expectedText
        )
        {
            return with.Text(expectedText, NULL_STRING);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expectedText"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            string expectedText,
            string customMessage
        )
        {
            return with.Text(expectedText, () => customMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="expectedText"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            string expectedText,
            Func<string> customMessageGenerator
        )
        {
            return with.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<XElement>(SELECTED_NODE, out var el) ||
                    !actual.TryGetMetadata<string>(XPATH_CONTEXT, out var xpath))
                {
                    return new EnforcedMatcherResult(false,
                        "no current attribute context; start with .To.Have.Element()"
                    );
                }

                var text = string.Join(
                    " ",
                    el?.Nodes()
                        .OfType<XText>()
                        .Select(n => n.Value)
                        .ToArray() ?? new string[0]
                );
                var passed = text == expectedText;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected {passed.AsNot()} to find text '{
                            expectedText
                        }' for node selected by '{
                            xpath
                        }'{(passed ? "" : " but found text '{text}' instead")}{
                            Dump(actual)
                        }",
                        customMessageGenerator
                    )
                );
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            Regex matcher
        )
        {
            return with.Text(matcher, NULL_STRING);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <param name="customMessage"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            Regex matcher,
            string customMessage
        )
        {
            return with.Text(matcher, () => customMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="with"></param>
        /// <param name="matcher"></param>
        /// <param name="customMessageGenerator"></param>
        /// <returns></returns>
        public static IMore<XDocument> Text(
            this IWith<XDocument> with,
            Regex matcher,
            Func<string> customMessageGenerator
        )
        {
            return with.AddMatcher(actual =>
            {
                if (!actual.TryGetMetadata<XElement>(SELECTED_NODE, out var el) ||
                    !actual.TryGetMetadata<string>(XPATH_CONTEXT, out var xpath))
                {
                    return new EnforcedMatcherResult(false,
                        "no current attribute context; start with .To.Have.Element()"
                    );
                }
                
                var text = string.Join(
                    " ",
                    el?.Nodes()
                        .OfType<XText>()
                        .Select(n => n.Value)
                        .ToArray() ?? new string[0]
                );
                var passed = matcher.IsMatch(text ?? "");
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $@"Expected {passed.AsNot()} to find text matching {
                            matcher
                        } for node selected by '{
                            xpath
                        }'{(passed ? "" : " but found text '{text}' instead")}{
                            Dump(actual)
                        }",
                        customMessageGenerator
                    )
                );
            });
        }

        private static string Dump(XDocument doc)
        {
            return $"\nfull document follows:\n{doc}";
        }
    }
}