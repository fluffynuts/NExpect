using System;
using Microsoft.AspNetCore.Http;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Builders;

namespace NExpect.Matchers.AspNet.Tests;

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
            response.Headers.Append(
                "Set-Cookie",
                $"{cookieName}={cookieValue}"
            );
            // Act
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue);
                },
                Throws.Nothing
            );

            var wrongCookie = GetAnother(cookieName);
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(wrongCookie)
                        .With.Value(cookieValue);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(wrongCookie)
                    .And.Message.Contains("Expected to find")
            );
            var wrongValue = GetAnother(cookieValue);
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(wrongValue);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(cookieValue)
                    .And.Message.Contains(wrongValue)
            );
            Assert.That(
                () =>
                {
                    Expect(response)
                        .Not.To.Have.Cookie(GetAnother(cookieName));
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertCookieIsExpired()
        {
            // Arrange
            var key = GetRandomString();
            var value = GetRandomString();
            var hasExpiredCookie = HttpResponseBuilder.Create()
                .WithCookie(
                    key,
                    value,
                    new CookieOptions()
                    {
                        MaxAge = TimeSpan.FromSeconds(-1)
                    }
                ).Build();

            var hasValidCookie = HttpResponseBuilder.Create()
                .WithCookie(
                    key,
                    value,
                    new CookieOptions()
                    {
                        MaxAge = TimeSpan.FromSeconds(120)
                    }
                ).Build();

            // Act
            Assert.That(() =>
            {
                Expect(hasExpiredCookie)
                    .To.Have.Cookie(key)
                    .With.Value(value)
                    .Which.Is.Expired();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(hasValidCookie)
                    .To.Have.Cookie(key)
                    .With.Value(value)
                    .Which.Is.Expired();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertCookieIsExpired_PastTense()
        {
            // Arrange
            var key = GetRandomString();
            var value = GetRandomString();
            var hasExpiredCookie = HttpResponseBuilder.Create()
                .WithCookie(
                    key,
                    value,
                    new CookieOptions()
                    {
                        MaxAge = TimeSpan.FromSeconds(-1)
                    }
                ).Build();

            var hasValidCookie = HttpResponseBuilder.Create()
                .WithCookie(
                    key,
                    value,
                    new CookieOptions()
                    {
                        MaxAge = TimeSpan.FromSeconds(120)
                    }
                ).Build();

            // Act
            Assert.That(() =>
            {
                Expect(hasExpiredCookie)
                    .To.Have.Cookie(key)
                    .With.Value(value)
                    .Which.Has.Expired();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(hasValidCookie)
                    .To.Have.Cookie(key)
                    .With.Value(value)
                    .Which.Has.Expired();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
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
            response.Cookies.Append(
                cookieName,
                cookieValue,
                new CookieOptions()
                {
                    Secure = true,
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromSeconds(maxAge),
                    Domain = domain
                }
            );

            var nonSecureCookieName = $"nonsecure-{GetRandomString()}";
            response.Cookies.Append(nonSecureCookieName, cookieValue);

            // Act
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain)
                        .And.To.Have.Max.Age(maxAge)
                        .And.To.Be.HttpOnly();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.Is.Secure()
                        .And.Has.Domain(domain)
                        .And.Has.Max.Age(maxAge)
                        .And.Is.HttpOnly();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain + ".com")
                        .And.To.Have.Max.Age(maxAge);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(cookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure()
                        .And.To.Have.Domain(domain)
                        .And.To.Have.Max.Age(maxAge + 1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(nonSecureCookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("secure")
            );
            Assert.That(
                () =>
                {
                    Expect(response)
                        .To.Have.Cookie(nonSecureCookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.HttpOnly();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("http-only")
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertCookieExpiration()
        {
            // Arrange
            var sessionKey = GetRandomString();
            var sessionValue = GetRandomString();
            var expiringKey = GetAnother(sessionKey);
            var expiringValue = GetRandomString();
            var expires = DateTimeOffset.Now.AddHours(1);
            var res = HttpResponseBuilder.Create()
                .WithCookie(sessionKey, sessionValue)
                .WithCookie(
                    expiringKey,
                    expiringValue,
                    new CookieOptions()
                    {
                        Expires = expires
                    }
                )
                .Build();
            Expect(res.Headers["Set-Cookie"])
                .To.Contain.Only(2).Items();
            // Act
            Assert.That(
                () =>
                {
                    Expect(res)
                        .To.Have.Cookie(expiringKey)
                        .With.Value(expiringValue)
                        .With.Expiration(expires);
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(res)
                        .To.Have.Cookie(sessionKey)
                        .With.Value(sessionValue)
                        .For.Session();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(res)
                        .To.Have.Cookie(sessionKey)
                        .With.Value(sessionValue)
                        .With.Expiration(expires);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(res)
                        .To.Have.Cookie(expiringKey)
                        .With.Value(expiringValue)
                        .For.Session();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [TestFixture]
        public class Issues
        {
            [Test]
            public void ShouldHandleCookieNameWithSpace()
            {
                // Arrange
                var cookieName = "the cookie";
                var expected = GetRandomWords();
                var res = HttpResponseBuilder.Create()
                    .WithCookie(cookieName, expected)
                    .Build();
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie(cookieName)
                            .With.Value(expected);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .Not.To.Have.Cookie(cookieName)
                            .With.Value(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Not.Have.Cookie(cookieName)
                            .With.Value(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                );
                // Assert
            }

            [Test]
            public void HeaderAccessShouldBeCaseInsensitive()
            {
                // Arrange
                var res = HttpResponseBuilder.BuildDefault();
                res.Headers["Set-Cookie"] = "foo=bar";
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(res.Headers)
                            .To.Contain.Key("set-cookie");
                    },
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void ShouldCorrectlyParsePathAndSameSite()
            {
                // Arrange
                var res = HttpResponseBuilder.Create()
                    .WithHeader("Set-Cookie", "le_cookie=le_value; Path=/; SameSite=Lax; Secure; HttpOnly")
                    .Build();
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie("le_cookie")
                            .With.Path("/");
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie("le_cookie")
                            .With.Value("le_value");
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie("le_cookie")
                            .With.SameSite(SameSiteMode.Lax);
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie("le_cookie")
                            .Which.Is.HttpOnly();
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(res)
                            .To.Have.Cookie("le_cookie")
                            .Which.Is.Secure();
                    },
                    Throws.Nothing
                );
                // Assert
            }
        }
    }
}