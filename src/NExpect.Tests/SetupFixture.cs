using NUnit.Framework;

namespace NExpect.Tests
{
    [SetUpFixture]
    public class SetupFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestUtils.ForceMessageLineBreaks();
            Assertions.EnableTracking();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Assertions.VerifyNoIncompleteAssertions();
        }
    }

}