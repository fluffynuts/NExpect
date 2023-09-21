using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Imported.PeanutButter.Utils;
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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        to.AddMatcher(
            actual =>
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
        this IStringToAfterNot to
    )
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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        to.AddMatcher(
            actual =>
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
        this IStringNotAfterTo to
    )
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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        to.AddMatcher(
            actual =>
                ResolveFileResultFor(actual, customMessageGenerator)
        );
        return to.More();
    }

    private static MatcherResult ResolveFileResultFor(
        string actual,
        Func<string> customMessageGenerator
    )
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
        this IA<string> a
    )
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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        a.AddMatcher(
            actual =>
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
            }
        );
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
        string customMessage
    )
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
        Func<string> customMessageGenerator
    )
    {
        a.AddMatcher(
            actual =>
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
            }
        );
        return a.More();
    }

    // collections
    /// <summary>
    /// Assert that all items in the collection are folders
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Folders(
        this ICollectionBe<string> be
    )
    {
        return be.Folders(NULL_STRING);
    }

    // collections
    /// <summary>
    /// Assert that all items in the collection are folders
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Folders(
        this ICollectionBe<string> be,
        string customMessage
    )
    {
        return be.Folders(() => customMessage);
    }

    // collections
    /// <summary>
    /// Assert that all items in the collection are folders
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Folders(
        this ICollectionBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            actual =>
            {
                return TestPaths(
                    actual,
                    o => o.IsFolder,
                    "all folders",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Tests that the given strings are all existing paths (files or folders)
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Exist(
        this ICollectionTo<string> to
    )
    {
        return to.Exist(NULL_STRING);
    }

    /// <summary>
    /// Tests that the given strings are all existing paths (files or folders)
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Exist(
        this ICollectionTo<string> to,
        string customMessage
    )
    {
        return to.Exist(() => customMessage);
    }

    /// <summary>
    /// Tests that the given strings are all existing paths (files or folders)
    /// </summary>
    /// <param name="to"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Exist(
        this ICollectionTo<string> to,
        Func<string> customMessageGenerator
    )
    {
        return to.AddMatcher(
            actual =>
            {
                return TestPaths(
                    actual,
                    o => o.IsFolder || o.IsFile,
                    "all existing filesystem entries",
                    customMessageGenerator
                );
            }
        );
    }

    /// <summary>
    /// Tests that the given strings are all existing files
    /// </summary>
    /// <param name="be"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Files(
        this ICollectionBe<string> be
    )
    {
        return be.Files(NULL_STRING);
    }

    /// <summary>
    /// Tests that the given strings are all existing files
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessage"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Files(
        this ICollectionBe<string> be,
        string customMessage
    )
    {
        return be.Files(() => customMessage);
    }

    /// <summary>
    /// Tests that the given strings are all existing paths (files or folders)
    /// </summary>
    /// <param name="be"></param>
    /// <param name="customMessageGenerator"></param>
    /// <returns></returns>
    public static ICollectionMore<string> Files(
        this ICollectionBe<string> be,
        Func<string> customMessageGenerator
    )
    {
        return be.AddMatcher(
            actual =>
            {
                return TestPaths(
                    actual,
                    o => o.IsFile,
                    "all files",
                    customMessageGenerator
                );
            }
        );
    }

    private class PathTest
    {
        public bool IsFolder { get; }
        public bool IsFile { get; }
        public string Path { get; }

        public PathTest(string path)
        {
            Path = path;
            IsFolder = Directory.Exists(path);
            IsFile = System.IO.File.Exists(path);
        }
    }

    private static string PrettyPrint(PathTest[] results)
    {
        var lines = new List<string>();
        foreach (var result in results)
        {
            var prefix = (result switch
            {
                { IsFolder: true } => "Folder",
                { IsFile: true } => "File",
                _ => "Missing"
            });

            lines.Add($"{prefix}: {result.Path}");
        }

        return lines.JoinWith("\n");
    }

    private static MatcherResult TestPaths(
        IEnumerable<string> actual,
        Func<PathTest, bool> tester,
        string subMessage,
        Func<string> customMessageGenerator
    )
    {
        var resolved = actual as string[] ??
            actual?.ToArray();
        if (resolved is null)
        {
            return new EnforcedMatcherResult(
                false,
                () => "No paths provided to test against"
            );
        }

        var results = resolved.Select(
            p => new PathTest(p)
        ).ToArray();


        var passed = results.All(tester);

        return new MatcherResult(
            passed,
            FinalMessageFor(
                () => $"Expected {passed.AsNot()}to find {subMessage}, but found:\n{PrettyPrint(results)}",
                customMessageGenerator
            )
        );
    }
}