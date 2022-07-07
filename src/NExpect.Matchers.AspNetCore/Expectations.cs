using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NExpect.Interfaces;

namespace NExpect;

/// <summary>
/// Adds AspNetCore convenience expectations around collection-like
/// types
/// </summary>
public static class AspNetCoreExpectations
{
    /// <summary>
    /// Treat an IFormFileCollection like a collection of IFormFile
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static ICollectionExpectation<IFormFile> Expect(
        IFormFileCollection files
    )
    {
        return NExpect.Expectations.Expect(
            files as IEnumerable<IFormFile>
        );
    }

    /// <summary>
    /// Treat an IFormCollection like a collection of KeyValuePair&lt;string, StringValues&gt;
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, StringValues>> Expect(
        IFormCollection form
    )
    {
        return NExpect.Expectations.Expect(
            form as IEnumerable<KeyValuePair<string, StringValues>>
        );
    }
}