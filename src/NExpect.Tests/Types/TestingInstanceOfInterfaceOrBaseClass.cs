using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;

namespace NExpect.Tests.Types
{
    [TestFixture]
    public class TestingInstanceOfInterfaceOrBaseClass
    {
        private interface ITestInterface
        {
        }

        private class TestClass : ITestInterface
        {
        }

        private class DerivedClass : TestClass
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
                    [TestFixture]
                    public class Instance
                    {
                        [TestFixture]
                        public class Of
                        {
                            [Test]
                            public void Negated_WhenIsInstance_ShouldThrow()
                            {
                                // Arrange
                                var sut = new TestClass();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expectations.Expect(sut).Not.To.Be.An.Instance.Of<TestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to not be an instance of"));
                                // Assert
                            }

                            [Test]
                            public void WhenIsInstance_ShouldNotThrow()
                            {
                                // Arrange
                                var sut = new TestClass();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expectations.Expect(sut).To.Be.An.Instance.Of<TestClass>();
                                }, Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void Negated_WhenIsNotInstance_ShouldNotThrow()
                            {
                                // Arrange
                                var sut = new TestClass();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expectations.Expect(sut).Not.To.Be.An.Instance.Of<AnotherTestClass>();
                                }, Throws.Nothing);
                                // Assert
                            }

                            [Test]
                            public void WhenIsNotInstance_ShouldThrow()
                            {
                                // Arrange
                                var sut = new TestClass();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expectations.Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to be an instance of"));
                                // Assert
                            }

                            [Test]
                            public void WithGenerics_ShouldThrowWithValidCalssName()
                            {
                                // Arrange
                                var sut = new GenericTestClass<TestClass>();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expectations.Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message
                                    .EqualTo(
                                    "Expected <TestingInstanceOfInterfaceOrBaseClass+GenericTestClass<NExpect.Tests.Types.TestingInstanceOfInterfaceOrBaseClass+TestClass>> to be an instance of <NExpect.Tests.Types.TestingInstanceOfInterfaceOrBaseClass+AnotherTestClass>"
                                    ));
                                // Assert
                            }

                            [TestFixture]
                            public class OperatingOnDerivedClass
                            {
                                [Test]
                                public void WhenProvidedObject_IsOfDerivedType_ShouldNotThrow()
                                {
                                    // Arrange
                                    var obj = new DerivedClass();
                                    // Pre-Assert
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expectations.Expect(obj).To.Be.An.Instance.Of<TestClass>();
                                    }, Throws.Nothing);
                                    // Assert
                                }

                                [Test]
                                public void Negated_WhenProvidedObject_IsOfDerivedType_ShouldThrow()
                                {
                                    // Arrange
                                    var obj = new DerivedClass();
                                    // Pre-Assert
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expectations.Expect(obj).Not.To.Be.An.Instance.Of<TestClass>();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                    // Assert
                                }
                            }

                            [TestFixture]
                            public class ExpectingInstanceOfInterface
                            {
                                [Test]
                                public void WhenProvidedObject_ImplementsInterface_ShouldNotThrow()
                                {
                                    // Arrange
                                    var obj = new TestClass();
                                    // Pre-Assert
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expectations.Expect(obj).To.Be.An.Instance.Of<ITestInterface>();
                                    }, Throws.Nothing);
                                    // Assert
                                }

                                [Test]
                                public void Negated_WhenProvidedObject_ImplementsInterface_ShouldThrow()
                                {
                                    // Arrange
                                    var obj = new TestClass();
                                    // Pre-Assert
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expectations.Expect(obj).Not.To.Be.An.Instance.Of<ITestInterface>();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                    // Assert
                                }
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
                                        Expectations.Expect(sut).Not.To.Be.An.Instance.Of<TestClass>("Custom Message");
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains("Custom Message"));
                                    // Assert
                                }
                            }

                            [TestFixture]
                            public class OperatingOnCollection
                            {
                                [Test]
                                public void InstanceOf_Negated_WhenIsInstance_ShouldThrow()
                                {
                                    // Arrange
                                    var sut = new List<TestClass>();
                                    // Pre-Assert
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expectations.Expect(sut).Not.To.Be.An.Instance.Of<List<TestClass>>();
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains("to not be an instance of"));
                                    // Assert
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}