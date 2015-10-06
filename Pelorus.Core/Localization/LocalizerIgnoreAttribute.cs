using System;

namespace Pelorus.Core.Localization
{
    /// <summary>
    /// Attribute to decorate properties that should not be adjusted by the localizer on entity types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizerIgnoreAttribute : Attribute
    {
        private readonly string _ignoreReason;

        /// <summary>
        /// Reason that the property is to be ignored by the localizer.
        /// </summary>
        // ReSharper disable ConvertToAutoProperty
        public string IgnoreReason => this._ignoreReason;

        // ReSharper restore ConvertToAutoProperty

        /// <summary>
        /// Create a new instance of the localizer attribute.
        /// </summary>
        public LocalizerIgnoreAttribute()
        {
            this._ignoreReason = string.Empty;
        }

        /// <summary>
        /// Create a new instance of the localizer attribute with the given reason.
        /// </summary>
        /// <param name="reason">Reason that the property is not to be localized.</param>
        public LocalizerIgnoreAttribute(string reason)
        {
            this._ignoreReason = reason;
        }
    }
}
