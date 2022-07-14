using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using NExpect.Exceptions;
using NExpect.Matchers.AspNet.Tests.Implementations;
using static NExpect.Expectations;
using static NExpect.AspNetCoreExpectations;
using static PeanutButter.RandomGenerators.RandomValueGen;
using FormCollection = NExpect.Matchers.AspNet.Tests.Implementations.FormCollection;
using FormFile = NExpect.Matchers.AspNet.Tests.Implementations.FormFile;

namespace NExpect.Matchers.AspNet.Tests
{
    [TestFixture]
    public class TestAspNetCoreExpectations
    {
        [Test]
        public void ShouldBeAbleToAssertAgainstEmptyFormCollection()
        {
            // Arrange
            var empty = new FormCollection() as IFormCollection;
            Expect(empty.Keys)
                .To.Be.Empty();
            // Act
            Assert.That(() =>
            {
                Expect(empty)
                    .To.Be.Empty();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(empty)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstNonEmptyFormCollection()
        {
            // Arrange
            var form = new FormCollection();
            var key = GetRandomString(10);
            var value = GetRandomString(10);
            form[key] = value;
            var cast = form as IFormCollection;
            // Act
            Assert.That(() =>
            {
                Expect(cast)
                    .Not.To.Be.Empty();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(cast)
                    .To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstEmptyFiles()
        {
            // Arrange
            var form = new FormCollection() as IFormCollection;
            Expect(form.Files.Count)
                .To.Equal(0);
            // Act
            Assert.That(() =>
            {
                Expect(form.Files)
                    .To.Be.Empty();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(form.Files)
                    .Not.To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstNonEmptyFiles()
        {
            // Arrange
            var form = new FormCollection();
            form.AddFile(new FormFile()
            {
                Name = GetRandomString(10),
                FileName = GetRandomFileName(),
                Content = new MemoryStream(GetRandomBytes())
            });
            Expect(form.Files.Count)
                .To.Equal(1);
            // Act
            Assert.That(() =>
            {
                Expect(form.Files)
                    .Not.To.Be.Empty();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(form.Files)
                    .To.Be.Empty();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstHeadersLikeADictionary()
        {
            // Arrange
            var headers = new HeaderDictionary();
            // Act
            Assert.That(() =>
            {
                Expect(headers)
                    .To.Be.Empty();
            }, Throws.Nothing);

            headers["key"] = "value";
            Assert.That(() =>
            {
                Expect(headers)
                    .Not.To.Be.Empty();
                Expect(headers)
                    .To.Contain.Key("key")
                    .With.Value("value");
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstCookiesLikeADictionary()
        {
            // Arrange
            var cookies = new RequestCookies();
            var cast = cookies as IRequestCookieCollection;
            // Act
            Assert.That(() =>
            {
                Expect(cast)
                    .To.Be.Empty();
            }, Throws.Nothing);

            cookies["foo"] = "bar";
            Assert.That(() =>
            {
                Expect(cast)
                    .Not.To.Be.Empty();
                Expect(cast)
                    .To.Contain.Key("foo")
                    .With.Value("bar");
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldBeAbleToTreatRouteDataValuesLikeADictionary()
        {
            // Arrange
            var routeValues = new RouteValueDictionary();
            var key = GetRandomString(10);
            var value = GetRandomString(10);
            routeValues[key] = value;
            // Act
            Assert.That(() =>
            {
                Expect(routeValues)
                    .To.Contain.Key(key)
                    .With.Value(value);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(routeValues)
                    .To.Contain.Key(key + "1")
                    .With.Value(value);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(routeValues)
                    .To.Contain.Key(key)
                    .With.Value(value + "1");
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(routeValues)
                    .Not.To.Contain.Key(key)
                    .With.Value(value);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}