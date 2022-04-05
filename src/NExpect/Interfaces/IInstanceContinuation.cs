using System;

// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Continuation for Instance
/// </summary>
public interface IInstanceContinuation :
    IHasActual<Type>,
    ICanAddMatcher<Type>
{

}