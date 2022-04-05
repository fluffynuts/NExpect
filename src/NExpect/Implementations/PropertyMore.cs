using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class PropertyMore<T>
    : More<T>, IPropertyMore<T>
{
    public IHave<T> Have { get; }

    public PropertyMore(IMore<T> wrapped, IHave<T> have)
        : base((wrapped as More<T>)?.ActualFetcher)
    {
        SetParent(
            (have as IExpectationContext<T>)?.TypedParent
        );
        Have = have;
    }

    /// <summary>
    /// Provides the overridden .With for property continuations
    /// </summary>
    public new IPropertyWith<T> With => CreateWith();

    /// <summary>
    /// Provides the overridden .And for property continuations
    /// </summary>
    public new IPropertyAnd<T> And => CreateAnd();

    private IPropertyWith<T> CreateWith()
    {
        var result = Next<PropertyWith<T>>();
        result.SetHave(Have);
        return result;
    }

    private IPropertyAnd<T> CreateAnd()
    {
        var result = Next<PropertyAnd<T>>();
        result.SetHave(Have);
        return result;
    }
}