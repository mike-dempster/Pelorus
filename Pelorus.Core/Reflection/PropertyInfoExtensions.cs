using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace Pelorus.Core.Reflection
{
    /// <summary>
    /// Extension methods for testing and operating on property info objects.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Determine if a property is decorated with the given attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type to test for.</typeparam>
        /// <param name="subject">Property info object to test.</param>
        /// <returns>True if the property is decorated with the given attribute type otherwise false.</returns>
        public static bool HasAttribute<TAttribute>(this PropertyInfo subject)
            where TAttribute : Attribute
        {
            return HasAttribute<TAttribute>(subject, false);
        }

        /// <summary>
        /// Determine if a property is decorated with the given attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type to test for.</typeparam>
        /// <param name="subject">Property info object to test.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
        /// <returns>True if the property is decorated with the given attribute type otherwise false.</returns>
        public static bool HasAttribute<TAttribute>(this PropertyInfo subject, bool inherit)
            where TAttribute : Attribute
        {
            return subject.GetCustomAttributes(typeof(TAttribute), inherit).Any();
        }

        /// <summary>
        /// Get the property info of a property expression.
        /// </summary>
        /// <typeparam name="T">Type of the property's parent object.</typeparam>
        /// <typeparam name="TResult">Type of the property.</typeparam>
        /// <param name="expression">Property expression identifying the target property.</param>
        /// <returns>PropertyInfo of the target property.</returns>
        public static PropertyInfo Property<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            MemberExpression member = null;

            if (expression.Body is UnaryExpression)
            {
                var body = expression.Body as UnaryExpression;
                member = body.Operand as MemberExpression;
            }
            else if (expression.Body is MemberExpression)
            {
                member = expression.Body as MemberExpression;
            }

            if (null == member)
            {
                return null;
            }

            var property = member.Member as PropertyInfo;

            return property;
        }
    }
}
