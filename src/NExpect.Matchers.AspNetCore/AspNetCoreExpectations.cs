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
        return Expectations.Expect(
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
        return Expectations.Expect(
            form as IEnumerable<KeyValuePair<string, StringValues>>
        );
    }

    /// <summary>
    /// Treat an IHeaderDictionary like an IDictionary&lt;string, StringValues&gt;
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, StringValues>> Expect(
        IHeaderDictionary headers
    )
    {
        return Expectations.Expect(headers as IDictionary<string, StringValues>);
    }

    /// <summary>
    /// Treat an IHeaderDictionary like an IDictionary&lt;string, StringValues&gt;
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, StringValues>> Expect(
        HeaderDictionary headers
    )
    {
        return Expect(headers as IHeaderDictionary);
    }

    /// <summary>
    /// Treat an IRequestCookieCollection like a collection of KeyValuePair&lt;string, string&gt;
    /// </summary>
    /// <param name="cookies"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, string>> Expect(
        IRequestCookieCollection cookies
    )
    {
        return Expectations.Expect(cookies as IEnumerable<KeyValuePair<string, string>>);
    }

    /// <summary>
    /// Treat an IQueryCollection like a collection of KeyValuePair&lt;string, string&gt;
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, StringValues>> Expect(
        IQueryCollection query
    )
    {
        return Expectations.Expect(query as IEnumerable<KeyValuePair<string, StringValues>>);
    }
}