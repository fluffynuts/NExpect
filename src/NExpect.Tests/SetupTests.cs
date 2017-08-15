using NUnit.Framework;

namespace NExpect.Tests
{
    [SetUpFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
//            example of how to register an assertions factory
//            Assertions.RegisterAssertionsFactory(s => new AssertionException(s));
        }
    }
}