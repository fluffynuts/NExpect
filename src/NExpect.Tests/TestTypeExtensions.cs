using NUnit.Framework;
using NExpect;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestTypeExtensions
    {
        public interface ITestInterface
        {
        }

        public class TestClass : ITestInterface
        {
        }

        [TestFixture]
        public class To
        {
            [TestFixture]
            public class Be
            {
                [TestFixture]
                public class An
                {
//                    [Test]
//                    public void InstanceOf_WhenIsInstance_ShouldNotThrow()
//                    {
//                        // Arrange
//                        var sut = new TestClass();
//                        // Pre-Assert
//                        // Act
//                        Assert.That(() =>
//                        {
//                            Expect(sut).To.Be.A<ITestInterface>();
//                        }, Throws.Nothing);
//                        // Assert
//                    }
//
//                    [Test]
//                    public void InstanceOf_Negated_WhenIsInstance_ShouldThrow()
//                    {
//                        // Arrange
//                        var sut = new TestClass();
//                        // Pre-Assert
//                        // Act
//                        Assert.That(() =>
//                        {
//                            Expect(sut).Not.To.Be.An.Instance.Of<TestClass>();
//                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
//                            .With.Message.Contains("not to be of type"));
//                        // Assert
//                    }
                }
            }
        }
    }
}