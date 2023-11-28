using System;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Mvc;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

// ReSharper disable MemberCanBePrivate.Global

namespace NExpect;

/// <summary>
/// Provides model matchers for view result continuations
/// </summary>
public static class ViewResultModelMatchers
{
    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IAnd<ViewResult> and,
        Func<TModel, bool> matcher
    ) where TModel : class
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IAnd<ViewResult> and,
        Func<TModel, bool> matcher,
        string customMessage
    ) where TModel : class
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IAnd<ViewResult> and,
        Func<TModel, bool> matcher,
        Func<string> customMessageGenerator
    ) where TModel : class
    {
        return and.AddMatcher(actual => VerifyModel(actual?.Model, "view", matcher, customMessageGenerator));
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        Func<object, bool> matcher
    )
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        Func<object, bool> matcher,
        string customMessage
    )
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        Func<object, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel(actual?.Model, "view", matcher, customMessageGenerator)
        );
    }


    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        object expected
    )
    {
        return and.Model(expected, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        object expected,
        string customMessage
    )
    {
        return and.Model(expected, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IAnd<ViewResult> and,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel<object>(
                    actual?.Model,
                    "view",
                    o => VerifyDeepEquality(o, expected),
                    customMessageGenerator
                )
        );
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IAnd<PartialViewResult> and,
        Func<TModel, bool> matcher
    ) where TModel : class
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IAnd<PartialViewResult> and,
        Func<TModel, bool> matcher,
        string customMessage
    ) where TModel : class
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IAnd<PartialViewResult> and,
        Func<TModel, bool> matcher,
        Func<string> customMessageGenerator
    ) where TModel : class
    {
        return and.AddMatcher(
            actual => VerifyModel(
                actual?.Model,
                "partial view",
                matcher,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        Func<object, bool> matcher
    )
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        Func<object, bool> matcher,
        string customMessage
    )
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        Func<object, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(actual => VerifyModel(actual?.Model, "view", matcher, customMessageGenerator));
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        object expected
    )
    {
        return and.Model(expected, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        object expected,
        string customMessage
    )
    {
        return and.Model(expected, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IAnd<PartialViewResult> and,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel<object>(actual?.Model, "view", o => VerifyDeepEquality(o, expected), customMessageGenerator)
        );
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IWith<ViewResult> and,
        Func<TModel, bool> matcher
    ) where TModel : class
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IWith<ViewResult> and,
        Func<TModel, bool> matcher,
        string customMessage
    ) where TModel : class
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model<TModel>(
        this IWith<ViewResult> and,
        Func<TModel, bool> matcher,
        Func<string> customMessageGenerator
    ) where TModel : class
    {
        return and.AddMatcher(actual => VerifyModel(actual?.Model, "view", matcher, customMessageGenerator));
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        Func<object, bool> matcher
    )
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        Func<object, bool> matcher,
        string customMessage
    )
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        Func<object, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel(actual?.Model, "view", matcher, customMessageGenerator)
        );
    }


    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        object expected
    )
    {
        return and.Model(expected, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        object expected,
        string customMessage
    )
    {
        return and.Model(expected, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWith<ViewResult> and,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel<object>(
                    actual?.Model,
                    "view",
                    o => VerifyDeepEquality(o, expected),
                    customMessageGenerator
                )
        );
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IWith<PartialViewResult> and,
        Func<TModel, bool> matcher
    ) where TModel : class
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IWith<PartialViewResult> and,
        Func<TModel, bool> matcher,
        string customMessage
    ) where TModel : class
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model<TModel>(
        this IWith<PartialViewResult> and,
        Func<TModel, bool> matcher,
        Func<string> customMessageGenerator
    ) where TModel : class
    {
        return and.AddMatcher(
            actual => VerifyModel(
                actual?.Model,
                "partial view",
                matcher,
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        Func<object, bool> matcher
    )
    {
        return and.Model(matcher, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        Func<object, bool> matcher,
        string customMessage
    )
    {
        return and.Model(matcher, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="matcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        Func<object, bool> matcher,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(actual => VerifyModel(actual?.Model, "view", matcher, customMessageGenerator));
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        object expected
    )
    {
        return and.Model(expected, NULL_STRING);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        object expected,
        string customMessage
    )
    {
        return and.Model(expected, () => customMessage);
    }

    /// <summary>
    /// Validates the model on the view result continuation using the provided matcher function
    /// </summary>
    /// <param name="and"></param>
    /// <param name="expected"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWith<PartialViewResult> and,
        object expected,
        Func<string> customMessageGenerator
    )
    {
        return and.AddMatcher(
            actual =>
                VerifyModel<object>(actual?.Model, "view", o => VerifyDeepEquality(o, expected), customMessageGenerator)
        );
    }

    private static bool VerifyDeepEquality(object o, object expected)
    {
        var tester = new DeepEqualityTester(o, expected)
        {
            RecordErrors = true,
            VerbosePropertyMismatchErrors = true
        };
        var result = tester.AreDeepEqual();
        if (result)
        {
            return true;
        }

        throw new Exception(
            $"Expected\n{o.Stringify()}\n{false.AsNot()}to deep equal\n{expected.Stringify()}\n{string.Join("\n", tester.Errors)}"
        );
    }

    private static MatcherResult VerifyModel<TModel>(
        object model,
        string resultType,
        Func<TModel, bool> matcher,
        Func<string> customMessageGenerator
    ) where TModel : class
    {
        if (matcher is null)
        {
            return new EnforcedMatcherResult(false, "model matcher may not be null");
        }

        var typedModel = model as TModel;
        if (typedModel is null && model is not null)
        {
            return new EnforcedMatcherResult(
                false,
                $"Actual model type ({model.GetType()}) does not match expected model type ({typeof(TModel)}) - matcher cannot run"
            );
        }

        bool passed;
        var exceptionMessage = null as string;
        try
        {
            passed = matcher(typedModel);
        }
        catch (Exception ex)
        {
            passed = false;
            exceptionMessage = ex.Message;
        }

        return new MatcherResult(
            passed,
            FinalMessageFor(
                () =>
                    $"Expected {passed.AsNot()}to match model on {resultType} result{(exceptionMessage is null ? null : $"\n{exceptionMessage}")}",
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWithout<ViewResult> without
    )
    {
        return without.Model(NULL_STRING);
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWithout<ViewResult> without,
        string customMessage
    )
    {
        return without.Model(() => customMessage);
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<ViewResult> Model(
        this IWithout<ViewResult> without,
        Func<string> customMessageGenerator
    )
    {
        return without.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(false, "view result is null");
                }

                var passed = actual.Model is null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {passed.AsNot()}to find a null model on the view result",
                        customMessageGenerator
                    )
                );
            }
        );
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWithout<PartialViewResult> without
    )
    {
        return without.Model(NULL_STRING);
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWithout<PartialViewResult> without,
        string customMessage
    )
    {
        return without.Model(() => customMessage);
    }

    /// <summary>
    /// Asserts that the view result has a null model
    /// </summary>
    /// <param name="without"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static IMore<PartialViewResult> Model(
        this IWithout<PartialViewResult> without,
        Func<string> customMessageGenerator
    )
    {
        return without.AddMatcher(
            actual =>
            {
                if (actual is null)
                {
                    return new EnforcedMatcherResult(false, "view result is null");
                }

                var passed = actual.Model is null;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {passed.AsNot()}to find a null model on the view result",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}