using System;
using System.Collections.Generic;
using Imported.PeanutButter.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect;

/// <summary>
/// Provides matchers for ModelStateDictionary objects
/// </summary>
public static class ModelStateMatchers
{
    /// <summary>
    /// Asserts that the model state dictionary has errors
    /// </summary>
    /// <param name="have"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, ModelStateEntry>> Errors(
        this ICollectionHave<KeyValuePair<string, ModelStateEntry>> have
    )
    {
        return have.Errors(NULL_STRING);
    }

    /// <summary>
    /// Asserts that the model state dictionary has errors
    /// </summary>
    /// <param name="have"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, ModelStateEntry>> Errors(
        this ICollectionHave<KeyValuePair<string, ModelStateEntry>> have,
        string customMessage
    )
    {
        return have.Errors(() => customMessage);
    }

    /// <summary>
    /// Asserts that the model state dictionary has errors
    /// </summary>
    /// <param name="have"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<KeyValuePair<string, ModelStateEntry>> Errors(
        this ICollectionHave<KeyValuePair<string, ModelStateEntry>> have,
        Func<string> customMessageGenerator
    )
    {
        return have.AddMatcher(
            collection =>
            {
                if (
                    !collection.TryGetMetadata<ModelStateDictionary>("__actual__", out var actual) ||
                    actual is null
                )
                {
                    return new EnforcedMatcherResult(
                        false,
                        FinalMessageFor(
                            () => "Unable to assert model state errors on a null model state dictionary",
                            customMessageGenerator
                        )
                    );
                }

                var passed = actual.ErrorCount > 0;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => $"Expected {passed.AsNot()}to find errors (found {actual.ErrorCount})",
                        customMessageGenerator
                    )
                );
            }
        );
    }
}