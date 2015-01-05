using System;
using System.Diagnostics;
using System.Globalization;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Contains methods for formatting exception information for human readability.
    /// </summary>
    internal sealed class ExceptionInfoFormatter
    {
        /// <summary>
        /// Format the message of the exception and the inner exceptions.
        /// </summary>
        /// <param name="ex">Outer most exception to format.</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatMessage(Exception ex)
        {
            if (null == ex.InnerException)
            {
                return ex.Message;
            }

            string innerMessage = FormatMessage(ex.InnerException);
            string formattedMessage = string.Format(CultureInfo.InvariantCulture, "{0} --> {1}", ex.Message, innerMessage);

            return formattedMessage;
        }

        /// <summary>
        /// Format the stack trace of the exception and the inner exceptions.
        /// </summary>
        /// <param name="ex">Outer most exception to format.</param>
        /// <returns>Formatted stack trace for the exception and all inner exceptions.</returns>
        public static string FormatStackTrace(Exception ex)
        {
            if (null == ex.InnerException)
            {
                return ex.StackTrace;
            }

            string innerStack = FormatStackTrace(ex.InnerException);
            string formattedStack = string.Format(
                CultureInfo.InvariantCulture,
                "{0}\n{1}-- End of inner stack trace --\n{2}",
                innerStack,
                new string(' ', Trace.IndentSize),
                ex.StackTrace);

            return formattedStack;
        }
    }
}
