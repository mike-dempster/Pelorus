using System;
using System.ServiceModel.Configuration;

namespace Pelorus.Core.ServiceModel.ExceptionLogging
{
    /// <summary>
    /// Configuration element for WCF behavior to catch unhandled exceptions.
    /// </summary>
    public class ExceptionLoggingBehaviorElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Create a new instance of the behavior.
        /// </summary>
        /// <returns>Exception logging behavior instance</returns>
        protected override object CreateBehavior()
        {
            return new ExceptionLoggingBehavior();
        }

        /// <summary>
        /// Type of the exception logging behavior.
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(ExceptionLoggingBehavior); }
        }
    }
}