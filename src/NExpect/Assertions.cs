using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;
using NExpect.Implementations;

// ReSharper disable UnusedMember.Global

namespace NExpect;

/// <summary>
/// Used by NExpect to throw assertion errors
/// </summary>
public static class Assertions
{
    /// <summary>
    /// The default StringComparison to use for assertions which support
    /// comparing strings
    /// </summary>
    public static StringComparison DefaultStringComparison { get; set; } = StringComparison.InvariantCulture;

    /// <summary>
    /// Register your own factory for generating assertion exceptions.
    /// Threads may override this factory.
    /// </summary>
    /// <param name="generator">Func to invoke when NExpect needs an assertion exception</param>
    /// <typeparam name="T">Type of exception</typeparam>
    public static void RegisterAssertionsFactory<T>(
        Func<string, T> generator
    ) where T : Exception
    {
        using var _ = new AutoLocker(GeneratorLock);
        _usingCustomAssertions = true;
        _assertionsGenerator = (s, e) => generator(GenerateMessageFor(e, s))
            ?? throw new ArgumentNullException(nameof(generator));
    }

    /// <summary>
    /// Register your own global factory for generating assertion exceptions
    /// where you assertion can also take an inner exception. Threads may
    /// override this factory.
    /// </summary>
    /// <param name="generator"></param>
    /// <typeparam name="T"></typeparam>
    public static void RegisterAssertionsFactory<T>(
        Func<string, Exception, T> generator
    ) where T : Exception
    {
        using var _ = new AutoLocker(GeneratorLock);
        _usingCustomAssertions = false;
        _assertionsGenerator = (s, e) => generator(GenerateMessageFor(e, s), e)
            ?? throw new ArgumentNullException(nameof(generator));
    }

    /// <summary>
    /// Installs an assertions generator for this thread only
    /// </summary>
    /// <param name="generator"></param>
    /// <typeparam name="T"></typeparam>
    public static void RegisterAssertionsFactoryForCurrentThread<T>(
        Func<string, Exception, T> generator
    ) where T : Exception
    {
        ThreadAssertionGenerators[Thread.CurrentThread] = generator;
    }

    /// <summary>
    /// Uninstalls any assertions generator for this thread, if there is one
    /// </summary>
    public static void RemoveAssertionsFactoryForCurrentThread()
    {
        ThreadAssertionGenerators.TryRemove(Thread.CurrentThread, out var _);
    }

    /// <summary>
    /// Resets the global factory for generating assertion exceptions to the default
    /// (ie, throws UnmetExpectationException instances)
    /// </summary>
    public static void UseDefaultAssertionsFactory()
    {
        _assertionsGenerator = null;
    }

    /// <summary>
    /// Temporarily revert to using the default assertions factory (global)
    /// </summary>
    /// <returns></returns>
    public static IDisposable TemporarilyUseDefaultAssertionsFactory()
    {
        if (!_usingCustomAssertions)
        {
            return new NullDisposable();
        }

        var originalGenerator = _assertionsGenerator;
        return new AutoResetter(
            () =>
            {
                FlipLock.Wait();
                UseDefaultAssertionsFactory();
            },
            () =>
            {
                RegisterAssertionsFactory(originalGenerator);
                FlipLock.Release();
            }
        );
    }

    /// <summary>
    /// Temporarily revert to UnmetExpectationExceptions, for this thread only
    /// </summary>
    /// <returns></returns>
    public static IDisposable TemporarilyUseDefaultAssertionsFactoryForThisThread()
    {
        var hadThreadSpecificGenerator = ThreadAssertionGenerators.TryGetValue(
            Thread.CurrentThread,
            out var originalGenerator
        );
        return new AutoResetter(
            () => RegisterAssertionsFactoryForCurrentThread(
                (s, e) => new UnmetExpectationException(s, e)
            ),
            () =>
            {
                if (hadThreadSpecificGenerator)
                {
                    RegisterAssertionsFactoryForCurrentThread(originalGenerator);
                }
                else
                {
                    RemoveAssertionsFactoryForCurrentThread();
                }
            }
        );
    }

    private static string GenerateMessageFor(Exception exception, string s)
    {
        return exception == null
            ? s
            : $"{s}\n{exception.Message}\n{exception.StackTrace}";
    }

