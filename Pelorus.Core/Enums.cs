using System;

namespace Pelorus.Core
{
    /// <summary>
    /// Helper methods for working with enums.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Parse a string for the name of an enum value.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum that is represented by the string.</typeparam>
        /// <param name="enumName">String to parse for an enum value.</param>
        /// <returns>
        /// Enum value represented by the string or default(TEnum) if the enum value could not be parsed from the string.
        /// </returns>
        public static TEnum Parse<TEnum>(string enumName)
            where TEnum : struct
        {
            TEnum value;
            bool result = Enum.TryParse<TEnum>(enumName, out value);

            if(result)
            {
                return value;
            }

            return default(TEnum);
        }
    }
}
