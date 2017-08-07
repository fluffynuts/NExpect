using NUnit.Framework;

namespace NExpect.Tests
{
    [SetUpFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Assertions.RegisterAssertionsFactory(s => new AssertionException(s));
        }
    }
}