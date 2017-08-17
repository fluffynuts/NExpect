namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation to provide the ".An" grammar
    /// </summary>
    /// <typeparam name="T">Type of the continuation</typeparam>
    public interface IAn<T> : ICanAddMatcher<T>
    {
        IInstance<T> Instance { get; }
    }
}