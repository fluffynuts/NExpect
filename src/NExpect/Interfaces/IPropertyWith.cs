using System;

namespace NExpect.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPropertyWith<T> : IWith<T>
{
    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>();
    
    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <param name="customMessage"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>(string customMessage);
    
    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>(Func<string> customMessageGenerator);

    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <param name="matcher"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> matcher
    );

    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <param name="matcher"></param>
    /// <param name="customMessage"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> matcher,
        string customMessage
    );

    /// <summary>
    /// Tests if the property in the current context has an attribute with the given type
    /// </summary>
    /// <param name="attributeMatcher"></param>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Attribute<TAttribute>(
        Func<TAttribute, bool> attributeMatcher,
        Func<string> customMessageGenerator
    );
    
    /// <summary>
    /// Tests if the property in the current context has the given type
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Type<TProperty>();

    /// <summary>
    /// Tests if the property in the current context has the given type
    /// </summary>
    /// <param name="customMessage"></param>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Type<TProperty>(
        string customMessage
    );

    /// <summary>
    /// Tests if the property in the current context has the given type
    /// </summary>
    /// <param name="customMessageGenerator"></param>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns></returns>
    IPropertyMore<T> Type<TProperty>(
        Func<string> customMessageGenerator
    );
}