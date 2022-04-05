using System;
using System.Collections.Concurrent;
using System.Threading;
using Imported.PeanutButter.Utils;
using NExpect.Exceptions;

// ReSharper disable UnusedMember.Global

namespace NExpect;

/// <summary>
/// Used by NExpect to throw assertion errors
/// </summary>
public static class Assertions
{
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
            Thread.CurrentThread, out var originalGenerator
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
            });
    }

    private static string GenerateMessageFor(Exception exception, string s)
    {
        return exception == null
            ? s
            : $"{s}\n{exception.Message}\n{exception.StackTrace}";
    }

    private static bool _usingCustomAssertions = false;
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
}

internal class NullDisposable : IDisposable
{
    public void Dispose()
    {
        // intentionally left blank
    }
}