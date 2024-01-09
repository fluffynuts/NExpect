using System.Text.RegularExpressions;
using System.Xml.Linq;
using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Matchers.Xml.Tests
{
    [TestFixture]
    public class TestXmlMatchers
    {
        [Test]
        public void ShouldMatchElementsOnADocument()
        {
            // Arrange
            var xml = "<root><parent><child></child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child");
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//foo/bar");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldMatchAttributesOnADocumentElement()
        {
            // Arrange
            var xml = @"<root><parent><child attrib=""value""></child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib");
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("foo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldMatchAttributeValues()
        {
            // Arrange
            var xml = @"<root><parent><child attrib=""value""></child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib")
                        .Having.Value("value");
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib")
                        .Having.Value("moo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldMatchElementText_String()
        {
            // Arrange
            var xml = @"<root><parent><child attrib=""value"">Some Text</child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Text("Some Text");
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Text("Some Other Text");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldMatchElementText_Regex()
        {
            // Arrange
            var xml = @"<root><parent><child attrib=""some text"">Some Text</child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Text(new Regex("^Some Text$", RegexOptions.IgnoreCase));
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Text("Some Other Text");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldMatchAttributeValue_Regex()
        {
            // Arrange
            var xml = @"<root><parent><child attrib=""some text"">Some Text</child></parent></root>";
            var doc = XDocument.Parse(xml);
            // Act
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib")
                        .Having.Value(new Regex("^Some Text$", RegexOptions.IgnoreCase));
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib")
                        .Having.Value(new Regex("Some", RegexOptions.IgnoreCase));
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(doc)
                        .To.Have.Element("//root/parent/child")
                        .With.Attribute("attrib")
                        .Having.Value(new Regex("Some Other Text"));
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldAssertEqualityForTheSameXmlDocWithWhiteSpaceDifferences()
        {
            // Arrange
            var doc1 = XDocument.Parse(@"<root><parent><child attrib=""some text"">Some Text</child></parent></root>");
            var doc2 = XDocument.Parse(
                @"<root>
         <parent>
    <child 
        attrib=""some text"">Some Text</child>
 </parent>
</root>"
            );
            var doc3 = XDocument.Parse(
                @"<root>
         <parent>
    <child 
        attrib=""some text!"">Some Text</child>
 </parent>
</root>"
            );


            // Act
            Assert.That(
                () =>
                {
                    Expect(doc1)
                        .To.Be.Equivalent.To(doc2);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(doc1)
                        .To.Be.Equivalent.To(doc3);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        "first difference found at character"
                    )
            );

            Assert.That(() =>
            {
            Expect(doc1)
                .Not.To.Be.Equivalent.To(doc2);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains("(but they are equivalent)"));

            Assert.That(
                () =>
                {
                    Expect(doc2)
                        .Not.To.Be.Equivalent.To(doc3);
                },
                Throws.Nothing
            );
            // Assert
        }
    }
}