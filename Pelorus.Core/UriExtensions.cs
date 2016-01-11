using System;
using System.Collections.Generic;

namespace Pelorus.Core
{
    /// <summary>
    /// Extension methods for working with Uri's.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the query arguments of the source Uri.
        /// </summary>
        /// <param name="source">Uri to parse the query arguments from.</param>
        /// <returns>Dictionary of query argument key/value pairs.</returns>
        public static IDictionary<string, string> GetQueryArguments(this Uri source)
        {
            if ((null == source) || (string.IsNullOrWhiteSpace(source.Query)))
            {
                return null;
            }

            var queryArguments = new Dictionary<string, string>();
            var arguments = source.Query.TrimStart('?')
                                  .Split('&');

            foreach (var arg in arguments)
            {
                var separatorIndex = arg.IndexOf('=');
                string escapedKey = arg.Substring(0, separatorIndex);
                string escapedValue = arg.Substring(separatorIndex + 1);
                string key = Uri.UnescapeDataString(escapedKey);
                string value = Uri.UnescapeDataString(escapedValue);

                queryArguments.Add(key, value);
            }

            return queryArguments;
        }
    }
}
