namespace NExpect.Interfaces;

/// <summary>
/// Provides the .On grammar interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOn<T>: ICanAddMatcher<T>
{
    /// <summary>
    /// Provides the .On.A grammar
    /// </summary>
    IAn<T> An { get; }

    /// <summary>
    /// Provides the .On.An grammar
    /// </summary>
    IA<T> A { get; }
}