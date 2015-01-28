using System;
using System.Linq;

namespace Pelorus.Core
{
    /// <summary>
    /// Extension methods for working with strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Concatenate a collection of strings and trim the resulting sequence from the start of the string.
        /// </summary>
        /// <param name="subject">String to trim.</param>
        /// <param name="args">Collection of strings to concatenate and trim from the subject string.</param>
        /// <returns>Trimmed string.</returns>
        public static string ConcatAndTrimStart(this string subject, params string[] args)
        {
            string concat = string.Join(string.Empty, args);
            string trimmed = subject.Substring(concat.Length);

            return trimmed;
        }

        /// <summary>
        /// Convert a string to a Base64 representation.
        /// </summary>
        /// <param name="source">String to convert to Base64.</param>
        /// <returns>Base64 string of the source string.</returns>
        public static string ToBase64String(this string source)
        {
            if (null == source)
            {
                return null;
            }

            if (string.IsNullOrEmpty(source))
            {
                return "====";
            }

            var base64Bytes = source.Select(e => (byte)e)
                                    .ToArray();
            string base64String = Convert.ToBase64String(base64Bytes);

            return base64String;
        }

        /// <summary>
        /// Convert a Base64 string to the original string.
        /// </summary>
        /// <param name="source">Base64 string to convert to clear text.</param>
        /// <returns>Clear text string represented by the given Baes64 string.</returns>
        public static string FromBase64String(this string source)
        {
            if (null == source)
            {
                return null;
            }

            if ("====" == source)
            {
                return string.Empty;
            }

            var clearTextBytes = Convert.FromBase64String(source);
            var clearTextChars = clearTextBytes.Select(e => (char)e)
                                               .ToArray();
            string clearTextString = new string(clearTextChars);

            return clearTextString;
        }
    }
}
