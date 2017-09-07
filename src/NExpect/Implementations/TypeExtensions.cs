using System;
using System.Reflection;

namespace NExpect.Implementations
{
    internal static class TypeExtensions
    {
        private static readonly BindingFlags _findPublicPropertyAnywhereInInheritenceChain =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        internal static PropertyInfo GetPublicInstanceProperty(this Type t, string name)
        {
            return t.GetProperty(name, _findPublicPropertyAnywhereInInheritenceChain);
        }

        internal static PropertyInfo[] GetPublicInstanceProperties(this Type t)
        {
            return t.GetProperties(_findPublicPropertyAnywhereInInheritenceChain);
        }

        internal static T TryGetPropertyValue<T>(this object o, string prop)
        {
            var propInfo = o.GetType().GetPublicInstanceProperty(prop);
            if (propInfo == null)
                return default(T);
            try
            {
                return (T) propInfo.GetValue(o);
            }
            catch
            {
                return default(T);
            }
        }
    }
}