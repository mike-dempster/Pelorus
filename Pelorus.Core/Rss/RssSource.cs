using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// RSS source represents the The RSS channel that an item came from.
    /// </summary>
    public class RssSource
    {
        /// <summary>
        /// The value is the name of the RSS channel that the item came from, derived from its &lt;title&gt;.
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// URL to the XMLization of the source.
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
