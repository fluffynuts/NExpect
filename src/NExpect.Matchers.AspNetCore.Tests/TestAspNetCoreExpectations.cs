using System.IO;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using NExpect.Exceptions;
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
        
    }
}