namespace NExpect.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPropertyMore<T> : IMore<T>
{
    /// <summary>
    /// The .With continuation for property matching
    /// </summary>
    /// <returns></returns>
    new IPropertyWith<T> With { get; }

    /// <summary>
    /// The .And continuation for property matching
    /// </summary>
    new IPropertyAnd<T> And { get; }
}