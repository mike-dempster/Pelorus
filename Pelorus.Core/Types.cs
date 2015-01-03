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

            var subjectType = typeof(T);
            var converter = TypeDescriptor.GetConverter(subjectType);

            if (converter.CanConvertFrom(subjectType))
            {
                return (T)converter.ConvertFrom(subject);
            }

            converter = TypeDescriptor.GetConverter(subject);

            if (converter.CanConvertTo(subjectType))
            {
                return (T)converter.ConvertTo(subject, subjectType);
            }

            return default(T);
        }
    }
}
