using Microsoft.AspNetCore.Http;

namespace NExpect.Matchers.AspNet.Tests.Implementations
{
    internal class RequestCookies 
        : StringMap, IRequestCookieCollection
    {
    }
}