using NExpect.Interfaces;

namespace NExpect.Utilities;

/// <summary>
/// NB: the following is a workaround for
///     non-standard matchers and is not recommended
/// </summary>
public static class ForgetExtension
{
    /// <summary>
    /// NB: the following is a workaround for
    ///     non-standard matchers(*) and is NOT recommended
    ///     - rather fix your assertions to use the provided
    ///       syntax, or raise an issue as to why you can't
    /// 
    /// For the case where some non-standard legacy matcher can't be
    /// rewritten in a hurry, you'll want to `.Forget()` it.
    ///
    /// * ie, one of the following as the first line
    /// in your matcher:
    /// return continuation.AddMatcher(...)
    /// return continuation.Compose(...)
    ///
    /// If you cannot update matchers and still want to verify
    /// no incomplete assertions you'll need to explicitly tell
    /// NExpect to stop tracking the member you're extending off of,
    /// ie:
    ///
    /// public static MyContinuation Foo(
    ///   this ITo&lt;MyType&gt; to
    /// )
    /// {
    ///     to.Forget(); // &lt;--
    ///     return new MyContinuation(to.GetActual());
    /// }
    /// </summary>
    /// <param name="m"></param>
    /// <typeparam name="T"></typeparam>
    public static void Forget<T>(
        this ICanAddMatcher<T> m
    )
    {
        Assertions.Forget(m);
    }
}