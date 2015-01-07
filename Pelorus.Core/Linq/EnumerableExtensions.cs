using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Pelorus.Core.Linq
{
    /// <summary>
    /// Extension methods for objects that implement IEnumerable or IEnumerable&lt;T&gt;.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Projects each element of a sequence to a new form.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the collection.</typeparam>
        /// <typeparam name="TResult">Type of the value returned by the selector.</typeparam>
        /// <param name="source">Collection to project to a new form.</param>
        /// <param name="selector">A transformation function to apply to each element.</param>
        /// <returns>Collection of items projected from type T to type TResult.</returns>
        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            if (null == source)
            {
                yield break;
            }

            foreach (var element in source)
            {
                yield return selector(element);
            }
        }

        /// <summary>
        /// Projects each element of a sequence to a new form.
        /// </summary>
        /// <typeparam name="T">Expected type of the elements in the collection.</typeparam>
        /// <typeparam name="TResult">Type of the value returned by the selector.</typeparam>
        /// <param name="source">Collection to project to a new form.</param>
        /// <param name="selector">A transformation function to apply to each element.</param>
        /// <returns>Collection of items projected from type T to type TResult.</returns>
        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable subject, Func<T, TResult> expression)
        {
            if (null == subject)
            {
                yield break;
            }

            foreach (var element in subject)
            {
                if (element is T)
                {
                    yield return expression((T)element);
                }

                string exMsg = string.Format(
                        CultureInfo.InvariantCulture,
                        "Element of type '{0}' cannot be cast to type '{1}'.",
                        element.GetType().FullName,
                        typeof(T).FullName);
                throw new InvalidCastException(exMsg);
            }
        }
    }
}
