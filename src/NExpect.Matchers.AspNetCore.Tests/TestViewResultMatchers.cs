using Microsoft.AspNetCore.Mvc;
using NExpect.Exceptions;
using PeanutButter.TestUtils.AspNetCore.Builders;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NExpect.Matchers.AspNet.Tests;

[TestFixture]
public class TestViewResultMatchers
{
    [TestFixture]
    public class MatchingViewResults
    {
        [Test]
        public void ShouldMatchPositiveViewName()
        {
            // Arrange
            var viewResult = new ViewResult()
            {
                ViewName = GetRandomString(10)
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .Not.To.Be.A.View()
                    .With.Name(viewResult.ViewName);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldFailWithIncorrectViewName()
        {
            // Arrange
            var viewResult = new ViewResult()
            {
                ViewName = GetRandomString()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(GetAnother(viewResult.ViewName));
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }


        [Test]
        public void ShouldMatchModelWithMatcher()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new ViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model(o => o.DeepEquals(model));
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Model(o => o.DeepEquals(model))
                    .And.Name(viewResult.ViewName);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model(model);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model(o => o.DeepEquals(other));
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        [Repeat(100)]
        public void ShouldMatchModelWithTypedMatcher()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new ViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model<Model>(o => o.Name == model.Name);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model<Model>(o => o.Name == other.Name);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldPerformDeepEqualityOnProvidedModelObject()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new ViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model(model);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.View()
                    .With.Name(viewResult.ViewName)
                    .And.Model(other);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains("Property value mismatch"));
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertNoModel()
        {
            // Arrange
            var noModel = new ViewResult()
            {
                ViewName = "no-model"
            };
            var hasModel = new ViewResult()
            {
                ViewName = "has-model",
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(new object())
                    .Build()
            };
            // Act
            Assert.That(() =>
            {
                Expect(noModel)
                    .To.Be.A.View()
                    .With.Name("no-model")
                    .And.Without.Model();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(hasModel)
                    .To.Be.A.View()
                    .With.Name("has-model")
                    .And.Without.Model();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }

    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class MatchingPartialViewResults
    {
        [Test]
        public void ShouldMatchPositiveViewName()
        {
            // Arrange
            var viewResult = new PartialViewResult()
            {
                ViewName = GetRandomString()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .Not.To.Be.A.View()
                    .With.Name(viewResult.ViewName);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldFailWithIncorrectViewName()
        {
            // Arrange
            var viewResult = new PartialViewResult()
            {
                ViewName = GetRandomString()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(GetAnother(viewResult.ViewName));
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldMatchModelWithMatcher()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new PartialViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model(o => o.DeepEquals(viewResult.ViewData.Model));
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model(o => o.DeepEquals(other));
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldMatchModelWithTypedMatcher()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new PartialViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model<Model>(o => o.Name == model.Name);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model<Model>(o => o.Name == other.Name);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }

        [Test]
        public void ShouldPerformDeepEqualityTestingOnProvidedModel()
        {
            // Arrange
            var model = GetRandom<Model>();
            var other = GetRandom<Model>();
            var viewResult = new PartialViewResult()
            {
                ViewName = GetRandomString(),
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(model)
                    .Build()
            };

            // Act
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model(model);
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(viewResult)
                    .To.Be.A.PartialView()
                    .With.Name(viewResult.ViewName)
                    .And.Model(other);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                .With.Message.Contains("Property value mismatch"));
            // Assert
        }

        [Test]
        public void ShouldBeAbleToAssertNoModel()
        {
            // Arrange
            var noModel = new PartialViewResult()
            {
                ViewName = "no-model"
            };
            var hasModel = new PartialViewResult()
            {
                ViewName = "has-model",
                ViewData = ViewDataDictionaryBuilder.Create()
                    .WithModel(new object())
                    .Build()
            };
            // Act
            Assert.That(() =>
            {
                Expect(noModel)
                    .To.Be.A.PartialView()
                    .With.Name("no-model")
                    .And.Without.Model();
            }, Throws.Nothing);
            Assert.That(() =>
            {
                Expect(hasModel)
                    .To.Be.A.PartialView()
                    .With.Name("has-model")
                    .And.Without.Model();
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            // Assert
        }
    }
}