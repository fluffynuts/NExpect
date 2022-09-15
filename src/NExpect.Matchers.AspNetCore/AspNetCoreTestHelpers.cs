using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
// ReSharper disable MemberCanBePrivate.Global

namespace NExpect
{
    /// <summary>
    /// Implementation of IOptions&lt;MvcOptions&gt; to be used
    /// for generating ViewDataDictionaries, or whatever else
    /// you might like (:
    /// </summary>
    public class OptionsAccessor : IOptions<MvcOptions>
    {
        /// <inheritdoc />
        public MvcOptions Value { get; } = new MvcOptions();
    }

    /// <summary>
    /// Provides some test helpers for working with asp.net core
    /// </summary>
    public static class AspNetCoreTestHelpers
    {
        /// <summary>
        /// Create a ViewDataDictionary for the provided model
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ViewDataDictionary CreateViewDataFor<T>(
            T model
        )
        {
            var result = CreateViewData();
            result.Model = model;
            return result;
        }

        /// <summary>
        /// Creates a ViewDataDictionary without a model
        /// </summary>
        /// <returns></returns>
        public static ViewDataDictionary CreateViewData()
        {
            // Long ago and far away, one used to just be able to new
            // up a ViewDataDictionary. Now look at this mess. :|
            var compositeProvider = new DefaultCompositeMetadataDetailsProvider(
                new[] { new DefaultBindingMetadataProvider() }
            );
            var optionsAccessor = new OptionsAccessor();
            var provider = new DefaultModelMetadataProvider(
                compositeProvider,
                optionsAccessor
            );
            return new ViewDataDictionary(
                provider,
                new ModelStateDictionary()
            );
        }
    }
}