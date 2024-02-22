using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
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
    /// Facilitates treating a QueryString object like
    /// a dictionary for assertions
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, string>> Expect(
        QueryString queryString
    )
    {
        return Expectations.Expect(
            queryString.AsDictionary()
        );
    }

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

    /// <summary>
    /// Treat a RouteValueDictionary like a collection of KeyValuePair&lt;string, object&gt;
    /// </summary>
    /// <param name="routeValueDictionary"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, object>> Expect(
        RouteValueDictionary routeValueDictionary
    )
    {
        return Expectations.Expect(
            routeValueDictionary as IDictionary<string, object>
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<string, ModelStateEntry>> Expect(
        ModelStateDictionary dict
    )
    {
        return Expectations.Expect(
            dict?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static ICollectionExpectation<KeyValuePair<object, ValidationStateEntry>> Expect(
        ValidationStateDictionary dict
    )
    {
        return Expectations.Expect(
            dict?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        );
    }

    /// <summary>
    /// Converts PathString expectations into simple string expectations
    /// </summary>
    /// <param name="pathString"></param>
    /// <returns></returns>
    public static IStringExpectation Expect(
        PathString pathString
    )
    {
        return Expectations.Expect($"{pathString}");
    }

    /// <summary>
    /// Converts IServiceCollection expectations into simple
    /// collection expectations
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static ICollectionExpectation<ServiceDescriptor> Expect(
        IServiceCollection serviceCollection
    )
    {
        return Expectations.Expect(
            serviceCollection.AsEnumerable()
        );
    }
    /// <summary>
    /// Converts IServiceCollection expectations into simple
    /// collection expectations
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static ICollectionExpectation<ServiceDescriptor> Expect(
        ServiceCollection serviceCollection
    )
    {
        return Expect(serviceCollection as IServiceCollection);
    }

    /// <summary>
    /// Starts a collection matching assertion on a StringValues
    /// value
    /// </summary>
    /// <param name="sv"></param>
    /// <returns></returns>
    public static ICollectionExpectation<string> Expect(
        StringValues sv
    )
    {
        return Expectations.Expect(sv.ToArray());
    }
}