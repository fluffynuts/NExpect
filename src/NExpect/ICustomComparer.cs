using System;

namespace NExpect;

/// <summary>
/// Base (collection) interface for custom comparers. You want to
/// implement ICustomComparer&lt;T1, T2&gt; if you're looking here at all.
/// </summary>
internal interface ICustomComparer
{
    /// <summary>
    /// The type this converter can convert from
    /// </summary>
    public Type LeftType { get; }

    /// <summary>
    /// The type this converter can convert to
    /// </summary>
    public Type RightType { get; }
}

/// <summary>
/// Implement a custom converter to test types which either don't convert
/// well (eg char vs decimal) or types which don't implement IComparable
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight"></typeparam>
internal interface ICustomComparer<TLeft, TRight> : ICustomComparer
{
    /// <summary>
    /// Performs the same job as the IComparable interface:
    /// - return -1 if  left &lt; right
    /// - return 0 if left == right
    /// - return 1 if left &gt; right
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    int Compare(TLeft left, TRight right);
}