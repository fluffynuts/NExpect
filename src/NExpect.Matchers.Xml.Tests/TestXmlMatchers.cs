using System.Text.RegularExpressions;
using System.Xml.Linq;
using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Matchers.Xml.Tests;

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
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child");
            }
        ).Not.To.Throw();

        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//foo/bar");
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldMatchAttributesOnADocumentElement()
    {
        // Arrange
        var xml = @"<root><parent><child attrib=""value""></child></parent></root>";
        var doc = XDocument.Parse(xml);
        // Act
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib");
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("foo");
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldMatchAttributeValues()
    {
        // Arrange
        var xml = @"<root><parent><child attrib=""value""></child></parent></root>";
        var doc = XDocument.Parse(xml);
        // Act
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib")
                    .Having.Value("value");
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib")
                    .Having.Value("moo");
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldMatchElementText_String()
    {
        // Arrange
        var xml = @"<root><parent><child attrib=""value"">Some Text</child></parent></root>";
        var doc = XDocument.Parse(xml);
        // Act
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Text("Some Text");
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Text("Some Other Text");
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldMatchElementText_Regex()
    {
        // Arrange
        var xml = @"<root><parent><child attrib=""some text"">Some Text</child></parent></root>";
        var doc = XDocument.Parse(xml);
        // Act
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Text(new Regex("^Some Text$", RegexOptions.IgnoreCase));
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Text("Some Other Text");
            }
        ).To.Throw<UnmetExpectationException>();
        // Assert
    }

    [Test]
    public void ShouldMatchAttributeValue_Regex()
    {
        // Arrange
        var xml = @"<root><parent><child attrib=""some text"">Some Text</child></parent></root>";
        var doc = XDocument.Parse(xml);
        // Act
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib")
                    .Having.Value(new Regex("^Some Text$", RegexOptions.IgnoreCase));
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib")
                    .Having.Value(new Regex("Some", RegexOptions.IgnoreCase));
            }
        ).Not.To.Throw();
        Expect(
            () =>
            {
                Expect(doc)
                    .To.Have.Element("//root/parent/child")
                    .With.Attribute("attrib")
                    .Having.Value(new Regex("Some Other Text"));
            }
        ).To.Throw<UnmetExpectationException>();
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
        Expect(
            () =>
            {
                Expect(doc1)
                    .To.Be.Equivalent.To(doc2);
            }
        ).Not.To.Throw();

        Expect(
            () =>
            {
                Expect(doc1)
                    .To.Be.Equivalent.To(doc3);
            }
        ).To.Throw<UnmetExpectationException>();

        Expect(
            () =>
            {
                Expect(doc1)
                    .Not.To.Be.Equivalent.To(doc2);
            }).To.Throw<UnmetExpectationException>()
            .With.Message.Containing("(but they are equivalent)");

        Expect(
            () =>
            {
                Expect(doc2)
                    .Not.To.Be.Equivalent.To(doc3);
            }).Not.To.Throw();
        // Assert
    }
}