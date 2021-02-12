// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
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
    }
}