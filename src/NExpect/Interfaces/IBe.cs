// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Be" grammar continuation
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface IBe<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Negates the current continuation
        /// </summary>
        INotAfterBe<T> Not { get; }

        /// <summary>
        /// Prepares to check for equality
        /// </summary>
        IEqualityContinuation<T> Equal { get; }

        /// <summary>
        /// Prepares to check for the test value to be greater than the expected value
        /// </summary>
        IGreaterContinuation<T> Greater { get; }

        /// <summary>
        /// Prepares to check for the test value to be less than the expected value
        /// </summary>
        ILessContinuation<T> Less { get; }

        /// <summary>
        /// Adds the ".A" continuation extension point
        /// </summary>
        IA<T> A { get; }

        /// <summary>
        /// Adds the ".An" continuation extension point
        /// </summary>
        IAn<T> An { get; }

        /// <summary>
        /// Adds .Null so you can continue with .Or.Empty() or .Or.Whitespace()
        /// Use the extension method .Null() when you just want to test for null.
        /// </summary>
        INull<T> Null { get; }

        /// <summary>
        /// Provides the .For continuation extension point
        /// </summary>
        IFor<T> For { get; }
        
        /// <summary>
        /// Starts deep equality testing on the extracted property
        /// </summary>
        IDeep<T> Deep { get; }
        /// <summary>
        /// Starts intersection equality testing on the extracted property
        /// </summary>
        IIntersection<T> Intersection { get; }
    }
}