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

        internal static void Register(SweepableItem ctx)
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