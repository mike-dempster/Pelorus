using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
                return null;
            }

            return Enumerable.Select<T, TResult>(source, selector);
        }

        /// <summary>
        /// Projects each element of a sequence to a new form.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the collection.</typeparam>
        /// <typeparam name="TResult">Type of the value returned by the selector.</typeparam>
        /// <param name="source">Collection to project to a new form.</param>
        /// <param name="selector">A transformation function to apply to each element.</param>
        /// <returns>Collection of items projected from type T to type TResult.</returns>
        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable source, Func<T, TResult> expression)
        {
            if (null == source)
            {
                yield break;
            }

            foreach (var element in source)
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

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The System.Collections.Generic.IEnumerable<T> to return the first element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> source)
        {
            if (null == source)
            {
                return default(T);
            }

            return Enumerable.FirstOrDefault<T>(source);
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable<T> to return the first element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        public static object FirstOrDefault(this IEnumerable source)
        {
            if (null == source)
            {
                return null;
            }

            var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            var firstElement = enumerator.Current;

            return firstElement;
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">The System.Collections.Generic.IEnumerable<T> to return the first element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        public static T FirstOrDefault<T>(this IEnumerable source)
        {
            if (null == source)
            {
                return default(T);
            }

            var enumerator = source.GetEnumerator();
            bool result = enumerator.MoveNext();

            if (false == result)
            {
                return default(T);
            }

            var firstElement = enumerator.Current;

            if (firstElement is T)
            {
                return (T)firstElement;
            }

            string exMsg = string.Format(
                        CultureInfo.InvariantCulture,
                        "Element of type '{0}' cannot be cast to type '{1}'.",
                        firstElement.GetType().FullName,
                        typeof(T).FullName);
                throw new InvalidCastException(exMsg);
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="source">The IEnumerable to check for emptiness.</param>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        public static bool Any(this IEnumerable source)
        {
            if (null == source)
            {
                return false;
            }

            var enumerator = source.GetEnumerator();
            bool result = enumerator.MoveNext();

            return result;
        }
    }
}