    private static bool _usingCustomAssertions;
    private static Func<string, Exception, Exception> _assertionsGenerator;
    private static readonly SemaphoreSlim FlipLock = new(1);
    private static readonly SemaphoreSlim GeneratorLock = new(1);

    private static readonly ConcurrentDictionary<Thread, Func<string, Exception, Exception>>
        ThreadAssertionGenerators = new();

    internal static void Throw(
        string message
    )
    {
        Throw(message, null);
    }

    internal static void Throw(
        string message,
        Exception innerException
    )
    {
        if (ThreadAssertionGenerators.TryGetValue(Thread.CurrentThread, out var generator))
        {
            throw generator.Invoke(message, innerException);
        }

        throw _assertionsGenerator?.Invoke(message, innerException)
            ?? new UnmetExpectationException(message, innerException);
    }

    private static readonly ConcurrentDictionary<Guid, SweepableItem> InFlightContexts = new();

    /// <summary>
    /// Enable expectation tracking so that incomplete
    /// expectations can be found via one of
    /// - AsserNoIncompleteExpectations
    /// - WarnOfIncompleteExpectations
    /// You may also temporarily suspend tracking with
    /// Suspend()
    /// </summary>
    /// <returns></returns>
    public static void EnableTracking()
    {
        _enabled = true;
    }

    /// <summary>
    /// Disable expectation tracking. If you only want to disable
    /// for a brief period, rather use the IDisposable from Suspend
    /// to ensure that tracking is re-enabled after the block
    /// you're suspending for
    /// </summary>
    public static void DisableTracking()
    {
        _enabled = true;
    }

    private static bool IsDisabledOrSuspended => !_enabled || _suspendCount > 0;

    private static int _suspendCount;
    private static bool _enabled;

    /// <summary>
    /// Performs a sweep for incomplete expectations
    /// and will throw an IncompleteExpectationException
    /// if any are found.
    /// </summary>
    public static void VerifyNoIncompleteAssertions()
    {
        var collected = FindIncompleteItems();
        if (!collected.Any())
        {
            return;
        }

        throw new IncompleteExpectationException(
            collected.ToArray()
        );
    }

    /// <summary>
    /// Warns via stderr of incomplete expectations, but
    /// does not throw anything.
    /// </summary>
    public static void WarnOfIncompleteAssertions()
    {
        WarnOfIncompleteAssertions(Console.Error.WriteLine);
    }

    /// <summary>
    /// Warns via the provided writer of incomplete expectations,
    /// but does not throw anything
    /// </summary>
    /// <param name="writer"></param>
    public static void WarnOfIncompleteAssertions(
        Action<string> writer
    )
    {
        var collected = FindIncompleteItems();
        if (!collected.Any())
        {
            return;
        }

        var message = IncompleteExpectationException.GenerateErrorFor(
            collected
        );
        writer(message);
    }

    private static SweepableItem[] FindIncompleteItems()
    {
        if (IsDisabledOrSuspended)
        {
            throw new InvalidOperationException(
                ""
            );
        }

        var keys = InFlightContexts.Keys.ToArray();
        var collected = new List<SweepableItem>();
        foreach (var k in keys)
        {
            InFlightContexts.TryRemove(k, out var item);
            collected.Add(item);
        }

        return collected.ToArray();
    }

    internal static void Track(SweepableItem ctx)
    {
        if (IsDisabledOrSuspended)
        {
            return;
        }

        InFlightContexts.TryAdd(ctx.Identifier, ctx);
    }

    internal static T Forget<T>(T owner)
    {
        if (owner is SweepableItem sweepableItem)
        {
            InFlightContexts.TryRemove(sweepableItem.Identifier, out _);
        }

        if (owner is IWrappingContinuation { Wrapped: SweepableItem wrappedSweepableItem })
        {
            InFlightContexts.TryRemove(wrappedSweepableItem.Identifier, out _);
        }

        return owner;
    }


    /// <summary>
    /// Suspend expectation tracking until the provided
    /// suspension object is disposed
    /// </summary>
    /// <returns></returns>
    public static IDisposable SuspendTracking()
    {
        return new SuspendIncompleteExpectationTracking();
    }

    internal static void SetSuspended()
    {
        Interlocked.Increment(ref _suspendCount);
    }

    internal static void ResumeTracking()
    {
        Interlocked.Decrement(ref _suspendCount);
    }
}