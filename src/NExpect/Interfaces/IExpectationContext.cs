// ReSharper disable UnusedMemberInSuper.Global
namespace NExpect.Interfaces;

/// <summary>
/// Defines the most basic expectation context contract
/// </summary>
public interface IExpectationContext
{
    /// <summary>
    /// The parent context. Expectation logic has to run through all
    /// generations until either failure or all generations are exhausted
    /// (passing)
    /// </summary>
    IExpectationContext Parent { get; }
}

/// <summary>
/// An expectation context which has a parent with explicit type
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IExpectationContext<T> : 
    IExpectationContext,
    IExpectationParentContext<T>
{
    /// <summary>
    /// The typed parent of this context
    /// </summary>
    IExpectationContext<T> TypedParent { get; set; }
}