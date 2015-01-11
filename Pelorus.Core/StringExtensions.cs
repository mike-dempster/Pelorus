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
    }
}
