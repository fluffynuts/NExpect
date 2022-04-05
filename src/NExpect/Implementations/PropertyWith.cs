using System;
using System.Linq;
using System.Reflection;
using Imported.PeanutButter.Utils;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
using static NExpect.Implementations.MessageHelpers;

namespace NExpect.Implementations;

// ReSharper disable once ClassNeverInstantiated.Global
internal class PropertyWith<T>
    : With<T>, IPropertyWith<T>
{
    public IHave<T> Have { get; private set; }

    public PropertyWith(Func<T> actualFetcher) : base(actualFetcher)
    {
    }

    public void SetHave(IHave<T> have)
    {
        Have = have;
    }

    public IPropertyMore<T> Attribute<TAttribute>()
    {
        return Attribute<TAttribute>(
            null,
            () => NULL_STRING
        );
    }

    public IPropertyMore<T> Attribute<TAttribute>(string customMessage)
    {
        return Attribute<TAttribute>(
            null,
            () => customMessage
        );
    }

    public IPropertyMore<T> Attribute<TAttribute>(Func<string> customMessageGenerator)
    {
        return Attribute<TAttribute>(
            null,
            customMessageGenerator
        );
    }

    public IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> matcher
    )
    {
        return Attribute<TAttribute>(
            matcher,
            () => NULL_STRING
        );
    }

    public IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> matcher,
        string customMessage
    )
    {
        return Attribute<TAttribute>(
            matcher,
            () => customMessage
        );
    }

    public IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> attributeMatcher,
        Func<string> customMessageGenerator
    )
    {
        return new PropertyMore<T>(
            Have.AddMatcher(actual =>
            {
                var havePropInfo = actual.TryGetMetadata<PropertyInfo>(
                    ReflectiveExtensions.METADATA_KEY_PROPERTY_INFO,
                    out var propertyInfo
                );
                if (!havePropInfo)
                {
                    return new EnforcedMatcherResult(false, () => "No propInfo found for current expectation chain");
                }

                var attribs = propertyInfo.GetCustomAttributes()
                    .OfType<TAttribute>()
                    .Where(attributeMatcher ?? (a => true))
                    .ToArray();
                var passed = attribs.Any();
                var property = NameOf(propertyInfo);
                var attrib = typeof(TAttribute).Name;
                return new MatcherResult(
                    passed,
                    FinalMessageFor(
                        () => attributeMatcher is null
                            ? $"Expected {passed.AsNot()}to find matching [{attrib}] decoration on {property}"
                            : $"Expected {passed.AsNot()}to find [{attrib}] decoration on {property}",
                        customMessageGenerator
                    )
                );
            }),
            Have
        );
    }

    private static string NameOf(PropertyInfo propertyInfo)
    {
        return $"{propertyInfo.DeclaringType}.{propertyInfo.Name}";
    }

    public IPropertyMore<T> Type<TProperty>()
    {
        return Type<TProperty>(() => NULL_STRING);
    }

    public IPropertyMore<T> Type<TProperty>(
        string customMessage
    )
    {
        return Type<TProperty>(() => customMessage);
    }

    public IPropertyMore<T> Type<TProperty>(
        Func<string> customMessageGenerator
    )
    {
        return new PropertyMore<T>(
            Have.AddMatcher(actual =>
                {
                    var havePropInfo = actual.TryGetMetadata<PropertyInfo>(
                        ReflectiveExtensions.METADATA_KEY_PROPERTY_INFO,
                        out var prop
                    );
                    if (!havePropInfo)
                    {
                        return new EnforcedMatcherResult(false,
                            () => "No propInfo found for current expectation chain");
                    }

                    var expected = typeof(TProperty);
                    var passed = prop.PropertyType == expected;
                    return new MatcherResult(
                        passed,
                        FinalMessageFor(
                            () => $"Expected {NameOf(prop)} {passed.AsNot()}to have type {expected}",
                            customMessageGenerator
                        )
                    );
                }
            ),
            Have);
    }
}