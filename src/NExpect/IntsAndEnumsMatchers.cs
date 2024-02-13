using System;
using System.Collections.Generic;
using System.Linq;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global


namespace NExpect;

/// <summary>
/// Provides matchers to make it more convenient
/// to compare enum values and integer values
/// </summary>
public static class IntsAndEnumsMatchers
{
    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this ITo<T> to,
        int expected
    ) where T : Enum
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this ITo<T> to,
        int expected,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this ITo<T> to,
        int expected,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return ResolveResult(
            to,
            expected,
            customMessageGenerator,
            comparingEnumToInt: true
        );
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> to,
        int expected
    ) where T : Enum
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> to,
        int expected,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this IToAfterNot<T> to,
        int expected,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return ResolveResult(
            to,
            expected,
            customMessageGenerator,
            comparingEnumToInt: true
        );
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> to,
        int expected
    ) where T : Enum
    {
        return to.Equal(expected, NULL_STRING);
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> to,
        int expected,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            expected,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if a provided enum value is equal to the
    /// provided integer value. Useful for comparing int
    /// fields from db objects against enum values.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<T> Equal<T>(
        this INotAfterTo<T> to,
        int expected,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return ResolveResult(to, expected, customMessageGenerator, comparingEnumToInt: true);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this ITo<long> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this ITo<long> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this ITo<long> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual => VerifyEquality(
                enumValue,
                actual,
                customMessageGenerator,
                comparingEnumToInt: false
            )
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this IToAfterNot<long> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this IToAfterNot<long> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this IToAfterNot<long> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual => VerifyEquality(
                enumValue,
                actual,
                customMessageGenerator,
                comparingEnumToInt: false
            )
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this INotAfterTo<long> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this INotAfterTo<long> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long> Equal<T>(
        this INotAfterTo<long> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual => VerifyEquality(
                enumValue,
                actual,
                customMessageGenerator,
                comparingEnumToInt: false
            )
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this ITo<long?> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this ITo<long?> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this ITo<long?> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual =>
                VerifyEquality(
                    enumValue,
                    actual,
                    customMessageGenerator,
                    comparingEnumToInt: false
                )
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this IToAfterNot<long?> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this IToAfterNot<long?> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this IToAfterNot<long?> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual => VerifyEquality(
                enumValue,
                actual,
                customMessageGenerator,
                comparingEnumToInt: false
            )
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this INotAfterTo<long?> to,
        T enumValue
    ) where T : Enum
    {
        return to.Equal(enumValue, NULL_STRING);
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this INotAfterTo<long?> to,
        T enumValue,
        string customMessage
    ) where T : Enum
    {
        return to.Equal(
            enumValue,
            () => customMessage
        );
    }

    /// <summary>
    /// Tests if the provided integer value is equal
    /// to the provided enum value
    /// </summary>
    /// <param name="to"></param>
    /// <param name="enumValue"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IMore<long?> Equal<T>(
        this INotAfterTo<long?> to,
        T enumValue,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        return to.AddMatcher(
            actual => VerifyEquality(
                enumValue,
                actual,
                customMessageGenerator,
                comparingEnumToInt: false
            )
        );
    }


    private static IMore<T> ResolveResult<T>(
        this ICanAddMatcher<T> canAddMatcher,
        int expected,
        Func<string> customMessageGenerator,
        bool comparingEnumToInt
    ) where T : Enum
    {
        return canAddMatcher.AddMatcher(
            actual => VerifyEquality(
                actual,
                expected,
                customMessageGenerator,
                comparingEnumToInt
            )
        );
    }

    private static IMatcherResult VerifyEquality<T>(
        T enumValue,
        long? nullableIntValue,
        Func<string> customMessageGenerator,
        bool comparingEnumToInt
    ) where T : Enum
    {
        if (nullableIntValue is null)
        {
            return new EnforcedMatcherResult(
                false,
                () => "Cannot compare null with an enum value"
            );
        }

        var intValue = nullableIntValue.Value;
        var earlyFail = VerifyWithinRange<T>(intValue, customMessageGenerator);
        if (earlyFail is not null)
        {
            return earlyFail;
        }

        var passed = (int)(object)enumValue == intValue;
        return new MatcherResult(
            passed,
            FinalMessageFor(
                () => comparingEnumToInt
                    ? $"Expected {passed.AsNot()}to receive value '{intValue}', but received '{enumValue}'"
                    : $"Expected {passed.AsNot()}to receive value '{enumValue}' but received '{intValue}'",
                customMessageGenerator
            )
        );
    }

    private static IMatcherResult VerifyWithinRange<T>(
        long value,
        Func<string> customMessageGenerator
    ) where T : Enum
    {
        var possibleValues = new HashSet<long>(
            Enum.GetValues(typeof(T))
                .Cast<object>()
                .Select(e => (long)(int)e)
                .ToArray()
        );
        return possibleValues.Contains(value)
            ? null
            : new EnforcedMatcherResult(
                false,
                FinalMessageFor(
                    () =>
                        $"{value} cannot be mapped onto a pure value of {typeof(T)}; FLAGS enums are not (yet) supported.",
                    customMessageGenerator
                )
            );
    }
}