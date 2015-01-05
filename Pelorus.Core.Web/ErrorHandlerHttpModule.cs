using Pelorus.Core.Diagnostics;
using System;
using System.Web;

namespace Pelorus.Core.Web
{
    /// <summary>
    /// Catches unhandled exceptions and logs them with the trace listeners.
    /// </summary>
    public class ErrorHandlerHttpModule : IHttpModule
    {
        /// <summary>
        /// Initialize the HTTP module to respond to unhandled exceptions.
        /// </summary>
        /// <param name="context">
        /// An HttpApplication that provides access to the methods, properties, and events common to all application objects within an 
        /// ASP.NET application.
        /// </param>
        public void Init(HttpApplication context)
        {
            context.Error += this.ErrorHandler;
        }

        /// <summary>
        /// Dispose of any resources held by the HTTP module.
        /// </summary>
        public void Dispose()
        {
            // No resources to dispose
        }

        /// <summary>
        /// Logs unhandled exceptions to the trace listeners.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ErrorHandler(object sender, EventArgs e)
        {
            var context = sender as HttpApplication;

            if (null == context)
            {
                return;
            }

            var exception = context.Server.GetLastError();
            Logging.LogException(exception);
        }
    }
}
