using System;
using Microsoft.AspNetCore.Http;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Builders;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestHttpResponseMatchers
    {
        [TestFixture]
        public class Cookies
        {
            [Test]
            public void ShouldTestCookieNameAndValue()
            {
                // Arrange
                var cookieName = GetRandomString(4);
                var cookieValue = GetRandomString(4);
                var response = HttpResponseBuilder.BuildDefault();
                response.Headers.Add(
                    "Set-Cookie",
                    $"{cookieName}={cookieValue}"
                );
                // Act
                Assert.That(() =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue);
                }, Throws.Nothing);

                var wrongCookie = GetAnother(cookieName);
                Assert.That(() =>
                    {
                        Expect(response)
                            .To.Have.Cookie(wrongCookie)
                            .With.Value(cookieValue);
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(wrongCookie)
                        .And.Message.Contains("Expected to find")
                );
                var wrongValue = GetAnother(cookieValue);
                Assert.That(() =>
                    {
                        Expect(response)
                            .To.Have.Cookie(cookieName)
                            .With.Value(wrongValue);
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(cookieValue)
                        .And.Message.Contains(wrongValue)
                );
                Assert.That(() =>
                {
                    Expect(response)
                        .Not.To.Have.Cookie(GetAnother(cookieName));
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void ShouldTestCookieSecureHttpOnlyDomain()
            {
                // Arrange
                var response = HttpResponseBuilder.BuildDefault();
                var cookieName = $"secure-{GetRandomString()}";
                var cookieValue = GetRandomString();
                var maxAge = GetRandomInt();
                var domain = GetRandomHostname();
                response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
                {
                    Secure = true,
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromSeconds(maxAge),
                    Domain = domain
                });
                
                var nonSecureCookieName = $"nonsecure-{GetRandomString()}";
                response.Cookies.Append(nonSecureCookieName, cookieValue);

                // Act
                Assert.That(() =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain)
                        .And.To.Have.Max.Age(maxAge)
                        .And.To.Be.HttpOnly();
                }, Throws.Nothing);
                Assert.That(() =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.Is.Secure()
                        .And.Has.Domain(domain)
                        .And.Has.Max.Age(maxAge)
                        .And.Is.HttpOnly();
                }, Throws.Nothing);

                Assert.That(() =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain + ".com")
                        .And.To.Have.Max.Age(maxAge);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain)
                        .And.To.Have.Max.Age(maxAge + 1);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                Assert.That(() =>
                    {
                        Expect(response)
                            .To.Have.Cookie(nonSecureCookieName)
                            .With.Value(cookieValue)
                            .And.To.Be.Secure();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("secure")
                );
                Assert.That(() =>
                    {
                        Expect(response)
                            .To.Have.Cookie(nonSecureCookieName)
                            .With.Value(cookieValue)
                            .And.To.Be.HttpOnly();
                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("http-only")
                );
                // Assert
            }
        }
    }
}