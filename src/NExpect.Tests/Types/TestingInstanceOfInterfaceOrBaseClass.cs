using System;
using System.Collections.Generic;
using NExpect.Exceptions;
using NExpect.MatcherLogic;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.Types
{
    [TestFixture]
    public class TestingInstanceOfInterfaceOrBaseClass
    {
        private interface ITestInterface
        {
        }

        public class TestClass : ITestInterface
        {
            public int Id { get; set; }
            public string Name { get; set; }
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
            public class Implement
            {
                public interface IService
                {
                }

                public class Implementation : IService
                {
                }

                [TestFixture]
                public class Errors
                {
                    [Test]
                    public void ShouldBarfIfTExpectedIsNotAnInterface()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Implement<Implementation>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("is not an interface"));
                        // Assert
                    }
                }

                [TestFixture]
                public class UsingGenericSyntax
                {
                    [Test]
                    public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Implement<IService>();
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));

                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_AltSyntax_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));


                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement<IService>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));
                        // Assert
                    }
                }

                [TestFixture]
                public class UsingExpectedType
                {
                    [Test]
                    public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Implement(typeof(IService));
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));

                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_AltSyntax_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Implementation);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService));
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));


                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to implement"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Implementation).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Implement(typeof(IService), customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(IService).PrettyName()));
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Inherit
            {
                public class Base
                {
                }

                public class Derived : Base
                {
                }

                public interface IService
                {
                }

                [TestFixture]
                public class Errors
                {
                    [Test]
                    public void ShouldBarfIfTExpectedAnInterface()
                    {
                        // Arrange
                        var sut = typeof(Derived);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Inherit<IService>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("is not a class"));
                        // Assert
                    }
                }

                [TestFixture]
                public class UsingGenericSyntax
                {
                    [Test]
                    public void PositiveAssertion_WhenShouldPass_ShouldNotThrow()
                    {
                        // Arrange
                        var sut = typeof(Derived);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Inherit<Base>();
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Derived);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to inherit"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Derived).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Base).PrettyName()));

                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to inherit"));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Derived).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).Not.To.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Base).PrettyName()));
                        // Assert
                    }

                    [Test]
                    public void NegativeAssertion_AltSyntax_WhenShouldPassPositive_ShouldThrow()
                    {
                        // Arrange
                        var sut = typeof(Derived);
                        var customMessage = GetRandomString();
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to inherit"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Derived).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>();
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Base).PrettyName()));


                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(customMessage));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain("not to inherit"));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Derived).PrettyName()));
                        Assert.That(() =>
                        {
                            Expect(sut).To.Not.Inherit<Base>(customMessage);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message
                            .Contain(typeof(Base).PrettyName()));
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class InheritGeneric
            {
                public class MyList<T> : List<T>
                {
                }

                public class MyIntList : MyList<int>
                {
                }

                [Test]
                public void ShouldPassWithSystemGeneric()
                {
                    // Arrange
                    var sut = typeof(MyIntList);
                    // Act
                    Assert.That(() =>
                    {
                        Expect(sut).To.Inherit<MyList<int>>();
                        Expect(sut).To.Inherit<List<int>>();
                    }, Throws.Nothing);
                    // Assert
                }

                public abstract class SomeAbstractParent<T1, T2, T3>
                {
                }

                public class SomeT1
                {
                }

                public class SomeT2
                {
                }

                public class SomeT3
                {
                }

                public class SomeDerivedClass : SomeAbstractParent<SomeT1, SomeT2, SomeT3>
                {
                }

                [Test]
                public void ShouldPassWithConvolutedLocalGeneric()
                {
                    // Arrange
                    var sut = typeof(SomeDerivedClass);
                    // Act
                    Assert.That(() =>
                    {
                        Expect(sut).To.Inherit<SomeAbstractParent<SomeT1, SomeT2, SomeT3>>();
                    }, Throws.Nothing);
                    // Assert
                }
            }

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
                                var customMessage = GetRandomString(10);
                                var sut = new TestClass();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of<TestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to not be an instance of"));

                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of<TestClass>(customMessage);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(customMessage));

                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of<TestClass>(() => customMessage);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to not be an instance of"));

                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of(typeof(TestClass));
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to not be an instance of"));

                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of(typeof(TestClass), customMessage);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(customMessage));

                                Assert.That(() =>
                                {
                                    Expect(sut).Not.To.Be.An.Instance.Of(typeof(TestClass), () => customMessage);
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains(customMessage));


                                // Assert
                            }

                            [TestFixture]
                            public class AlternativeTypeValidationSyntax
                            {
                                [Test]
                                public void ShouldBeAbleToAssertAnObjectHasAType()
                                {
                                    // Arrange
                                    var obj = new TestClass();
                                    // Act
                                    Assert.That(
                                        () => Expect(obj)
                                            .To.Have.Type(typeof(TestClass)),
                                        Throws.Nothing
                                    );
                                    Assert.That(
                                        () => Expect(obj).To.Have.Type(typeof(AlternativeTypeValidationSyntax)),
                                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    );
                                    Assert.That(
                                        () => Expect(obj)
                                            .Not.To.Have.Type(typeof(TestClass)),
                                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    );
                                    Assert.That(
                                        () => Expect(obj)
                                            .To.Not.Have.Type(typeof(TestClass)),
                                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    );
                                    Assert.That(
                                        () => Expect(obj)
                                            .Not.To.Have.Type(typeof(AlternativeTypeValidationSyntax)),
                                        Throws.Nothing
                                    );
                                    Assert.That(
                                        () => Expect(obj)
                                            .To.Not.Have.Type(typeof(AlternativeTypeValidationSyntax)),
                                        Throws.Nothing
                                    );
                                    // Assert
                                }
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
                                    Expect(sut).To.Be.An.Instance.Of<TestClass>();
                                }, Throws.Nothing);

                                Assert.That(() =>
                                {
                                    Expect(sut).To.Be.An.Instance.Of(typeof(TestClass));
                                }, Throws.Nothing);
                                // AsserEt
                            }

                            [TestFixture]
                            public class FluentlyTestingOnResolvedInstaceType
                            {
                                [Test]
                                public void ShouldFacilitateFurtherTestingWithBooleanMatcher()
                                {
                                    // Arrange
                                    var sut = GetRandom<TestClass>();
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => o.Id == sut.Id);
                                    }, Throws.Nothing);

                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o =>
                                            {
                                                throw new Exception("oopsie");
#pragma warning disable CS0162
                                                return false;
#pragma warning restore CS0162
                                            });
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                        .With.Message.Contains("oopsie"));

                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => o.Id == sut.Id)
                                            .And
                                            .With(o => o.Name == sut.Name);
                                    }, Throws.Nothing);

                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => o.Id == sut.Id + 1);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                    // Assert
                                }

                                [Test]
                                public void ShouldFacilitateFurtherTestingWithComplexMatcher()
                                {
                                    // Arrange
                                    var sut = GetRandom<TestClass>();
                                    // Act
                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => Check(o.Id == sut.Id));
                                    }, Throws.Nothing);

                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => o.Id == sut.Id)
                                            .And
                                            .With(o => o.Name == sut.Name);
                                    }, Throws.Nothing);

                                    Assert.That(() =>
                                    {
                                        Expect(sut)
                                            .To.Be.An.Instance.Of<TestClass>()
                                            .With(o => o.Id == sut.Id + 1);
                                    }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                    // Assert

                                    IMatcherResult Check(bool result)
                                    {
                                        return new ResultOfMatcher(
                                            () => result,
                                            "Some message"
                                        );
                                    }
                                }

                                private class ResultOfMatcher : IMatcherResult
                                {
                                    public bool Passed { get; }
                                    public string Message { get; }
                                    public Exception LocalException { get; set; }

                                    public ResultOfMatcher(
                                        Func<bool> logic,
                                        string message
                                    )
                                    {
                                        Passed = logic();
                                        Message = message;
                                    }
                                }
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
                                    Expect(sut)
                                        .Not.To.Be.An.Instance.Of<AnotherTestClass>();
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
                                    Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message.Contains("to be an instance of"));
                                // Assert
                            }

                            [Test]
                            public void WithGenerics_ShouldThrowWithValidClassName()
                            {
                                // Arrange
                                var sut = new GenericTestClass<TestClass>();
                                // Pre-Assert
                                // Act
                                Assert.That(() =>
                                {
                                    Expect(sut).To.Be.An.Instance.Of<AnotherTestClass>();
                                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                                    .With.Message
                                    .EqualTo(
                                        "Expected\n<TestingInstanceOfInterfaceOrBaseClass+GenericTestClass<NExpect.Tests.Types.TestingInstanceOfInterfaceOrBaseClass+TestClass>>\nto be an instance of\n<NExpect.Tests.Types.TestingInstanceOfInterfaceOrBaseClass+AnotherTestClass>"
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
                                        Expect(obj).To.Be.An.Instance.Of<TestClass>();
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
                                        Expect(obj).Not.To.Be.An.Instance.Of<TestClass>();
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
                                        Expect(obj).To.Be.An.Instance.Of<ITestInterface>();
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
                                        Expect(obj).Not.To.Be.An.Instance.Of<ITestInterface>();
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
                                        Expect(sut).Not.To.Be.An.Instance.Of<TestClass>("Custom Message");
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
                                        Expect(sut).Not.To.Be.An.Instance.Of<List<TestClass>>();
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