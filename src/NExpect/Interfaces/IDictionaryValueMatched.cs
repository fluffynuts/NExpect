namespace NExpect.Interfaces
{
    /// <summary>
    /// Allows for the `.Value.Matched.By` syntax on dictionary matching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDictionaryValueMatched<T>: ICanAddMatcher<T>
    {
    }
}