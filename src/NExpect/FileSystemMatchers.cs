using System;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace NExpect;

/// <summary>
/// Provides matchers for filesystem objects
/// </summary>
public static class FileSystemMatchers
{
    /// <summary>
    /// Expects a file or folder to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IStringMore Exist(
        this ITo<string> to
    )
    {
        return to.Exist(NULL_STRING);
    }

    /// <summary>
    /// Expects a file or folder to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this ITo<string> to,
        string customMessage)
    {
        return to.Exist(() => customMessage);
    }

    /// <summary>
    /// Expects a file or folder to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this ITo<string> to,
        Func<string> customMessageGenerator)
    {
        to.AddMatcher(actual =>
            ResolveFileResultFor(actual, customMessageGenerator)
        );
        return to.More();
    }

    /// <summary>
    /// Expects a file or folder not to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringToAfterNot to)
    {
        return to.Exist(NULL_STRING);
    }

    /// <summary>
    /// Expects a file or folder not to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringToAfterNot to,
        string customMessage)
    {
        return to.Exist(() => customMessage);
    }

    /// <summary>
    /// Expects a file or folder not to exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringToAfterNot to,
        Func<string> customMessageGenerator)
    {
        to.AddMatcher(actual =>
            ResolveFileResultFor(actual, customMessageGenerator)
        );
        return to.More();
    }

    /// <summary>
    /// Expects a file or folder to not exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringNotAfterTo to)
    {
        return to.Exist(NULL_STRING);
    }

    /// <summary>
    /// Expects a file or folder to not exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringNotAfterTo to,
        string customMessage)
    {
        return to.Exist(() => customMessage);
    }

    /// <summary>
    /// Expects a file or folder to not exist at the provided path
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Exist(
        this IStringNotAfterTo to,
        Func<string> customMessageGenerator)
    {
        to.AddMatcher(actual =>
            ResolveFileResultFor(actual, customMessageGenerator)
        );
        return to.More();
    }

    private static MatcherResult ResolveFileResultFor(
        string actual,
        Func<string> customMessageGenerator)
    {
        var passed = System.IO.File.Exists(actual) ||
            System.IO.Directory.Exists(actual);
        return new MatcherResult(
            passed,
            FinalMessageFor(
                () => $"Expected {actual} {passed.AsNot()}to exist",
                customMessageGenerator
            )
        );
    }

    /// <summary>
    /// Expects the provided path to be a file
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IStringMore File(
        this IA<string> a)
    {
        return a.File(NULL_STRING);
    }

    /// <summary>
    /// Expects the provided path to be a file
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore File(
        this IA<string> a,
        string customMessage)
    {
        return a.File(() => customMessage);
    }

    /// <summary>
    /// Expects the provided path to be a file
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore File(
        this IA<string> a,
        Func<string> customMessageGenerator)
    {
        a.AddMatcher(actual =>
        {
            var passed = System.IO.File.Exists(actual);
            var isDir = System.IO.Directory.Exists(actual);
            var extra = isDir
                ? " but found a folder there instead"
                : " but found nothing";
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected {actual} {passed.AsNot()}to be a file{extra}",
                    customMessageGenerator
                )
            );
        });
        return a.More();
    }

    /// <summary>
    /// Expects the provided path to be a folder
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IStringMore Folder(
        this IA<string> a
    )
    {
        return a.Folder(NULL_STRING);
    }

    /// <summary>
    /// Expects the provided path to be a folder
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessage">Custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Folder(
        this IA<string> a,
        string customMessage)
    {
        return a.Folder(() => customMessage);
    }

    /// <summary>
    /// Expects the provided path to be a folder
    /// </summary>
    /// <param name="a"></param>
    /// <param name="customMessageGenerator">Generates a custom message to add to failure messages</param>
    /// <returns></returns>
    public static IStringMore Folder(
        this IA<string> a,
        Func<string> customMessageGenerator)
    {
        a.AddMatcher(actual =>
        {
            var passed = System.IO.Directory.Exists(actual);
            var isFile = System.IO.File.Exists(actual);
            var extra = isFile
                ? " but found a file there instead"
                : " but found nothing";
            return new MatcherResult(
                passed,
                FinalMessageFor(
                    () => $"Expected {actual} {passed.AsNot()}to be a folder{extra}",
                    customMessageGenerator
                )
            );
        });
        return a.More();
    }
}