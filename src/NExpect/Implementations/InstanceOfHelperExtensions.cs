using System.Linq;
using NExpect.Interfaces;

namespace NExpect.Implementations
{
    internal static class InstanceOfHelperExtensions
    {
        public static bool IsNegated(this IExpectationContext context)
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
            var concrete = current as ExpectationBase;
            return concrete?.IsNegated ?? TryReadIsNegatedOn(current);
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