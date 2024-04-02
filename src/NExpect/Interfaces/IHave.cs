// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Continuation to provide the ".Have" grammar
/// </summary>
/// <typeparam name="T">Type of the continuation</typeparam>
public interface IHave<T>: ICanAddMatcher<T>
{
    /// <summary>
    /// ".A" grammar continuation extension point
    /// </summary>
    IA<T> A { get; }
        
    /// <summary>
    /// ".An" grammar continuation extension point
    /// </summary>
    IAn<T> An { get; }

    /// <summary>
    /// ".Been" grammar continuation extension point
    /// </summary>
    IBeen<T> Been { get; }

    /// <summary>
    /// ".Max" grammar continuation extension point
    /// </summary>
    IMax<T> Max { get; }

    /// <summary>
    /// ".Default" grammar continuation extension point
    /// </summary>
    IDefault<T> Default { get; }

    /// <summary>
    /// .Valid grammar continuation extension point
    /// </summary>
    IValid<T> Valid { get; }

    /// <summary>
    /// .Optional grammar continuation extension point
    /// </summary>
    IOptional<T> Optional { get; }
    
    /// <summary>
    /// .Required grammar continuation extension point
    /// </summary>
    IRequired<T> Required { get; }
}