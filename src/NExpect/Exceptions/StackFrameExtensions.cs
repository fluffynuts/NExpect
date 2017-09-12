using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NExpect.Exceptions
{
    internal static class StackFrameExtensions
    {
        private static readonly Assembly _thisAssembly =
            typeof(UnmetExpectationException).Assembly;

        internal static bool IsFromOrReferencesThisAssembly(
            this StackFrame frame
        )
        {
            var methodInfo = frame.GetMethod();
            if (methodInfo?.DeclaringType?.Assembly == _thisAssembly)
                return true;
            var parameters = methodInfo?.GetParameters() ?? new ParameterInfo[0];
            return parameters.Any(p => p.ParameterType.Assembly == _thisAssembly);
        }
    }
}