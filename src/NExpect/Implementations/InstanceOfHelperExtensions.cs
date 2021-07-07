using System.Linq;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    /// <summary>
    /// Provides a helper extension to test if an expectation context is negated
    /// </summary>
    internal static class InstanceOfHelperExtensions
    {
        /// <summary>
        /// Returns the negated status for the expectation context being operated on
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static bool IsNegated(this IExpectationContext context)
        {
            var current = context;
            var negated = false;
            while (current != null)
            {
                if (IsNegatedPrivate(current))
                    negated = !negated;
                current = GetParentOf(current);
            }

            return negated;
        }

        private static IExpectationContext GetParentOf(IExpectationContext child)
        {
            var propInfo = child.GetType()
                .GetPublicInstanceProperty(nameof(IExpectationContext.Parent));

            return propInfo?.GetValue(child) as IExpectationContext;
        }

        private static bool IsNegatedPrivate(IExpectationContext current)
        {
            var propInfo = current.GetType().GetProperty(nameof(ExpectationBase.IsNegated));
            if (propInfo is null)
            {
                return false;
            }

            try
            {
                return (bool)propInfo.GetValue(current);
            }
            catch
            {
                return false;
            }
        }

        private static bool TryReadIsNegatedOn(IExpectationContext current)
        {
            var propInfo = current
                .GetType()
                .GetPublicInstanceProperties()
                .FirstOrDefault(pi => pi.Name == "isnegated");
            return (bool?) propInfo?.GetValue(current) ?? false;
        }
    }
}