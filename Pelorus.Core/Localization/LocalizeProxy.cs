using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Pelorus.Core.Localization
{
    /// <summary>
    /// This class implements a proxy layer that will transform all DateTime values being passed in to a method on the instance to UTC
    /// and it will transform the returned DatETime values from the method to a given time zone.
    /// </summary>
    public class LocalizeProxy : RealProxy
    {
        /// <summary>
        /// Underlying object instance.
        /// </summary>
        protected readonly object _instance;

        /// <summary>
        /// Time zone to localize the date time properties to.
        /// </summary>
        protected TimeZoneInfo _timeZone;

        /// <summary>
        /// Create a new instance of the localize proxy and return a transparent proxy to the given instance.
        /// </summary>
        /// <typeparam name="T">Type of the target instance that the proxy will use.</typeparam>
        /// <param name="instance">Instance of the object to create a proxy for.</param>
        /// <param name="timeZone">Time zone to use for localizing the DateTime values.</param>
        /// <returns>Transparent localize proxy.</returns>
        public static T CreateTransparentProxy<T>(T instance, TimeZoneInfo timeZone)
        {
            var proxy = new LocalizeProxy(instance, timeZone);
            return (T)proxy.GetTransparentProxy();
        }

        /// <summary>
        /// Create a new localize proxy instance for the given object instance and time zone.
        /// </summary>
        /// <param name="instance">Instance of the object to create a proxy for.</param>
        /// <param name="timeZone">Time zone to use for localizing the DateTime values.</param>
        public LocalizeProxy(object instance, TimeZoneInfo timeZone)
        {
            this._instance = instance;
            this._timeZone = timeZone;
        }

        /// <summary>
        /// Create a new localize proxy instance for the given object instance and time zone.
        /// </summary>
        /// <param name="instance">Instance of the object to create a proxy for.</param>
        /// <param name="timeZone">Time zone to use for localizing the DateTime values.</param>
        /// <param name="instanceType">Type of the instance to create the proxy for.</param>
        public LocalizeProxy(object instance, TimeZoneInfo timeZone, Type instanceType)
            : base(instanceType)
        {
            this._instance = instance;
            this._timeZone = timeZone;
        }

        /// <summary>
        /// Invoke a method on the target instance.
        /// </summary>
        /// <param name="msg">Method call message.</param>
        /// <returns>Return message with the data returned from the method.</returns>
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = new MethodCallMessageWrapper((IMethodCallMessage)msg);
            var methodBase = methodCall.MethodBase as MethodBase;
            object[] inArgs = (0 == methodCall.InArgCount) ? null : new object[methodCall.InArgCount];
            var localizer = new LocalizeCore();

            for (int i = 0; i < methodCall.InArgCount; i++)
            {
                var localizedArg = localizer.TraverseObject(methodCall.InArgs[i], this.LocalizeToUtc);
                inArgs[i] = localizedArg;
            }

            var resultUtc = methodBase.Invoke(this._instance, inArgs);
            var result = localizer.TraverseObject(resultUtc, this.LocalizeToLocal);
            var resultMessage = new ReturnMessage(result, methodCall.Args, methodCall.ArgCount, methodCall.LogicalCallContext, methodCall);

            return resultMessage;
        }

        /// <summary>
        /// Localize DateTime values in the method's request to the UTC timezone.
        /// </summary>
        /// <param name="subject">DateTime value to localize.</param>
        /// <returns>Localized DateTime value.</returns>
        private DateTime LocalizeToUtc(DateTime subject)
        {
            if (subject != null)
            {
                return TimeZoneInfo.ConvertTimeToUtc(subject, this._timeZone);
            }

            return subject;
        }

        /// <summary>
        /// Localize DateTime values in the method's response to the user's local time zone.
        /// </summary>
        /// <param name="subject">DateTime value to localize.</param>
        /// <returns>Localized DateTime value.</returns>
        private DateTime LocalizeToLocal(DateTime subject)
        {
            if (subject != null)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(subject, this._timeZone);
            }

            return subject;
        }
    }
}
