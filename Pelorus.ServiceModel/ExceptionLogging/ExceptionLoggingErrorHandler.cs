using Pelorus.Core.Diagnostics;
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Pelorus.ServiceModel.ExceptionLogging
{
    /// <summary>
    /// Log an exception that was thrown in the WCF service call.
    /// </summary>
    public class ExceptionLoggingErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Log the exception.
        /// </summary>
        /// <param name="error">Exception that is being handled.</param>
        /// <returns>Returns false.</returns>
        public bool HandleError(Exception error)
        {
            Logging.LogException(error);
            return false;
        }

        /// <summary>
        /// Enables the creation of a custom System.ServiceModel.FaultException&gt;TDetail&lt; that is returned from an exception in the course of a
        /// service method.
        /// </summary>
        /// <param name="error">Exception that was thrown.</param>
        /// <param name="version">Version of the message.</param>
        /// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }
    }
}
