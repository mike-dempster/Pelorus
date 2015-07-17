using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Pelorus.Core.Reflection
{
    /// <summary>
    /// Extension methods for testing and operating on Type objects.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determine if the subject type implements the given interface.
        /// </summary>
        /// <typeparam name="TInterface">Interface type to test for.</typeparam>
        /// <param name="subject">Type object describing the type to test.</param>
        /// <returns>True if the type implements the given interface otherwise false.</returns>
        public static bool ImplementsInterface<TInterface>(this Type subject)
        {
            return subject.GetInterfaces()
                          .Contains(typeof (TInterface));
        }

        /// <summary>
        /// Get a method implemented on type T.
        /// </summary>
        /// <typeparam name="T">Type that implements the method.</typeparam>
        /// <param name="methodExpression">Method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T>(Expression<Action<T>> methodExpression)
        {
            var expr = methodExpression.Body as MethodCallExpression;

            if (null == expr)
            {
                return null;
            }

            return expr.Method;
        }

        /// <summary>
        /// Get a method with no arguments.
        /// </summary>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var methodInfo = subject.GetMethod(methodName, new Type[0]);

            return methodInfo;
        }

        /// <summary>
        /// Get a method with one argument.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T>(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var argTypes = new[]
            {
                typeof (T)
            };
            var methodInfo = subject.GetMethod(methodName, argTypes);

            return methodInfo;
        }

        /// <summary>
        /// Get a method with two arguments.
        /// </summary>
        /// <typeparam name="T0">Type of the first argument.</typeparam>
        /// <typeparam name="T1">Type of the second argument.</typeparam>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T0, T1>(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var argTypes = new[]
            {
                typeof (T0),
                typeof (T1)
            };
            var methodInfo = subject.GetMethod(methodName, argTypes);

            return methodInfo;
        }

        /// <summary>
        /// Get a method with three arguments.
        /// </summary>
        /// <typeparam name="T0">Type of the first argument.</typeparam>
        /// <typeparam name="T1">Type of the second argument.</typeparam>
        /// <typeparam name="T2">Type of the third argument.</typeparam>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T0, T1, T2>(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var argTypes = new[]
            {
                typeof (T0),
                typeof (T1),
                typeof (T2)
            };
            var methodInfo = subject.GetMethod(methodName, argTypes);

            return methodInfo;
        }

        /// <summary>
        /// Get a method with four arguments.
        /// </summary>
        /// <typeparam name="T0">Type of the first argument.</typeparam>
        /// <typeparam name="T1">Type of the second argument.</typeparam>
        /// <typeparam name="T2">Type of the third argument.</typeparam>
        /// <typeparam name="T3">Type of the forth argument.</typeparam>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T0, T1, T2, T3>(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var argTypes = new[]
            {
                typeof (T0),
                typeof (T1),
                typeof (T2),
                typeof (T3)
            };
            var methodInfo = subject.GetMethod(methodName, argTypes);

            return methodInfo;
        }

        /// <summary>
        /// Get a method with five arguments.
        /// </summary>
        /// <typeparam name="T0">Type of the first argument.</typeparam>
        /// <typeparam name="T1">Type of the second argument.</typeparam>
        /// <typeparam name="T2">Type of the third argument.</typeparam>
        /// <typeparam name="T3">Type of the forth argument.</typeparam>
        /// <typeparam name="T4">Type of the fifth argument.</typeparam>
        /// <param name="subject">Type to search for the method on.</param>
        /// <param name="methodName">Name of the method to search for.</param>
        /// <returns>MethodInfo of the specified method.</returns>
        public static MethodInfo Method<T0, T1, T2, T3, T4>(this Type subject, string methodName)
        {
            if ((null == subject) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return null;
            }

            var argTypes = new[]
            {
                typeof (T0),
                typeof (T1),
                typeof (T2),
                typeof (T3),
                typeof (T4)
            };
            var methodInfo = subject.GetMethod(methodName, argTypes);

            return methodInfo;
        }
    }
}
