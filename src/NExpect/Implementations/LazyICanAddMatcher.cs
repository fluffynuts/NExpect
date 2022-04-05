using System;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class LazyICanAddMatcher<T>
    : ExpectationContext<T>,
      ICanAddMatcher<T>,
      IHasActual<T>
{
    private readonly object _wrapped;

    internal LazyICanAddMatcher(
        object wrapped)
    {
        _wrapped = wrapped;
        SetParent(wrapped as IExpectationContext<T>);
    }

    public T Actual => TryResolveActual();

    private PropertyInfo ActualProp =>
        _actualProp ??= _wrapped?.GetType().GetProperty(nameof(IHasActual<T>.Actual));

    private PropertyInfo _actualProp;

    private T TryResolveActual()
    {
        if (ActualProp is null)
        {
            throw new UnauthorizedAccessException(
                new[]
                {
                    $"Unable to lazy-evaluate actual value on {_wrapped}",
                    "This either means you've found a bug in NExpect, or you've",
                    "attempted to extend NExpect and hit a problem. Either way,",
                    "please raise an issue at GitHub (https://github.com/fluffynuts/NExpect)"
                }.JoinWith("\n")
            );
        }

        var actual = ActualProp.GetValue(_wrapped);
        if (actual is null)
        {
            if (default(T) is null)
            {
                return default;
            }

            return ThrowCannotCast(null);
        }

        var actualType = actual.GetType();
        return typeof(T).IsAssignableFrom(actualType)
            ? (T) actual
            : ThrowCannotCast(actual);
    }

    private T ThrowCannotCast(object actual)
    {
        throw new InvalidOperationException(
            $"Cannot cast {actual} to type {typeof(T)}"
        );
    }
}