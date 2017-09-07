using NUnit.Framework;
using NExpect.Exceptions;
using static NExpect.Expectations;

namespace NExpect.Tests
{
    [TestFixture]
    public class TestTypeExtensions
    {
        private interface ITestInterface
        {
        }

        private class TestClass : ITestInterface
        {
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class AnotherTestClass : ITestInterface
        {
        }

        // ReSharper disable once UnusedTypeParameter
        private class GenericTestClass<T>
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
                    [Test]
                    public void InstanceOf_Negated_WhenIsInstance_ShouldThrow()
                    {
                        // Arrange
                        var sut = new TestClass();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Be.An.Instance.Of<TestClass>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("to not be of type"));
                        // Assert
                    }

                    [Test]
                    public void InstanceOf_WhenIsInstance_ShouldNotThrow()
                    {
                        // Arrange
                        var sut = new TestClass();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Be.An.Instance.Of<TestClass>();
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void InstanceOf_Negated_WhenIsNotInstance_ShouldNotThrow()
                    {
                        // Arrange
                        var sut = new TestClass();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Be.An.Instance.Of<AnotherTestClass>();
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void InstanceOf_WhenIsNotInstance_ShouldThrow()
                    {
                        // Arrange
                        var sut = new TestClass();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("to be of type"));
                        // Assert
                    }

                    [Test]
                    public void InstanceOf_WithGenerics_ShouldThrowWithValidCalssName()
                    {
                        // Arrange
                        var sut = new GenericTestClass<TestClass>();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("TestTypeExtensions+GenericTestClass<NExpect.Tests.TestTypeExtensions+TestClass>"));
                        // Assert
                    }
                    
                    [TestFixture]
                    public class CustomMessage
                    {
                        [Test]
                        public void InstanceOf_Negated_WhenIsInstance_ShouldThrow()
                        {
                            // Arrange
                            var sut = new TestClass();
                            // Pre-Assert
                            // Act
                            Assert.That(() =>
                            {
                                Expect(sut).Not.To.Be.An.Instance.Of<TestClass>("Custom Message");
                            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains("Custom Message"));
                            // Assert
                        }
                    }
                }
            }
        }
    }
}