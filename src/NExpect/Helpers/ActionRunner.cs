using System;
using System.Diagnostics;
using Imported.PeanutButter.Utils;

namespace NExpect.Helpers;

internal static class ActionRunner
{
    public static TimeSpan Run(Action action)
    {
        return Run(action, true);
    }

    public static TimeSpan RunSuppressed(Action action)
    {
        return Run(action, false);
    }

    private static TimeSpan Run(Action action, bool throwError)
    {
        var stopwatch = new Stopwatch();
        try
        {
            stopwatch.Start();
            action();
        }
        catch
        {
            if (throwError)
            {
                throw;
            }
        }
        finally
        {
            stopwatch.Stop();
            action.SetMetadata(META_KEY_RUNTIME, stopwatch.Elapsed);
        }

        return stopwatch.Elapsed;
    }

    public const string META_KEY_RUNTIME = "__action_runtime__";
}