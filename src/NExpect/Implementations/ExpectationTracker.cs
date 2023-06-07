using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NExpect.Implementations
{
    /// <summary>
    /// Enable sweeping for incomplete expectations
    /// </summary>
    public static class ExpectationTracker
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ConcurrentDictionary<Guid, SweepableItem> InFlightContexts = new();

        private static int _suspendCount;

        /// <summary>
        /// Performs a sweep for incomplete expectations
        /// and will throw an IncompleteExpectationException
        /// if any are found.
        /// </summary>
        public static void AssertNoIncompleteExpectations()
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
        public static void WarnOfIncompleteExpectations()
        {
            WarnOfIncompleteExpectations(Console.Error.WriteLine);
        }

        /// <summary>
        /// Warns via the provided writer of incomplete expectations,
        /// but does not throw anything
        /// </summary>
        /// <param name="writer"></param>
        public static void WarnOfIncompleteExpectations(
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
            var keys = InFlightContexts.Keys.ToArray();
            var collected = new List<SweepableItem>();
            foreach (var k in keys)
            {
                InFlightContexts.TryRemove(k, out var item);
                collected.Add(item);
            }

            return collected.ToArray();
        }

        internal static void Register(SweepableItem ctx)
        {
            if (_suspendCount > 0)
            {
                return;
            }

            InFlightContexts.TryAdd(ctx.Identifier, ctx);
        }

        internal static void Forget(object owner)
        {
            if (owner is SweepableItem sweepableItem)
            {
                InFlightContexts.TryRemove(sweepableItem.Identifier, out _);
            }

            if (owner is IWrappingContinuation { Wrapped: SweepableItem wrappedSweepableItem })
            {
                InFlightContexts.TryRemove(wrappedSweepableItem.Identifier, out _);
            }
        }

        /// <summary>
        /// Suspend expectation tracking until the provided
        /// suspension object is disposed
        /// </summary>
        /// <returns></returns>
        public static IDisposable Suspend()
        {
            return new SuspendIncompleteExpectationTracking();
        }

        internal static void SetSuspended()
        {
            Interlocked.Increment(ref _suspendCount);
        }

        internal static void Resume()
        {
            Interlocked.Decrement(ref _suspendCount);
        }
    }

    /// <summary>
    /// Temporarily suspends any incomplete expectation tracking
    /// - this should only ever be useful within NExpect tests
    /// </summary>
    internal class SuspendIncompleteExpectationTracking : IDisposable
    {
        /// <summary>
        /// Suspends tracking until this object is disposed
        /// </summary>
        public SuspendIncompleteExpectationTracking()
        {
            ExpectationTracker.SetSuspended();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ExpectationTracker.Resume();
        }
    }
}