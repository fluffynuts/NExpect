using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestNSubstituteMatching
{

    [Test]
    public async Task ShouldBeAbleToAssertAgainstISessionMethods()
    {
        // Arrange
        var session = Substitute.For<ISession>();
        // Act
        await session.LoadAsync();
        // Assert
        await Expect(session)
            .To.Have.Received(1)
            .LoadAsync();
    }
}