namespace NExpect.Interfaces;

/// <summary>
/// Provides the attachment point for Value.Deep.Equal.To(...)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDictionaryValueEqual<T>: ICanAddMatcher<T>
{
}