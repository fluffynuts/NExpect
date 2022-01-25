using System;

namespace NExpect.Interfaces;

/// <summary>
/// An expectation context for an action
/// </summary>
public interface IActionExpectation : IExpectation<Action>
{
    /// <summary>
    /// The time it took to run this action
    /// </summary>
    IExpectation<TimeSpan> RunTime { get; }
}