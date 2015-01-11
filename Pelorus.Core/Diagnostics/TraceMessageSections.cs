namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Sections of a trace message that are sent to trace sources.
    /// </summary>
    internal enum TraceMessageSections : short
    {
        /// <summary>
        /// Header section of the trace message.
        /// </summary>
        Header = 0,

        /// <summary>
        /// Body of the trace message.
        /// </summary>
        Body = 1,

        /// <summary>
        /// Footers of the trace message.
        /// </summary>
        Footers = 2
    }
}
