using System;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations;

/// <summary>
/// Factory to create continuations where the continuation is an IExpectationContext&lt;T&gt;
/// </summary>
public static class ContinuationFactory
{
    internal static T2 Create<T1, T2>(
        Func<T1> actualGenerator,
        IExpectationContext<T1> parent,
        Action<T2> afterConstruction = null
    ) where T2 : IExpectationContext<T1>
    {
        ExpectationTracker.Forget(parent); // parent is being discarded
        var result = (T2) Activator.CreateInstance(typeof(T2), actualGenerator);

        result.TypedParent = parent;
        afterConstruction?.Invoke(result);
        if (result is IResetNegation &&
            result.IsNegated())
        {
            result.Negate();
        }

        parent.CopyAllMetadataTo(result);

        return result;
    }
}