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
        }
    }
}