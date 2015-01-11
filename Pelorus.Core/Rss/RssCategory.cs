using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// The RSS category defines categories for the channel and item members of the RSS feed.
    /// </summary>
    public class RssCategory
    {
        /// <summary>
        /// Name of the category.
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Domain that identifies a categorization taxonomy.
        /// </summary>
        [XmlAttribute("domain")]
        public string Domain { get; set; }
    }
}
