using System.IO;
using System.IO.Compression;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestGzippedDataMatchers
    {
        [Test]
        public void ShouldBeAbleToAssertIfDataIsGzipped()
        {
            // Arrange
            var unzippedData = GetRandomBytes();
            var zippedData = unzippedData.GZip();
            // Act
            Assert.That(
                () =>
                {
                    Expect(zippedData)
                        .To.Be.GZipped();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(unzippedData)
                        .Not.To.Be.GZipped();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(unzippedData)
                        .To.Not.Be.GZipped();
                },
                Throws.Nothing
            );
            
            Assert.That(
                () =>
                {
                    Expect(zippedData)
                        .Not.To.Be.GZipped();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(zippedData)
                        .To.Not.Be.GZipped();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(unzippedData)
                        .To.Be.GZipped();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            
            
            
            // Assert
        }
    }
}