using System.Linq;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Matchers.AspNetMvc.Tests;

[TestFixture]
public class TestPeanutButterProperlyImported
{
    [TestCase("PeanutButter.")]
    public void ShouldNotExportNamespaceStartingWith_(
        string prefix
    )
    {
        // Arrange
        // Act
        var invalid = typeof(Expectations)
            .GetAssembly()
            .GetTypes()
            .Where(t => t.Namespace?.StartsWith(prefix) ?? false)
            .ToArray();
        // Assert
        Expect(invalid)
            .To.Be.Empty();
    }
}