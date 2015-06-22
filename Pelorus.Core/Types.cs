using System;
using System.ComponentModel;

namespace Pelorus.Core
{
    /// <summary>
    /// Helper methods for casting objects to and from different types.
    /// </summary>
    public static class Types
    {
        /// <summary>
        /// Convert the given object to type 'T'.  If the object cannot be cast to type T then return the default value of T.
        /// </summary>
        /// <typeparam name="T">Type to cast object to.</typeparam>
        /// <param name="subject">Object to cast.</param>
        /// <returns>Subject cast to type T or the default value ot type T if a converter does not exist.</returns>
        public static T Cast<T>(object subject)
        {
            if (null == subject)
            {
                return default(T);
            }

            var subjectType = subject.GetType();
            var converter = TypeDescriptor.GetConverter(subjectType);

            if (converter.CanConvertFrom(subjectType))
            {
                return (T)converter.ConvertFrom(subject);
            }

            if (converter.CanConvertTo(typeof(T)))
            {
                return (T)converter.ConvertTo(subject, typeof(T));
            }

            converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.CanConvertTo(subjectType))
            {
                return (T)converter.ConvertTo(subject, subjectType);
            }

            if (converter.CanConvertFrom(subjectType))
            {
                return (T)converter.ConvertFrom(subject);
            }

            return default(T);
        }

        /// <summary>
        /// Convert the given object to type targetType.  If the object cannot be cast to type targetType then return null.
        /// </summary>
        /// <param name="subject">Object to cast.</param>
        /// <param name="targetType">Type to convert the given object to.</param>
        /// <returns>Subject cast to type targetType or null if a converter does not exist.</returns>
        public static object Cast(object subject, Type targetType)
        {
            if (null == subject)
            {
                return null;
            }

            var subjectType = subject.GetType();
            var converter = TypeDescriptor.GetConverter(subjectType);

            if (converter.CanConvertFrom(subjectType))
            {
                return converter.ConvertFrom(subject);
            }

            if (converter.CanConvertTo(targetType))
            {
                return converter.ConvertTo(subject, targetType);
            }

            converter = TypeDescriptor.GetConverter(targetType);

            if (converter.CanConvertTo(subjectType))
            {
                return converter.ConvertTo(subject, subjectType);
            }

            if (converter.CanConvertFrom(subjectType))
            {
                return converter.ConvertFrom(subject);
            }

            return null;
        }
    }
}
