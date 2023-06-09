using System;
using Imported.PeanutButter.Utils;
using NExpect.Helpers;
using NExpect.Interfaces;

namespace NExpect.Implementations;

internal class ActionExpectation
    : Expectation<Action>,
      IActionExpectation
{
    public IExpectation<TimeSpan> RunTime =>
        ResolveRuntimeExpectation();

    private IExpectation<TimeSpan> ResolveRuntimeExpectation()
    {
        Assertions.Forget(this);
        if (!Actual.TryGetMetadata<TimeSpan>(ActionRunner.META_KEY_RUNTIME, out var runTime))
        {
            runTime = ActionRunner.RunSuppressed(Actual);
        }

        return Expectations.Expect(runTime);
    }

    public ActionExpectation(Action actual) : base(actual)
    {
    }
}