﻿using System;
using NExpect.MatcherLogic;

namespace NExpect.Interfaces;

/// <summary>
/// Typed parent context; this allows access to
/// negation and running matchers from the current
/// context
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IExpectationParentContext<out T>
{
    /// <summary>
    /// Negate the current expectation
    /// </summary>
    void Negate();
    /// <summary>
    /// Reset all negation on the current expectation
    /// </summary>
    void ResetNegation();

    /// <summary>
    /// Run a matcher from the current expectation context
    /// </summary>
    /// <param name="matcher"></param>
    IMatcherResult RunMatcher(Func<T, IMatcherResult> matcher);
}