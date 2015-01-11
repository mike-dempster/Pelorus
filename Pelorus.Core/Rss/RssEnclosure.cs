using System.Xml.Serialization;

namespace Pelorus.Core.Rss
{
    /// <summary>
    /// The RSS enclosure defines a media object that is attached to an item within an RSS channel.
    /// </summary>
    public class RssEnclosure
    {
        /// <summary>
        /// URL to the enclosed media is located
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }

        /// <summary>
        /// The size of the media object in bytes.
        /// </summary>
        [XmlAttribute("length")]
        public long Length { get; set; }

        /// <summary>
        /// MIME type of the media object.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
