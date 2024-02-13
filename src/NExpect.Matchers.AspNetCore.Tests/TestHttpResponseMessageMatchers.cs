using System.Net.Http;
using NExpect.Exceptions;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestHttpResponseMessageMatchers
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
            var message = new HttpResponseMessage();
            message.Headers.Add(
                "Set-Cookie",
                $"{cookieName}={cookieValue}"
            );
            // Act
            Assert.That(() =>
            {
                Expect(message)
                    .To.Have.Cookie(cookieName)
                    .With.Value(cookieValue);
            }, Throws.Nothing);

            var wrongCookie = GetAnother(cookieName);
            Assert.That(() =>
                {
                    Expect(message)
                        .To.Have.Cookie(wrongCookie)
                        .With.Value(cookieValue);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(wrongCookie)
                    .And.Message.Contains("Expected to find")
            );
            var wrongValue = GetAnother(cookieValue);
            Assert.That(() =>
                {
                    Expect(message)
                        .To.Have.Cookie(cookieName)
                        .With.Value(wrongValue);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(cookieValue)
                    .And.Message.Contains(wrongValue)
            );
            Assert.That(() =>
            {
                Expect(message)
                    .Not.To.Have.Cookie(GetAnother(cookieName));
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldTestCookieSecureHttpOnlyDomain()
        {
            // Arrange
            var message = new HttpResponseMessage();
            var cookieName = GetRandomString();
            var cookieValue = GetRandomString();
            var maxAge = GetRandomInt();
            var domain = GetRandomHostname();
            message.Headers.Add(
                $"Set-Cookie",
                $"{cookieName}={cookieValue}; Secure; HttpOnly; Max-Age={maxAge}; Domain={domain}"
            );

            var nonSecureCookieName = GetAnother(cookieName);
            message.Headers.Add(
                "Set-Cookie",
                $"{nonSecureCookieName}={cookieValue}"
            );

            // Act
            Assert.That(() =>
            {
                Expect(message)
                    .To.Have.Cookie(cookieName)
                    .With.Value(cookieValue)
                    .And.To.Be.Secure()
                    .And.To.Have.Domain(domain)
                    .And.To.Have.Max.Age(maxAge)
                    .And.To.Be.HttpOnly();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(message)
                    .To.Have.Cookie(cookieName)
                    .With.Value(cookieValue)
                    .And.Is.Secure()
                    .And.Has.Domain(domain)
                    .And.Has.Max.Age(maxAge)
                    .And.Is.HttpOnly();
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(message)
                    .To.Have.Cookie(cookieName)
                    .With.Value(cookieValue)
                    .And.To.Be.Secure()
                    .And.To.Have.Domain(domain + ".com")
                    .And.To.Have.Max.Age(maxAge);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(message)
                    .To.Have.Cookie(cookieName)
                    .With.Value(cookieValue)
                    .And.To.Be.Secure()
                    .And.To.Have.Domain(domain)
                    .And.To.Have.Max.Age(maxAge + 1);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
                {
                    Expect(message)
                        .To.Have.Cookie(nonSecureCookieName)
                        .With.Value(cookieValue)
                        .And.To.Be.Secure();
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("secure")
            );
            Assert.That(() =>
                {
                    Expect(message)
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