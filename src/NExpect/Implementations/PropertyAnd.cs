using System;
using NExpect.Implementations.Fluency;
using NExpect.Interfaces;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class PropertyAnd<T>
    : And<T>,
      IPropertyAnd<T>
{
    public new IHave<T> Have { get; private set; }
    public new IPropertyWith<T> With => CreateWith();

    public PropertyAnd(Func<T> actualFetcher) : base(actualFetcher)
    {
    }

    public void SetHave(IHave<T> have)
    {
        Have = have;
    }
    
    private IPropertyWith<T> CreateWith()
    {
        var result = Next<PropertyWith<T>>();
        result.SetHave(Have);
        return result;
    }
}