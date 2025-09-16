using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.Utils;

namespace NExpect.Tests.ObjectEquality;

[TestFixture]
public class TestReflectiveMatchers
{
    [TestFixture]
    public class TestingPropertyValueByName
    {
        public class Data
        {
            public int id { get; set; }

            [Comment("this is commented!")]
            public bool IsCommented { get; set; }

            public bool IsNotCommented { get; set; }

            [Comment("moo-cakes")]
            public void CommentedMethod()
            {
            }

            public int Add(int a, int b)
            {
                return a + b;
            }
        }

        [Test]
        public void ShouldHandleMatchingNameAndValue()
        {
            // Arrange
            var data = new Data
            {
                id = 1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Value<Data>(1);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHandlePropertyNameMismatch()
        {
            // Arrange
            var badData = new
            {
                foo = "bar"
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(badData)
                        .To.Have.Property("id")
                        .With.Value(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        $"{badData.GetType().PrettyName()} to have a public property named 'id'"
                    )
            );
            Assert.That(
                () =>
                {
                    Expect(badData)
                        .Not.To.Have.Property("id")
                        .With.Value(1);
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldHandleValueMismatchOnFoundProperty()
        {
            // Arrange
            var data = new
            {
                id = 1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Value(2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("expected to find value of 2 for property 'id', but found 1")
            );

            Assert.That(
                () =>
                {
                    Expect(data)
                        .Not.To.Have.Property("id")
                        .With.Value(2);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("not to have property 'id'")
            );
            // Assert
        }

        [Test]
        public void ShouldFacilitateTypeTesting()
        {
            // Arrange
            var data = new Data()
            {
                id = 1
            };
            // Act
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Type(typeof(int))
                        .And.Value(1);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .Of.Type(typeof(int))
                        .With.Value(1);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Value(1)
                        .And.Type(typeof(int));
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property("id")
                        .With.Type(typeof(string))
                        .And.Value(1);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(
                        "Expected property 'Data.id' to be of type 'System.String', but it has type 'System.Int32'"
                    )
            );
            // Assert
        }

        [Test]
        public void ShouldBehaveOnTypeAsType()
        {
            // Arrange
            var type = typeof(Data);
            // Act
            Assert.That(
                () =>
                {
                    Expect(type)
                        .To.Have.Property(nameof(Data.id))
                        .With.Type(typeof(int));
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstMethodAttributes()
        {
            // Arrange
            var type = typeof(Data);
            var sut = GetRandom<Data>();
            // Act
            Assert.That(() =>
            {
                Expect(type)
                    .To.Have.Method(nameof(Data.CommentedMethod))
                    .With.Attribute<CommentAttribute>(o => o.Comment == "moo-cakes");
            }, Throws.Nothing);

            Assert.That(() =>
            {
                Expect(sut)
                    .To.Have.Method(nameof(Data.CommentedMethod))
                    .With.Attribute<CommentAttribute>(o => o.Comment == "moo-cakes");
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstPropertyAttributes()
        {
            // Arrange
            var data = GetRandom<Data>();
            // Act
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property(nameof(data.IsCommented))
                        .With.Attribute<CommentAttribute>();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(data)
                            .To.Have.Property(nameof(data.IsCommented))
                            .With.Type<bool>()
                            .And
                            .With
                            .Attribute<CommentAttribute>(o => o.Comment == "this is commented!");
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property(nameof(data.IsCommented))
                        .With.Type<string>("I wanted a string!")
                        .And
                        .With.Attribute<CommentAttribute>(o => o.Comment == "this is commented!");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("I wanted a string!")
            );
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property(nameof(data.IsCommented))
                        .With.Type<bool>()
                        .And
                        .With.Attribute<CommentAttribute>(o => o.Comment == "moo");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(data)
                        .To.Have.Property(nameof(data.IsCommented))
                        .With.Attribute<RequiredAttribute>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }
    }

    [TestFixture]
    public class AssertingAgainstMethods
    {
        [Test]
        public void ShouldBeAbleToAssertMethodPresence()
        {
            // Arrange
            var cow = new Cow();
            var goodMethod = nameof(cow.Moo);
            var badMethod = "MooMoo";
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(goodMethod);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .Not.To.Have.Method(goodMethod);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(badMethod);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(badMethod)
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodPresenceOnType()
        {
            // Arrange
            var cow = typeof(Cow);
            var goodMethod = nameof(Cow.Moo);
            var badMethod = "MooMoo";
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(goodMethod);
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .Not.To.Have.Method(goodMethod);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(badMethod);
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains(badMethod)
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodAttributes()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Moo))
                        .With.Attribute<CommentAttribute>();
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Moo))
                        .With.Attribute<CommentAttribute>(
                            c => c.Comment == "it's what cows do"
                        );
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.NotMoo))
                        .With.Attribute<CommentAttribute>(
                            c => c.Comment == "it's what cows do"
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("[Comment]")
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.Moo))
                        .With.Attribute<CommentAttribute>(
                            c => c.Comment == "it's not what cows do"
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("[Comment]")
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodDoesNotHaveAttribute()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.NotMoo))
                        .Without.Attribute<CommentAttribute>();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToDiscriminateBetweenOverloads()
        {
            // Arrange
            var cow = new Cow();

            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(
                            "NotMoo2",
                            mi => !mi.GetParameters().Any()
                        );
                    Expect(cow)
                        .To.Have.Method(
                            "NotMoo2",
                            mi => mi.GetParameters().FirstOrDefault()?.ParameterType == typeof(string)
                        );
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(cow)
                        .Not.To.Have.Method(
                            "NotMoo2",
                            mi => !mi.GetParameters().Any()
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method("NotMoo2");
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("single")
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodParametersAgainstInstance()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Add))
                        .With.Parameter("a")
                        .Of.Type(typeof(int))
                        .With.Attribute<FrobNozzleAttribute>();
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Add))
                        .With.Parameter("b")
                        .Of.Type(typeof(int));
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Add))
                        .Which.Returns(typeof(int));
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter()
                        .Of.Type<string>();
                },
                Throws.Nothing
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodParametersAgainstType()
        {
            // Arrange
            var cow = typeof(Cow);
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.Add))
                        .With.Parameter("a")
                        .Of.Type(typeof(int));
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.Add))
                        .With.Parameter("b")
                        .Of.Type(typeof(int));
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.Add))
                        .Which.Returns(typeof(int));
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.Echo))
                        .With.Parameter()
                        .Of.Type<string>();
                },
                Throws.Nothing
            );
            // Assert
        }

        [TestFixture]
        public class OptionalAndRequiredParameters
        {
            [Test]
            public void ShouldBeAbleToAssertOptionalParameterOnType()
            {
                // Arrange
                var type = typeof(Cow);
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(type)
                            .To.Have.Method("Random")
                            .With.Optional.Parameter("minimum")
                            .With.Type(typeof(int));
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(type)
                            .To.Have.Method("Random")
                            .With.Required.Parameter("minimum")
                            .With.Type(typeof(int));
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(
                            "parameter minimum on Cow.Random to be required"
                        )
                    );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertOptionalParameterOnMethodInfo()
            {
                // Arrange
                var method = typeof(Cow)
                    .GetMethod(nameof(Cow.Random));
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(method)
                            .To.Have.Optional.Parameter("minimum")
                            .With.Type(typeof(int));
                    },
                    Throws.Nothing
                );
                Assert.That(
                    () =>
                    {
                        Expect(method)
                            .To.Have.Required.Parameter("minimum")
                            .With.Type(typeof(int));
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains(
                            "parameter minimum on Cow.Random to be required"
                        )
                    );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertRequiredParameterOnType()
            {
                // Arrange
                var type = typeof(Cow);
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(type)
                            .To.Have.Method("Add")
                            .With.Required.Parameter("a")
                            .With.Type(typeof(int));
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(type)
                            .To.Have.Method("Add")
                            .With.Optional.Parameter("a")
                            .With.Type(typeof(int));
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("parameter a on Cow.Add to be optional")
                );
                // Assert
            }

            [Test]
            public void ShouldBeAbleToAssertRequiredParameterOnMethodInfo()
            {
                // Arrange
                var method = typeof(Cow)
                    .GetMethod(nameof(Cow.Add));
                // Act
                Assert.That(
                    () =>
                    {
                        Expect(method)
                            .To.Have.Required.Parameter("a")
                            .With.Type(typeof(int));
                    },
                    Throws.Nothing
                );

                Assert.That(
                    () =>
                    {
                        Expect(method)
                            .To.Have.Optional.Parameter("a")
                            .With.Type(typeof(int));
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("parameter a on Cow.Add to be optional")
                );
                // Assert
            }
        }

        [Test]
        public void ShouldBeAbleToAssertParameterless()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.NotMoo))
                        .With.No.Parameters();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Add))
                        .With.No.Parameters();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertMethodParameterAttributes()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter("toEcho")
                        .Of.Type<string>()
                        .With.Attribute<NotNullAttribute>();
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter()
                        .Of.Type<string>()
                        .With.Attribute<NotNullAttribute>();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter()
                        .Of.Type<string>()
                        .With.Attribute<NotUsedAttribute>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Contains("[NotUsed]")
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter("toEcho")
                        .Of.Type<string>()
                        .With.Attribute<NotUsedAttribute>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Match(".*'toEcho'.*\\[NotUsed\\]")
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstMultipleMethodAttributes()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.MultipleComments))
                        .With.Attribute<CommentAttribute>(
                            a => a.Comment.Contains("best of times")
                        );
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.MultipleComments))
                        .With.Attribute<CommentAttribute>(
                            a => a.Comment.Contains("dark and stormy night")
                        );
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(Cow.MultipleComments))
                        .With.Attribute<CommentAttribute>(
                            o => o.Comment.Contains("yabba dabba doo")
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstParameterThenMethodInfo()
        {
            // Arrange
            var cow = new Cow();
            // Act
            Assert.That(
                () =>
                {
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Parameter()
                        .Of.Type<string>()
                        .With.Attribute<NotNullAttribute>();
                    Expect(cow)
                        .To.Have.Method(nameof(cow.Echo))
                        .With.Attribute<CommentAttribute>();
                },
                Throws.Nothing
            );

            // Assert
        }
    }

    [TestFixture]
    public class AssertingAgainstTypes
    {
        [Test]
        public void ShouldBeAbleToAssertTypeHasAttribute()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(typeof(HasAttribute))
                        .To.Have.Attribute<CommentAttribute>();
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(typeof(HasAttribute))
                        .Not.To.Have.Attribute<CommentAttribute>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Match("HasAttribute.*\\[Comment\\]")
            );

            Assert.That(
                () =>
                {
                    Expect(typeof(HasNoAttribute))
                        .Not.To.Have.Attribute<CommentAttribute>();
                },
                Throws.Nothing
            );

            Assert.That(
                () =>
                {
                    Expect(typeof(HasNoAttribute))
                        .To.Have.Attribute<CommentAttribute>();
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.Match("HasNoAttribute.*\\[Comment\\]")
            );

            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertAgainstAttributeData()
        {
            // Arrange
            // Act
            Assert.That(
                () =>
                {
                    Expect(typeof(HasAttribute))
                        .To.Have.Attribute<CommentAttribute>(
                            a => a.Comment == "has a comment"
                        );
                },
                Throws.Nothing
            );
            Assert.That(
                () =>
                {
                    Expect(typeof(HasAttribute))
                        .To.Have.Attribute<CommentAttribute>(
                            a => a.Comment == "has no comment"
                        );
                },
                Throws.Exception.InstanceOf<UnmetExpectationException>()
            );
            // Assert
        }

        [Comment("has a comment")]
        public class HasAttribute
        {
        }

        public class HasNoAttribute
        {
        }
    }

    public class NotNullAttribute : Attribute
    {
    }

    public class NotUsedAttribute : Attribute
    {
    }

    public class SuperCow : Cow
    {
        [Comment("super-cow")]
        public override void Overrideable()
        {
            base.Overrideable();
        }
    }

    [AttributeUsage(
        AttributeTargets.All,
        AllowMultiple = true,
        Inherited = true
    )]
    public class CommentAttribute : Attribute
    {
        public string Comment { get; }

        public CommentAttribute(
            string comment
        )
        {
            Comment = comment;
        }
    }

    public class FrobNozzleAttribute: Attribute
        {
            }

    public class Cow
    {
        [Comment("it's what cows do")]
        public void Moo()
        {
        }

        public void NotMoo()
        {
        }

        public void NotMoo2()
        {
        }

        public void NotMoo2(string msg)
        {
            Console.WriteLine(msg);
        }

        public int Add([FrobNozzle]int a, int b)
        {
            return a + b;
        }

        [Comment("echos")]
        public string Echo([NotNull] string toEcho)
        {
            return $"echo: {toEcho}";
        }

        public virtual void Overrideable()
        {
        }

        [Comment("It was a dark and stormy night")]
        [Comment("It was the best of times, it was the worst of times")]
        public void MultipleComments()
        {
        }

        public int Random(int minimum = 0)
        {
            return GetRandomInt(minimum, minimum + 10);
        }
    }
}
