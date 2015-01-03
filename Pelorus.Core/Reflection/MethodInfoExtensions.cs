using System;
using System.Reflection;

namespace Pelorus.Core.Reflection
{
    /// <summary>
    /// Extension methods for testing and operating on method info objects.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Determine if a method is decorated with the given attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type to test for.</typeparam>
        /// <param name="subject">Method info object to test.</param>
        /// <returns>True if the method is decorated with the given attribute type otherwise false.</returns>
        public static bool HasAttribute<TAttribute>(this MethodInfo subject)
            where TAttribute : Attribute
        {
            return HasAttribute<TAttribute>(subject, false);
        }

        /// <summary>
        /// Determine if a method is decorated with the given attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type to test for.</typeparam>
        /// <param name="subject">Method info object to test.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
        /// <returns>True if the method is decorated with the given attribute type otherwise false.</returns>
        public static bool HasAttribute<TAttribute>(this MethodInfo subject, bool inherit)
            where TAttribute : Attribute
        {
            return null == subject.GetCustomAttributes(typeof(TAttribute), inherit);
        }
    }
}
